using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Collections;



namespace Printer_Form

{
    static class QRC

    {
        public static void CreateQRCode(string absoluteSave, string qrdata, int temp, int size)
        {
            try
            {
                QRCodeEncoder encoder = new QRCodeEncoder();
                encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
                encoder.QRCodeScale = size;//大小(值越大生成的二维码图片像素越高)
                encoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;//错误效验、错误更正(有4个等级)
                encoder.QRCodeBackgroundColor = Color.White;
                encoder.QRCodeForegroundColor = Color.Black;
                System.Drawing.Image image = encoder.Encode(qrdata, Encoding.UTF8);
                System.IO.Directory.CreateDirectory(absoluteSave);
                image.Save("d:\\img\\" + temp + ".png", ImageFormat.Png);

                image.Dispose();

            }
            catch (Exception e)
            {

            }
        }


    }
    public class PrintDirectClass
    {
        private int printNum = 0;//多页打印
        private string imageFile = "D:\\img\\1.png";//单个图片文件
        private ArrayList fileList = new ArrayList();//多个图片文件

        public void PrintPreview()
        {
            PrintDocument docToPrint = new PrintDocument();

            docToPrint.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.docToPrint_BeginPrint);
            docToPrint.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.docToPrint_EndPrint);
            docToPrint.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.docToPrint_PrintPage);
            docToPrint.DefaultPageSettings.Landscape = false;
            docToPrint.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";


            //PrintDialog printDialog = new PrintDialog();
            //printDialog.AllowSomePages = true;
            //printDialog.ShowHelp = true;
            //printDialog.Document = docToPrint;

            //if (printDialog.ShowDialog() == DialogResult.OK)
            //{
            docToPrint.Print();
            
            //}


        }
        private void docToPrint_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (fileList.Count == 0)
                fileList.Add(imageFile);
        }
        private void docToPrint_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
        private void docToPrint_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //图片抗锯齿
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Stream fs = new FileStream(fileList[printNum].ToString().Trim(), FileMode.Open, FileAccess.Read);
            System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;
            int width = image.Width;
            int height = image.Height;
            if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
            {
                width = e.MarginBounds.Width;
                height = image.Height * e.MarginBounds.Width / image.Width;
            }
            else
            {
                height = e.MarginBounds.Height;
                width = image.Width * e.MarginBounds.Height / image.Height;
            }
            int big = 300;
            int a = e.PageBounds.Width / 2 - (image.Width + big) / 2;
            int b = e.PageBounds.Height / 2 - (image.Height + big) / 2;
            //DrawImage参数根据打印机和图片大小自行调整
            //System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
            //System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, width, height);
            if (image.Height < 310)
            {
                e.Graphics.DrawImage(image, a, b, image.Width + big, image.Height + big);
                //    System.Drawing.Rectangle destRect1 = new System.Drawing.Rectangle(0, 30, image.Width, image.Height);
                //    e.Graphics.DrawImage(image, destRect1, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
            }
            else
            {
                e.Graphics.DrawImage(image, 0, 0, image.Width + 20, image.Height);
                //    System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle(0, 0, image.Width, image.Height);
                //    e.Graphics.DrawImage(image, destRect2, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
            }

            if (printNum < fileList.Count - 1)
            {
                printNum++;
                e.HasMorePages = true;//HasMorePages为true则再次运行PrintPage事件
                return;
            }
            e.HasMorePages = false;
        }
    }
}
