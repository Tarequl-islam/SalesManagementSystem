using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesManagementSystem.Gateway;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Manager
{
    public class SalesManager
    {
        private SalesGateway salesGateway;

        public SalesManager()
        {
            salesGateway=new SalesGateway();
        }
        public List<SelectListItem> GetAllItemsForDropdown()
        {
            List<Item> itemList = salesGateway.GetAllItems();
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "--Select--",
                    Value = ""
                }
            };
            foreach (Item item in itemList)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = item.Name;
                selectListItem.Value = item.Id.ToString();
                selectListItems.Add(selectListItem);
            }
            return selectListItems;
        }

        public string Save(Item item)
        {
            int rowEffect = salesGateway.Save(item);
            if (rowEffect > 0)
            {
                return "Save Successful";
            }
            else
            {
                return "Save Failed";
            }
        }

        public List<Item> GetAllSalesItem()
        {
            return salesGateway.GetAllSalesItem();
        }

        public Item GetSelectedItem(int itmId)
        {
            return salesGateway.GetSelectedItem(itmId);
        }

        public int GetTotalSumPrice()
        {
            return salesGateway.GetTotalSumPrice();
        }

        public string SaveSales(Sale sale)
        {
            int rowEffect = salesGateway.SaveSales(sale);
            if (rowEffect > 0)
            {
                return "Save Successful";
            }
            else
            {
                return "Save Failed";
            }
        }

        public int ClearSalesItem()
        {
            return salesGateway.ClearSalesItem();
        }

        public List<Sale> GetAllSalesList()
        {
            return salesGateway.GetAllSalesList();
        }
    }
}