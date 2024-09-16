using Microsoft.EntityFrameworkCore;

namespace foci.Models
{
    public class FociDBContext : DbContext
    {
        public FociDBContext(DbContextOptions<FociDBContext> options) : base(options)
        {
            
            for (int i = 0; i < Meccsek.Count(); i++)
            {
                Csapat csapat = new Csapat();
                csapat.CsapatNeve = i.ToString();
                csapat.KapottGolok = 1;
                csapat.LottGolok = 1;
                csapat.JatszottMerkozesekSzama = 1;
                csapat.VesztettMeccsek = 1;
                csapat.Helyezes = 1;
                csapat.NyertMeccsek = 1;
            }
            foreach (var item in Meccsek.Select(x=> x.VendegNev).Join(Meccsek))
            {
                
            }
        }
        public DbSet<Meccs> Meccsek { get; set; }
        public DbSet<Csapat> Csapatok { get; set; }
    }
}
