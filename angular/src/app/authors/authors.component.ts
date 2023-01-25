import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
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
  constructor(private authorService: AuthorService){
    this.buildForm();
    // this.get(2);
    this.getList();
  }

  buildForm(){
    this.selected = {} as AuthorDto;
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
      alert(this.selected.id ? "Author updated successfully." : "Author saved successfully.");
      this.getList();
      this.form.reset();
      this.buildForm();
    });
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
      alert("Author deleted successfully");
      this.getList();
    }); 
  }
}

