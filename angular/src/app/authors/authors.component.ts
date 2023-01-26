import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthorService,  } from './author.service';
import { CreateUpdateAuthorDto ,AuthorDto} from './model';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent {
form: FormGroup = new FormGroup({});
data: AuthorDto[]=[];
selected = {} as AuthorDto;
  constructor(private authorService: AuthorService, 
    private toastr: ToastrService
    ){
    this.buildForm();
    this.getList();
  }

  buildForm(){
    this.form = new FormGroup({
      authorName: new FormControl(this.selected.authorName, Validators.required),
      status: new FormControl(this.selected.status??false)
    });
  }

  save(){
    const dto:CreateUpdateAuthorDto = {
      id: this.selected.id ?? 0,
      authorName: this.form.get("authorName")?.value,
      status: this.form.get("status")?.value??false
    };

    var request = this.selected.id ? this.authorService.update(dto): this.authorService.create(dto);
    request.subscribe(s => {
      const msg = this.selected.id ? "Author updated successfully." : "Author saved successfully.";
      this.toastr.success(msg);
      this.getList();
      this.form.reset();
      this.selected = {} as AuthorDto;
      this.buildForm();
    },
    (error) => {
      this.toastr.error(error);
    }
    );
  }

  getList(){
    this.authorService.getList().subscribe((s: AuthorDto[]) =>{
      this.data = s;
    })
  }

  edit(id?: number){
    this.authorService.get(id as number).subscribe(d => {
      this.selected = d;
      this.buildForm();
    });
  }
  delete(id?: number){
    this.authorService.delete(id as number).subscribe(d => {
      this.toastr.success("Author deleted successfully", "Deleted", {positionClass: 'toast-top-right'});
      this.getList();
    }); 
  }
}

