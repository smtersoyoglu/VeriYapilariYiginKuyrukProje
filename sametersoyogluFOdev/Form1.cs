using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sametersoyogluFOdev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class dugum
        {
            public int sayi;
            public int prosesno;
            public dugum baglanti;
        }
        public dugum ilk;
        public dugum son;

        public dugum yigin;
        public dugum islonkuyruk, islarkakuyruk,
            p1onkuyruk, p1arkakuyruk,
            p2onkuyruk, p2arkakuyruk,
            p3onkuyruk, p3arkakuyruk;


        private void silP1()
        {
            //P1 in ilk eleman silme
            if (p1onkuyruk != null)
            {
                p1onkuyruk = p1onkuyruk.baglanti;
            }
        }

        private void silP2()
        {
            if (p2onkuyruk != null)
            {
                p2onkuyruk = p2onkuyruk.baglanti;
            }
        }

        private void silP3()
        {
            if (p3onkuyruk != null)
            {
                p3onkuyruk = p3onkuyruk.baglanti;
            }
        }

        public void islkuyruksiralama(int sayi, int prosesno)
        {
            dugum yeni = new dugum();
            yeni.sayi = sayi;
            yeni.prosesno = prosesno;

            if ((islonkuyruk == null) || (islarkakuyruk == null))
            {
                // Eğer kuyruk boşsa ilk ve son düğümü ekleme
                islonkuyruk = yeni;
                islarkakuyruk = yeni;
            }
            else
            {
                dugum onceki = islonkuyruk;
                dugum sonraki = islarkakuyruk;
                // yeni düğümün eklenip eklenmediğine bakma
                bool eklendiMi = false;
                //sonraki düğüm null olana kadar döngü devam 
                while (sonraki != null)
                {
                    // yeni düğümün öncelik değeri sonraki düğümün öncelik düğümüne bakarak önceki düğümü sonra ki düğüm olarak atadım
                    if (yeni.sayi <= sonraki.sayi)
                    {
                        onceki = sonraki;
                    }
                    else
                    {
                        // sonraki düğüm islemci başındaysa 
                        if (sonraki == islonkuyruk)
                        {
                            yeni.baglanti = sonraki;
                            islonkuyruk = yeni;
                        }
                        else
                        {
                            yeni.baglanti = sonraki;
                            onceki.baglanti = yeni;
                        }
                        //yeni dugum eklendi
                        eklendiMi = true;
                        break;
                    }
                    // sonraki dugumu guncelleme
                    sonraki = sonraki.baglanti;
                }
                // yeni dugum eklenmediyse yapma
                if (eklendiMi == false)
                {
                    islarkakuyruk.baglanti = yeni;
                    islarkakuyruk = yeni;
                }
            }

        }

        private void gosterKuyruk()
        {
            txtBox_islemci.Text = null;
            //boş degilse
            if (islarkakuyruk != null)
            {
                // gecici dugum islemci baslangıcı olarak atama
                dugum gecici = islonkuyruk;
                while (gecici != null)
                {
                    txtBox_islemci.Text += "p" + gecici.prosesno.ToString() + "-" + gecici.sayi + "<--";
                    // gecici dugumu bir sonraki atama yıgındaki tüm elemanalrı tarama yapar bitene kadar
                    gecici = gecici.baglanti;
                }
            }
        }

        public void yiginaEkle()
        {
            dugum yeni = new dugum();

            //islemci on kuyruk boş degilse den yeni dugumun özelliklerini atama yaptım
            if (islonkuyruk != null)
            {
                yeni.prosesno = islonkuyruk.prosesno;
                yeni.sayi = islonkuyruk.sayi;
                yeni.baglanti = yigin;
                yigin = yeni;

                islonkuyruk = islonkuyruk.baglanti;
                gosterKuyruk();
            }
        }

        private void gosterP1()
        {
            dugum gecici = p1onkuyruk;
            listBoxP1.Items.Add("p" + gecici.prosesno.ToString() + "-" + gecici.sayi.ToString());
        }

        private void gosterP2()
        {
            dugum gecici = p2onkuyruk;
            listBoxP2.Items.Add("p" + gecici.prosesno.ToString() + "-" + gecici.sayi.ToString());
        }

        private void gosterP3()
        {
            dugum gecici = p3onkuyruk;
            listBoxP3.Items.Add("p" + gecici.prosesno.ToString() + "-" + gecici.sayi.ToString());
        }


        public void yiginListele(int P1, int P2, int P3)
        {
            listBoxPbiten.Items.Clear();
            dugum gecici = yigin;

            while (gecici != null)
            {
                // gecici dugumun prosesno özelligi p1 p2 p3 degiskenlerine esitse veya ile sagladıktan sonra proses no ya göre yazdırma 
                if ((gecici.prosesno == P1) || (gecici.prosesno == P2) || (gecici.prosesno == P3))
                {
                    listBoxPbiten.Items.Add("p" + gecici.prosesno.ToString() + "-" + gecici.sayi.ToString());
                }
                gecici = gecici.baglanti;
                // gecici dugumu yıgında bir sonraki elemana atama
            }
        }


        public void checkboxKontrolEt()
        {
            // checkboxlara tıklanıp tıklanmadığını kontrol etme işlemi
            if ((!cbP1.Checked) && (!cbP2.Checked) && (!cbP3.Checked))
                MessageBox.Show("Seçim yapmadınız!, Lütfen Seçim yapınız. ");
            else if ((cbP1.Checked) && (!cbP2.Checked) && (!cbP3.Checked))
                yiginListele(1, 0, 0);
            else if ((!cbP1.Checked) && (cbP2.Checked) && (!cbP3.Checked))
                yiginListele(0, 2, 0);
            else if ((!cbP1.Checked) && (!cbP2.Checked) && (cbP3.Checked))
                yiginListele(0, 0, 3);
            else if ((cbP1.Checked) && (cbP2.Checked) && (!cbP3.Checked))
                yiginListele(1, 2, 0);
            else if ((!cbP1.Checked) && (cbP2.Checked) && (cbP3.Checked))
                yiginListele(0, 2, 3);
            else if ((!cbP1.Checked) && (cbP2.Checked) && (!cbP3.Checked))
                yiginListele(1, 0, 3);
            else
                yiginListele(1, 2, 3);

        }

        private void btnProsesGoster_Click(object sender, EventArgs e)
        {
            checkboxKontrolEt();
        }


        private void t_islemci_Tick(object sender, EventArgs e)
        {
            t_islemci.Interval = 1000 / trackBar_islemci.Value;
            yiginaEkle();
        }


        private void tP1_Tick(object sender, EventArgs e)
        {
            Random random1 = new Random();
            tP1.Interval = 1000 / trackBarP1.Value;
            listBoxP1.Text = trackBarP1.Value.ToString();
            dugum yeni = new dugum();
            // yeni dugumde sayi ozelligine 0-5 arasında random sayı atama
            yeni.sayi = random1.Next(0, 6);
            yeni.prosesno = 1;
            //proses 1 on kuyruk boş ise
            if (p1onkuyruk == null)
            {
                //p1 baş ve son atmaa yeni dugume
                p1onkuyruk = yeni;
                p1arkakuyruk = yeni;
            }
            else
            {
                p1arkakuyruk.baglanti = yeni;
                p1arkakuyruk = yeni;
            }

            // proses1
            gosterP1();
            islkuyruksiralama(yeni.sayi, yeni.prosesno);
            gosterKuyruk();
            silP1();
        }

        private void tP2_Tick(object sender, EventArgs e)
        {
            Random random2 = new Random();
            tP2.Interval = 1000 / trackBarP2.Value;
            listBoxP2.Text = trackBarP2.Value.ToString();
            dugum yeni = new dugum();
            yeni.sayi = random2.Next(0, 6);
            yeni.prosesno = 2;

            if (p2onkuyruk == null)
            {
                p2onkuyruk = yeni;
                p2arkakuyruk = yeni;
            }
            else
            {
                p2arkakuyruk.baglanti = yeni;
                p2arkakuyruk = yeni;
            }

            gosterP2();
            islkuyruksiralama(yeni.sayi, yeni.prosesno);
            gosterKuyruk();
            silP2();
        }

        private void tP3_Tick(object sender, EventArgs e)
        {
            Random random3 = new Random();
            tP3.Interval = 1000 / trackBarP3.Value;
            listBoxP3.Text = trackBarP3.Value.ToString();
            dugum yeni = new dugum();
            yeni.sayi = random3.Next(0, 6);
            yeni.prosesno = 3;

            if (p3onkuyruk == null)
            {
                p3onkuyruk = yeni;
                p3arkakuyruk = yeni;
            }
            else
            {
                p3arkakuyruk.baglanti = yeni;
                p3arkakuyruk = yeni;
            }

            gosterP3();
            islkuyruksiralama(yeni.sayi, yeni.prosesno);
            gosterKuyruk();
            silP3();
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            btnBaslat.BackColor = Color.Red;
            btnDurdur.BackColor = Color.MediumBlue;
            t_islemci.Enabled = true;
            tP1.Enabled = true;
            tP2.Enabled = true;
            tP3.Enabled = true;

        }

        private void btnDurdur_Click(object sender, EventArgs e)
        {
            btnBaslat.BackColor = Color.MediumBlue;
            btnDurdur.BackColor = Color.Green;
            t_islemci.Enabled = false;
            tP1.Enabled = false;
            tP2.Enabled = false;
            tP3.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar_islemci.SetRange(1, 7);
            trackBarP1.SetRange(1, 7);
            trackBarP2.SetRange(1, 7);
            trackBarP3.SetRange(1, 7);

        }
    }
}
