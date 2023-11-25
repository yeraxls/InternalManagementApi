using Microsoft.EntityFrameworkCore;

public static class IoC
{

    public static IServiceCollection AddDependency(this IServiceCollection services)
    {
        services.AddScoped<IClientServices, ClientServices>();
        services.AddScoped<IInvoiceServices, InvoiceServices>();

        return services;
    }

    public static IServiceCollection AddDBContexts(this IServiceCollection services)
    {
        services.AddDbContext<BaseRepository>(opt => opt.UseInMemoryDatabase("baseRepository"));
        
        return services;
    }

}