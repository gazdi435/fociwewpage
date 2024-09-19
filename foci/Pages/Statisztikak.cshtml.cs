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

        public IList<Meccs> Meccs { get; set; } = default!;
        public IList<Csapat> Csapatok { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Meccs = await _context.Meccsek.ToListAsync();

            List<Csapat> csapatokList = new List<Csapat>();

            foreach (string csapatNev in Meccs.Select(x => x.HazaiNev).ToList().Concat(Meccs.Select(x=>x.VendegNev)).Distinct().ToList())
            {
                List<Meccs> hazaiCsapat = Meccs.Where(x => x.HazaiNev == csapatNev).ToList();
                List<Meccs> vendegCsapat = Meccs.Where(x => x.VendegNev == csapatNev).ToList();


                Csapat csapat = new Csapat();

                csapat.CsapatNeve = csapatNev;
                csapat.JatszottMerkozesekSzama = hazaiCsapat.Count() + vendegCsapat.Count();
                csapat.KapottGolok = hazaiCsapat.Sum(x => x.VendegVeg) + vendegCsapat.Sum(x => x.HazaiVeg);
                csapat.LottGolok = hazaiCsapat.Sum(x => x.HazaiVeg) + vendegCsapat.Sum(x => x.VendegVeg);
                csapat.NyertMeccsek = hazaiCsapat.Count(x => x.HazaiVeg > x.VendegVeg) + vendegCsapat.Count(x => x.VendegVeg > x.HazaiVeg);
                csapat.VesztettMeccsek = vendegCsapat.Count(x => x.HazaiVeg > x.VendegVeg) + hazaiCsapat.Count(x => x.VendegVeg > x.HazaiVeg);
                csapat.Dontetlen = vendegCsapat.Count(x => x.HazaiVeg == x.VendegVeg) + hazaiCsapat.Count(x => x.VendegVeg == x.HazaiVeg);
                csapat.Pontszam = csapat.NyertMeccsek * 3 + csapat.Dontetlen;
                csapat.Helyezes = 0;

                csapatokList.Add(csapat);

            }

            Csapatok = csapatokList.OrderByDescending(x=>x.Pontszam).ToList();

            //for (int i = 0; i < Csapatok.Count(); i++)
            //{
            //    Csapatok.OrderByDescending(x => x.Pontszam).ToList()[i].Helyezes = i + 1;
            //}

            int helyezes = 1;
            for (int i = 0; i < Csapatok.Count(); i++)
            {
                if (i != 0)
                {
                    if (Csapatok[i-1].Pontszam != Csapatok[i].Pontszam)
                    {
                        helyezes++;
                    }
                }

                Csapatok[i].Helyezes = helyezes;
            }


        }
    }
}
