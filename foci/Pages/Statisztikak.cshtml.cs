using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using foci.Models;

namespace foci.Pages
{
    public class StatisztikakModel : PageModel
    {
        private readonly foci.Models.FociDBContext _context;

        public StatisztikakModel(foci.Models.FociDBContext context)
        {
            _context = context;
        }

        public Csapat Csapat { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var csapat = await _context.Csapatok.FirstOrDefaultAsync(m => m.CsapatNeve == id);
            if (csapat == null)
            {
                return NotFound();
            }
            else
            {
                Csapat = csapat;
            }
            return Page();
        }
    }
}
