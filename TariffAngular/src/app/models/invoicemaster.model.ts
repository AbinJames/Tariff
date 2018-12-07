import { RuleDetails } from "./ruledetails.model";

export class InvoiceMaster
{
    invoiceId:number;
    invoiceName:string;
    isActive:number;
    ruleView:RuleDetails[];
}

export class EditThisRuleInInvoice{
    rule:Boolean[]; 
  }