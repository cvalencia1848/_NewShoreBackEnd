import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Journey } from "../models/journey";
import { JourneyDTo } from '../models/journeyDTo';
import { Currency } from "../models/currency";

@Injectable({
    providedIn: 'root'
  })
export class JourneyService{
    constructor(private http: HttpClient){}

    baseUrl: string = "https://localhost:7235";
    
    getFligths(journeyDTo: JourneyDTo): Observable<Journey>{
        return this.http.post<Journey>(`${this.baseUrl}/GetRoutes`, journeyDTo);
    }

    getCurrency(current: string):Observable<Currency>{
        return this.http.get<Currency>(`${this.baseUrl}/GetCurrency/?currency=${current}`);
    }


}