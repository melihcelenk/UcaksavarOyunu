
using System.Drawing;

namespace NDPProje
{
    class Ucak:Sekil
    {
        Image resim;
        public Ucak(int pencereGenislik, int pencereYukseklik) : base(pencereGenislik, pencereYukseklik)
        {
            Genislik = 50;
            Yukseklik = 50;
            
            X = 15 * RastgeleSayi.SayiUret(3,(pencereGenislik-50)/15);
            Y = 0;
            resim = KaynakYoneticisi.ResimAl("ucak.png");
        }
        public override void Ciz(Graphics grafik)
        {
          
                grafik.DrawImage(resim, X, Y, Genislik, Yukseklik);
        }
    }
}
