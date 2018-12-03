import { RuleSet } from "./ruleset.model";

export class InvoiceSet
{
    id:number;
    invoiceName:string;
    ruleView:RuleSet[];
    isActive:number;
}