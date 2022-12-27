using Microsoft.AspNetCore.Mvc;
using api_poc.Models;
using api_poc.Models.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using api_poc.DTO;

namespace api_poc.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
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
        [AllowAnonymous] 
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
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            Product product = _productService.GetById(id);
            if (product == null) return NotFound();
            return product;
        }

        //POST : api/Product
        ///<summary>
        /// Adds a new product
        /// </summary>
        /// <param name="product">The new product</param>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public ActionResult<Product> PostProduct(ProductDTO product)
        {
            Product newProduct;
            try
            {
                newProduct = new Product(product.Name, product.Image, product.Price, product.Description);
                _productService.Add(newProduct);
                _productService.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetProduct), new { productId = newProduct.Id }, newProduct);
        }
    }
}
