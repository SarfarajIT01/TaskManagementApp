using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;

        //private const string CurrentUserId = "U001";
        //private const string CurrentUserName = "Sarfaraj";

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Tasks
        public async Task<IActionResult> Index(string searchString, string status)
        {
            var tasks = _context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(status))
            {
                tasks = tasks.Where(t => t.Status == status);
            }

            return View(await tasks.OrderByDescending(t => t.CreatedOn).ToListAsync());
        }

        // GET: /Tasks/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            return View(task);
        }

        // GET: /Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedOn = DateTime.Now;
                task.CreatedById = await GenerateUserIdAsync("C");
                //task.CreatedByName = CurrentUserName;

                _context.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: /Tasks/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskItem task)
        {
            if (id != task.Id) return NotFound();

            // Get existing created info
            var createdInfo = await _context.Tasks
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.CreatedById,
                    c.CreatedByName,
                    c.CreatedOn
                })
                .FirstOrDefaultAsync();

            if (createdInfo == null)
                return NotFound();

            task.CreatedById = createdInfo.CreatedById;
            task.CreatedByName = createdInfo.CreatedByName;
            task.CreatedOn = createdInfo.CreatedOn;

            if (ModelState.IsValid)
            {
                task.UpdatedOn = DateTime.Now;

                if (task.UpdatedByName == createdInfo.CreatedByName)
                {
                    task.UpdatedById = createdInfo.CreatedById;
                }
                else
                {
                    task.UpdatedById = await GenerateUserIdAsync("U");
                }

                _context.Update(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }



        // GET: /Tasks/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            return View(task);
        }

        // POST: /Tasks/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task!);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> GenerateUserIdAsync(string prefix)
        {
            string? lastId = await _context.Tasks
                .Select(t => prefix == "C" ? t.CreatedById : t.UpdatedById)
                .Where(id => id != null && id.StartsWith(prefix))
                .OrderByDescending(id => id)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastId))
            {
                nextNumber = int.Parse(lastId.Substring(1)) + 1;
            }

            return $"{prefix}{nextNumber:D4}";
        }


    }
}
