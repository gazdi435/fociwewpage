using Microsoft.EntityFrameworkCore;

namespace foci.Models
{
    [PrimaryKey("CsapatNeve")]
    public class Csapat
    {
        public string CsapatNeve { get; set; }
        public int Helyezes { get; set; }
        public int JatszottMerkozesekSzama {get; set;}
        public int LottGolok { get; set; }
        public int KapottGolok { get; set; }
        public int NyertMeccsek { get; set; }
        public int VesztettMeccsek { get; set; }
        public int Dontetlen { get; set; }
        public int Pontszam { get; set; }

    }
}
