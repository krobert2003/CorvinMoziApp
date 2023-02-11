using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorvinMoziApp
{
    class Terem
    {
        string nev;
        int sorok;
        int szekekSzama;
        char[,] ulesek;
        Image nevadokep = null;

        public string Nev { get => nev; set => nev = value; }
        public int Sorok { get => sorok; set => sorok = value; }
        public int Szekek { get => szekekSzama; set => szekekSzama = value; }
        public char[,] Ulesek { get => ulesek; set => ulesek = value; }
        public Image Nevadokep { get => nevadokep; set => nevadokep = value; }
        /// <summary>
        /// Az osztály konstruktora
        /// </summary>
        /// <param name="nev">A vetítőterem neve</param>
        /// <param name="sorok">A teremben lévő széksorok száma</param>
        /// <param name="szekekSzama">Az egy sorban lévő székek száma</param>
        /// <param name="ulesek">Az egyes székek adatait tartalmazó tömb</param>
        /// <param name="nevadokep">A terem névadójának a bitképe</param>
        public Terem(string nev, int sorok, int szekekSzama, char[,] ulesek, Image nevadokep)
        {
            this.nev = nev;
            this.sorok = sorok;
            this.szekekSzama = szekekSzama;
            this.ulesek = ulesek;
            this.nevadokep = nevadokep;
        }
        /// <summary>
        /// A terembe eladott jegyek utáni bevétel Ft-ban
        /// </summary>
        public int Bevetel()
        {
            int sum = 0;
            for (int i = 0; i < ulesek.GetLength(0); i++)
            {
                for (int j = 0; j < ulesek.GetLength(1); j++)
                {
                    switch (ulesek[i, j])
                    {
                        case 'F':
                            sum += 1700;
                            break;
                        case 'D':
                            sum += 1200;
                            break;
                        default:
                            break;
                    }
                }
            }
            return sum;
        }
        public double Kihasznaltsag()
        {
            double ures = 0;
            for (int i = 0; i < ulesek.GetLength(0); i++)
            {
                for (int j = 0; j < ulesek.GetLength(1); j++)
                {
                    if (ulesek[i, j] == '\0')
                    {
                        ures++;
                    }
                }
            }
            return ures / (ulesek.GetLength(0) * ulesek.GetLength(1));
        }

        public bool KetUres(out string teremNev, out int sor, out int szek1, out int szek2)
        {
            bool van = false;
            teremNev = null;
            sor = 0;
            szek1 = 0;
            szek2 = 0;
            for (int i = 0; i < ulesek.GetLength(0); i++)
            {
                for (int j = 0; j < ulesek.GetLength(1) - 1; j++)
                {
                    if (ulesek[i, j] == '\0' && ulesek[i, j + 1] == '\0')
                    {
                        van = true;
                        teremNev = nev;
                        sor = i;
                        szek1 = j;
                        szek2 = j + 1;
                    }
                }
            }
            return van;
        }
    }
}
