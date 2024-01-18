using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rabbit.API.Models;

namespace Rabbit.API.Services
{
    public interface IProductServices
    {
        public IEnumerable < Product > GetProductList();
        public Product GetProductById(int id);
        public Product AddProduct(Product product);
        public Product UpdateProduct(Product product);
        public bool DeleteProduct(int Id);
    }
}