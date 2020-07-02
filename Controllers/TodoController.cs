using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoListMvc.Models;
using TodoListMvc.Data;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TodoListMvc.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoListMvcContext _context;

        public TodoController(TodoListMvcContext context)
        {
            _context = context;
        }

        // GET: Todo/Show/:id
        // public IActionResult Show(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var todoItem = _context.TodoItem.FirstOrDefault(item => item.Id == id);

        //     if (todoItem == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(todoItem);
        // }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title")] TodoItem todoItem)
        {
            _context.TodoItem.Add(new TodoItem { IsDone = false, Title = todoItem.Title });

            var saveResult = await _context.SaveChangesAsync();

            if (saveResult != 1)
            {
                return NotFound();
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == null) return NotFound();

            var todoItem = _context.TodoItem.Where(_todo => _todo.Id == Id).SingleOrDefault();

            if (todoItem == null) return NotFound();

            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Done(int Id)
        {
             if (Id == null) return NotFound();

            var todoItem = _context.TodoItem.Where(_todo => _todo.Id == Id).SingleOrDefault();

            if (todoItem == null) return NotFound();

            todoItem.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();

            if (saveResult == 1)
                return RedirectToAction(nameof(Index));
            else
                return NotFound();
        }
        public IActionResult Index()
        {
            var ItemList = _context.TodoItem.ToList();
            return View(ItemList);
        }
    }
}
