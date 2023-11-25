using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternalManagement.Controllers;

[ApiController]
[Route("[Controller]")]
public class InvoiceController : ControllerBase
{
    private readonly ILogger<InvoiceController> _logger;
    private readonly IInvoiceServices _invoiceServices;

    public InvoiceController(ILogger<InvoiceController> logger, IInvoiceServices invoiceServices)
    {
        _logger = logger;
        _invoiceServices = invoiceServices;
    }

    [HttpGet()]
    public async Task<IActionResult> GetInvoices()
    {
        try
        {
            return Ok(await _invoiceServices.GetInvoices());
        }
        catch
        {
            return BadRequest(new List<Invoice>());
        }
    }

    [HttpPost()]
    public async Task<IActionResult> CreateInvoice([FromBody] NewInvoiceVM invoice)
    {
        try
        {
            await _invoiceServices.CreateInvoice(invoice);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateInvoice([FromBody] InvoiceVM invoice)
    {
        try
        {
            await _invoiceServices.UpdateInvoice(invoice);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        try
        {
            await _invoiceServices.DeleteInvoice(id);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}
