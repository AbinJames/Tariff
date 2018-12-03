import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CreateInvoiceComponent} from './tariff/create-invoice/create-invoice.component';
import {ViewInvoiceComponent} from './tariff/view-invoice/view-invoice.component';

const routes: Routes = [
  { path: 'create', component: CreateInvoiceComponent},
  { path: 'view', component: ViewInvoiceComponent },
  { path: '', redirectTo: '/view', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
