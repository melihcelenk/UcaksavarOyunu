using System.Collections.Generic;
using System.Drawing;

namespace NDPProje
{
    class KaynakYoneticisi
    {
        private static Dictionary<string,Image> resimler = new Dictionary<string,Image>();

        public static Image ResimAl(string resimAdi)
        {
            if (!resimler.ContainsKey(resimAdi))
            {
                resimler[resimAdi] = Image.FromFile(resimAdi);
            }
            return resimler[resimAdi];
        }
    }
}
