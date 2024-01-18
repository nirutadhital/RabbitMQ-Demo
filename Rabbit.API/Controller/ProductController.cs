using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rabbit.API.Models;
using Rabbit.API.RabbitMQ;
using Rabbit.API.Services;

namespace Rabbit.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
         private readonly IProductServices productService;
        private readonly IRabbitMQProducer _rabitMQProducer;
        public ProductController(IProductServices _productService, IRabbitMQProducer rabitMQProducer) {
                productService = _productService;
                _rabitMQProducer = rabitMQProducer;
            }

        [HttpGet("productlist")]
        public IEnumerable < Product > ProductList() {
                var productList = productService.GetProductList();
                return productList;
        }

            [HttpGet("getproductbyid")]
        public Product GetProductById(int Id) {
                return productService.GetProductById(Id);
            }
            
        [HttpPost("addproduct")]
        public Product AddProduct(Product product) {

            _rabitMQProducer.SendProductMessageAsync(product);
            // var productData = productService.AddProduct(product);
                // send the inserted product data to the queue and consumer will listening this data from queue
                // return  productData;
                return null;
                 
        }

        [HttpPut("updateproduct")]
        public Product UpdateProduct(Product product) {
                return productService.UpdateProduct(product);
        }

        [HttpDelete("deleteproduct")]
        public bool DeleteProduct(int Id) {
            return productService.DeleteProduct(Id);
        }
    }
}