import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private authenticationService:AuthenticationService) { }

  brand:string = '';
  user: User;
  ngOnInit(): void {
    this.authenticationService.user.subscribe(u=>this.user=u);
  }

  logout() {
    this.authenticationService.logout();
}

}
