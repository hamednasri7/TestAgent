using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Pages.Users;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public User Input { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Users.Add(Input);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "کاربر با موفقیت افزوده شد.";
        return RedirectToPage("Index");
    }
}
