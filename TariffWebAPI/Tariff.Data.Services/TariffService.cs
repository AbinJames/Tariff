using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tariff.Data.Models;
using Tariff.Data.Services.Context;
using Tariff.Data.Services.Models;

namespace Tariff.Data.Services
{
    public class TariffService
    {
        private readonly TariffContext tariffContext;
        public TariffService(TariffContext context)
        {
            tariffContext = context;
        }
        //Initializing database with values if it is empty
        public void InitializeDatabase()
        {
            //Initialize the InvoiceMaster table if it is empty
            if (tariffContext.InvoiceMaster.Count() == 0)
            {
                var item = new InvoiceMaster()
                {
                    invoiceName = "Sample Invoice 1",
                    isActive = 0
                };
                tariffContext.InvoiceMaster.Add(item);
                item = new InvoiceMaster()
                {
                    invoiceName = "Sample Invoice 2",
                    isActive = 0
                };
                tariffContext.InvoiceMaster.Add(item);
                tariffContext.SaveChanges();
            }

            //Initialize the ParameterMaster table if it is empty
            if (tariffContext.ParameterMaster.Count() == 0)
            {
                var item = new ParameterMaster()
                {
                    parameterName = "VesselCapacity",
                    isActive = 0
                };
                tariffContext.ParameterMaster.Add(item);
                item = new ParameterMaster()
                {
                    parameterName = "VesselType",
                    isActive = 0
                };
                tariffContext.ParameterMaster.Add(item);
                item = new ParameterMaster()
                {
                    parameterName = "LOA",
                    isActive = 0
                };
                tariffContext.ParameterMaster.Add(item);
                tariffContext.SaveChanges();
            }

            //Initialize the RuleDetails table if it is empty
            if (tariffContext.RuleDetails.Count() == 0)
            {
                var item = new RuleDetails()
                {
                    ruleValue = "Sample Rule 1",
                    invoiceId = 1,
                    parameterId = 1,
                    isActive = 0
                };
                tariffContext.RuleDetails.Add(item);
                item = new RuleDetails()
                {
                    ruleValue = "Sample Rule 2",
                    invoiceId = 1,
                    parameterId = 2,
                    isActive = 0
                };
                tariffContext.RuleDetails.Add(item);
                item = new RuleDetails()
                {
                    ruleValue = "Sample Rule 3",
                    invoiceId = 2,
                    parameterId = 1,
                    isActive = 0
                };
                tariffContext.RuleDetails.Add(item);
                item = new RuleDetails()
                {
                    ruleValue = "Sample Rule 4",
                    invoiceId = 2,
                    parameterId = 2,
                    isActive = 0
                };
                tariffContext.RuleDetails.Add(item);
                tariffContext.SaveChanges();
            }
        }

        public IEnumerable<ParameterMaster> GetParameters()
        {
            //return data in ParameterMaster Table
            return tariffContext.ParameterMaster;
        }

        public IEnumerable<InvoiceViewModel> GetInvoiceData()
        {
            //Get rules from RuleDetails and parameterName from ParameterMaster for corresponding rules
            //Add result as RuleViewModel object
            var ruleList = (from r in tariffContext.RuleDetails
                            join p in tariffContext.ParameterMaster
                            on r.parameterId equals p.parameterId
                            select new RuleViewModel
                            {
                                id = r.ruleId,
                                invoiceId = r.invoiceId,
                                parameterName = p.parameterName,
                                ruleValue = r.ruleValue,
                                isActive = r.isActive
                            })
                            .ToList();
            //Get invoice from InvoiceMaster and corresponding rules from ruleList
            //Save as InvoiceViewModel object and send to client
            return (from i in tariffContext.InvoiceMaster
                    join r in ruleList
                    on i.invoiceId equals r.invoiceId
                    into ruleGroup
                    select new InvoiceViewModel
                    {
                        id = i.invoiceId,
                        invoiceName = i.invoiceName,
                        ruleView = ruleGroup,
                        isActive = i.isActive
                    })
                    .ToList();
        }

        //function to edit invoice details
        public async void EditInvoice(int id, InvoiceMaster invoiceMaster)
        {

            tariffContext.Entry(invoiceMaster).State = EntityState.Modified;

            try
            {
                await tariffContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // function to edit rule details
        public void EditRule(int id, RuleDetails ruleDetails)
        {
            tariffContext.Entry(ruleDetails).State = EntityState.Modified;

            try
            {
                tariffContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // function to add invoice and corresponding rules to database
        public void AddInvoice(InvoicePostModel invoicePostModel)
        {

            //get invoice details from InvoicePostModel and save to database
            InvoiceMaster invoiceMaster = new InvoiceMaster()
            {
                invoiceName = invoicePostModel.invoiceName,
                //convert boolean isActive in InvoicePostModel to byte isActive for InvoiceMaster
                isActive = invoicePostModel.isActive ? Convert.ToByte(1) : Convert.ToByte(0)
            };
            tariffContext.InvoiceMaster.Add(invoiceMaster);

            //get each rule from InvoicePostModel and save to database with invoiceId from above saved invoice
            foreach (Rule rule in invoicePostModel.ruleList)
            {
                RuleDetails ruleDetails = new RuleDetails()
                {
                    invoiceId = invoiceMaster.invoiceId,
                    parameterId = Convert.ToInt32(rule.parameterId),
                    ruleValue = rule.ruleValue,
                    //convert boolean isActive in InvoicePostModel for each rule to byte isActive for RuleDetails
                    isActive = rule.isActive ? Convert.ToByte(1) : Convert.ToByte(0)
                };
                tariffContext.RuleDetails.Add(ruleDetails);
            }
            tariffContext.SaveChanges();

        }


        public void AddRule(RuleDetails ruleDetails)
        {
            tariffContext.RuleDetails.Add(ruleDetails);
            tariffContext.SaveChanges();
        }
        public void DeleteRule(int id)
        {
            //Find RuleDetails entry if present in table
            var ruleDetails = tariffContext.RuleDetails.Find(id);
            //If found delete from table
            tariffContext.RuleDetails.Remove(ruleDetails);
            tariffContext.SaveChanges();
        }

        public void DeleteRuleDetailsFromInvoice(int id)
        {
            //Find RuleDetails entry if present in table
            var ruleDetails = tariffContext.RuleDetails.Find(id);
            //If not found display error message
            if (ruleDetails == null)
            {
                return;
            }
            //If found delete from table
            tariffContext.RuleDetails.Remove(ruleDetails);
            tariffContext.SaveChanges();
        }

        public InvoiceMaster DeleteInvoice(int id)
        {
            //Find RuleDetails entry if present in table
            var invoiceMaster = tariffContext.InvoiceMaster.Find(id);
            //If found delete first delete all corressponding rules from table
            var rules = tariffContext.RuleDetails.Where(rule => rule.invoiceId == id).ToList();
            foreach (var rule in rules)
            {
                DeleteRuleDetailsFromInvoice(rule.ruleId);
            }
            //Then delete invoice from table
            tariffContext.InvoiceMaster.Remove(invoiceMaster);
            tariffContext.SaveChanges();
            return invoiceMaster;
        }
    }
}
