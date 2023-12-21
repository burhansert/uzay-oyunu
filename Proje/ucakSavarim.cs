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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    class ucakSavarim
    {
        public int ucakSavarHiz = 40; //her yon tusuna basildiginda kac bir oteleme alacagi
        public int ucakSavarGenislik = 64;
        public int ucakSavarYukseklik = 64;

        public PictureBox ucakSavar;
        public PictureBox ucakSavarEkle(Form Form1)  //formdaki ucakSavar in adi ucakSavar
        {
            ucakSavar = new PictureBox();
            ucakSavar.ImageLocation = "ucakSavar.png";
            ucakSavar.Name = "ucakSavar";
            ucakSavar.SizeMode = PictureBoxSizeMode.StretchImage;
            ucakSavar.Size = new Size(ucakSavarGenislik, ucakSavarYukseklik);
            //ucakSavar yerle bitisik konuma getirildi
            ucakSavar.Location = new Point(Form1.ClientSize.Width / 2, Form1.ClientSize.Height - ucakSavar.Height);
            ucakSavar.BackColor = Color.Transparent;
            Form1.Controls.Add(ucakSavar);

            Form1.Invalidate();
            return ucakSavar;
        }
    }
}
