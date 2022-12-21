using Microsoft.AspNetCore.Mvc;
using api_poc.Models;
using api_poc.Models.IService;

namespace api_poc.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService context)
        {
            _productService = context;
        }

        //GET : api/Product
        ///<summary>
        /// Get a list of products
        /// </summary>
        /// <return>Array of products</return>
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetAll();
        }

        //GET : api/Product/1
        ///<summary>
        /// Get the product with given id
        /// </summary>
        /// <param name="Id">The id of the product</param>
        /// <return>The product</return>
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            Product product = _productService.GetById(id);
            if (product == null) return NotFound();
            return product;
        }
    }
}
