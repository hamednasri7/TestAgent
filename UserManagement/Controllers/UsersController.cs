using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users
            .OrderByDescending(u => u.Id)
            .ToListAsync();
        return View(users);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "کاربر با موفقیت افزوده شد.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(user);
        }

        var existing = await _context.Users.FindAsync(id);
        if (existing is null)
        {
            return RedirectToAction(nameof(Index));
        }

        existing.FirstName = user.FirstName;
        existing.LastName = user.LastName;
        existing.Age = user.Age;
        existing.Mobile = user.Mobile;

        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "کاربر با موفقیت ویرایش شد.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(user);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is not null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            TempData["StatusMessage"] = "کاربر با موفقیت حذف شد.";
        }

        return RedirectToAction(nameof(Index));
    }
}
