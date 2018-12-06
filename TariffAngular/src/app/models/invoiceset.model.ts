import { RuleDetails } from "./ruledetails.model";

export class InvoiceSet
{
    id:number;
    invoiceName:string;
    ruleView:RuleDetails[];
    isActive:number;
}