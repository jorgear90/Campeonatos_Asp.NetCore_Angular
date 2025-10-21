import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FilterComponent } from './components/filter/filter.component';
import { IsLoadingComponent } from './components/is-loading/is-loading.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { ReturnButtonComponent } from './components/return-button/return-button.component';
import { SearchInputComponent } from './components/search-input/search-input.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [FilterComponent, IsLoadingComponent, PaginationComponent, ReturnButtonComponent,
    SearchInputComponent],
  imports: [
    CommonModule, FormsModule,
  ],
  exports: [FilterComponent, IsLoadingComponent, PaginationComponent, ReturnButtonComponent,
    SearchInputComponent, CommonModule]
})
export class SharedModule { }
