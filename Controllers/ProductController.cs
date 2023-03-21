using ContentPlatformInterview.Data;
using ContentPlatformInterview.Utils;
using ContentPlatformInterview.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ContentPlatformInterview.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly ApiContext _context;

    private readonly IMapper _mapper;

    public ProductController(ApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public JsonResult NewProduct(Product product)
    {
        var result = _context.Products.Find(product.Id) ;

        if (result != null)
        {
            return new JsonResult(BadRequest("There is alredy a product with this Id"));
        };

        product.FinalPrice = CalculateFinalPrice(product);

        _context.Products.Add(product);

        _context.SaveChanges();

        var productResponse = MapperProductResponse(product);

        return new JsonResult(Ok(productResponse)); 
    }

    [HttpGet]
    public JsonResult GetProduct(int id)
    {
        var result = _context.Products.Find(id);

        if (result == null) 
        {
            return new JsonResult(NotFound("There is no product for this id"));
        };

        var productResponse = MapperProductResponse(result);

        return new JsonResult(Ok(productResponse));
    }

    [HttpPatch]
    public JsonResult UpdateProduct(Product product)
    {
        var result = _context.Products.Find(product.Id);

        if (result == null)
        {
            return new JsonResult(NotFound("There is no product for this id"));
        };

        _context.Entry(result).State = EntityState.Detached;

        product.FinalPrice = CalculateFinalPrice(product);

        result = product;

        _context.Products.Update(result);
        _context.SaveChanges();

        var productResponse = MapperProductResponse(product);

        return new JsonResult(Ok(productResponse));
    }

    [HttpDelete]
    public JsonResult DeleteProduct(int id)
    {
        var result = _context.Products.Find(id);

        if (result == null)
        {
            return new JsonResult(NotFound("There is no product for this id"));
        };

        _context.Entry(result).State = EntityState.Detached;

        _context.Products.Remove(result);
        _context.SaveChanges();

        return new JsonResult(Ok());
    }

    [HttpGet]
    public JsonResult GetInsideRangeProduct(double minimumPrice, double maximumPrice)
    {
        var resultList = _context.Products.Where(x => (maximumPrice > x.FinalPrice && x.FinalPrice > minimumPrice) && x.Quantity > 0) ;

        if (!resultList.Any())
        {
            return new JsonResult(NotFound("There is no product in this terms"));
        };

        List<ProductResponse> productResponseList = new List<ProductResponse>();

        foreach (var product in resultList)
        {
            var productResponse = MapperProductResponse(product);

            productResponseList.Add(productResponse);
        }

        return new JsonResult(Ok(productResponseList));
    }



    private static float CalculateFinalPrice(Product product)
    {       
        var finalPrice = product.Price * product.Quantity + (product.Price * TypeProductTax.ProductsTax(product.ProductType));
     
        return (float)finalPrice;    
    }

    private ProductResponse MapperProductResponse(Product product)
    {
        ProductResponse productResponse = _mapper.Map<ProductResponse>(product);

        switch (product!.ProductType)
        {
            case "Drink":
                productResponse.Size = $"{productResponse.Size}ocl";
                break;
            case "Food":
                productResponse.Size = $"{productResponse.Size}g";
                break;
        }

        return productResponse;
    }
}
