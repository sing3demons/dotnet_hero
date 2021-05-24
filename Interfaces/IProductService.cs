using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_hero.Entities;

namespace dotnet_hero.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> FindAll();
        Task<Product> FindOne(int id);
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(Product product);

        Task<IEnumerable<Product>> Search(string name);
    }
}
