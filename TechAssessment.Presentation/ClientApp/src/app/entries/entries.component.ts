import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource, MatPaginator } from '@angular/material';
import { PhoneBooksClient, EntriesClient, EntryViewModel } from '../tech-assessment-api';
import { AuthenticationService, Logger } from '@app/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import {AddEntryComponent} from './add-entry/add-entry.component';

export interface DialogData {
  PhoneBookId: number;
}

const log = new Logger('Entries');

@Component({
  selector: 'app-entries',
  templateUrl: './entries.component.html',
  styleUrls: ['./entries.component.scss']
})
export class EntriesComponent implements OnInit {
  
  listData: MatTableDataSource<any>;
  displayedColumns: string[] = ['name', 'phoneNumber'];
  id: number;
  private sub: any;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private route: ActivatedRoute,
    private entriesClient: EntriesClient,
    private phoneBooksClient: PhoneBooksClient,
    private auth: AuthenticationService,
    public dialog: MatDialog) 
  {
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id']; 
      this.getPhoneBookEntries();
    });
    this.dialog.afterAllClosed.subscribe(() => {
      this.getPhoneBookEntries();
    });
  }

  getPhoneBookEntries() {
    this.phoneBooksClient.getPhoneBookEntries(this.id).subscribe(
      response => {
        this.listData = new MatTableDataSource<EntryViewModel>(response.entries);
        this.listData.sort = this.sort;
        this.listData.paginator = this.paginator;
      },
      error => console.error(error)
    );
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.entriesClient.searchPhonebookEntry(filterValue).subscribe(
      response => {
        this.listData = new MatTableDataSource<EntryViewModel>(response.entries);
        this.listData.sort = this.sort;
        this.listData.paginator = this.paginator;
      },
      error => console.error(error)
    );
  }

  openDialog() {
    this.dialog.open(AddEntryComponent, {
      data: { PhoneBookId: this.id }
    });
  }

}
