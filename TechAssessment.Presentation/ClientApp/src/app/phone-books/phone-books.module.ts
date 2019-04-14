import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from '@app/material.module';
import { PhoneBooksRoutingModule } from './phone-books-routing.module';
import { PhoneBooksComponent } from './phone-books.component';

@NgModule({
  imports: [CommonModule, TranslateModule, FlexLayoutModule, MaterialModule, PhoneBooksRoutingModule],
  declarations: [PhoneBooksComponent]
})
export class BooksModule {}
