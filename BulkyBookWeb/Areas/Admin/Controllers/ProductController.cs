using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Collections.Immutable;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BulkyBook.Utility;
using BulkyBook.Models.Models;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {   
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id) 
        {
            /*IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });*/  // EF Projection  select only one coloumn

            // Method 1: -> ViewBag   ->Controller to view not wise versa

            /*ViewBag.CategoryList = CategoryList;*/   // ViewBag.key = value; 

            // Method 2: -> ViewData   ->Controller to view not wise versa

            /*ViewData["CategoryList"] = CategoryList;*/
            ProductVM productVM = new()
            {
                // CategoryList = CategoryList,
                 CategoryList = _unitOfWork.Category.GetAll().ToList().Select(u =>
                new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
                Product = new Product()
            };
            // Method 3: -> TempData
            if (id == null || id == 0)
            {
                // create
                return View(productVM);
            }
            else
            {
                // update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties:"ProductImages" //(name must be same as in data->ApplicationDbContext file)
                    );
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM ProductVM, List<IFormFile>? files)
        {
            if (ModelState.IsValid)
            {
                if (ProductVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(ProductVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(ProductVM.Product);
                }

                _unitOfWork.Save();

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = @"images\product\Product-" + ProductVM.Product.Id;
                        string finalPath = Path.Combine(wwwRootPath, productPath);

                        if (!Directory.Exists(finalPath))
                            Directory.CreateDirectory(finalPath);

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        ProductImage productImage = new()
                        {
                            ImageUrl = @"\" + productPath + @"\" + fileName,
                            ProductId = ProductVM.Product.Id,
                        };

                        if (ProductVM.Product.ProductImages == null)
                            ProductVM.Product.ProductImages = new List<ProductImage>();

                        ProductVM.Product.ProductImages.Add(productImage);
                        // _unitOfWork.ProductImage.Add(productImage);   // one way 

                    }

                    _unitOfWork.Product.Update(ProductVM.Product);
                    _unitOfWork.Save();
                }

                TempData["sucess"] = "Product created/updated successfully";
                return RedirectToAction("Index");
            }

            else
            {
                ProductVM.CategoryList = _unitOfWork.Category.GetAll().ToList().Select(u => new SelectListItem {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                return View(ProductVM);
            }
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u=>u.Id == id);
            if(productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid) {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["sucess"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        /*        public IActionResult Delete(int? id)
                {
                    if(id == null || id == 0)
                    {
                        return NotFound();
                    }
                    Product? productFromDb = _unitOfWork.Product.Get(u=>u.Id==id);
                    if (productFromDb == null)
                    {
                        return NotFound() ;
                    }
                    return View();
                }*/
        /*        [HttpPost, ActionName("Delete")]
                public IActionResult DeletePOST(int? id)
                {
                    Product? obj = _unitOfWork.Product.Get(u=>u.Id==id);

                    if (obj == null) { 
                        return NotFound();
                    }
                    _unitOfWork.Product.Remove(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Product deleted successfully";
                    return RedirectToAction("Index");
                }*/


        public IActionResult DeleteImage(int imageId)
        {
            var ImageToBeDelete = _unitOfWork.ProductImage.Get(u => u.Id == imageId);
            var productId = ImageToBeDelete.ProductId;
            if (ImageToBeDelete != null)
            {
                if (!string.IsNullOrEmpty(ImageToBeDelete.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, ImageToBeDelete.ImageUrl.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.ProductImage.Remove(ImageToBeDelete);
                _unitOfWork.Save();

                TempData["success"] = "Deleted successfuly";
            }
            return RedirectToAction(nameof(Upsert), new {id=productId});

        }

        #region API CALL

        [HttpGet]
        public IActionResult GetAll() {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            /* var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, 
                                productToBeDeleted.ImageUrl.Trim('\\'));
             if(System.IO.File.Exists(oldImagePath))
             {
                 System.IO.File.Delete(oldImagePath);
             }*/

            string productPath = @"images\product\Product-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

                Directory.Delete(finalPath);
            }


            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfully" });
        }

        #endregion

    }
}