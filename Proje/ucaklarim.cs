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
    class ucaklarim
    {
        public ucaklarim(Form Form1)
        {
            formGenislik = Form1.ClientSize.Width;
            formYukseklik = Form1.ClientSize.Height;
        }

        int formGenislik;
        int formYukseklik;

        Random rnd = new Random();

        public int oyunDuraklat = 0;//başka bir program acikken oyun duraklatilacak
        //oyun duraklatilmazsa program hata veriyor

        public int ucakSayisi = 0;

        public int ucakHiz = 1; //her ucak asagi oteleme timer'i calistiginda, ucaklarin kac birim asagi otelenecegi

        public int ucakGenislik = 64;
        public int ucakYukseklik = 64;

        public ArrayList ucakDler = new ArrayList(); //ucaklarin uzerindeki dikdortgenler
        public ArrayList ucaklar = new ArrayList();


        public void ucakEkle(ucaklarim ucaklarim1, Form Form1, Label label4, Label label5, Timer ucakEkle, Timer timerUcakAsagi)  //dusman ucaklar rondom koordinatlarda ekleniyor
        {
            PictureBox ucak = new PictureBox();
            ucak.ImageLocation = "ucak.png";
            ucak.Name = ucaklarim1.ucakSayisi.ToString();
            ucak.SizeMode = PictureBoxSizeMode.StretchImage;
            ucak.Size = new Size(ucaklarim1.ucakGenislik, ucaklarim1.ucakYukseklik);
            try
            {
                ucak.Location = new Point(rnd.Next(1, Form1.ClientSize.Width - ucaklarim1.ucakGenislik), label4.Bottom);
                ucak.BackColor = Color.Transparent;
                Form1.Controls.Add(ucak);
                ucaklarim1.ucaklar.Add(ucak);

                ucaklarim1.ucakSayisi++;
                ucaklarim1.ucakDikdortgenEkle(ucak);
            }
            catch //(Exception hataTuru)
            {
                //MessageBox.Show("Hata meydana geldi." + hataTuru);
                ucakEkle.Stop();
                timerUcakAsagi.Stop();

                label5.Visible = true;
                label5.Text = "Oyundan çıktığınız için\n";
                label5.Text += "Oyun duraklatıldı.\n";
                label5.Text += "Oyuna devam etmek için\n";
                label5.Text += "Tab tuşuna basınız.";
                label5.Location = new Point(formGenislik / 2 - label5.Width / 2, formYukseklik / 2 - label5.Height / 2);
            }

        }
        public void ucakDikdortgenEkle(PictureBox ucak)
        {
            Rectangle ucakD = new Rectangle(); //ucak savarin etrafina dikdortgen ciziliyor
            ucakD.X = ucak.Left;
            ucakD.Y = ucak.Top;
            ucakD.Height = ucak.Height;
            ucakD.Width = ucak.Width;
            ucakDler.Add(ucakD);
        }

        //ucak asagi gittikce cevresindeki dikdortgeninde koordinati guncellenecek
        public void ucakDikdortgenKoordinatGuncelle(PictureBox ucak, int indis)
        {
            Rectangle ucakD = new Rectangle(); //ucak savarin etrafina dikdortgen ciziliyor
            ucakD.X = ucak.Left;
            ucakD.Y = ucak.Top;
            ucakD.Height = ucak.Height;
            ucakD.Width = ucak.Width;
            ucakDler[indis] = ucakD;
        }
        public void yenile()
        {
            ucakSayisi = 0;
            ucaklar.Clear();
            ucakDler.Clear();
        }
    }
}
