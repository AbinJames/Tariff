import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import{HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TariffService } from './tariff.service';
import { RouterModule, Routes } from '@angular/router';
import {CreateInvoiceComponent} from './tariff/create-invoice/create-invoice.component';
import {ViewInvoiceComponent} from './tariff/view-invoice/view-invoice.component';
import { TariffModule } from './tariff/tariff.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    TariffModule,
    AppRoutingModule
  ],
  providers: [TariffService],
  bootstrap: [AppComponent]
})
export class AppModule { }
