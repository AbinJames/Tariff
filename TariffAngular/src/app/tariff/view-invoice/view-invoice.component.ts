import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TariffService } from 'src/app/tariff.service';
import { InvoiceSet } from 'src/app/models/invoiceset.model';
import { InvoiceMaster } from 'src/app/models/invoicemaster.model';
import { RuleDetails } from 'src/app/models/ruledetails.model';
import { ParameterMaster } from 'src/app/models/parametermaster.model';
import { RuleSet } from 'src/app/models/ruleset.model';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-view-invoice',
  templateUrl: './view-invoice.component.html',
  styleUrls: ['./view-invoice.component.css']
})

export class ViewInvoiceComponent implements OnInit {

  constructor(private http: HttpClient, private tariffService: TariffService) { }
  invoices: InvoiceSet[];
  tempInvoices: InvoiceSet[];
  checkBoxInvoices: InvoiceSet[];
  textBoxInvoices: InvoiceSet[];
  newRuleInvoiceId: number;
  invoice: InvoiceMaster = {
    invoiceId: null,
    invoiceName: "default",
    isActive: 0
  };
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
  ngOnInit() {
    //get invoice and corresponding rules from service

    this.tariffService.getInvoices().subscribe(invoiceList => {
      //main invoice list
      this.invoices = invoiceList;
      //backup invoice list for populating the main list after filtering
      this.tempInvoices = invoiceList;
      //invoice list after filtering based on isActive Filtering
      this.checkBoxInvoices = invoiceList;
      this.textBoxInvoices = invoiceList;
      console.log("Invoices " + JSON.stringify(this.invoices));
    });

    //get list of parameters from service
    this.tariffService.getParameters().subscribe(parameterList => {
      this.parameters = parameterList;
      console.log("Parameters " + JSON.stringify(this.parameters));
    });
  }
  //function to change collapse icon
  changeIcon(id: number): void {
    //change arrow icon on click
    if (status == "true") {
      //show down arrow when rules are not being displayed
      document.getElementById("arrow_" + id.toString()).setAttribute("class", "fa fa-angle-down");
    }
    else {
      //show up arrow when rules are being displayed
      document.getElementById("arrow_" + id.toString()).setAttribute("class", "fa fa-angle-up");
    }
  }

  //Function to delete rule of an invoice
  deleteRule(id: number): void {
    //Call service to delete rule
    this.tariffService.deleteRule(id);
    var deleteInvoiceId = 0;
    //Find invoice with corresponding rule and delete the rule from invoice;
    this.invoices.forEach(function (invoiceValue) {
      invoiceValue.ruleView.forEach(function (ruleValue) {
        if (ruleValue.id == id) {
          deleteInvoiceId = ruleValue.invoiceId;
        }
      });
    });
    this.invoices.find(item => item.id == deleteInvoiceId).ruleView = this.invoices.find(item => item.id == deleteInvoiceId).ruleView.filter(rule => rule.id != id);
    this.invoices = this.tempInvoices;
    (<HTMLElement>document.getElementById("deletion_close")).click();
  }

  //Function to delete invoice
  deleteInvoice(id: number): void {
    //Call service to delete invoice
    this.tariffService.deleteInvoice(id);
    this.invoices = this.tempInvoices = this.invoices.filter(item => item.id != id);
    (<HTMLElement>document.getElementById("deletion_close")).click();
  }

  //function to enable invoice fields to editable
  setEditInvoice(id: number): void {
    //The edit and delete buttons are set to hidden
    document.getElementById("editInvoicebutton_" + id).hidden = true;
    document.getElementById("deleteInvoicebutton_" + id).hidden = true;
    //The save and cancel buttons are set to visible
    document.getElementById("saveInvoicebutton_" + id).hidden = false;
    document.getElementById("cancelInvoicebutton_" + id).hidden = false;
    //Remove readonly attribute of corresponding invoice name and class to border-1 to display input border
    document.getElementById("invoiceName_" + id).removeAttribute("readonly");
    document.getElementById("invoiceName_" + id).setAttribute("class", "border-2");
    //Remove hidden attribute of corresponding invoice isActive 
    document.getElementById("invoiceIsActive_" + id).hidden = false;
    document.getElementById("invoiceCheckIcons_" + id).hidden = true;
  }

  //function to cancel editting invoice
  cancelEdittingInvoice(id: number, type: string): void {
    //The edit and delete buttons are set to visible
    document.getElementById("editInvoicebutton_" + id).hidden = false;
    document.getElementById("deleteInvoicebutton_" + id).hidden = false;
    //The save and cancel buttons are set to hidden
    document.getElementById("saveInvoicebutton_" + id).hidden = true;
    document.getElementById("cancelInvoicebutton_" + id).hidden = true;
    //Set readonly attribute of corresponding invoice name and class to border-0 to remove input border
    document.getElementById("invoiceName_" + id).setAttribute("readonly", "true");
    document.getElementById("invoiceName_" + id).setAttribute("class", "border-0");
    //set hidden attribute of corresponding invoice isActive 
    document.getElementById("invoiceIsActive_" + id).hidden = true;
    document.getElementById("invoiceCheckIcons_" + id).hidden = false;
    if (type == 'cancel') {
      //Reset text of invoiceName in case of any changes
      var invoice = this.invoices.find(item => item.id === id);
      //Since document.getElementById("invoiceName_" + id) doesnot have value property
      //<HTMLInputElement> is added to cast it into HTMLElement Type
      //Same goes for document.getElementById("invoiceIsActive_" + id)
      (<HTMLInputElement>document.getElementById("invoiceName_" + id)).value = invoice.invoiceName;
      (<HTMLInputElement>document.getElementById("invoiceIsActive_" + id)).checked = invoice.isActive == 0 ? false : true;
    }
  }

  //function to save changes made to invoice
  saveEdittedInvoice(id: number): void {
    //Set invoice model with values from view being editted
    this.invoice.invoiceId = id;
    this.invoice.invoiceName = (<HTMLInputElement>document.getElementById("invoiceName_" + id)).value.toString();
    this.invoice.isActive = (<HTMLInputElement>document.getElementById("invoiceIsActive_" + id)).checked ? 1 : 0;
    //Call service to edit Invoice
    this.tariffService.editInvoice(id, this.invoice);
    this.cancelEdittingInvoice(id, 'save');
  }

  //function to enable rule fields to editable
  setEditRule(id: number): void {
    //The edit and delete buttons are set to hidden
    document.getElementById("editRulebutton_" + id).hidden = true;
    document.getElementById("deleteRulebutton_" + id).hidden = true;
    //The save and cancel buttons are set to visible
    document.getElementById("saveRulebutton_" + id).hidden = false;
    document.getElementById("cancelRulebutton_" + id).hidden = false;
    //Remove readonly attribute of corresponding rule value and class to border-1 to display input border
    document.getElementById("ruleValue_" + id).removeAttribute("readonly");
    document.getElementById("ruleValue_" + id).setAttribute("class", "border-2");
    //Set parameter name field as hidden and show option selection in its place
    document.getElementById("ruleParameterName_" + id).hidden = true;
    document.getElementById("ruleParameterOption_" + id).hidden = false;
    //Remove hidden attribute of corresponding rule isActive 
    document.getElementById("ruleIsActive_" + id).hidden = false;
    document.getElementById("ruleCheckIcons_" + id).hidden = true;
  }

  //function to cancel editting rule
  cancelEdittingRule(invoiceId: number, ruleId: number, type: string): void {
    console.log(invoiceId, ruleId);
    //The edit and delete buttons are set to visible
    document.getElementById("editRulebutton_" + ruleId).hidden = false;
    document.getElementById("deleteRulebutton_" + ruleId).hidden = false;
    //The save and cancel buttons are set to hidden
    document.getElementById("saveRulebutton_" + ruleId).hidden = true;
    document.getElementById("cancelRulebutton_" + ruleId).hidden = true;
    //Set readonly attribute of corresponding rulevalue and class to border-0 to remove input border
    document.getElementById("ruleValue_" + ruleId).setAttribute("readonly", "true");
    document.getElementById("ruleValue_" + ruleId).setAttribute("class", "border-0");
    //Set hidden attribute of corresponding rule isActive 
    document.getElementById("ruleIsActive_" + ruleId).hidden = true;
    document.getElementById("ruleCheckIcons_" + ruleId).hidden = false;
    //Set option selection as hidden and show parameter name field  in its place
    document.getElementById("ruleParameterName_" + ruleId).hidden = false;
    document.getElementById("ruleParameterOption_" + ruleId).hidden = true;
    if (type == 'cancel') {
      //Reset text of ruleValue in case of any changes
      var invoice = this.invoices.find(item => item.id == invoiceId);
      console.log(invoice.ruleView);
      //Since document.getElementById("invoiceName_" + id) doesnot have value property
      //<HTMLInputElement> is added to cast it into HTMLElement Type
      (<HTMLInputElement>document.getElementById("ruleValue_" + ruleId)).value = invoice.ruleView.find(rule => rule.id == ruleId).ruleValue;
      (<HTMLInputElement>document.getElementById("ruleIsActive_" + ruleId)).checked = invoice.ruleView.find(rule => rule.id == ruleId).isActive == 0 ? false : true;
    }
  }

  //function to save changes made to invoice
  saveEdittedRule(invoiceId: number, ruleId: number): void {
    //Set rule model with values from view being editted
    this.ruleDetails.ruleId = ruleId;
    this.ruleDetails.invoiceId = invoiceId;
    this.ruleDetails.parameterId = Number((<HTMLInputElement>document.getElementById("ruleParameterId_" + ruleId)).value);
    this.ruleDetails.ruleValue = (<HTMLInputElement>document.getElementById("ruleValue_" + ruleId)).value.toString();
    this.ruleDetails.isActive = (<HTMLInputElement>document.getElementById("ruleIsActive_" + ruleId)).checked ? 1 : 0;
    //Call service to edit Invoice
    this.tariffService.editRule(ruleId, this.ruleDetails);
    this.cancelEdittingRule(invoiceId, ruleId, 'save');
  }
  //function to add constraints to modal
  showRuleModal(invoiceId: number): void {
    console.log("clicked");
    this.newRuleInvoiceId = invoiceId;
    var parameterNameList = [];
    var invoice = this.invoices.find(item => item.id == invoiceId);
    //get list of parameters from service
    invoice.ruleView.forEach(function (value) {
      parameterNameList.push(value.parameterName);
    });
    this.tariffService.getParameters().subscribe(parameterList => {
      var newList = [];
      parameterList.forEach(function (value) {
        if (!parameterNameList.includes(value.parameterName)) {
          newList.push(value);
        }
      });
      this.modalParameters = newList;
      console.log("Invoices " + JSON.stringify(this.modalParameters));
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
      this.tariffService.getInvoices().subscribe(invoiceList => {
        //main invoice list
        this.invoices = invoiceList;
        //backup invoice list for populating the main list after filtering
        this.tempInvoices = invoiceList;
        //invoice list after filtering based on isActive Filtering
        this.checkBoxInvoices = invoiceList;
        console.log("Invoices " + JSON.stringify(this.invoices));
      });
    });


  }

  //function to enable filtering
  enableFilter(): void {
    console.log("clicked");
    //Set all elements for filtering to visible
    document.getElementById("buttonBeforeFilter").hidden = true;
    document.getElementById("nameFilter").hidden = false;
    document.getElementById("isActiveFilter").hidden = false;
    document.getElementById("buttonAfterFilter").hidden = false;
  }

  //function to disable filtering
  disableFilter(): void {
    console.log("clicked");
    //Set all elements for filtering to hidden and previous elements to visible
    document.getElementById("buttonBeforeFilter").hidden = false;
    document.getElementById("nameFilter").hidden = true;
    document.getElementById("isActiveFilter").hidden = true;
    document.getElementById("buttonAfterFilter").hidden = true;
    //reinitialize invoice lists
    this.invoices = this.tempInvoices;
    this.checkBoxInvoices = this.tempInvoices;
  }

  //function to filter between Active and Not Active
  isActiveFiltering(): void {
    if ((<HTMLInputElement>document.getElementById("isActiveFilter")).value == "2") {
      //filter invoices from textbox filterlist
      this.invoices = this.textBoxInvoices.filter(item => item.isActive == 1);
      //save checkbox only filtered invoices
      this.checkBoxInvoices = this.tempInvoices.filter(item => item.isActive == 1);
    }
    else if ((<HTMLInputElement>document.getElementById("isActiveFilter")).value == "3") {
      //filter invoices from textbox filterlist
      this.invoices = this.textBoxInvoices.filter(item => item.isActive == 0);
      //save checkbox only filtered invoices
      this.checkBoxInvoices = this.tempInvoices.filter(item => item.isActive == 0);
    }
    else {
      //set invoices to textbox filterlist where no checkbox filtering has been done
      this.invoices = this.textBoxInvoices;
    }
  }

  //function to filter invoice name
  nameFiltering(): void {
    console.log("change");
    //filter invoices from checkbox filtered invoicelist
    this.invoices = this.checkBoxInvoices.filter(item => item.invoiceName.toLowerCase().includes((<HTMLInputElement>document.getElementById("nameFilter")).value.toLowerCase()));
    //set textbox ony filtered list of invoice
    //no checkbox filtering included
    this.textBoxInvoices = this.tempInvoices.filter(item => item.invoiceName.toLowerCase().includes((<HTMLInputElement>document.getElementById("nameFilter")).value.toLowerCase()));
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
