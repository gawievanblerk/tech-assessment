import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import {EntriesClient, AddEntryToPhonebookCommand, EntryViewModel} from '../../tech-assessment-api';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DialogData } from '../entries.component';

@Component({
  selector: 'app-add-entry',
  templateUrl: './add-entry.component.html',
  styleUrls: ['./add-entry.component.scss']
})

export class AddEntryComponent {
  formGroup: FormGroup;
  titleAlert: string = 'This field is required';
  post: any = '';
  formtitle: any = 'Add Phone Book Entry';

  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<AddEntryComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private entriesClient: EntriesClient
  ) {}

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.formGroup = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(60)]],
      phoneNumber: ['', [Validators.required, Validators.minLength(7), Validators.maxLength(13)]]
    });
  }

  get name() {
    return this.formGroup.get('name') as FormControl;
  }

  get phoneNumber() {
    return this.formGroup.get('phoneNumber') as FormControl;
  }

  onSubmit(post: any) {
    let entry = new EntryViewModel({
      id : 0,
      name : this.name.value,
      phoneNumber : this.phoneNumber.value,
      phoneBookId : this.data.PhoneBookId
    });
    let command = new AddEntryToPhonebookCommand({
      entry : entry
    });
    this.entriesClient.addEntryToPhonebook(command).subscribe(
      response => {
        alert(`Entry ${this.name.value} successfully created`);
        this.dialogRef.close();
      },
      error => {
        alert(`Entry ${this.name.value} could not be created : ${error}`);
        console.error(error);
        this.dialogRef.close();
      }
    );
  }
}