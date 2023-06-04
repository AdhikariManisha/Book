import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialog } from '../confirmation-dialog/confirmation-dialog';
import { AuthorService, } from './author.service';
import { CreateUpdateAuthorDto, AuthorDto, AuthorFilter } from './model';
import { MatDialogRef, MatDialog } from '@angular/material/dialog'
import { PagedResultResponseDto } from '../models/PagedResultResponseDto';
import { ResponseModal } from '../models/ResponseModel';
import { PagedResultDto } from '../models/PageResultDto';
import { MatSort, Sort } from '@angular/material/sort';
import { PagedAndSortedResultResponseDto } from '../models/PagedAndSortedResultResponseDto';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent {
  form: FormGroup = new FormGroup({});
  selected = {} as AuthorDto;
  dialogRef = {} as MatDialogRef<ConfirmationDialog>;
  isModalOpen = false as Boolean;
  selectedIds: number[] = [];
  checkBoxAll: boolean = false;
  filter = {} as AuthorFilter;
  showFilter: boolean = false;
  page = {
    skipCount : 0,
    takeCount : 5,
    sorting: ""
  } as PagedAndSortedResultResponseDto;

  data = {
    totalCount: 0,
    items: ([] as AuthorDto[])
  } as PagedResultDto<AuthorDto>;

  
  constructor(private authorService: AuthorService,
    private toastr: ToastrService,
    private dialog: MatDialog
  ) {
    this.search();
    this.buildForm();
  }

  buildForm() {
    this.form.reset();
    this.form = new FormGroup({
      authorName: new FormControl(this.selected.authorName, Validators.required),
      status: new FormControl(this.selected.status ?? false)
    });
  }

  save() {
    const dto: CreateUpdateAuthorDto = {
      id: this.selected.id ?? 0,
      authorName: this.form.get("authorName")?.value,
      status: this.form.get("status")?.value ?? false
    };

    var request = this.selected.id ? this.authorService.update(dto) : this.authorService.create(dto);
    request.subscribe(s => {
      const msg = this.selected.id ? "Author updated successfully." : "Author saved successfully.";
      this.toastr.success(msg);
      this.search();
      this.selected = {} as AuthorDto;
      this.buildForm();
      this.isModalOpen = false;
    }
    );
  }

  edit(id?: number) {
    this.isModalOpen = true;
    this.authorService.get(id as number).subscribe(d => {
      this.selected = d;
      this.buildForm();
    });
  }

  delete(id?: number) {
    this.dialogRef = this.dialog.open(ConfirmationDialog, {
      disableClose: false
    })
    this.dialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this item?';
    this.dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.authorService.delete(id as number).subscribe(d => {
          this.toastr.success("Author deleted successfully", "Deleted", { positionClass: 'toast-top-right' });
          this.search();
        });
      }
      this.dialogRef = {} as MatDialogRef<ConfirmationDialog>;
    });
  }

  create() {
    this.isModalOpen = true;
    this.buildForm();
  }

  closeModal() {
    this.isModalOpen = false;
    this.selected = {} as AuthorDto;
    this.buildForm();
  }
  deleteMany() {
    this.dialogRef = this.dialog.open(ConfirmationDialog, {
      disableClose: false
    })
    this.dialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete these items?';
    this.dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (!this.selectedIds.length || !this.selectedIds) {
          this.toastr.error("Please select Author.");
          this.checkBoxAll = false;
          return;
        }
        this.authorService.deleteMany(this.selectedIds).subscribe((d) => {
          this.toastr.success("Deleted");
          this.selectedIds = [];
          this.checkBoxAll = false;
          this.search();
        });
      }
      this.dialogRef = {} as MatDialogRef<ConfirmationDialog>;
    });
  }

  onSelectAll() {
    this.selectedIds = [];
    if (this.checkBoxAll) {
      this.data.items.forEach(s => {
        this.selectedIds.push((s.id as number));
      });
    }
    var selectedAuthors = document.querySelectorAll('.checkbox-author');
    selectedAuthors.forEach(s => {
      (s as HTMLInputElement).checked = this.checkBoxAll;
    });
  }

  onSelect(cb: any, id?: number) {
    if (cb.checked) {
      this.selectedIds.push((id as number));
    }
    else {
      let index = this.selectedIds.indexOf((id as number));
      if (index !== -1) {
        this.selectedIds.splice(index, 1);
      }
    }
  }
  
  search(){
    this.authorService.getListByFilter(this.page, this.filter).subscribe((s: ResponseModal<PagedResultDto<AuthorDto>>) => {
      this.data = s.data;
    })
  }

  toggleFilter(){
    this.showFilter = !this.showFilter;
  }

  resetFilters(){
    this.filter = {} as AuthorDto;
  }
  rows = [
    {authorName: 'John Doe', age: 30, gender: 'M'},
    {authorName: 'Jane Doe', age: 35, gender: 'F'},
    {authorName: 'Bob Smith', age: 40, gender: 'M'}
  ];
  displayedColumns: string[] = ["id", "authorName", "status", "actions"];
  // columns = [
  //   {name: 'Person Name', prop: 'name'},
  //   {prop: 'age'},
  //   {prop: 'gender'},
  // ]

  changePage(event: any){
    this.page.takeCount = event.pageSize;
    this.page.skipCount = event.pageIndex * this.page.takeCount;
    this.search();
  }

  sortData(sortState: Sort){
    this.page.sorting = `${sortState.active} ${sortState.direction}`;
    console.log(sortState);
    this.search();
  }
}

