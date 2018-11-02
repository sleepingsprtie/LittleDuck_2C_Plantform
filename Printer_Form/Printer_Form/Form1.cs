using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Printer_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        int temp = 1;
        private void button1_Click(object sender, EventArgs e)
        {

            String A, B, C;
            String AbsoluteSave = "d:\\img";
            A = textBox1.Text;
            C = StaticClass.StringEncoder(A);
            B = StaticClass.StringDecder(C);
            QRC.CreateQRCode(AbsoluteSave, C, temp);
            PrintDirectClass p = new PrintDirectClass();
            p.PrintPreview();
            textBox1.Text = "";



        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String A, B, C;
            String AbsoluteSave = "d:\\img";
            A = textBox1.Text;
            C = StaticClass.StringEncoder(A);
            B = StaticClass.StringDecder(C);
            QRC.CreateQRCode(AbsoluteSave, C, temp);
            PrintDirectClass p = new PrintDirectClass();
            p.PrintPreview();
            textBox1.Text = "";
        }
    }
}
