using ProductAPI.Domain.Contract.Repositories;
using ProductAPI.Domain.Entities;


namespace ProductAPI.Infra.Repositories
{
    public class ProdutoRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProdutoRepository(ProductAPIContext context) 
            : base(context) { }


    }
}
