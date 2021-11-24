using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using SalesManagementSystem.Manager;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Controllers
{
	public class SaleController : Controller
	{
		private SalesManager salesManager;
		public SaleController()
		{
			salesManager = new SalesManager();
		}
		//
		[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
		public class MultipleButtonAttribute : ActionNameSelectorAttribute
		{
			public string Name { get; set; }
			public string Argument { get; set; }
			public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
			{
				var isValidName = false;
				var keyValue = string.Format("{0}:{1}", Name, Argument);
				var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);
				if (value != null)
				{
					controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
					isValidName = true;
				}
				return isValidName;
			}
		}
		// GET: /Sale/
		[HttpGet]
		public ActionResult Create()
		{
			ViewBag.Items = salesManager.GetAllItemsForDropdown();
			ViewBag.SalesItems = salesManager.GetAllSalesItem();
		    ViewBag.TotalPrice = salesManager.GetTotalSumPrice();
			return View();
		}
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult Create(Sale sale)
        {
            if (ModelState.IsValid)
            {
                string message = salesManager.SaveSales(sale);
                if (message=="Save Successful")
                {
                    ModelState.Clear();
                    salesManager.ClearSalesItem();
                }
                ViewBag.MessageSale = message;
            }
            else
            {
                ViewBag.MessageSale = "Model State Invalid";
            }
            ViewBag.Items = salesManager.GetAllItemsForDropdown();
            ViewBag.SalesItems = salesManager.GetAllSalesItem();
            ViewBag.TotalPrice = salesManager.GetTotalSumPrice();
            return View();
        }

		[HttpPost]
		[MultipleButton(Name = "action", Argument = "Save")]
		public ActionResult Save(Item item)
		{
			ViewBag.Message = salesManager.Save(item);
			//ModelState.Clear();
			ViewBag.Items = salesManager.GetAllItemsForDropdown();
			ViewBag.SalesItems = salesManager.GetAllSalesItem();
            ViewBag.TotalPrice = salesManager.GetTotalSumPrice();
			return View("Create");
		}

	    public JsonResult GetSelectedItem(int itmId)
	    {
	        Item item = salesManager.GetSelectedItem(itmId);
	        return Json(item);
	    }

        public ActionResult Index()
        {
            List<Sale> salesList = salesManager.GetAllSalesList();
            return View(salesList);
        }
	}
}