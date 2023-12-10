using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSTK
{
    public partial class Form2 : Form
    {
        public Form2(string author, string pack, string desc)
        {
            InitializeComponent();
            textBox1.Text = pack;
            textBox2.Text = desc;
            textBox3.Text = author;
        }

        public string Author => textBox3.Text;
        public string Pack => textBox1.Text;
        public string Desc => textBox2.Text;

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
