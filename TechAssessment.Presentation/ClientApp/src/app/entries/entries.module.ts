import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from '@app/material.module';
import { EntriesRoutingModule } from './entries-routing.module';
import { EntriesComponent } from './entries.component';
import { AddEntryComponent } from './add-entry/add-entry.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [EntriesComponent, AddEntryComponent],
  imports: [
    CommonModule, 
    TranslateModule, 
    FlexLayoutModule, 
    MaterialModule, 
    EntriesRoutingModule, 
    FormsModule, 
    ReactiveFormsModule
  ],
  entryComponents: [AddEntryComponent]
})
export class EntriesModule {}
