using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using DataAccess.Entities;
using DataAccess.IRepositories;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoRepositoty _toDoRepo;
        private readonly IUserRepository _userRepo;

        public HomeController(IToDoRepositoty toDoRepositoty, IUserRepository  userRepository)
        {
            _toDoRepo  = toDoRepositoty; 
            _userRepo = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRepo.GetAllUserAsync(HttpContext.RequestAborted);
            var selectListItems = new SelectList(users, "ID","FullName");
            ViewData["SearchByUser"] = selectListItems;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // GET ALLTODO
        [HttpGet]
        public async Task<IActionResult> GetAllToDo()
        {
            try
            {
                var todos = await _toDoRepo.GetAllTodoAsync(HttpContext.RequestAborted);
              
                return Json(todos);
            }
            catch
            {
                return BadRequest();
            }
        }
        // INSERT TODO
        [HttpPost]
        public async Task<IActionResult> InsertToDo([FromBody] Todo model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newToDoId = await _toDoRepo.AddTodoAsync(model, HttpContext.RequestAborted);
                    var todos = await _toDoRepo.GetTodoAsync(newToDoId, HttpContext.RequestAborted);
                    return todos == null ? NotFound() : RedirectToAction("index");
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
        // GET TODO
        [HttpGet]
        public async Task<ActionResult<Todo>> GetToDoById(int id)
        {
            var todo = await _toDoRepo.GetTodoByIdAsync(id, HttpContext.RequestAborted);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }
        // DELETE TODO
        [HttpDelete]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            try
            {
                await _toDoRepo.DeleteTodoAsync(id, HttpContext.RequestAborted);
                return Json(new { code = 1, msg = "Huỷ thành công" });
            }
            catch (Exception ex)
            {

                return Json(new { code = -99, msg = ex.Message });
            }

        }
        // UPDATE TODO
        [HttpPost]
        public async Task<IActionResult> UpdateTodo( int id,[FromBody] Todo model)
        {
            try
            {
                await _toDoRepo.UpdateTodoAsync(id, model, HttpContext.RequestAborted);
                return Json(new { code = 1, msg = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {

                return Json(new { code = -99, msg = ex.Message });
            }

        }
        // INSERT USER
        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUserId = await _userRepo.AddUserAsync(model, HttpContext.RequestAborted);
                    var users = await _userRepo.GetUserAsync(newUserId, HttpContext.RequestAborted);
                    return users == null ? NotFound() : RedirectToAction("index");
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
        // GET ALLUSER
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _userRepo.GetAllUserAsync(HttpContext.RequestAborted);

                return Json(users);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
