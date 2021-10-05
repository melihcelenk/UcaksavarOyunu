using System;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;

namespace NDPProje
{
    class AnaPencere : Form
    {
        Label bilgilendirmeYazisi = new Label();
        Ucaksavar ucksvr;
        
        List<Mermi> mermiler = new List<Mermi>();
        List<Ucak> ucaklar = new List<Ucak>();
        Timer zaman = new Timer();
        int skor = 0;
        int altin = 0;
        int ucakDogmaSuresi = 140;
        int zamanSayac = 0;

        SoundPlayer muzik = new SoundPlayer();
        SoundPlayer bitisMuzigi = new SoundPlayer();


        public AnaPencere(int genislik, int yukseklik)
        {
            //PENCERE İLE İLGİLİ İŞLEMLER
            Width = genislik;
            Height = yukseklik;
            BackColor = System.Drawing.Color.Black;
            //MÜZİK İLE İLGİLİ İŞLEMLER
            muzik.SoundLocation = Application.StartupPath + "\\muzik.wav"; ;
            bitisMuzigi.SoundLocation = Application.StartupPath + "\\bitis.wav";
            muzik.Play();

            ucksvr = new Ucaksavar(Width, Height);

            //this.ClientSize.Height;
            Paint += AnaPencere_Paint;
            KeyDown += AnaPencere_KeyDown;
            YaziEkle();

            zaman.Interval = 5;
            zaman.Tick += Zaman_Tick;
            zaman.Start();
       
            DoubleBuffered = true;
        }

        ~AnaPencere()
        {
            zaman.Stop();
        }

        

        private void AnaPencere_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    if (zaman.Enabled)
                    {
                        ucksvr.X -= 15;
                    }
                  
                    break;
                case Keys.Right:
                case Keys.D:
                    if (zaman.Enabled)
                    {
                        ucksvr.X += 15;
                    }
                    break;
                case Keys.Space:
                    if (zaman.Enabled)
                    {
                        Mermi mrm = new Mermi(ucksvr.X + 35, ucksvr.Y - 24, Width, Height);
                        mermiler.Add(mrm);
                    }
                    break;
                case Keys.Enter:
                    BackColor = System.Drawing.Color.Black;
                    bitisMuzigi.Stop();
                    muzik.Play();
                    zamanSayac = 0;
                    ucakDogmaSuresi = 140;
                    zaman.Start();
                    bilgilendirmeYazisi.Visible = false;
                    
                    ucaklar = new List<Ucak>();
                    mermiler = new List<Mermi>();
                    ucksvr = new Ucaksavar(Width, Height);
                    break;
                case Keys.Escape:
                    MessageBox.Show("Çıkış yaptınız.", "Çıkış");
                    Close();
                    break;
            }
            Invalidate();
            
            
        }//Key_Down sonu
        
        private void Zaman_Tick(object sender, EventArgs e)
        {
            zamanSayac++;
            // YENİ UÇAK NESNESİNİN DOĞDUĞU YER
            if(zamanSayac%ucakDogmaSuresi==0) ucaklar.Add(new Ucak(Width,Height));
            
            if (zamanSayac > 599) ucakDogmaSuresi = 60;
            else if (zamanSayac > 299) ucakDogmaSuresi = 100;

            List<int> silinecekUcaklar = new List<int>();
            List<int> silinecekMermiler = new List<int>();

            for (int i=0; i<ucaklar.Count; i++)
            {
                ucaklar[i].Y += 2;
                
                // OYUNUN BİTTİĞİ YER
                if (ucaklar[i].Y + ucaklar[i].Yukseklik > Width  - 220) 
                {
                    zaman.Stop();
                    muzik.Stop();
                    bitisMuzigi.Play();
                    BackColor = System.Drawing.Color.DarkRed;
                    altin += skor * 10;
                    bilgilendirmeYazisi.Text = "OYUN BİTTİ\n" + "Skorunuz: " + skor + "\nToplam Kazandığınız Altın: " + altin + "\nDevam etmek için Enter tuşuna basınız.";
                    bilgilendirmeYazisi.Visible = true;
                    skor = 0;
                }
                for (int k = 0; k < mermiler.Count; k++)
                {
                    //MERMİ İLE UÇAĞIN ÇARPIŞMA ANI
                    //if (mermiler[k].Y - ucaklar[i].Y < 3 && Math.Abs(mermiler[k].X - (ucaklar[i].X + ucaklar[i].Genislik / 2)) < 25)
                    //{
                    //    skor++;
                    //    silinecekMermiler.Add(k);
                    //    silinecekUcaklar.Add(i);
                    //}

                    if (mermiler[k].Y - (ucaklar[i].Y + ucaklar[i].Yukseklik) < -47)
                    {
                        if ((mermiler[k].X - (ucaklar[i].X + ucaklar[i].Genislik) < 0) && (ucaklar[i].X - (mermiler[k].X + mermiler[k].Genislik) < 0))
                        {

                            if (!silinecekUcaklar.Contains(i) && !silinecekMermiler.Contains(k))
                            {
                                skor++;
                                silinecekMermiler.Add(k);
                                silinecekUcaklar.Add(i);
                            }
                        }
                    }

                }

            }
            for (int i = 0; i < mermiler.Count; i++)
            {
             
                mermiler[i].Y -= 3;

                if (mermiler[i].Y < 3) mermiler.RemoveAt(i);

            }
            // ÇARPIŞMADAN SONRA SİLİNECEK ELEMANLAR BURADA SİLİNİR
            // BU ALANI DEĞİŞTİRMEYİNİZ. EĞER ELEMANLARI İÇERDE SİLERSENİZ COUNT DEĞERİ DEĞİŞECEĞİNDEN HATA VERECEKTİR.
            foreach (var i in silinecekMermiler)
            {
                mermiler.RemoveAt(i);
            }
            foreach(var i in silinecekUcaklar)
            {
               // if(ucaklar.Count>i)
                ucaklar.RemoveAt(i);
            }
            Invalidate();
        }//Zaman_Tick sonu
        
        
        private void AnaPencere_Paint(object sender, PaintEventArgs e)
        {
            ucksvr.Ciz(e.Graphics);


            for (int i = 0; i < mermiler.Count; i++)
            {
                    mermiler[i].Ciz(e.Graphics);
            }
            for (int i = 0; i < ucaklar.Count; i++)
            {
                    ucaklar[i].Ciz(e.Graphics);
            }

        }

        public void YaziEkle()
        {
            bilgilendirmeYazisi = new System.Windows.Forms.Label();
            SuspendLayout();

            bilgilendirmeYazisi.AutoSize = true;
            bilgilendirmeYazisi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            bilgilendirmeYazisi.ForeColor = System.Drawing.Color.Yellow;
            bilgilendirmeYazisi.BackColor = System.Drawing.Color.Black;
            bilgilendirmeYazisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            bilgilendirmeYazisi.Location = new System.Drawing.Point(200, 200);
            bilgilendirmeYazisi.Name = "bilgilendirmeYazisi";
            bilgilendirmeYazisi.Size = new System.Drawing.Size(95, 36);
            bilgilendirmeYazisi.TabIndex = 0;
            bilgilendirmeYazisi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            bilgilendirmeYazisi.Text = "";
            bilgilendirmeYazisi.Visible = false;
            Controls.Add(bilgilendirmeYazisi);
        }
    }
}
