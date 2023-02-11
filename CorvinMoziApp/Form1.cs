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

namespace CorvinMoziApp
{

    public partial class Form1 : Form
    {
        Mozi Corvin = new Mozi("CorvinMozi.csv");
        int akt = 0; 
        Image[] kepek = new Image[] { Image.FromFile("ures.png"), Image.FromFile("gyerek.png"), Image.FromFile("felnott.png") };
        readonly int meret = 25;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Termet_Betolt();
        }
        void Termet_Betolt()
        {
            this.Text = Corvin.termek[akt].Nev + " terem";
            pictureBox_Nevado.Image = Corvin.termek[akt].Nevadokep;
            panel1.Controls.Clear();
            for (int i = 0; i < Corvin.termek[akt].Sorok; i++)
            {
                for (int j = 0; j < Corvin.termek[akt].Szekek; j++)
                {
                    PictureBox gomb = new PictureBox();
                    gomb.SetBounds(j * meret, i * meret, meret, meret);
                    gomb.Padding = new Padding(3);
                    switch (Corvin.termek[akt].Ulesek[i, j])
                    {
                        case 'F':
                            gomb.Image = kepek[2];
                            break;
                        case 'D':
                            gomb.Image = kepek[1];
                            break;
                        default:
                            gomb.Image = kepek[0];
                            break;
                    }
                    gomb.SizeMode = PictureBoxSizeMode.StretchImage;
                  
                    int i2 = i;
                    int j2 = j;
                    gomb.Click += (e, o) =>
                    {
                        switch (Corvin.termek[akt].Ulesek[i2, j2])
                        {
                            case 'F':
                                Corvin.termek[akt].Ulesek[i2, j2] = '0';
                                break;
                            case 'D':
                                Corvin.termek[akt].Ulesek[i2, j2] = 'F';
                                break;
                            default:
                                Corvin.termek[akt].Ulesek[i2, j2] = 'D';
                                break;
                        }
                        Termet_Betolt();
                    };
                    panel1.Controls.Add(gomb);
                }
            }
            if (akt == 0)
            {
                button1.Visible = false;
                button2.Visible = true;
            }
            else if (akt == Corvin.termek.Count - 1)
            {
                button1.Visible = true;
                button2.Visible = false;
            }
            else
            {
                button1.Visible = true;
                button2.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            akt--;
            if (akt <= 0)
            {
                akt = Corvin.termek.Count - 1;
            }
            Termet_Betolt();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            akt++;
            if (akt >= Corvin.termek.Count)
            {
                akt = 0;
            }
            Termet_Betolt();
        }




        private void button_Mentes_Click(object sender, EventArgs e)
        {
            Corvin.Mentes();
        }

        private void button_Statisztika_Click(object sender, EventArgs e)
        {
            string fajlnev = "statisztika_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            double sum = 0;
            string sor;
            using (StreamWriter sw = new StreamWriter(fajlnev))
            {

                sw.WriteLine(vonal());
                sw.WriteLine("Az egyes termek bevétele a jegyárusításból:\n");
                sw.WriteLine(vonal());
                foreach (Terem item in Corvin.termek)
                {
                    double bevetel = item.Bevetel();
                    sum += bevetel;
                    sor = $"{item.Nev,25} terem: {bevetel.ToString("#,##0"),8} Ft";
                    sw.WriteLine(sor);
                }
                sw.WriteLine(vonal());
                sw.WriteLine($"{"összesen".PadLeft(31)}: {sum.ToString("#,##0"),8} Ft");

              
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine(vonal());
                sw.WriteLine("Az üres helyek és az összes helyek aránya:");
                sw.WriteLine(vonal());
                foreach (Terem item in Corvin.termek)
                {
                    sw.WriteLine($"{item.Nev,25} = {(item.Kihasznaltsag() * 100).ToString("0.00"),6}%");
                }

                sw.WriteLine("\n\n" + vonal());
                bool van = false;
                string teremNev = null;
                int szekSor = 0;
                int szek1 = 0;
                int szek2 = 0;
                foreach (Terem item in Corvin.termek)
                {
                    if (item.KetUres(out teremNev, out szekSor, out szek1, out szek2))
                    {
                        van = true;
                        break;
                    }
                }
                if (van)
                {
                    sw.WriteLine($"A {teremNev} terem {szekSor + 1}. széksorában szabad egymás mellett a {szek1 + 1}. és {szek2 + 1}. szék!");
                }
                else
                {
                    sw.WriteLine("Egyik teremben sem található egymás melletti két üres szék!");
                }
                sw.WriteLine(vonal());
            }
            Statisztika eredmeny = new Statisztika(fajlnev);
            eredmeny.ShowDialog();
        }

        string vonal()
        {
            return new string('-', 45);
        }

       
    }
}