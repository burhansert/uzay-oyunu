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
    class carpismalar
    {
        public int vurulanUcakSayisi = 0;

        public void yenile()
        {
            vurulanUcakSayisi = 0;
        }
        public Boolean carpmaDenetimi(int ucakSayisi, int atesSayisi, ucaklarim ucaklarim1, ateslerim ateslerim1)
        {
            Boolean carpisma = false;

            for (int i = 0; i < ucaklarim1.ucakSayisi; i++)
            {
                for (int j = 0; j < ateslerim1.atesSayisi; j++)
                {
                    Rectangle ucak = (Rectangle)ucaklarim1.ucakDler[i];
                    Rectangle ates = (Rectangle)ateslerim1.atesDler[j];

                    //ates ve ucak kesiyorsa
                    if (ucak.IntersectsWith(ates))
                    {
                        //vurulan ucak listeden ve ekrandan siliniyor
                        PictureBox gecici1 = (PictureBox)ucaklarim1.ucaklar[i];
                        gecici1.Dispose();
                        ucaklarim1.ucaklar.RemoveAt(i);
                        ucaklarim1.ucakDler.RemoveAt(i); //ucagin dikdortgeni listeden siliniyor
                        ucaklarim1.ucakSayisi--;

                        //vurulan ates listeden ve ekrandan siliniyor
                        PictureBox gecici2 = (PictureBox)ateslerim1.atesler[j];
                        gecici2.Dispose(); //ekrandan siliniyor
                        ateslerim1.atesler.RemoveAt(j); //listeden siliniyor
                        ateslerim1.atesDler.RemoveAt(j);//atesin dikdortgeni listeden siliniyor
                        ateslerim1.atesSayisi--;

                        vurulanUcakSayisi++;
                        carpisma = true;

                        break;
                    }
                }
            }
            return carpisma;
        }
    }
}
