using ProductAPI.Domain.Contract.Repositories;
using ProductAPI.Domain.Contract.Services;
using ProductAPI.Infra.Repositories;
using ProductAPI.Services.Services;

namespace ProductAPI.Application.Configuration
{
    public class DependencyInjection
    {
        public static void Configure(IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IProductRepository, ProdutoRepository>();

            //Services
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
