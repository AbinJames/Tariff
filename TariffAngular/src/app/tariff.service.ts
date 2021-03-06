import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { InvoiceMaster } from './models/invoicemaster.model';
import { ParameterMaster } from './models/parametermaster.model';
import { Observable } from 'rxjs/internal/Observable';
import { RuleDetails } from './models/ruledetails.model';

@Injectable({
  providedIn: 'root'
})
export class TariffService {

  constructor(private http: HttpClient, private router: Router) { }
  printToConsole(arg) {
    console.log(arg);
  }
  baseUrl: string = 'https://localhost:44357/api/Tariff/';

  getInvoices(): Observable<InvoiceMaster[]> {
    //Get Invoice and corresponding rules from API
    return this.http.get<InvoiceMaster[]>(this.baseUrl + 'Invoice');
  }

  addInvoice(invoice: InvoiceMaster): void {
    //Post Invoice and corresponding rules to API to be saved in Database
    this.http.post(this.baseUrl + 'AddInvoice', invoice).subscribe(result => {
      console.log(result);
      //navigate to view component after post
      this.router.navigate(['view']);
    });
  }

  addRule(rule: RuleDetails) {
    // this.http.post(this.baseUrl + 'AddRule', rule).subscribe(
    //   result => {
    //     console.log("result");
    //   }
    // );
    let promise = new Promise((resolve, reject) => {
      //Promise is used so that the invoicelist is refreshed after adding new rule
      this.http.post(this.baseUrl + 'AddRule', rule)
        .toPromise()
        .then(
          res => { // Success
            console.log(res);
            resolve();
          }
        );
    });
    return promise;
  }

  getParameters(): Observable<ParameterMaster[]> {
    //Get Parmeters from API
    return this.http.get<ParameterMaster[]>(this.baseUrl + 'ParameterMaster');
  }

  deleteRule(id: number): void {
    //Delete rule with the passed id
    this.http.delete(this.baseUrl + "DeleteRule/" + id).subscribe(result => {
      console.log("Rule deleted");
    });
  }

  deleteInvoice(id: number): void {
    //Delete invoice with the passed id
    this.http.delete(this.baseUrl + "DeleteInvoice/" + id).subscribe(result => {
      console.log("Invoice deleted");
    });
  }

  editInvoice(id: number, invoice: InvoiceMaster): void {
    //Edit invoice with the passed id
    this.http.put(this.baseUrl + "EditInvoice/" + id, invoice).subscribe(result => {
      console.log("Invoice edited");
    });
  }

  editRule(id: number, rule: RuleDetails): void {
    //Edit rule with the passed id
    this.http.put(this.baseUrl + "EditRule/" + id, rule).subscribe(result => {
      console.log("Rule edited");
    });
  }
}
