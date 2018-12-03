using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TariffAPI.Models;

namespace TariffAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TariffController : ControllerBase
    {
        private readonly TariffContext _context;

        public TariffController(TariffContext context)
        {
            _context = context;
            //Initializing Database
            InitializeDatabase();
        }

        //Initializing database with values if it is empty
        public void InitializeDatabase()
        {
            //Initialize the InvoiceMaster table if it is empty
            if (_context.InvoiceMaster.Count() == 0)
            {
                var item = new InvoiceMaster()
                {
                    invoiceName = "Sample Invoice 1",
                    isActive = 0
                };
                _context.InvoiceMaster.Add(item);
                item = new InvoiceMaster()
                {
                    invoiceName = "Sample Invoice 2",
                    isActive = 0
                };
                _context.InvoiceMaster.Add(item);
                _context.SaveChanges();
            }

            //Initialize the ParameterMaster table if it is empty
            if (_context.ParameterMaster.Count() == 0)
            {
                var item = new ParameterMaster()
                {
                    parameterName = "VesselCapacity",
                    isActive = 0
                };
                _context.ParameterMaster.Add(item);
                item = new ParameterMaster()
                {
                    parameterName = "VesselType",
                    isActive = 0
                };
                _context.ParameterMaster.Add(item);
                item = new ParameterMaster()
                {
                    parameterName = "LOA",
                    isActive = 0
                };
                _context.ParameterMaster.Add(item);
                _context.SaveChanges();
            }

            //Initialize the RuleDetails table if it is empty
            if (_context.RuleDetails.Count() == 0)
            {
                var item = new RuleDetails()
                {
                    ruleValue = "Sample Rule 1",
                    invoiceId = 1,
                    parameterId = 1,
                    isActive = 0
                };
                _context.RuleDetails.Add(item);
                item = new RuleDetails()
                {
                    ruleValue = "Sample Rule 2",
                    invoiceId = 1,
                    parameterId = 2,
                    isActive = 0
                };
                _context.RuleDetails.Add(item);
                item = new RuleDetails()
                {
                    ruleValue = "Sample Rule 3",
                    invoiceId = 2,
                    parameterId = 1,
                    isActive = 0
                };
                _context.RuleDetails.Add(item);
                item = new RuleDetails()
                {
                    ruleValue = "Sample Rule 4",
                    invoiceId = 2,
                    parameterId = 2,
                    isActive = 0
                };
                _context.RuleDetails.Add(item);
                _context.SaveChanges();
            }
        }
        //API link https://localhost:44355/api/Tariff
        [HttpGet]
        public IActionResult GetTariff()
        {
            return Ok("WebAPI Running");
        }

        //API link api/Tariff/InvoiceMaster
        [HttpGet("InvoiceMaster")]
        public IEnumerable<InvoiceMaster> GetInvoiceMaster()
        {
            //return data in InvoiceMaster Table
            return _context.InvoiceMaster;
        }

        //API link api/Tariff/ParameterMaster
        [HttpGet("ParameterMaster")]
        public IEnumerable<ParameterMaster> GetParameterMaster()
        {
            //return data in ParameterMaster Table
            return _context.ParameterMaster;
        }

        //API link api/Tariff/RuleDetails
        [HttpGet("RuleDetails")]
        public IEnumerable<RuleDetails> GetRuleDetails()
        {
            //return data in RuleDetails Table
            return _context.RuleDetails;
        }

        //API link api/Tariff/Invoice
        [HttpGet("Invoice")]
        public IEnumerable<InvoiceViewModel> GetInvoice()
        {
            //Get rules from RuleDetails and parameterName from ParameterMaster for corresponding rules
            //Add result as RuleViewModel object
            var ruleList = (from r in _context.RuleDetails
                            join p in _context.ParameterMaster
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
            return (from i in _context.InvoiceMaster
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

        // GET: api/Tariff/InvoiceMaster/5
        [HttpGet("InvoiceMaster/{id}")]
        public async Task<IActionResult> GetInvoiceMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceMaster = await _context.InvoiceMaster.FindAsync(id);

            if (invoiceMaster == null)
            {
                return NotFound();
            }

            return Ok(invoiceMaster);
        }

        // PUT: api/Tariff/EditInvoice/5
        [HttpPut("EditInvoice/{id}")]
        public async Task<IActionResult> EditInvoiceMaster([FromRoute] int id, [FromBody] InvoiceMaster invoiceMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceMaster.invoiceId)
            {
                return BadRequest();
            }

            _context.Entry(invoiceMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // PUT: api/Tariff/EditRule/5
        [HttpPut("EditRule/{id}")]
        public async Task<IActionResult> EditRuleDetails([FromRoute] int id, [FromBody] RuleDetails ruleDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ruleDetails.ruleId)
            {
                return BadRequest();
            }

            _context.Entry(ruleDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Tariff/AddInvoice
        [HttpPost("AddInvoice")]
        public async Task<IActionResult> PostInvoiceMaster([FromBody] InvoicePostModel invoicePostModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //get invoice details from InvoicePostModel and save to database
            InvoiceMaster invoiceMaster = new InvoiceMaster()
            {
                invoiceName = invoicePostModel.invoiceName,
                //convert boolean isActive in InvoicePostModel to byte isActive for InvoiceMaster
                isActive = invoicePostModel.isActive?Convert.ToByte(1): Convert.ToByte(0)
            };
            _context.InvoiceMaster.Add(invoiceMaster);
            _context.SaveChanges();

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
                _context.RuleDetails.Add(ruleDetails);
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetInvoiceMaster", new { id = invoiceMaster.invoiceId }, invoiceMaster);
        }

        // POST: api/Tariff/AddRule
        [HttpPost("AddRule")]
        public async Task<IActionResult> PostRuleDetails([FromBody] RuleDetails ruleDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RuleDetails.Add(ruleDetails);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRuleDetails", new { id = ruleDetails.ruleId }, ruleDetails);
        }

        // DELETE RULE: api/Tariff/DeleteRule/5
        [HttpDelete("DeleteRule/{id}")]
        public async Task<IActionResult> DeleteRuleDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Find RuleDetails entry if present in table
            var ruleDetails = await _context.RuleDetails.FindAsync(id);
            //If not found display error message
            if (ruleDetails == null)
            {
                return NotFound();
            }
            //If found delete from table
            _context.RuleDetails.Remove(ruleDetails);
            await _context.SaveChangesAsync();

            return Ok(ruleDetails);
        }

        public void DeleteRuleDetailsFromInvoice(int id)
        {
            //Find RuleDetails entry if present in table
            var ruleDetails = _context.RuleDetails.Find(id);
            //If not found display error message
            if (ruleDetails == null)
            {
                return ;
            }
            //If found delete from table
            _context.RuleDetails.Remove(ruleDetails);
            _context.SaveChanges();
        }

        // DELETE RULE: api/Tariff/DeleteInvoice/5
        [HttpDelete("DeleteInvoice/{id}")]
        public async Task<IActionResult> DeleteInvoiceMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Find RuleDetails entry if present in table
            var invoiceMaster = await _context.InvoiceMaster.FindAsync(id);
            //If not found display error message
            if (invoiceMaster == null)
            {
                return NotFound();
            }
            //If found delete first delete all corressponding rules from table
            var rules = _context.RuleDetails.Where(rule => rule.invoiceId == id).ToList();
            foreach(var rule in rules)
            {
                DeleteRuleDetailsFromInvoice(rule.ruleId);
            }
            //Then delete invoice from table
            _context.InvoiceMaster.Remove(invoiceMaster);
            await _context.SaveChangesAsync();

            return Ok(invoiceMaster);
        }
    }
}