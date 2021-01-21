import { Component } from '@angular/core';
import { User } from './models/user';
import { AuthenticationService } from './services/authentication.service';
import { Role } from './models/role';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {

  user: User;

  constructor(
      private authenticationService: AuthenticationService
  ) {
      this.authenticationService.user.subscribe(user => this.user = user,);
  }  

  get isAdmin() {
    return this.user && this.user.role === Role.Admin;
}

}
