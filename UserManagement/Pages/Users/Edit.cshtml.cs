using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Pages.Users;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public User Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return RedirectToPage("Index");
        }

        Input = user;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _context.Users.FindAsync(Input.Id);
        if (user is null)
        {
            return RedirectToPage("Index");
        }

        user.FirstName = Input.FirstName;
        user.LastName = Input.LastName;
        user.Age = Input.Age;
        user.Mobile = Input.Mobile;

        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "کاربر با موفقیت ویرایش شد.";
        return RedirectToPage("Index");
    }
}
