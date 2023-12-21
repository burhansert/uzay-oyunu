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
using System.Drawing;
using System.Windows.Forms;

namespace Proje
{
    public partial class AnaForm : Form
    {
        #region baslangic degiskenleri
        Boolean sag = false, sol = false;
        bool spaceTus = false; //ates ediliyor mu, baslangicta hayir edilmiyor
        public PictureBox ucakSavar;
        Boolean oyunBasladi = false;
        #endregion

        #region uretilen nesneler
        ucaklarim ucaklarim1;
        ateslerim ateslerim1;
        ucakSavarim ucakSavarim1;
        oyunMuzikleri oyunMuziklerim1;
        carpismalar carpismalar1;
        #endregion

        #region Form Load
        private void Form1_Load(object sender, EventArgs e)
        {
            ucaklarim1 = new ucaklarim(AnaForm.ActiveForm);
            ateslerim1 = new ateslerim();
            ucakSavarim1 = new ucakSavarim();
            oyunMuziklerim1 = new oyunMuzikleri();
            carpismalar1 = new carpismalar();

            bilgiPaneliGuncelle();

            ucakSavar = ucakSavarim1.ucakSavarEkle(Form.ActiveForm);

            DoubleBuffered = true; //Goruntulerin daha kaliteli olmasi icin DoubleBuffered kullanildi

            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;

            timerUcakEkle = new Timer();
            timerUcakEkle.Interval = 1000; //yeni ucak eklenme hizi,tavsiye 1000
            timerUcakEkle.Tick += TimerUcakEkle_Tick;

            timerUcakAsagi = new Timer();
            timerUcakAsagi.Interval = 10; //ucak hareket hizi, tavsiye 10n
            timerUcakAsagi.Tick += TimerUcakAsagi_Tick;

            timerAtesYukari = new Timer();
            timerAtesYukari.Interval = 1; //merminin yukselme hizi
            timerAtesYukari.Tick += timerAtesYukari_Tick;

            timerSolTus = new Timer();
            timerSolTus.Interval = 100; //sola gitme hizi
            timerSolTus.Tick += TimerSolTus_Tick;

            timerSagTus = new Timer();
            timerSagTus.Interval = 100; //saga gitme hizi
            timerSagTus.Tick += TimerSagTus_Tick;

            timerSpaceTus = new Timer();
            timerSpaceTus.Interval = 250; //ucak savar hareketliyken, yeni ates uretme hizi
            timerSpaceTus.Tick += TimerSpaceTus_Tick;

            timerOyunMuzigiBasla = new Timer();
            timerOyunMuzigiBasla.Interval = 1000; //ucak vurulduktan ne kadar süre sonra oyun muzigi baslasin
            timerOyunMuzigiBasla.Tick += TimerOyunMuzigiBasla_Tick;

            label1.Location = new Point(this.Width - 275, 10);
            label2.Location = new Point(this.Width - 275, 35);
            label3.Location = new Point(this.Width - 275, 60);
            label4.Location = new Point(0, 0);
            label4.Size = new Size(this.Width, 95);
            label4.Text = "Oyunu başlatmak için ENTER tuşuna basın.\n";
            label4.Text += "Uçaksavarı hareket ettirmek için SAĞ/SOL YÖN TUŞLARINI kullanın.\n";
            label4.Text += "Ateş etmek için BOŞLUK tuşuna basın.";
            label4.Font = new Font(label4.Font.FontFamily, 14);

            label5.Font = new Font(label5.Font.FontFamily, 20);
           
        }
        public void oyunuKaybettinizMesaji()
        {
            label5.Text = "<--------Oyunu Kaybettiniz-------->";
            label5.Text += "\n   <--------Yeni Oyun İçin-------->";
            label5.Text += "\n     <------Enter'a basınız------>";
            label5.Location = new Point(this.ClientSize.Width / 2 - label5.Width / 2, this.ClientSize.Height / 2 - label5.Height / 2);
        }
        public AnaForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Timer'lar

        Timer timerAtesYukari;
        Timer timerOyunMuzigiBasla; //ucaklar vurulduktan belirli bir sure sonra oyun muzigi tekrar basliyor
        Timer timerSolTus; //tus sola basiliysa ne yapilacak
        Timer timerSagTus; //tus saga basiliysa ne yapilacak
        Timer timerSpaceTus; //tus space'a basiliysa ne yapilacak
        Timer timerUcakEkle;
        Timer timerUcakAsagi;
        private void TimerOyunMuzigiBasla_Tick(object sender, EventArgs e)
        {
            oyunMuziklerim1.oyunMuzigi.PlayLooping();
            timerOyunMuzigiBasla.Stop();  //muzigin tekrar tekrar bastan baslamasi engellendi
        }

        private void TimerSpaceTus_Tick(object sender, EventArgs e)
        {
            if (oyunBasladi)
                if (ateslerim1.atesEkle(ateslerim1, carpismalar1, ucaklarim1, ucakSavar, timerOyunMuzigiBasla, AnaForm.ActiveForm, sol, sag) == true)
                    carpismaSonrasiIslemler();
        }

        private void TimerSagTus_Tick(object sender, EventArgs e)
        {
            if (this.ucakSavar.Right + ucakSavarim1.ucakSavarHiz <= this.ClientSize.Width)
                this.ucakSavar.Location = new Point(this.ucakSavar.Location.X + ucakSavarim1.ucakSavarHiz, this.ucakSavar.Location.Y);
            if (spaceTus == true)
            {
                timerSpaceTus.Start();
            }
            else
            {
                timerSpaceTus.Stop();
            }
        }

        private void TimerSolTus_Tick(object sender, EventArgs e)
        {
            if (this.ucakSavar.Location.X - ucakSavarim1.ucakSavarHiz >= 0)
                this.ucakSavar.Location = new Point(this.ucakSavar.Location.X - ucakSavarim1.ucakSavarHiz, this.ucakSavar.Location.Y);
            if (spaceTus == true)
            {
                timerSpaceTus.Start();
            }
            else
            {
                timerSpaceTus.Stop();
            }
        }
        private void timerAtesYukari_Tick(object sender, EventArgs e) //atesleri yukari cikariyor
        {
            //atesler ve dikdortgenler yukari tasiniyor
            for (int i = 0; i < ateslerim1.atesSayisi; i++)
            {
                PictureBox gecici = (PictureBox)(ateslerim1.atesler[i]);
                gecici.Location = new Point(gecici.Location.X, gecici.Location.Y - ateslerim1.atesHiz);

                ateslerim1.atesDikdortgenGuncelle(gecici, i);
            }
            if (carpismalar1.carpmaDenetimi(ucaklarim1.ucakSayisi, ateslerim1.atesSayisi, ucaklarim1, ateslerim1) == true)
            {
                carpismaSonrasiIslemler();
            }

            PictureBox EnUsttekiAtes; //en ustteki ates

            //eger ates varsa
            if (ateslerim1.atesler.Count > 0)
            {
                EnUsttekiAtes = (PictureBox)(ateslerim1.atesler[0]);
                if (EnUsttekiAtes.Top <= label4.Bottom)
                {   //yukariya carpan mermi listeden ve ekrandan siliniyor
                    EnUsttekiAtes.Dispose(); //ekrandan siliniyor
                    ateslerim1.atesler.RemoveAt(0); //listeden siliniyor
                    ateslerim1.atesDler.RemoveAt(0);//atesin dikdortgeni listeden siliniyor
                    ateslerim1.atesSayisi--;
                    bilgiPaneliGuncelle();
                }
            }
        }

        private void TimerUcakAsagi_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ucaklarim1.ucakSayisi; i++)
            {
                PictureBox gecici;
                gecici = (PictureBox)ucaklarim1.ucaklar[i];
                gecici.Location = new Point(gecici.Location.X, gecici.Location.Y + ucaklarim1.ucakHiz);

                ucaklarim1.ucakDikdortgenKoordinatGuncelle(gecici, i);
            }

            //ekranda ucak varsa
            if (ucaklarim1.ucaklar.Count > 0)
            {
                PictureBox gecici2 = (PictureBox)ucaklarim1.ucaklar[0];

                //ucaklar yere carpinca oyun bitiyor
                if (gecici2.Bottom > this.ClientSize.Height)
                {
                    oyunBasladi = false;
                    timerUcakAsagi.Stop();
                    timerUcakEkle.Stop();
                    oyunuKaybettinizMesaji();
                    label5.Visible = true;
                    
                }
            }
        }
        private void TimerUcakEkle_Tick(object sender, EventArgs e)
        {
            ucaklarim1.ucakEkle(ucaklarim1, AnaForm.ActiveForm, label4,label5, timerUcakEkle,timerUcakAsagi);
            bilgiPaneliGuncelle();
            Invalidate(); //goruntuyu yenile
        }
        #endregion

        #region Bazi fonksiyonlar

        private void carpismaSonrasiIslemler()
        {
            bilgiPaneliGuncelle();
            timerOyunMuzigiBasla.Stop();
            oyunMuziklerim1.ucakVuruldu.Play();
            timerOyunMuzigiBasla.Start();
        }

        public void bilgiPaneliGuncelle()
        {
            label1.Text = "Vurulan Düşman Sayısı: " + carpismalar1.vurulanUcakSayisi;
            label2.Text = "Mevcut  Düşman Sayısı: " + ucaklarim1.ucakSayisi;
            label3.Text = "Ekrandaki Mermi Sayısı: " + ateslerim1.atesSayisi;
        }

        void oyunYenile()
        {
            PictureBox geciciUcak;
            for (int i = 0; i < ucaklarim1.ucakSayisi; i++)
            {
                geciciUcak = (PictureBox)(ucaklarim1.ucaklar[i]);
                geciciUcak.Dispose();
            }
            ucaklarim1.yenile();

            PictureBox geciciAtes;
            for (int i = 0; i < ateslerim1.atesSayisi; i++)
            {
                geciciAtes = (PictureBox)(ateslerim1.atesler[i]);
                geciciAtes.Dispose();
            }
            ateslerim1.yenile();
            carpismalar1.yenile();

            bilgiPaneliGuncelle();

            timerUcakAsagi.Start();
            timerUcakEkle.Start();
            timerAtesYukari.Start();

            label5.Visible = false;
        }

        #endregion

        #region Tus Yakalama
        private void Form1_KeyUp(object sender, KeyEventArgs e)  //tustan elimizi cekince
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    timerUcakEkle.Start();
                    timerUcakAsagi.Start();
                    label5.Visible = false;
                    break;
                case Keys.Space:
                    spaceTus = false;
                    timerSpaceTus.Stop();
                    break;
                case Keys.Left:
                    timerSolTus.Stop();
                    sol = false;
                    break;
                case Keys.Right:
                    timerSagTus.Stop();
                    sag = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)  //tuslara basiliyken
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    timerSolTus.Start();

                    sol = true;
                    break;
                case Keys.Right:
                    timerSagTus.Start();
                    //   if (ates == true) atesEkle();
                    sag = true;
                    break;
                case Keys.Space:
                    if (oyunBasladi)
                        if (ateslerim1.atesEkle(ateslerim1, carpismalar1, ucaklarim1, ucakSavar, timerOyunMuzigiBasla, AnaForm.ActiveForm, sol, sag) == true)
                            carpismaSonrasiIslemler();
                    spaceTus = true;
                    break;
                case Keys.Return:
                    oyunBasladi = true;
                    oyunYenile();
                    break;
            }

            Invalidate(); //goruntuyu yenile
        }

        #endregion
    }
}
