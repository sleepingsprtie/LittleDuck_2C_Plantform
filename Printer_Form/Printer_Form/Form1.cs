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
                QRC.CreateQRCode(AbsoluteSave, C, temp,8);
                //Console.WriteLine(A);
                Console.WriteLine(C);
                Console.WriteLine(B);
            pictureBox1.ImageLocation = "d:\\img\\1.png";
            printDocument1.Print();


        }

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //e.Graphics.DrawImage(pictureBox1.Image, 20, 20);
            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;
            int width = pictureBox1.Image.Width;
            int height = pictureBox1.Image.Height;
            if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
            {
                width = e.MarginBounds.Width;
                height = pictureBox1.Image.Height * e.MarginBounds.Width / pictureBox1.Image.Width;
            }
            else
            {
                height = e.MarginBounds.Height;
                width = pictureBox1.Image.Width * e.MarginBounds.Height / pictureBox1.Image.Height;
            }
            int big = 300;
            int a = e.PageBounds.Width / 2 - (pictureBox1.Image.Width +big) / 2;
            int b = e.PageBounds.Height / 2 - (pictureBox1.Image.Height + big) / 2;
            e.Graphics.DrawImage(pictureBox1.Image, a, b, pictureBox1.Image.Width + big, pictureBox1.Image.Height + big);
            

            
        }
    }
}
