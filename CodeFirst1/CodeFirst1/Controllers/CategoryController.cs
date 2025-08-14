using CodeFirst1.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ProductContext _context;
        public CategoryController(ProductContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> lstcategories = _context.Categories.ToList();  //List of categories that is assigned to lstcategories
            return View(lstcategories);
        }

        public IActionResult Details(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.CategoryId==id); //Take Particular id of category from the database
            return View(category);
        }

        //GET Method to get the view only
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST to get category properties
        [HttpPost]
        public IActionResult Create(Category cat)
        {
            _context.Categories.Add(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");     //Can also redirect to index action method or view
        }

        //GET of edit is similar to details code as used only for displaying
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.CategoryId==id); 
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id,Category newcat) //without update
        {
          Category existingcat = _context.Categories.FirstOrDefault(c => c.CategoryId==id); //Get the existing category
            if (existingcat != null)
            {
                existingcat.CategoryName = newcat.CategoryName; //Update the name of the category
                _context.SaveChanges(); //Save changes to the database
            }
            return RedirectToAction("Index"); //Redirect to index action method or view
        }

        //GET of delete
        [HttpGet]

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.CategoryId==id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int id, Category cat) 
        {
            Category existingcat = _context.Categories.FirstOrDefault(c => c.CategoryId==id); //Get the existing category
            if (existingcat != null)
            {
                _context.Categories.Remove(existingcat); //Remove the category from the database
                _context.SaveChanges(); //Save changes to the database
            }
            return RedirectToAction("Index"); //Redirect to index action method or view
        }









    }
}
