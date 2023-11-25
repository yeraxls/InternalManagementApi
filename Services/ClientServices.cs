using Microsoft.EntityFrameworkCore;

public class ClientServices : IClientServices
{
    private readonly BaseRepository _baseRepository;
    public ClientServices(BaseRepository baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<List<ClientVM>> GetClients()
    {
        return await _baseRepository.Queryable<Client>().Select(c => GenerateModelClient(c)).ToListAsync();
    }
    public async Task CreateClient(NewClientVM client)
    {
        var clientdb = GenerateModelClientdb(client);
        await _baseRepository.Insertar(clientdb);
        _baseRepository.SalvarCambios();
    }

    public async Task UpdateClient(ClientVM client)
    {
        var clientdb = await getClientForDB(client.Id);
        if (clientdb == null)
            throw new Exception();
        UpdateModel(client, clientdb);
        _baseRepository.SalvarCambios();
    }

    public async Task DeleteClient(int id)
    {
        var clientdb = await getClientForDB(id);
        _baseRepository.Eliminar(clientdb);
        _baseRepository.SalvarCambios();
    }

     private Client GenerateModelClientdb(NewClientVM client)
    {
        return new Client
        {
            Name = client.Name,
            Surname = client.Surname,
            Phone = client.Phone,
            Mail = client.Mail
        };
    }
    private ClientVM GenerateModelClient(Client client)
    {
        return new ClientVM
        {
            Id = client.Id,
            Name = client.Name,
            Surname = client.Surname,
            Phone = client.Phone,
            Mail = client.Mail
        };
    }

    private void UpdateModel(ClientVM client, Client clientdb)
    {
        clientdb.Name = client.Name;
        clientdb.Surname = client.Surname;
        clientdb.Phone = client.Phone;
        clientdb.Mail = client.Mail;
    }

    private async Task<Client> getClientForDB(int id)
    {
        return await _baseRepository.Queryable<Client>(c => c.Id == id).FirstOrDefaultAsync();
    }
}

public interface IClientServices
{
    Task<List<ClientVM>> GetClients();
    Task CreateClient(NewClientVM client);
    Task UpdateClient(ClientVM client);
    Task DeleteClient(int id);
}