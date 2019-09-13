using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.Models;

namespace web_api.RepositoryServices
{
    public class ProductRepository
    {
        public Product[] GetSampleProductsData()
        {
            List<Product> prods = new List<Product>()
            {
                new Product() { Id = 24, Name = "Sony 50 HD", Description = "50in HD Televioon"},
                new Product() { Id = 25, Name = "ZBook 800T", Description = "HP Zbook Intel"}
            };
            return prods.ToArray();
        }
    }
}
