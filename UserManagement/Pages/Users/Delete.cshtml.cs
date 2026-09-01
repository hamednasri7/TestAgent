using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Pages.Users;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context)
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
        var user = await _context.Users.FindAsync(Input.Id);
        if (user is not null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            TempData["StatusMessage"] = "کاربر با موفقیت حذف شد.";
        }

        return RedirectToPage("Index");
    }
}
