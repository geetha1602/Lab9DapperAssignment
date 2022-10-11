using Lab9CrudUsingDapper.Models;
using Lab9CrudUsingDapper.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab9CrudUsingDapper.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IProductServices _productServices;

        public ProductController(IConfiguration configuration, IProductServices productServices)
        {
            _configuration = configuration;
            _productServices = productServices;
        }
    
        public IActionResult Index()
        {
            ProductViewModel model = new ProductViewModel();
            model.ProductsList = _productServices.GetAllProducts().ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult InsertUpdateProduct(Product product)
        {
            ProductViewModel model = new ProductViewModel();
            string url = Request.Headers["Referer"].ToString();

            string result = string.Empty;
            if (product.ProductId > 0)
            {
                result = _productServices.UpdateProduct(product);
                TempData["SuccessMsg"] = "Update Sucessfully";
                return Redirect(url);
            }
            else
            {
                result = _productServices.InsertProduct(product);
                TempData["SuccessMsg"] = "Save Sucessfully";
                return Redirect(url);
            }

             

            return View();
        }

        [HttpPost]
        public IActionResult DeleteProduct(int ProductId)
        {
            string url = Request.Headers["Referer"].ToString();

            string result = _productServices.DeleteProduct(ProductId);
            return Json(new { message = "Delete Sucessfully" }); 


        }
    }
}
