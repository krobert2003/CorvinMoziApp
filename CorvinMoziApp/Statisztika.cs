using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class Statisztika : Form
    {
        string stat = null;
        public Statisztika(string fajlnev)
        {
            InitializeComponent();
            stat = fajlnev;
        }

        private void Statisztika_Load(object sender, EventArgs e)
        {
            textBox1.Font = new Font(FontFamily.GenericMonospace, textBox1.Font.Size);
            if (File.Exists(stat))
            {
                textBox1.Text = string.Join("\r\n", File.ReadAllLines(stat));
            }
            else
            {
                MessageBox.Show(stat + " fájl nem található!");
                return;
            }
            textBox1.Select(0, 0);
        }


    }
}