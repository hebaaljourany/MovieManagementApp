import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { CategoryService, CategoryDto } from '@proxy/categories';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';


@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.scss'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter } 
  ],
})
export class CategoryFormComponent implements OnInit {
  category = { items: [], totalCount: 0 } as PagedResultDto<CategoryDto>;
  selectedCategory = {} as CategoryDto;
  form: FormGroup;
  isModalOpen = false;

  constructor(
    public readonly list: ListService,
    private categoryService: CategoryService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService // inject the ConfirmationService
  ) {}
  loadingData = true;
  ngOnInit() {
    const categoryStreamCreator = (query) => this.categoryService.getList(query);

    this.list.hookToQuery(categoryStreamCreator).subscribe((response) => {
     
      this.category = response;
         console.log(this.category);
         this.loadingData = false;
       }, 
       (error) => {
         console.log(error);
    });
  }
  createCategory() {
    this.selectedCategory = {} as CategoryDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  editCategory(id: string) {
    this.categoryService.get(id).subscribe((category) => {
     
      this.selectedCategory= category;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDeleteThisCategory', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.categoryService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      CategoryName: [this.selectedCategory.categoryName||'', Validators.required],
      
    });
  }
  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedCategory.id
      ? this.categoryService.update(this.selectedCategory.id, this.form.value)
      : this.categoryService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
 

}
