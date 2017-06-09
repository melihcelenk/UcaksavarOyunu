
using System.Drawing;

namespace NDPProje
{
    class Ucaksavar:Sekil
    {
        Image resim;
       
        public Ucaksavar(int pencereGenislik, int pencereYukseklik):base(pencereGenislik,pencereYukseklik)
        {
            Genislik = 80;
            Yukseklik = 80;
            
            X = 345;
            Y = 481;


            resim = KaynakYoneticisi.ResimAl("ucaksavar.png");

        }

        public override void Ciz(Graphics grafik)
        {
            grafik.DrawImage(resim, X, Y, Genislik, Yukseklik);
        }

    }
}
