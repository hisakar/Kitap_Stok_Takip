import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { AppRoutingModule } from './routing/app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { AppRoutes } from './routing/app.routes';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './headers/header/header.component';
import { AdminHeaderComponent } from './headers/admin-header/admin-header.component';
import { FooterComponent } from './footer/footer.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ProductComponent } from './product/product.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { Ng2SearchPipeModule } from 'ng2-search-filter';
//import { OrderModule } from 'ngx-order-pipe';
import { NgxPaginationModule } from 'ngx-pagination';
import { LogComponent } from './log/log.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AdminComponent } from './admin/admin.component';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { ProfileComponent } from './profile/profile.component';
import { DemandComponent } from './demand/demand.component';
import { FavoriteComponent } from './favorite/favorite.component';
import { CategoryComponent } from './category/category.component';
import { PublisherComponent } from './publisher/publisher.component';
import { ChartComponent } from './chart/chart.component';
import { OrderComponent } from './order/order.component';
import { WishComponent } from './wish/wish.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    AdminHeaderComponent,
    FooterComponent,
    NotFoundComponent,
    ProductComponent,
    LogComponent,
    LoginComponent,
    RegisterComponent,
    AdminComponent,
    ProfileComponent,
    DemandComponent,
    FavoriteComponent,
    CategoryComponent,
    PublisherComponent,
    ChartComponent,
    OrderComponent,
    WishComponent
  ],
  imports: [
    BrowserModule,
    Ng2SearchPipeModule,
    //OrderModule,
    NgxPaginationModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule.forRoot(AppRoutes)
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
