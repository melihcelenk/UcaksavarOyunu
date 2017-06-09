
using System.Drawing;

namespace NDPProje
{
    class Mermi : Sekil
    {
        Image resim;
       
        public Mermi(int x, int y, int pencereGenislik, int pencereYukseklik):base(pencereGenislik,pencereYukseklik)
        {
            Genislik = 10;
            Yukseklik = 24;
            X = x;
            Y = y;
            resim = KaynakYoneticisi.ResimAl("mermi.png");
        }
        public override void Ciz(Graphics grafik)
        {
         
                grafik.DrawImage(resim, X, Y, Genislik, Yukseklik);
        }
    }
}
