import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { ActorService, ActorDto } from '@proxy/actors';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

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
  selectedImage: File | null = null;
  imageUrl: string | null = null; // لحفظ رابط الصورة

  constructor(
    public readonly list: ListService,
    private actorService: ActorService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService // inject the ConfirmationService
  ) {}

  loadingData = true;
  ngOnInit() {
    const actorStreamCreator = (query) => this.actorService.getList(query);
    
    this.list.hookToQuery(actorStreamCreator).subscribe((response) => {
      this.actor = response;
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
    this.imageUrl = null; // إعادة تعيين رابط الصورة
  }

  editActor(id: string) {
    this.actorService.get(id).subscribe((actor) => {
      this.selectedActor = actor;
      this.buildForm();
      this.isModalOpen = true;
      this.imageUrl = actor.actorImage; // تعيين رابط الصورة عند التعديل
    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDeleteThisActor', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.actorService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      ActorName: [this.selectedActor.actorName || '', Validators.required],
      ActorImage: [null] // إضافة حقل الصورة
    });
  }

  onImageSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      this.selectedImage = file;
      const reader = new FileReader();
      reader.onload = () => {
        this.imageUrl = reader.result as string; // تعيين رابط الصورة للعرض
      };
      reader.readAsDataURL(file);
    }
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedActor.id
      ? this.actorService.update(this.selectedActor.id, {
          ...this.form.value,
          ActorImage: this.imageUrl // تعيين رابط الصورة
        })
      : this.actorService.create({
          ...this.form.value,
          ActorImage: this.imageUrl // تعيين رابط الصورة
        });

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
