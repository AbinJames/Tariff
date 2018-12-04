using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TariffAPI.Models;
using TariffAPI.BusinessService;

namespace TariffAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TariffController : ControllerBase
    {
        private DataService dataService;
        public TariffController(TariffContext context)
        {
            dataService = new DataService(context);
            //Initializing Database
            dataService.InitializeDatabase();
        }

       
        //API link https://localhost:44355/api/Tariff
        [HttpGet]
        public IActionResult GetTariff()
        {
            return Ok("WebAPI Running");
        }

        //API link api/Tariff/ParameterMaster
        [HttpGet("ParameterMaster")]
        public IEnumerable<ParameterMaster> GetParameterMaster()
        {
            //return data in ParameterMaster Table
            return dataService.GetParameters();
        }

        //API link api/Tariff/Invoice
        [HttpGet("Invoice")]
        public IEnumerable<InvoiceViewModel> GetInvoice()
        {
            return dataService.GetInvoiceData();
        }

        // PUT: api/Tariff/EditInvoice/5
        [HttpPut("EditInvoice/{id}")]
        public IActionResult EditInvoiceMaster([FromRoute] int id, [FromBody] InvoiceMaster invoiceMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceMaster.invoiceId)
            {
                return BadRequest();
            }

            dataService.EditInvoice(id,invoiceMaster);

            return NoContent();
        }

        // PUT: api/Tariff/EditRule/5
        [HttpPut("EditRule/{id}")]
        public IActionResult EditRuleDetails([FromRoute] int id, [FromBody] RuleDetails ruleDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ruleDetails.ruleId)
            {
                return BadRequest();
            }

            dataService.EditRule(id, ruleDetails);

            return NoContent();
        }

        // POST: api/Tariff/AddInvoice
        [HttpPost("AddInvoice")]
        public IActionResult PostInvoiceMaster([FromBody] InvoicePostModel invoicePostModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dataService.AddInvoice(invoicePostModel);
            return NoContent();
        }

        // POST: api/Tariff/AddRule
        [HttpPost("AddRule")]
        public IActionResult PostRuleDetails([FromBody] RuleDetails ruleDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dataService.AddRule(ruleDetails);
            return Ok();
        }

        // DELETE RULE: api/Tariff/DeleteRule/5
        [HttpDelete("DeleteRule/{id}")]
        public IActionResult DeleteRuleDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dataService.DeleteRule(id);
            return Ok();
        }

        // DELETE RULE: api/Tariff/DeleteInvoice/5
        [HttpDelete("DeleteInvoice/{id}")]
        public IActionResult DeleteInvoiceMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dataService.DeleteInvoice(id);
            return Ok();
        }
    }
}