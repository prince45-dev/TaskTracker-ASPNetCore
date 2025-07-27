public class TasksController : Controller
{
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Account");

        var tasks = _context.Tasks.Where(t => t.UserId == userId).ToList();
        return View(tasks);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(TaskItem task)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Account");

        if (ModelState.IsValid)
        {
            task.UserId = userId.Value;
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Edit(int id)
    {
        var task = _context.Tasks.Find(id);
        return View(task);
    }

    [HttpPost]
    public IActionResult Edit(TaskItem updated)
    {
        _context.Tasks.Update(updated);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var task = _context.Tasks.Find(id);
        _context.Tasks.Remove(task);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
