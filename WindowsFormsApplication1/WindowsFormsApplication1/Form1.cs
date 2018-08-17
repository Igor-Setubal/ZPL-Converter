using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            System.IO.StreamReader sr = new
            System.IO.StreamReader(openFileDialog1.FileName);

            var test = (sr.ReadToEnd());
            byte[] zpl = Encoding.UTF8.GetBytes(test);
            //adjust print density (8dpmm), label width (4 inches), label height (6 inches), and label index (0) as necessary
            var request = (HttpWebRequest)WebRequest.Create("http://api.labelary.com/v1/printers/8dpmm/labels/5x8/0/");
            request.Method = "POST";
            request.Accept = "application/pdf"; // omit this line to get PNG images back
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = zpl.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(zpl, 0, zpl.Length);
            requestStream.Close();

            try
            {

                var response = (HttpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                SaveFileDialog SaveFile = new SaveFileDialog();
                //var fileStream = File.Create("C:\\Users\\st1ferreig\\Downloads\\lmich4.pdf"); // change file name for PNG images
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                   
                    var fileStream = File.Create((((System.Windows.Forms.FileDialog)(SaveFile)).FileName.ToString() + ".pdf"));
                    responseStream.CopyTo(fileStream);
                    responseStream.Close();


                    using (StreamWriter sw = new StreamWriter(fileStream))
                    {
                        sw.Write(fileStream.ToString());
                        MessageBox.Show("Etiqueta zebra criada !", "Aviso");
                    }

                }


            }

            catch (WebException ex)
            {
                Console.WriteLine("Código da etiqueta errado ! Error: {0}", ex.Status);
            }

        }


        public static Stream fileStream { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName);
                    //System.Diagnostics.Process.Start(openFileDialog1.FileName);

                        button1.Enabled = true;

                    sr.Close();

                }
        
            }

            catch (WebException sr) 
            {
                Console.WriteLine("Código da etiqueta errado ! Error: {0}", sr.Status);
            }
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

    }
}

        
    
        
    

