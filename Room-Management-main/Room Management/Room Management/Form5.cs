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

namespace Room_Management
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ds = textBox1.Text;
            string ic = textBox2.Text;
            string id = textBox3.Text;
            string pa = textBox4.Text;

            string filepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + @"\RoomMNG" + @"\config.txt";
            string textToWrite = $@"Data Source={ds}\SQLEXPRESS;Initial Catalog={ic};User ID={id};Password={pa}";
      
            using (StreamWriter writer = new StreamWriter(filepath, false))
            {
                writer.WriteLine(textToWrite);
            }

            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
