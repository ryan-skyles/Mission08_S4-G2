using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission8_S4_G2.Models;
using System.Diagnostics;

namespace Mission8_S4_G2.Controllers
{
    public class HomeController : Controller
    {
        private ITasksRepository _repository;

        public HomeController(ITasksRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var tasks = _repository.GetAllTasks();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(
                _repository.GetCategories(),
                "CategoryId",
                "CategoryName");

            return View(new Models.Task());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _repository.AddTask(task);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(
                _repository.GetCategories(),
                "CategoryId",
                "CategoryName");

            return View(task);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _repository.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(
                _repository.GetCategories(),
                "CategoryId",
                "CategoryName");

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateTask(task);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(
                _repository.GetCategories(),
                "CategoryId",
                "CategoryName");

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var task = _repository.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            _repository.DeleteTask(task);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkCompleted(int id)
        {
            var task = _repository.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            task.Completed = true;
            _repository.UpdateTask(task);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
