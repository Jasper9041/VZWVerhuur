using System.Linq;
using System.Web.Mvc;
using SportsStore.Models.Domain;
using System;
using System.Collections.Generic;
using PagedList;

namespace SportsStore.Controllers
{
    
    public class StoreController : Controller
    {

        private IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;


        public StoreController(IProductRepository productsRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productsRepository;
            this.categoryRepository = categoryRepository;
        }

        public ActionResult Index(int? page , string categoryId)
        {
            Category category;
            IEnumerable<Product> products = productRepository.FindAll().Where(i => i.InStock == true).OrderBy(p => p.Name).ToList();
            if (categoryId == null)
                products = productRepository.FindAll().Where(i => i.InStock == true).OrderBy(b => b.Name).ToList();
            else
            {
                category = categoryRepository.FindByName(categoryId);
                products = category.Products.Where(i => i.InStock == true).OrderBy(b => b.Name);
            }
            //ViewBag.Categories = GetCategoriesSelectList(categoryId);
            ViewBag.Alle = categoryRepository.FindAll();
            ViewBag.CategoryId = categoryId;
            int pageSize = 25;
            int pageNumber = (page ?? 1);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_StorePartial",products.ToPagedList(pageNumber,pageSize));
            }

            return View(products.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Details(int categoryId)
        {
            Product product = productRepository.FindById(categoryId);
            if(product == null || !product.InStock)
            {
                TempData["info"] = "Het product bestaat niet of is momenteel niet beschikbaar.";
                return View("Index");
            }
            return View(product);
        }
        private SelectList GetCategoriesSelectList(int selectedValue = 0)
        {
            return new SelectList(categoryRepository.FindAll().OrderBy(g => g.Name),
                "CategoryId", "Name", selectedValue);
        }

    }
}
