import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/users/user.service';
import { Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserLoginDto } from '../services/users/model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form: FormGroup = new FormGroup({});
  constructor(private userService: UserService, private router: Router, private toastr: ToastrService ){
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
    this.userService.login(dto).subscribe((res) => {
      localStorage.setItem("access_token", res.token);
      this.toastr.success("Login successfully.", "SUCCESS");
      this.router.navigate(["/"]);

    });
  }
}