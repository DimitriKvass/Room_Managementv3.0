using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace Room_Management
{
    public partial class Form1 : Form
    {

        public SqlConnection cnn;
        public SqlDataReader dataReader;
        public SqlCommand naredba;

        public string output = "";
        public string cs;
        public string fcs;
        public string sql;
        public string usrn = " ";
        public string pass = " ";

        public class user
        {
            public int ID;
            public string usr;
            public string pas;
            public string fuln;
            public bool active;

            bool p1 = false;
            bool p2 = false;
            bool p3 = false;
            bool p4 = false;
            bool p5 = false;
            bool p6 = false;

            bool Logedin = false;

            public void login(int a,string b,string c,string d,bool e)
            {
                this.ID = a;
                this.usr = b;
                this.pas = c;
                this.fuln = d;
                this.active = e;
            }

            public void prava(bool a,bool b,bool c,bool d,bool e,bool f)
            {
                this.p1 = a;
                this.p2 = b;
                this.p3 = c;
                this.p4 = d;
                this.p5 = e;
                this.p6 = f;
            }

            public void SetLogin(bool l)
            {
                this.Logedin = l;
            }

            public int returnId()
            {
                return this.ID;
            }

            public string returnUsr()
            {
                return this.usr;
            }

            public string returnPass()
            {
                return this.pas;
            }

            public string returnFulln()
            {
                return this.fuln;
            }

            public bool returnAct()
            {
                return this.active;
            }

            public bool isLogdin()
            {
                return this.Logedin;
            }
        }

        public static user usr1 = new user();

        public Form1()
        {
            InitializeComponent();
        }

        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + @"\RoomMNG";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("Folder created successfully.");
            }
            else
            {
                Console.WriteLine("Folder already exists.");
            }

            string file = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + @"\RoomMNG" + @"\config.txt";

            if (!File.Exists(file))
            {
                using (FileStream fs = File.Create(file))
                {

                };

            }

            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    cs = @""+reader.ReadLine();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnn = new SqlConnection(cs);
                cnn.Open();
                usrn = textBox1.Text.ToString();
                pass = CreateMD5(textBox2.Text.ToString());
                sql = "select * from Users where Username='" + usrn + "' and Password='" + pass + "'";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    output += dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + '\n';
                    usr1.login(int.Parse(dataReader.GetValue(0).ToString()), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString(), bool.Parse(dataReader.GetValue(4).ToString()));
                    if (usr1.returnAct())
                    {
                        usr1.SetLogin(true);
                    }
                }

                naredba.Dispose();
                sql = "select * from UserPrava Where UserID=" + usr1.returnId() + " and Aktivno = 1 ";
                naredba = new SqlCommand(sql, cnn);
                dataReader.Close();
                dataReader = naredba.ExecuteReader();
                output = "";

                bool rp = false;
                bool bp = false;
                bool pr = false;
                bool dk = false;
                bool bk = false;
                bool uk = false;

                while (dataReader.Read())
                {
                    output = dataReader.GetValue(2).ToString();

                    if (output == "1")
                    {
                        rp = true;
                    }
                    else if (output == "2")
                    {
                        bp = true;
                    }
                    else if (output == "3")
                    {
                        pr = true;
                    }
                    else if (output == "4")
                    {
                        dk = true;
                    }
                    else if (output == "5")
                    {
                        bk = true;
                    }
                    else if (output == "6")
                    {
                        uk = true;
                    }
                }
                usr1.prava(rp, bp, pr, dk, bk, uk);

                cnn.Close();
                if (usr1.isLogdin())
                {
                    this.Hide();
                    Form2 form2 = new Form2();
                    form2.usr2.login(usr1.returnId(), usr1.returnUsr(), usr1.returnPass(), usr1.returnFulln(), usr1.returnAct());
                    form2.usr2.prava(rp, bp, pr, dk, bk, uk);
                    form2.Show();
                }
                else
                {
                    MessageBox.Show("Neispravni podaci!");
                }
            }
            catch
            {
                MessageBox.Show("Greska kod spajanja - provjeri config file!");
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 form5 = new Form5();
            form5.Show();
        }
    }
}
