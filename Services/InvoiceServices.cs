using Microsoft.EntityFrameworkCore;

public class InvoiceServices : IInvoiceServices
{
    private readonly BaseRepository _baseRepository;
    public InvoiceServices(BaseRepository baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<List<InvoiceVM>> GetInvoices()
    {
        return await _baseRepository.Queryable<Invoice>().Select(c => GenerateModelInvoice(c)).ToListAsync();
    }
    public async Task CreateInvoice(NewInvoiceVM invoice)
    {
        var invoicedb = GenerateModelInvoicedb(invoice);
        await _baseRepository.Insertar(invoicedb);
        _baseRepository.SalvarCambios();
    }

    public async Task UpdateInvoice(InvoiceVM invoice)
    {
        var invoicedb = await GetInvoiceForDB(invoice.Id);
        if (invoicedb == null)
            throw new Exception();
        UpdateModel(invoice, invoicedb);
        _baseRepository.SalvarCambios();
    }

    public async Task DeleteInvoice(int id)
    {
        var invoicedb = await GetInvoiceForDB(id);
        _baseRepository.Eliminar(invoicedb);
        _baseRepository.SalvarCambios();
    }

     private Invoice GenerateModelInvoicedb(NewInvoiceVM invoice)
    {
        return new Invoice
        {
            IdClient = invoice.IdClient,
            Amounth = invoice.Amounth,
            ItsPaid = invoice.ItsPaid
        };
    }
    private InvoiceVM GenerateModelInvoice(Invoice invoice)
    {
        return new InvoiceVM
        {
            Id = invoice.Id,
            IdClient = invoice.IdClient,
            Amounth = invoice.Amounth,
            ItsPaid = invoice.ItsPaid
        };
    }

    private void UpdateModel(InvoiceVM invoice, Invoice invoicedb)
    {
        invoicedb.ItsPaid = invoice.ItsPaid;
        invoicedb.Amounth = invoice.Amounth;
    }

    private async Task<Invoice> GetInvoiceForDB(int id)
    {
        return await _baseRepository.Queryable<Invoice>(c => c.Id == id).FirstOrDefaultAsync();
    }
}

public interface IInvoiceServices
{
    Task<List<InvoiceVM>> GetInvoices();
    Task CreateInvoice(NewInvoiceVM invoice);
    Task UpdateInvoice(InvoiceVM invoice);
    Task DeleteInvoice(int id);
}