import { InvoiceMaster } from "./invoicemaster.model";
import { ParameterMaster } from "./parametermaster.model";

export class RuleDetails
{
    ruleId:number;
    invoiceId:number;
    parameterId:number;
    ruleValue:string;
    isActive:number;
}