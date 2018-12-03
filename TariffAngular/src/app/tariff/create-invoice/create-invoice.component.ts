import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { TariffService } from 'src/app/tariff.service';
import { InvoiceMaster } from 'src/app/models/invoicemaster.model';
import { FormGroup, Validators, FormArray } from "@angular/forms";
import { ParameterMaster } from 'src/app/models/parametermaster.model';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-create-invoice',
  templateUrl: './create-invoice.component.html',
  styleUrls: ['./create-invoice.component.css']
})

export class CreateInvoiceComponent implements OnInit {

  constructor(private tariffService: TariffService,
    private http: HttpClient,
    private sanitizer: DomSanitizer,
    private formBuilder: FormBuilder) { }

  invoiceForm: FormGroup;
  showAddRule: boolean = true;
  showRuleTitle:boolean=false;
  parameterExists: boolean = false;
  parameters: ParameterMaster[];

  ngOnInit() {

    //Set invoice form with controls for
    //invoicename, isActive, and array of rules
    this.invoiceForm = this.formBuilder.group({
      invoiceName: ['', [Validators.required, Validators.minLength(2)]],
      isActive: [true, [Validators.required]]
      ,
      ruleList: this.formBuilder.array([])
    });

    //get list of parameters from service
    this.tariffService.getParameters().subscribe(parameterList => {
      this.parameters = parameterList;
      console.log("Invoices " + JSON.stringify(this.parameters));
    });
  }

  //function to specifiy controls
  //(parameterId, ruleValue, isActive)
  //for ruleList in InvoiceForm on button click
  createGroup(): FormGroup {
    return this.formBuilder.group({
      parameterId: ['', Validators.required],
      ruleValue: ['', Validators.required],
      isActive: [true, [Validators.required]],
    });
  }

  //function to add rule controls
  //(parameterId, ruleValue, isActive)
  //to ruleList in InvoiceForm on button click
  addRule() {
    //Display the row of titles in rules when AddRule is clicked
    if((this.invoiceForm.get('ruleList') as FormArray).length == 0)
    {
      this.showRuleTitle = true;
    }
    (this.invoiceForm.get('ruleList') as FormArray).push(this.createGroup());
    //If rules have been added for each parameter then the AddRule button will no longer be displayed
    //This is because parameters cannot be repeated in different rules
    if ((this.invoiceForm.get('ruleList') as FormArray).length == this.parameters.length) {
      this.showAddRule = false;
    }
  }

  //function to remove rule controls
  //(parameterId, ruleValue, isActive)
  //from ruleList in InvoiceForm on button click
  removeRule(index: number): void {
    (this.invoiceForm.get('ruleList') as FormArray).removeAt(index);

    //This is display AddRule button incase it is longer displayed due to the codition in addRule()
    if (!this.showAddRule) {
      this.showAddRule = true;
    }
  }

  //function to check if parameter is being repeated for rules
  checkChange(): void {
    //gets current list of rules in ruleList of InvoiceForm
    const index = (this.invoiceForm.get('ruleList') as FormArray).length;
    let parameterList = [];
    this.parameterExists = false;
    for (let i = 0; i < index; i++) {
      //If a parameterId is not in parameterList it is added to the list
      //If a parameterId is already in the parameterList then parameterId is repeating
      //then parameterExist is set as true to display error message and loop is exited
      if (parameterList[(this.invoiceForm.get('ruleList') as FormArray).at(i).get("parameterId").value] === undefined) {
        parameterList[(this.invoiceForm.get('ruleList') as FormArray).at(i).get("parameterId").value] = 1;
      }
      else {
        this.parameterExists = true;
        return;
      }
    }
  }
  saveInvoice(invoiceForm: FormGroup): void {
    console.log(invoiceForm.value);
    //Call service function to post invoiceform to database/api
    this.tariffService.addInvoice(invoiceForm.value);
  }

}
