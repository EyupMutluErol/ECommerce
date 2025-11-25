using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Models.GraphicModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.Controllers
{
    public class GraphicController : Controller
    {

        private readonly IProductService _productService;

        public GraphicController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult PieChart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VisualizeProductResult()
        {
            var data= GetProductList();
            return Json(data);
        }
        private List<PieChartModel> GetProductList()
        {


          var product =_productService.GetProductStockSummary();
            return product.Select(x => new PieChartModel
            {
                product = x.ProductName,
                stock = x.StockQuantity
            }).ToList();
         

        }

        [HttpGet]
        public IActionResult LineChart()
        {
            return View();

        }
        [HttpGet]
        public IActionResult VisualizeLineChart()
        {
            var data = GetLineChartList();
            return Json(data);
        }

        private List<LineChartModel> GetLineChartList()
        {


            var product = _productService.GetProductStockSummary();
            return product.Select(x => new LineChartModel
            {
                brand=x.ProductName,
                stock = x.StockQuantity
            }).ToList();


        }

        public IActionResult ColumnChart()
        {
            return View();
        }
        [HttpGet]
        public IActionResult VisualizeColumnChart()
        {
            var data = GetProductList();
            return Json(data);
        }

    }
}
