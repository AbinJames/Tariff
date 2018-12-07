import { Pipe, PipeTransform } from '@angular/core';
import { InvoiceMaster } from 'src/app/models/invoicemaster.model';

@Pipe({ name: 'invoiceFilter' })
export class InvoiceFilterPipe implements PipeTransform {
    transform(invoiceList: InvoiceMaster[], invoiceName: string, isActive: number,filterEnabled:boolean) {

        //if isActive is selected then filter invoiceList based on isActive value
        //-1 is the value  selecting all invoice value
        if(!filterEnabled)
        {
            return invoiceList;
        }
        if (isActive && isActive != -1) {
            invoiceList = invoiceList.filter(
                item =>
                    item.isActive == isActive
            );
        }
        //if invoiceName is not empty the filter invoice by invoiceName
        if (invoiceName) {
            invoiceList = invoiceList.filter(
                item =>
                    item.invoiceName.toLowerCase().includes(
                        invoiceName.toLowerCase()
                    )
            );
        }
        //return filtered list
        return invoiceList;
    }
}