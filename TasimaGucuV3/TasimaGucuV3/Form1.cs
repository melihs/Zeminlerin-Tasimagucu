using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace TasimaGucuV3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        hakkimda hk;
        #region hatalar için tanımlanan metot
        private void hata_mesaji(TextBox t)
        {
            if (t.Text == "")
            MessageBox.Show("eksik bilgi girişi var!");
        }
        #endregion
        #region textbox bilgi notları
        private void bilgi_uyari(TextBox tb)
        {
            // textbox üzerine gelindiğinde bilgi notu çıkar
            toolTip1.SetToolTip(tb, "ondalıklı değerleri  yazarken \",\"  kullanın");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbTemelSekli.Items.Add("Şerit Temel");
            cmbTemelSekli.Items.Add("Kare Temel");
            cmbTemelSekli.Items.Add("Daire Temel");
            cmbTemelSekli.Items.Add("Dikdörtgen Temel");

            for (int i = 0; i <= 50; i++)
            {
                cmbFiAcisi.Items.Add(i);
            }

            this.bilgi_uyari(txtKohezyon);
            this.bilgi_uyari(txtDogalBHA);
            this.bilgi_uyari(txtDerinlikTemel);
            this.bilgi_uyari(txtGenislikTemel);
            this.bilgi_uyari(txtUzunlukTemel);
            this.bilgi_uyari(txtGuvenlikKatsayisi);
        }
        #endregion
        #region hesaplama ve değişkenler
        public void btnHesapla_Click(object sender, EventArgs e)
        {
            try
            {
                double nc = double.Parse(txtNc.Text);
                double nq = double.Parse(txtNq.Text);
                double ng = double.Parse(txtNg.Text);
                double c8 = double.Parse(txtKohezyon.Text);
                double c10 = double.Parse(txtDogalBHA.Text);
                double c11 = double.Parse(txtDerinlikTemel.Text);
                double C16 = double.Parse(txtGuvenlikKatsayisi.Text);
                double k1 = double.Parse(txtK1.Text);
                double k2 = double.Parse(txtK2.Text);
                //double uzunlukTemel = double.Parse(txtUzunlukTemel.Text);
                double genislikTemel = double.Parse(txtGenislikTemel.Text);

                double qD = ((k1 * c8 * nc) + (c10 * c11 * nq) + (k2 * ng * genislikTemel * c10));
                double qS = (qD / C16);
                txtTasimaGucu.Text = qD.ToString();
                txtEminTasimaGucu.Text = qS.ToString();


                lbK1.BackColor = Color.Green;
                lbK1.ForeColor = Color.White;
                lbK2.BackColor = Color.Green;
                lbK2.ForeColor = Color.White;

            }
            //düzeltilecek kısa metot halinde yazılmalı..

            catch
            {
                this.hata_mesaji(txtUzunlukTemel);
                this.hata_mesaji(txtGenislikTemel);
                this.hata_mesaji(txtKohezyon);
                this.hata_mesaji(txtDogalBHA);
                this.hata_mesaji(txtDerinlikTemel);
                this.hata_mesaji(txtGuvenlikKatsayisi);
            }
        }
        #endregion
        #region textbox renk ayarları
        //textboxların ve label'ların renk değiştirmesini sağlayan metot yazıldı
        private void tboxRenkleri(TextBox txt, Label lb)
        {
            if (txt.Text != "")
            {
                lb.BackColor = Color.Green;
                lb.ForeColor = Color.White;
            }
            else
            {
                lb.BackColor = Color.LightGray;
                lb.ForeColor = Color.Black;
            }

        }
        private void txtGenislikTemel_TextChanged(object sender, EventArgs e)
        {
            this.tboxRenkleri(txtGenislikTemel, lbGenislik);
        }

        private void txtUzunlukTemel_TextChanged(object sender, EventArgs e)
        {
            this.tboxRenkleri(txtUzunlukTemel, lbTemUz);
        }

        private void txtGuvenlikKatsayisi_TextChanged(object sender, EventArgs e)
        {
            this.tboxRenkleri(txtGuvenlikKatsayisi, lbGuvenlik);
        }
        private void txtKohezyon_TextChanged(object sender, EventArgs e)
        {
            this.tboxRenkleri(txtKohezyon, lbKohezyon);
        }

        private void txtDerinlikTemel_TextChanged(object sender, EventArgs e)
        {
            this.tboxRenkleri(txtDerinlikTemel, lbDerinlik);
        }

        private void txtDogalBHA_TextChanged(object sender, EventArgs e)
        {
            this.tboxRenkleri(txtDogalBHA, lbDBHA);
        }
        #endregion
        #region textbox ve grupbox temizleme
        private void text_Sil()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;
                    tbox.Clear();
                }
            }
        }
        //grupbox ları silmek için metot yazıldı
        private void grupTemizle(GroupBox g)
        {

            foreach (Control item in g.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbox_1 = (TextBox)item;
                    tbox_1.Clear();
                }

            }
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {

            this.grupTemizle(gbSonuc);
            this.grupTemizle(grbKtablosu);
            this.grupTemizle(grbSabitler);
            this.grupTemizle(grbpar);
            cmbFiAcisi.Text = "";
            cmbTemelSekli.Text = "";
            this.text_Sil();
        }
        #endregion
        #region veri girişi denetleme
        //rakam dışında tüm girişleri engellendi(alphanümerikler hariç)
        private void txtKohezyon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar);
        }

        private void txtDogalBHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar);
        }

        private void txtDerinlikTemel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar);
        }

        private void txtGenislikTemel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar);
        }

        private void txtUzunlukTemel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar);
        }

        private void txtGuvenlikKatsayisi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar);

        }
        //elle bütün veri girişini engellendi

        private void txtK1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);
        }

        private void txtK2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);
        }

        private void txtNc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);
        }

        private void txtNq_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);
        }

        private void txtNg_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);
        }
        private void cmbTemelSekli_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);
        }

        private void txtTasimaGucu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);
        }

        private void txtEminTasimaGucu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);
        }

        private void cmbFiAcisi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);
        }
        #endregion
        #region fi açısı verilerinin bulunduğu kısım
        public void cmbFiAcisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbFiAcisi değerini cast yaptım ve fiSec'e atadım
            int fiSec = (int)cmbFiAcisi.SelectedItem;


            double[] Dizi_Nc = new double[]
            {
                 5.7,6,6.3 ,6.62 ,
                6.97,7.34,7.73,8.15,
                8.6,9.09,9.61,10.16,10.76,
                11.41,12.11,12.86,13.68,14.6,
                15.12,16.56,17.69,18.92,20.27,
                21.25,23.36,25.13,27.09,29.24,
                31.61,34.24,37.16,40.41,44.04,
                48.09,52.64,57.75,65.53,70.01,
                77.5,85.97,95.66,106.81,119.67,
                134.58,151.95,172.28,196.22,
                224.55,258.28,298.71,347.5
            };
            double[] Dizi_Nq = new double[]
           {
                1, 1.1,1.22,1.35,1.49,1.64,1.81,
                2,2.21,2.44,2.69,2.98,3.29,3.63,
                4.02,4.45,4.92,5.45,6.04,6.7,7.44,
                8.26,9.19,10.23,11.4,12.72,14.21,
                15.9,17.81,19.98,22.46,25.28,28.52,
               32.33,36.5,41.44,47.16,53.80,61.55,
               70.61,81.27,93.85,108.75,126.5,147.74,
               173.28,204.19,241.8,287.85,344.63,415.14
          };

            double[] Dizi_Ng = new double[]
                {
                    0,0.01,0.04,0.06,0.1,0.14,
                    0.2,0.27,0.35,0.44,0.56,
                    0.69,0.85,1.04,1.26,1.52,
                    1.82,2.18,2.59,3.07,3.64,
                    4.31,5.09,6,7.08,8.34,9.84,
                    11.62,13.7,16.18,19.13,22.65,
                    26.87,31.94,38.04,45.41,54.36,
                    65.27,78.61,95.03,115.31,140.51,
                    171.99,211.56,261.6,325.34,407.11,
                    512.84,650.67,831.99,1072.8
                };
            //Dizi setlerinin indeks numaralarını fiSec değerini vererek switch case ile tek tek indeksleri eşitlemekten kurtuldum:)
            txtNc.Text = Dizi_Nc[fiSec].ToString();
            txtNg.Text = Dizi_Ng[fiSec].ToString();
            txtNq.Text = Dizi_Nq[fiSec].ToString();
        }
        #endregion
        #region temel seçimi
        //temel şekline göre k sabitlerini değiştiren kısım
        private void cmbTemelSekli_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cmbTemelSekli.Text == "Şerit Temel")
                {
                    txtK1.Text = (1.0).ToString();
                    txtK2.Text = (0.5).ToString();
                    picTemelSekli.ImageLocation = "serit.jpg";
                    lbResim.Text = "Şerit Temel";
                }
                else if (cmbTemelSekli.Text == "Kare Temel")
                {
                    txtK1.Text = (1.3).ToString();
                    txtK2.Text = (0.4).ToString();
                    picTemelSekli.ImageLocation = "kare.jpg";
                    lbResim.Text = "Kare Temel";
                }
                else if (cmbTemelSekli.Text == "Daire Temel")
                {
                    txtK1.Text = (1.3).ToString();
                    txtK2.Text = (0.3).ToString();
                    picTemelSekli.ImageLocation = "daire.jpg";
                    lbResim.Text = "Daire Temel";
                }

                else if (cmbTemelSekli.Text == "Dikdörtgen Temel")
                {
                    picTemelSekli.ImageLocation = "dikdörtgen.jpg";
                    lbResim.Text = "Dikdörtgen Temel";
                    double uzunlukTemel = double.Parse(txtUzunlukTemel.Text);
                    double genislikTemel = double.Parse(txtGenislikTemel.Text);

                    txtK1.Text = (((0.2) * genislikTemel / uzunlukTemel) + 1).ToString();
                    txtK2.Text = ((0.5) - ((0.1) * genislikTemel / uzunlukTemel)).ToString();
                }

            }

            catch
            {
                MessageBox.Show(" Lütfen Temel uzunluğunu ve Temel genişliğini giriniz!");
                txtK1.Clear();
                txtK2.Clear();
                MessageBox.Show("Sonra tekrar temel şeklini seçiniz");
            }
        }
        #endregion
        #region çıkış işlemi
        // programdan çıkış işlemi
        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region txt dosyasına kaydetme
        public void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //kaydetmek isteğimiz yeri belirtmeye yarar
            SaveFileDialog kaydet = new SaveFileDialog();
            kaydet.Filter = "Metin dosyası |*.txt";
            //Bir dizine dosya kaydederken kontrol varsayılan olarak dizinde aynı dosya isminden başka bir tane dosyanın olup olmadığını kontrol eder.Eğer varsa bir mesaj ile üzerine yazmak isteyip istemediğinizi sorar. Eğer mesaj çıkarmadan aynı isimde yer alan dosyanın üzerine kayıt etmek isterseniz OverwritePrompt özelliğini false olarak setlemek gerekecektir.
            kaydet.OverwritePrompt = true;
            //Dosya adına dizinde var olmayan bir dosyanın adı yazıldığında kontrol dosyayı otomatik oluşturur.Eğer bunu kullanıcıdan gelecek yanıta göre yapmak istersek CreatePrompt özelliğini true olarak setlemek gerekecektir.Bu şekilde kullanıcıya dosyayı oluşturmak isteyip istemediğini soracaktır
            kaydet.CreatePrompt = true;
            if (kaydet.ShowDialog() == DialogResult.OK)
            {
                StreamWriter kayit = new StreamWriter(kaydet.FileName);
                kayit.WriteLine("--- " + grbpar.Text + " ---");
                kayit.Write(lbGenislik.Text + " ");
                kayit.Write(txtGenislikTemel.Text + " ");
                kayit.Write(lm.Text);
                kayit.WriteLine();
                kayit.Write(lbTemUz.Text + " ");
                kayit.Write(txtUzunlukTemel.Text + " ");
                kayit.Write(lmu2.Text);
                kayit.WriteLine();
                kayit.Write(lbKohezyon.Text + " ");
                kayit.Write(txtKohezyon.Text + " ");
                kayit.Write(lko.Text);
                kayit.WriteLine();
                kayit.Write(lbTemelSekli.Text + " ");
                kayit.Write(cmbTemelSekli.Text + " ");
                kayit.WriteLine();
                kayit.Write(lbFi.Text + " ");
                kayit.Write(cmbFiAcisi.Text + " ");
                kayit.WriteLine();
                kayit.Write(lbDBHA.Text + " ");
                kayit.Write(txtDogalBHA.Text + " ");
                kayit.Write(ldbha.Text);
                kayit.WriteLine();
                kayit.Write(lbDerinlik.Text + " ");
                kayit.Write(txtDerinlikTemel.Text + " ");
                kayit.Write(ltd.Text);
                kayit.WriteLine();
                kayit.Write(lbGuvenlik.Text + " ");
                kayit.Write(txtGuvenlikKatsayisi.Text + " ");
                kayit.WriteLine();
                kayit.WriteLine("--- " + gbSonuc.Text + " ---");
                kayit.Write(lbQd.Text + " ");
                kayit.Write(txtTasimaGucu.Text + " ");
                kayit.Write(birim.Text);
                kayit.WriteLine();
                kayit.Write(lbQs.Text + " ");
                kayit.Write(txtEminTasimaGucu.Text + " ");
                kayit.Write(label2.Text);
                kayit.WriteLine();
                kayit.WriteLine("--- " + grbSabitler.Text + " ---");
                kayit.Write(lbNc.Text + " ");
                kayit.Write(txtNc.Text + " ");
                kayit.WriteLine();
                kayit.Write(lbNq.Text + " ");
                kayit.Write(txtNq.Text + " ");
                kayit.WriteLine();
                kayit.Write(lbNg.Text + " ");
                kayit.Write(txtNg.Text + " ");
                kayit.WriteLine(); ;

                kayit.Close();

            }

        }


        #endregion

        private void hakkındaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            if (hk == null || hk.IsDisposed)
            {
                hk= new hakkimda();
                hk.Show();
                
            }
        }
    }
  
}
