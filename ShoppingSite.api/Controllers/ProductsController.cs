using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingSite.api.Data.DataModels.DTO;
using ShoppingSite.api.Data.DataModels.Entity;
using ShoppingSite.api.Data.DataModels.Model;
using ShoppingSite.api.Data.Services.Interfaces;
using System.Text.Json;

namespace ShoppingSite.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        int maxPageSize = 20;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts
            (string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if(pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var (productEntities, metadata) = await productService.GetAllProductsAsync(searchQuery, pageNumber, pageSize);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
            return Ok(mapper.Map<IEnumerable<Product>>(productEntities));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            ProductEntity? productEntity = await productService.GetProductByIdAsync(id);
            if(productEntity == null)
            {
                return NotFound("Product with the provided id was not found");
            }
            return Ok(mapper.Map<Product>(productEntity));
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(ProductDto product)
        {
            if(product == null)
            {
                return BadRequest("Please provide a product");
            }
            ProductEntity productEntity = mapper.Map<ProductEntity>(product);
            await productService.AddProductAsync(productEntity);
            await productService.SaveChangesAsync();
            
            Product toReturn = mapper.Map<Product>(productEntity);
            return CreatedAtRoute("GetProduct",
                new
                {
                    Id = productEntity.Id
                }, toReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateProduct(int id, ProductDto updatedProduct)
        {
            ProductEntity? productEntity = await productService.GetProductByIdAsync(id);
            if (productEntity == null)
            {
                return BadRequest("Product with the provided id was not found");
            }
            mapper.Map(updatedProduct, productEntity);
            return Ok(await productService.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            ProductEntity? productEntity = await productService.GetProductByIdAsync(id);
            if (productEntity == null)
            {
                return BadRequest("Product with the provided id was not found");
            }
            await productService.DeleteProductAsync(id);
            return Ok(await productService.SaveChangesAsync());
        }
    }
}
