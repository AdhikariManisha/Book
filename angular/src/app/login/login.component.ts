import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserLoginDto } from './model';
import { LoginService } from './login.service';
import { Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form: FormGroup = new FormGroup({});
  constructor(private loginService: LoginService, private router: Router, private toastr: ToastrService ){
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
    this.loginService.login(dto).subscribe((res) => {
      localStorage.setItem("access_token", res.token);
      this.toastr.success("Login successfully.", "SUCCESS");
      this.router.navigate(["/"]);
    }, (err) => {
      console.log(err);
      this.toastr.error(err.error.message);
    });
  }
}