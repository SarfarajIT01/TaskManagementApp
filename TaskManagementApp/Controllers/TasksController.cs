using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;

        // Simulated logged-in user (for assignment purpose)
        private const string CurrentUserId = "U001";
        private const string CurrentUserName = "Sarfaraj";

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

        // GET: /Tasks/Details/5
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

        // POST: /Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedOn = DateTime.Now;
                //task.UpdatedOn = DateTime.Now;
                task.CreatedById = CurrentUserId;
                //task.CreatedByName = CurrentUserName;

                _context.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }


        // GET: /Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            return View(task);
        }

        // POST: /Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskItem task)
        {
            if (id != task.Id) return NotFound();

            var createdInfo = await _context.Tasks
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.CreatedByName,
                    c.CreatedOn
                })
                .FirstOrDefaultAsync();


            task.CreatedByName = createdInfo?.CreatedByName;
            task.CreatedOn = createdInfo!.CreatedOn;

            if (ModelState.IsValid)
            {
                try
                {
                    task.UpdatedOn = DateTime.Now;
                    task.UpdatedById = CurrentUserId;
                    //task.UpdatedByName = CurrentUserName;

                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tasks.Any(e => e.Id == task.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: /Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            return View(task);
        }

        // POST: /Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task!);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
