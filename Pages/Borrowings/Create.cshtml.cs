using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sagau_Rares_Lab2.Data;
using Sagau_Rares_Lab2.Models;

namespace Sagau_Rares_Lab2.Pages.Borrowings
{
    public class CreateModel : PageModel
    {
        private readonly Sagau_Rares_Lab2.Data.Sagau_Rares_Lab2Context _context;

        public CreateModel(Sagau_Rares_Lab2.Data.Sagau_Rares_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            var bookList = _context.Book
            .Include(b => b.Author)
            .Select(x => new
            {
                x.ID,
                BookFullName = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
            });

            ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName");

            // Filter members to include only those with both a first name and a last name
            var memberList = _context.Member
                //.Where(m => !string.IsNullOrEmpty(m.FirstName) && !string.IsNullOrEmpty(m.LastName))
                .Select(m => new
                {
                    m.ID,
                    FullName = !string.IsNullOrEmpty(m.FirstName) && !string.IsNullOrEmpty(m.LastName) ? m.FirstName + " " + m.LastName : m.Email
                });

            ViewData["MemberID"] = new SelectList(memberList, "ID", "FullName");

            return Page();
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Borrowing == null || Borrowing == null)
            {
                return Page();
            }

            _context.Borrowing.Add(Borrowing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
