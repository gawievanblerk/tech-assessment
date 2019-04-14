import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource, MatPaginator } from '@angular/material';
import { PhoneBooksClient, EntriesClient, EntryViewModel, PhoneBookListViewModel, AddEntryToPhonebookCommand } from '../tech-assessment-api';
import { AuthenticationService, Logger } from '@app/core';
import { Router } from '@angular/router';

const log = new Logger('PhoneBooks');

@Component({
  selector: 'app-phone-books',
  templateUrl: './phone-books.component.html',
  styleUrls: ['./phone-books.component.scss']
})

export class PhoneBooksComponent implements OnInit {
  listData: MatTableDataSource<any>;
  displayedColumns: string[] = ['name', 'actions'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private phoneBooksClient: PhoneBooksClient,
    private entriesClient: EntriesClient,
    private auth: AuthenticationService,
    public router: Router
  ) { }

  ngOnInit() {
    this.getAllBooks();
  }

  ngAfterViewInit() { }

  getAllBooks() {
    this.phoneBooksClient.getAllPhoneBooks().subscribe(
      response => {
        let phoneBooksList = response;
        this.listData = new MatTableDataSource<PhoneBookListViewModel>(phoneBooksList.phoneBooks);
        this.listData.sort = this.sort;
        this.listData.paginator = this.paginator;
      },
      error => console.error(error)
    );
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.listData.filter = filterValue;
  }

  onViewEntries(phoneBookId : number) {
    this.router.navigate(['/entries', phoneBookId]);
  }

}


