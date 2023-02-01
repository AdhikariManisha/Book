import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialog } from '../confirmation-dialog/confirmation-dialog';
import { AuthorService, } from './author.service';
import { CreateUpdateAuthorDto, AuthorDto } from './model';
import { MatDialogRef, MatDialog } from '@angular/material/dialog'

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent {
  form: FormGroup = new FormGroup({});
  data: AuthorDto[] = [];
  selected = {} as AuthorDto;
  dialogRef = {} as MatDialogRef<ConfirmationDialog>;
  isModalOpen = false as Boolean;
  selectedIds: number[] = [];
  checkBoxAll: boolean = false;
  constructor(private authorService: AuthorService,
    private toastr: ToastrService,
    private dialog: MatDialog
  ) {
    this.getList();
  }

  buildForm() {
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
      this.getList();
      this.form.reset();
      this.selected = {} as AuthorDto;
      this.buildForm();
      this.isModalOpen = false;
    }
    );
  }

  getList() {
    this.authorService.getList().subscribe((s: AuthorDto[]) => {
      this.data = s;
    })
  }

  edit(id?: number) {
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
          this.getList();
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
  }
  deleteMany() {
    this.authorService.deleteMany(this.selectedIds).subscribe((d) => {
      this.toastr.success("Deleted");
      this.selectedIds = [];
      this.checkBoxAll = false;
      this.getList();
    });
  }
  onSelectAll(){
    this.selectedIds = [];
    if(this.checkBoxAll){
      this.data.forEach(s => {
        this.selectedIds.push((s.id as number));
      });
    }
    var selectedAuthors = document.querySelectorAll('.checkbox-author');
    selectedAuthors.forEach(s => {
      (s as HTMLInputElement).checked = this.checkBoxAll;
    });
  }
  
}

