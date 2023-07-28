import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserLoginDto } from './model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form: FormGroup = new FormGroup({});
  constructor(){
    this.buildfrom();
  }
  
  buildfrom() {
    this.form = new FormGroup({
      userName: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required)
    });
  }

  onSubmit() {
    const dto: UserLoginDto = {
      userName: this.form.get("userName")?.value,
      password: this.form.get("password")?.value
    }
    console.log(this.form);
    console.log(`UserLogin: ${JSON.stringify(dto)}`);
  }
}