import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { ActorService, ActorDto } from '@proxy/actors';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { CreateUpdateActorDto } from '@proxy/application/contracts/actors';
import { FileUploadService } from 'src/app/services/upload.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-actor-form',
  templateUrl: './actor-form.component.html',
  styleUrls: ['./actor-form.component.scss'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
  ],
})
export class ActorFormComponent implements OnInit {
  actor = { items: [], totalCount: 0 } as PagedResultDto<ActorDto>;
  selectedActor = {} as ActorDto;
  form: FormGroup;
  isModalOpen = false;
  selectedImage: any;

  constructor(
    public readonly list: ListService,
    private actorService: ActorService,
    private fileUploadService: FileUploadService,

    private fb: FormBuilder,
    private confirmation: ConfirmationService // inject the ConfirmationService
  ) {}

  loadingData = true;
  ngOnInit() {
    const actorStreamCreator = (query) => this.actorService.getList(query);
    
    this.list.hookToQuery(actorStreamCreator).subscribe((response) => {
      this.actor = response;
      this.actor.items.forEach(element => {
        element.thumbnail = "data:image/png;base64," + element.thumbnail;
      });
      console.log(this.actor);
      this.loadingData = false;
    }, 
    (error) => {
      console.log(error);
    });
  }

  createActor() {
    this.selectedActor = {} as ActorDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editActor(id: string) {
    this.actorService.get(id).subscribe((actor) => {
      this.selectedActor = actor;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: string) {
    this.confirmation.warn('Are You Sure To Delete This Actor', 'Are You Sure').subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
            this.actorService.delete(id).pipe(
                catchError((error) => {
                    this.confirmation.error('Failed to delete the actor. Please remove the associated movies first.', 'Delete Failed', { hideYesBtn: true });
                    return of(null);  // تعيد observable فارغة لعدم توقف التدفق
                })
            ).subscribe(() => this.list.get());
        }
    });
}

  buildForm() {
    this.form = this.fb.group({
      actorName: [this.selectedActor.actorName || '', Validators.required],
      actorImage: [null] // إضافة حقل الصورة
    });
  }


  onImageSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      this.selectedImage = file;
      this.form.get('actorImage').setValue("data");
    }
  }
  
  
  save() {
    if (this.form.invalid) {
      return;
    }
    console.log(this.form.value);
    
    let actorName = this.form.get('actorName').value;
    let input = {} as CreateUpdateActorDto;
    input.actorName = actorName;
    input.actorImageBlob = this.selectedImage;
    var form_data = new FormData();

    for(var key in input){
      if(key == 'movieIds' && input.movieIds.length == 0)
        continue;
      form_data.append(key, input[key]);
    }
    const request = this.selectedActor.id
      ? this.fileUploadService.updateActor(this.selectedActor.id, form_data)
      : this.fileUploadService.createActor(form_data);

    request.pipe(
      catchError((error) => {
          // عرض رسالة خطأ للمستخدم في حالة فشل العملية
          this.confirmation.error('Failed to save the actor. Actor name might already exist.', 'Save Failed', { hideYesBtn: true });
          return of(null);  // إرجاع observable فارغ لضمان استمرار التدفق
      })
  ).subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
