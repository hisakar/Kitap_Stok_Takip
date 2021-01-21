import { Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { LogComponent } from '../log/log.component';
import { LoginComponent } from '../login/login.component';
import { NotFoundComponent } from '../not-found/not-found.component';
import { ProductComponent } from '../product/product.component';
import { AuthGuard } from '../_helpers/auth.guard';
import { RegisterComponent } from '../register/register.component';
import { Role } from '../models/role';
import { AdminComponent } from '../admin/admin.component';
import { DemandComponent } from '../demand/demand.component';
import { FavoriteComponent } from '../favorite/favorite.component';
import { CategoryComponent } from '../category/category.component';
import { PublisherComponent } from '../publisher/publisher.component';
import { ChartComponent } from '../chart/chart.component';
import { OrderComponent } from '../order/order.component';
import { WishComponent } from '../wish/wish.component';

export const AppRoutes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'products',
    component: ProductComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin] },
  },
  {
    path: 'categories',
    component: CategoryComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin] },
  },
  {
    path: 'publishers',
    component: PublisherComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin] },
  },
  {
    path: 'logs',
    component: LogComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin] },
  },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin] },
  },
  {
    path: 'demand',
    component: DemandComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin,Role.User] },
  },
  {
    path: 'saved',
    component: FavoriteComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin,Role.User] },
  },
  {
    path: 'chart',
    component: ChartComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin,Role.User] },
  },
  {
    path: 'orders',
    component: OrderComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin] },
  },
  {
    path: 'wish-list',
    component: WishComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin] },
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', component: NotFoundComponent },
];
