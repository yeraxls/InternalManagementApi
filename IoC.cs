using Microsoft.EntityFrameworkCore;

public static class IoC
{

    public static IServiceCollection AddDependency(this IServiceCollection services)
    {
        services.AddScoped<IClientServices, ClientServices>();

        return services;
    }

    public static IServiceCollection AddDBContexts(this IServiceCollection services)
    {
        services.AddDbContext<BaseRepository>(opt => opt.UseInMemoryDatabase("baseRepository"));
        
        return services;
    }

}