import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewInvoiceComponent } from './view-invoice/view-invoice.component';
import { CreateInvoiceComponent } from './create-invoice/create-invoice.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
@NgModule({
  declarations: [ViewInvoiceComponent, CreateInvoiceComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class TariffModule { }
