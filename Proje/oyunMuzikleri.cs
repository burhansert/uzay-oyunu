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
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Proje
{
    class oyunMuzikleri
    {
        public oyunMuzikleri()
        {
            oyunMuzigi = new SoundPlayer();
            oyunMuzigi.SoundLocation = "oyunMuzigi.wav";
            oyunMuzigi.PlayLooping();

            ucakVuruldu = new SoundPlayer();
            ucakVuruldu.SoundLocation = "ucakVuruldu.wav";
        }
        public SoundPlayer oyunMuzigi;
        public SoundPlayer ucakVuruldu;
    }
}
