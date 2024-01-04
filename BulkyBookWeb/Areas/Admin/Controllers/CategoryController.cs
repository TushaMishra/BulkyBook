using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BulkyBook.Utility;
using BulkyBook.Models.Models;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        /*        private readonly ApplicationDbContext _db;
                public CategoryController(ApplicationDbContext db)
                {
                    _db = db;  
                }*/
        // private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork; // because it contain CategoryRepository inside it
                                                  //public CategoryController(ICategoryRepository db)
        /*        {
                    _categoryRepo = db;
                }*/
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /*        public IActionResult Index()
                {
                    List<Category> objCategoryList = _db.Categories.ToList();
                    return View(objCategoryList);
                }*/
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList(); //_categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The displayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                /* _db.Categories.Add(obj);
                _db.SaveChanges();*/
                /*_categoryRepo.Add(obj);
                _categoryRepo.Save();*/
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }   
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            /*Category? categoryFromDb = _db.Categories.Get(id);*/
            /*Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==id);*/
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            /*Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Name.Contains);*/
            /*            Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
                        Category? categoryFromDb2= _db.Categories.Where(u=>u.Id == id).FirstOrDefault();*/
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                /*               _db.Categories.Update(obj);
                               _db.Categories.SaveChange();
                */
                /*  
                 *  _categoryRepo.Update(obj);
                    _categoryRepo.Save();
                */
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            /*            Category? categoryFromDb = _db.Categories.Find(id);*/
            /*Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);*/
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            /*Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Name.Contains);*/
            /*            Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
                        Category? categoryFromDb2= _db.Categories.Where(u=>u.Id == id).FirstOrDefault();*/
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            /*            Category obj = _db.Categories.Find(id);*/
            /*Category obj = _categoryRepo.Get(u => u.Id == id);*/
            Category obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            /*            _db.Categories.Remove(obj);
                        _db.SaveChanges();*/
            /*_categoryRepo.Remove(obj);
            _categoryRepo.Save();*/
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}