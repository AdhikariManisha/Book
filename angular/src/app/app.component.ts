import { Component, OnInit } from '@angular/core';
import { TokenDto } from './login/model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  data: any;
  title = 'angular';
  user: string = "";
  constructor(private jwtHelperService: JwtHelperService, private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.checkLogin();
  }

  checkLogin() {
    const token = localStorage.getItem("access_token")
    let isExpired = false;
    if (token) {
      console.log(token);
      const decodedToken = this.jwtHelperService.decodeToken(token);
      console.log(decodedToken);
      isExpired = this.jwtHelperService.isTokenExpired(token);
      console.log(isExpired);
      if (isExpired) {
        localStorage.removeItem("access_token");
      }
      else {
        this.user = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      }
    }

    if (!token || isExpired) {
      this.router.navigate(['/login']);
    }
  }

  logout() {
    localStorage.removeItem("access_token");
    this.router.navigate(['/login']);
  }

  get isUserAuthenticated() {
    let isUserAuthenticated = false;
    const token = localStorage.getItem("access_token")
    if (token && !this.jwtHelperService.isTokenExpired(token)) {
      isUserAuthenticated = true;
    }
    return isUserAuthenticated;
  }
}