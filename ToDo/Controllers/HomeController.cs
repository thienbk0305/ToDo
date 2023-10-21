using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataAccess.Entities;
using DataAccess.DBContext;
using DataAccess.UnitOfWork;
using DataAccess.Repositories;
using DataAccess.IRepositories;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        private ToDoDBContext _dbContext;
        private readonly IToDoRepositoty _toDoRepo;
        private readonly IUserRepository _userRepo;

        public HomeController(ToDoDBContext dbContext, IToDoRepositoty toDoRepositoty, IUserRepository  userRepository)
        {
            _dbContext = dbContext;
            _toDoRepo  = toDoRepositoty; 
            _userRepo = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRepo.GetAllUser();
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
                var todos = await _toDoRepo.GetAllTodo();
              
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
                    var newToDoId = await _toDoRepo.AddTodo(model);
                    var todos = await _toDoRepo.GetTodo(newToDoId);
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
            var todo = await _toDoRepo.GetTodoById(id);

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
                await _toDoRepo.DeleteTodo(id);
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
                await _toDoRepo.UpdateTodo(id, model);
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
                    var newUserId = await _userRepo.AddUser(model);
                    var users = await _userRepo.GetUser(newUserId);
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
                var users = await _userRepo.GetAllUser();

                return Json(users);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}