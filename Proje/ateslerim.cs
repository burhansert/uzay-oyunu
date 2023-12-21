/*********************************************************************************************
**							     SAKARYA ÜNİVERSİTESİ
**                     BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**                          BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
**                         NESNEYE DAYALI PROGRAMLAMA DERSİ
**                              2016-2017 BAHAR DÖNEMİ
**
**                          PROJE NUMARASI.........:1
**                          ÖĞRENCİ ADI............:BURHAN SERT
**                          ÖĞRENCİ NUMARASI.......:U1312.10020
**                          DERSİN ALINDIĞI GRUP...:2/A
*********************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    class ateslerim
    {
        public int atesSayisi = 0;
        public int atesHiz = 10; //her ates yukari oteleme timer'i calistiginda, ateslerin kac birim yukari otelenecegi
        public int atesGenislik = 20;
        public int atesYukseklik = 60;

        public ArrayList atesler = new ArrayList();   //atesler bir listede saklaniyor
        public ArrayList atesDler = new ArrayList(); //ateslerin uzerindeki dikdortgenler

        public void yenile()
        {
            atesSayisi = 0;
            atesler.Clear();
            atesDler.Clear();
        }

        Boolean guncelle = false;
        public Boolean atesEkle(ateslerim ateslerim1, carpismalar carpismalar1, ucaklarim ucaklarim1, PictureBox ucakSavar, Timer timerOyunMuzigiBasla, Form Form1, Boolean sol, Boolean sag)     //ates  ucak savarin konumunda ekleniyor
        {
            timerOyunMuzigiBasla.Start();

            PictureBox son = new PictureBox();

            //ilk ates haric, son ateslerin referan degerleri aliniyor
            if (ateslerim1.atesSayisi != 0)
            { son = (PictureBox)ateslerim1.atesler[ateslerim1.atesSayisi - 1]; }

            //onceki ates yukselmeden yenisini uretmiyor
            if (son.Bottom < ucakSavar.Top || ateslerim1.atesSayisi == 0) //|| sol == true || sag == true
            {
                PictureBox ates = new PictureBox();
                ates.ImageLocation = "ates.png";
                ates.Name = ateslerim1.atesSayisi.ToString();
                ates.SizeMode = PictureBoxSizeMode.StretchImage;
                ates.Size = new Size(ateslerim1.atesGenislik, ateslerim1.atesYukseklik);
                ates.Location = new Point((ucakSavar.Left + ucakSavar.Right) / 2 - ateslerim1.atesGenislik / 2, ucakSavar.Top);
                ates.BackColor = Color.Transparent;
                Form1.Controls.Add(ates);
                ateslerim1.atesler.Add(ates);
                //  atesAdiSayac++;
                ateslerim1.atesSayisi++;
                ateslerim1.atesDikdortgenEkle(ates);
                if (carpismalar1.carpmaDenetimi(ucaklarim1.ucakSayisi, ateslerim1.atesSayisi, ucaklarim1, ateslerim1) == true)
                    guncelle = true;
            }
            return guncelle;
        }

        public void atesDikdortgenEkle(PictureBox ates)
        {
            Rectangle atesD = new Rectangle(); //ucak savarin etrafina dikdortgen ciziliyor
            //atesD..Name = atesAdiSayac.ToString();
            atesD.X = ates.Left;
            atesD.Y = ates.Top;
            atesD.Height = ates.Height;
            atesD.Width = ates.Width;
            atesDler.Add(atesD);
        }

        public void atesDikdortgenGuncelle(PictureBox ates, int indis)
        {
            Rectangle atesD = new Rectangle(); //ucak savarin etrafina dikdortgen ciziliyor
            //atesD..Name = atesAdiSayac.ToString();
            atesD.X = ates.Left;
            atesD.Y = ates.Top;
            atesD.Height = ates.Height;
            atesD.Width = ates.Width;
            atesDler[indis] = atesD;
        }
    }
}
