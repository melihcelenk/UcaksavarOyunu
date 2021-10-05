
using System.Drawing;

namespace NDPProje
{
    abstract class Sekil
    {
        private int x;
        private int y;
        private int genislik;
        private int yukseklik;
        private int pGenislik;
        private int pYukseklik;
        

        public abstract void Ciz(Graphics grafik);

        public Sekil(int pencereGenislik, int pencereYukseklik)
        {
            pGenislik = pencereGenislik;
            pYukseklik = pencereYukseklik;
        }

        public int X
        {
            get { return x; }
            set { if (value >= 0 && value < pGenislik-genislik-10) x = value; }
        }
        public int Y
        {
            get { return y; }
            set {
                if (value >= 0 && value < pYukseklik-yukseklik)
                    y = value;
            }
        }
        public int Genislik
        {
            get { return genislik; }
            set { genislik = value; }
        }
        public int Yukseklik
        {
            get { return yukseklik; }
            set { yukseklik = value; }
        }
    }
}
