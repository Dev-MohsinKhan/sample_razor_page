using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SampleRazorPage.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public User User { get; set; }

        public CreateUserModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // This handler can be used to load any data when the form is first displayed.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Return the same page with validation errors displayed
                return Page();
            }

            // Save the valid user data to the database
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            // Redirect to another page or display success message
            return RedirectToPage("Index");
        }
    }

    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
    }
}
