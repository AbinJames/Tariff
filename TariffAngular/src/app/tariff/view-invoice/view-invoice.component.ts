import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TariffService } from 'src/app/tariff.service';
import { InvoiceMaster, EditThisRuleInInvoice } from 'src/app/models/invoicemaster.model';
import { RuleDetails } from 'src/app/models/ruledetails.model';
import { ParameterMaster } from 'src/app/models/parametermaster.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-view-invoice',
  templateUrl: './view-invoice.component.html',
  styleUrls: ['./view-invoice.component.css']
})

export class ViewInvoiceComponent implements OnInit {

  constructor(private http: HttpClient, private tariffService: TariffService) { }
  invoiceList: InvoiceMaster[];
  ruleToggleClicked = [];
  editThisInvoice = [];
  editThisRuleInInvoice: EditThisRuleInInvoice[] = [];
  newRuleInvoiceId: number;
  invoice: InvoiceMaster = {
    invoiceId: null,
    invoiceName: "default",
    isActive: 0,
    ruleView: null
  };
  filterEnabled: boolean = false;
  ruleDetails: RuleDetails = {
    ruleId: null,
    invoiceId: null,
    parameterId: null,
    ruleValue: null,
    isActive: null
  };
  parameters: ParameterMaster[];
  modalParameters: ParameterMaster[];
  deleteId: number;
  deleteType: string;
  initObj: EditThisRuleInInvoice = {
    rule: []
  };
  rule: boolean[];
  ngOnInit() {
    //get invoice and corresponding rules from service
    this.getInvoiceList();

    //get list of parameters from service
    this.getParameterList()
  }

  getParameterList():void{
    this.tariffService.getParameters().subscribe(parameterList => {
      this.parameters = parameterList;
      console.log("Parameters " + JSON.stringify(this.parameters));
    });
  }

  getInvoiceList(): void {
    this.tariffService.getInvoices().subscribe(invoiceList => {
      this.invoiceList = invoiceList;
      console.log(JSON.stringify(this.invoiceList));
      console.log(this.invoiceList.length);
      //Set defualt status for toggle rule button icon
      for (let invoiceIndex = 0; invoiceIndex < this.invoiceList.length; invoiceIndex++) {
        this.ruleToggleClicked[invoiceIndex] = false;
        this.initObj = {
          rule: []
        };
        this.editThisInvoice[invoiceIndex] = false;
        this.editThisRuleInInvoice.push(this.initObj);
        console.log(JSON.stringify(this.invoiceList[invoiceIndex].ruleView));
        for (let ruleIndex = 0; ruleIndex < this.invoiceList[invoiceIndex].ruleView.length; ruleIndex++) {
          this.editThisRuleInInvoice[invoiceIndex].rule.push(false)
        }
      }
    });

  }
  //function to toggle collapse icon
  ruleDropDown(index: number) {
    this.ruleToggleClicked[index] = !this.ruleToggleClicked[index];
  }

  //Function to delete rule of an invoice
  deleteRule(id: number): void {
    //Call service to delete rule
    this.tariffService.deleteRule(id);
    var deleteInvoiceId = 0;
    //Find invoice with corresponding rule and delete the rule from invoice;
    this.invoiceList.forEach(function (invoiceValue) {
      invoiceValue.ruleView.forEach(function (ruleValue) {
        if (ruleValue.ruleId == id) {
          deleteInvoiceId = ruleValue.invoiceId;
        }
      });
    });
    this.invoiceList.find(item => item.invoiceId == deleteInvoiceId).ruleView = this.invoiceList.find(item => item.invoiceId == deleteInvoiceId).ruleView.filter(rule => rule.ruleId != id);

    (<HTMLElement>document.getElementById("deletion_close")).click();
  }

  //Function to delete invoice
  deleteInvoice(id: number): void {
    //Call service to delete invoice
    this.tariffService.deleteInvoice(id);
    this.invoiceList = this.invoiceList.filter(item => item.invoiceId != id);
    (<HTMLElement>document.getElementById("deletion_close")).click();
  }

  //function to enable invoice fields to editable
  setEditInvoice(index: number): void {
    //The edit and delete buttons are set to hidden
    this.editThisInvoice[index] = true;
  }

  //function to cancel editting invoice
  cancelEdittingInvoice(index: number, invoiceId: number, type: string): void {
    this.editThisInvoice[index] = false;
    (<HTMLInputElement>document.getElementById("invoiceIsActive_" + index)).checked = invoice.isActive == 0 ? false : true;
    if (type == 'cancel') {
      //Reset text of invoiceName in case of any changes
      var invoice = this.invoiceList.find(item => item.invoiceId === invoiceId);
      //Since document.getElementById("invoiceName_" + id) doesnot have value property
      //<HTMLInputElement> is added to cast it into HTMLElement Type
      //Same goes for document.getElementById("invoiceIsActive_" + id)
      (<HTMLInputElement>document.getElementById("invoiceName_" + index)).value = invoice.invoiceName;
      (<HTMLInputElement>document.getElementById("invoiceIsActive_" + index)).checked = invoice.isActive == 0 ? false : true;
    }
  }

  //function to save changes made to invoice
  saveEdittedInvoice(index: number, invoiceId: number): void {
    //Set invoice model with values from view being editted
    this.invoice.invoiceId = invoiceId;
    this.invoice.invoiceName = (<HTMLInputElement>document.getElementById("invoiceName_" + index)).value.toString();
    this.invoice.isActive = (<HTMLInputElement>document.getElementById("invoiceIsActive_" + index)).checked ? 1 : 0;
    this.invoiceList[index].isActive = this.invoice.isActive;
    //Call service to edit Invoice
    this.tariffService.editInvoice(invoiceId, this.invoice);
    this.cancelEdittingInvoice(index, invoiceId, 'save');
  }

  //function to enable rule fields to editable
  setEditRule(invoiceNo: number, ruleNo: number): void {
    //The edit and delete buttons are set to hidden
    this.editThisRuleInInvoice[invoiceNo].rule[ruleNo] = true;
  }

  //function to cancel editting rule
  cancelEdittingRule(invoiceId: number, ruleId: number, invoiceNo: number, ruleNo: number, type: string): void {
    //The edit and delete buttons are set to visible
    this.editThisRuleInInvoice[invoiceNo].rule[ruleNo] = false;
    if (type == 'cancel') {
      //Reset text of ruleValue in case of any changes
      var invoice = this.invoiceList.find(item => item.invoiceId == invoiceId);
      console.log(invoice.ruleView);
      //Since document.getElementById("invoiceName_" + id) doesnot have value property
      //<HTMLInputElement> is added to cast it into HTMLElement Type
      (<HTMLInputElement>document.getElementById("ruleValue_" + ruleId)).value = invoice.ruleView.find(rule => rule.ruleId == ruleId).ruleValue;
      (<HTMLInputElement>document.getElementById("ruleIsActive_" + ruleId)).checked = invoice.ruleView.find(rule => rule.ruleId == ruleId).isActive == 0 ? false : true;
    }
  }

  //function to save changes made to invoice
  saveEdittedRule(invoiceId: number, ruleId: number, invoiceNo: number, ruleNo: number): void {
    //Set rule model with values from view being editted
    this.ruleDetails.ruleId = ruleId;
    this.ruleDetails.invoiceId = invoiceId;
    console.log((<HTMLInputElement>document.getElementById("ruleParameterId_" + ruleId)).value,invoiceId);
    var parameterId = Number((<HTMLInputElement>document.getElementById("ruleParameterId_" + ruleId)).value);
    this.ruleDetails.parameterId = parameterId;
    
    this.ruleDetails.ruleValue = (<HTMLInputElement>document.getElementById("ruleValue_" + ruleId)).value.toString();
    this.ruleDetails.isActive = (<HTMLInputElement>document.getElementById("ruleIsActive_" + ruleId)).checked ? 1 : 0;
    this.invoiceList[invoiceNo].ruleView[ruleNo].isActive = this.ruleDetails.isActive;
    //Call service to edit Invoice
    console.log(JSON.stringify(this.ruleDetails));
    this.tariffService.editRule(ruleId, this.ruleDetails);
    this.cancelEdittingRule(invoiceId, ruleId, invoiceNo, ruleNo, 'save');
  }
  //function to add constraints to modal
  showRuleModal(invoiceId: number): void {
    console.log("clicked");
    this.newRuleInvoiceId = invoiceId;
    var parameterIdList = [];
    var invoice = this.invoiceList.find(item => item.invoiceId == invoiceId);
    //get list of parameters from service
    invoice.ruleView.forEach(function (value) {
      parameterIdList.push(value.parameterId);
    });
    this.tariffService.getParameters().subscribe(parameterList => {
      var newList = [];
      parameterList.forEach(function (value) {
        if (!parameterIdList.includes(value.parameterId)) {
          newList.push(value);
        }
      });
      this.modalParameters = newList;
      console.log("invoiceList " + JSON.stringify(this.modalParameters));
    });
    (<HTMLInputElement>document.getElementById("isActive")).checked = true;
  }

  //function to add new rule to database
  addNewRule(newRuleForm: NgForm): void {
    //check if checkbox is checked or not
    newRuleForm.value["isActive"] = newRuleForm.value["isActive"] == undefined ? true : newRuleForm.value["isActive"];
    this.ruleDetails = newRuleForm.value;
    //Set isActive as 1 if isActive is checked else 0
    this.ruleDetails.isActive = newRuleForm.value["isActive"] ? 1 : 0;
    this.ruleDetails.invoiceId = this.newRuleInvoiceId;
    //Service call to add new rule
    this.tariffService.addRule(this.ruleDetails).then(response => {
      //close addrule modal
      (<HTMLElement>document.getElementById("addnewrule_close")).click();
      //refresh invoice list
      this.getInvoiceList();
    });
  }

  //function to enable filtering
  toggleFilter(): void {
    this.filterEnabled = !this.filterEnabled;
  }

  //function to confirm deletion action
  confirmDelete(type: string, id: number): void {
    this.deleteType = type;
    this.deleteId = id;
  }

  //function to call corresponding deletion function
  delete(): void {
    if (this.deleteType == "invoice") {
      this.deleteInvoice(this.deleteId);
    }
    if (this.deleteType == "rule") {
      this.deleteRule(this.deleteId);
    }
  }
}
