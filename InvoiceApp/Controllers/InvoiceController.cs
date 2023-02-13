using InvoiceApp.Middlewares.Exceptions;
using InvoiceApp.Models.Models;
using InvoiceApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public ActionResult<InvoiceDto> CreateInvoice([FromBody]CreateInvoiceDto dto)
        {
            var userId = int.Parse(User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var result = _invoiceService.CreateInvoice(dto, userId);

            return Created($"/{userId}/{result.InvoiceNo}", null);
        }

        [HttpGet]
        public ActionResult<InvoiceDto> GetInvoice([FromQuery]int id)
        {
            var result = _invoiceService.GetInvoice(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InvoiceDto>> GetAll([FromHeader]int userId)
        {
            var result = _invoiceService.GetAllInvoices(userId);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public ActionResult<InvoiceDto> UpdateInvoice([FromBody]ModifyInvoiceDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = _invoiceService.UpdateInvoice(dto, userId);

            if (result == null)
            {
                return NotFound();
            }

            return Created($"/api/invoice/{userId}/{result.InvoiceNo}", result);
        }

        [HttpDelete]
        public ActionResult DeleteInvoice([FromQuery]int invoiceId)
        {
            _invoiceService.DeleteInvoice(invoiceId);
            return Ok();
        }
    }
}
