import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../models/book';
import { Chart } from '../models/chart';
import { Demand } from '../models/demand';
import { Fav } from '../models/fav';
import { url } from '../shared/url';

@Injectable({
  providedIn: 'root'
})
export class UserBookService {
  

  constructor(private http: HttpClient) { }


  addFavorite(fav: Fav) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.post<boolean>(url + 'favs',fav, httpOptions);
  }

  getFavBooksByUserId(userId){
    return this.http.get<Book[]>(url + `favs/${userId}`);
  }

  deleteSingleFavorite(fav: Fav){
    const httpOptions = {
      headers: new HttpHeaders({
      }),
      body: fav
    };
    return this.http.delete<any>(url + 'favs',httpOptions);
  }

  //-----------------------------------
  //-----------------------------------
  //-----------------------------------
  getAllCharts(): Observable<Chart[]> {
    return this.http.get<Chart[]>(url + 'charts');
  }

  addChartItem(chart: Chart) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.post<boolean>(url + 'charts',chart, httpOptions);
  }

  getChartItemsByUserId(userId){
    return this.http.get<Book[]>(url + `charts/${userId}`);
  }

  deleteSingleChartItem(chart: Chart){
    const httpOptions = {
      headers: new HttpHeaders({
      }),
      body: chart
    };
    return this.http.delete<any>(url + 'charts',httpOptions);
  }
  
  //-----------------------------------
  //-----------------------------------
  //-----------------------------------

  getAllDemands(): Observable<Demand[]> {
    return this.http.get<Demand[]>(url + 'demands');
  }

  addDemand(demand: Demand) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.post<Demand>(url + 'demands',demand, httpOptions);
  }

  getDemandByUserId(userId){
    return this.http.get<Demand[]>(url + `demands/${userId}`);
  }

  deleteSingleDemand(demand: Demand){
    const httpOptions = {
      headers: new HttpHeaders({
      }),
      body: demand
    };
    return this.http.delete<any>(url + 'demands',httpOptions);
  }
}
