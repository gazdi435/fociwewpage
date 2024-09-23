using foci.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace foci.Pages
{
    public class AdatokFeltoltesModel : PageModel
    {

        private readonly IWebHostEnvironment _env;
        private readonly FociDBContext _context;

        public AdatokFeltoltesModel(IWebHostEnvironment env, FociDBContext context)
        {
            _env= env;
            _context= context;
        }


        [BindProperty]
        public IFormFile Feltoltes {  get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()       
        {
            var UploadDirPath =Path.Combine(_env.ContentRootPath,"Uploads");
            var UploadFilePath = Path.Combine(UploadDirPath, Feltoltes.FileName);

            using (var stream = new FileStream(UploadFilePath, FileMode.Create))
            {
               await Feltoltes.CopyToAsync(stream);
            }

            StreamReader sr = new StreamReader(UploadFilePath);
            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var elemek = line.Split();
                Meccs meccs = new Meccs();
                meccs.Fordulo = int.Parse(elemek[0]);
                meccs.HazaiFelido = int.Parse(elemek[1]);
                meccs.VendegFelido = int.Parse(elemek[2]);
                meccs.HazaiVeg = int.Parse(elemek[3]);
                meccs.VendegVeg = int.Parse(elemek[4]);
                meccs.HazaiNev = elemek[5];
                meccs.VendegNev = elemek[6];

                
                if (!VanEMar(meccs))
                {
                    _context.Meccsek.Add(meccs);
                }
                

            }

            sr.Close();
            _context.SaveChanges();
            System.IO.File.Delete(UploadFilePath);
            return Page();

        }

        public bool VanEMar(Meccs meccs)
        {
            if (_context.Meccsek.Any(x => x.HazaiNev == meccs.HazaiNev && x.VendegNev == meccs.VendegNev))
            {
                return true;
            }

            return false;
        }
    }
}
