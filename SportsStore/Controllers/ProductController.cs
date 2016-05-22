using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Models.Domain;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {

        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public ActionResult Index(int categoryId = 0)
        {
            IEnumerable<Product> products;
            Category category;
            if (categoryId == 0)
                //products = productRepository.FindAll().OrderBy(b => b.Name).ToList();
            products = productRepository.FindAll().OrderBy(b => b.Category.Name).ThenBy(b => b.Name).ToList();
            else
            {
                category = categoryRepository.FindById(categoryId);
                products = category.Products.OrderBy(b => b.Name);
            }
            ViewBag.Categories = GetCategoriesSelectList(categoryId);
            ViewBag.CategoryId = categoryId;
            return View(products);
        }

        public ActionResult Edit(int id)
        {
            Product product = productRepository.FindById(id);
            if (product == null)
                return HttpNotFound();
            ViewBag.Categories = GetCategoriesSelectList(product.Category.CategoryId);
            return View(new ProductViewModel(product));
        }

        [HttpPost]
        public ActionResult Edit(int id, ProductViewModel productViewModel, HttpPostedFileBase image)
        {
            Product product = productRepository.FindById(id);
            if (product == null)
                return HttpNotFound();
            if (ModelState.IsValid)
            {
                
         

                    if (image != null)
                    {
                        product.ImageMimeType = image.ContentType;
                        product.ImageData = new byte[image.ContentLength];
                        image.InputStream.Read(product.ImageData, 0,
                            image.ContentLength);
                    }
                    //product.ImageData = productViewModel.ImageData;
                    // image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                    MapToProduct(productViewModel, product);
                    productRepository.SaveChanges();
                    TempData["info"] = $"Product {product.Name} werd aangepast";
                    return RedirectToAction("Index");
               
            }
            ViewBag.Categories = GetCategoriesSelectList(productViewModel.CategoryId);
            return View(productViewModel);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            ViewBag.Categories = GetCategoriesSelectList();
            return View("Edit", new ProductViewModel(product));
        }

        
        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel,HttpPostedFileBase image)
        {
            try
            {
                Product product = new Product();

                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0,
                        image.ContentLength);
                }
                productRepository.Add(product);
                MapToProduct(productViewModel, product);
                productRepository.SaveChanges();
                TempData["info"] = $"Product {product.Name} werd gecreëerd";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("",ex.Message);
            }
            
  ViewBag.Categories = GetCategoriesSelectList(productViewModel.CategoryId);
            return View("Edit",productViewModel);
        }
        public ActionResult Category()
        {
            return View(new CategoryViewModel());
        }
        [HttpPost]
        public ActionResult Category(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.Name = model.Name;
                if (categoryRepository.FindByName(category.Name) == null)
                {
                    categoryRepository.Add(category);
                    categoryRepository.SaveChanges();
                    
                }
                else
                {
                    TempData["Error"] = "Deze categorie bestaat al.";
                    return View(model);
                }
                TempData["info"] = $"Categorie {category.Name} werd gecreëerd";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Er is iets misgegaan. Probeer opnieuw";
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            Product product = productRepository.FindById(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Product product = productRepository.FindById(id);
                if (product == null)
                    return HttpNotFound();
                productRepository.Delete(product);
                productRepository.SaveChanges();
                TempData["info"] = $"Product { product.Name} werd verwijderd";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Verwijderen product mislukt. Probeer opnieuw. " +
                           "Als de problemen zich blijven voordoen, contacteer de  administrator." + ex.Message;
            }
            return RedirectToAction("Index");
        }

        private SelectList GetCategoriesSelectList(int selectedValue = 0)
        {
            return new SelectList(categoryRepository.FindAll().OrderBy(g => g.Name),
                "CategoryId", "Name", selectedValue);
        }

        private void MapToProduct(ProductViewModel productViewModel, Product product)
        {
            //if (image != null)
            //{
            //    product.ImageMimeType = image.ContentType;
            //    product.ImageData = new byte[image.ContentLength];
            //    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
            //}
            product.ArtikelCode = productViewModel.ArtikelCode;
            product.ProductId = productViewModel.ProductId;
            product.Name = productViewModel.Name;
            product.Description = productViewModel.Description;
            product.Price = productViewModel.Price;
            product.InStock = productViewModel.InStock;
            product.MaxAantal = productViewModel.MaxAantal;
           //product.ImageData = productViewModel.ImageData;
           //product.ImageMimeType = productViewModel.ImageMimeType;

            // product.Availability = productViewModel.Availability;
            product.Category = categoryRepository.FindById(productViewModel.CategoryId);
        }

        [AllowAnonymous]
        public FileContentResult GetImage(int productId)
        {
            Product p = productRepository.FindById(productId);

            if (p != null)
            {
                return File(p.ImageData, p.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}