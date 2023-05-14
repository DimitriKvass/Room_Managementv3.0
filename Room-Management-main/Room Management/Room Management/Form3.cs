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
    public partial class Form3 : Form
    {

        public SqlConnection cnn;
        public SqlDataReader dataReader;
        public SqlCommand naredba;
        public SqlDataAdapter adapter;

        public string output = "";
        public string cs;
        public string sql;

        public class user2
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

            bool isLogedin = false;

            public void login(int a, string b, string c, string d, bool e)
            {
                this.ID = a;
                this.usr = b;
                this.pas = c;
                this.fuln = d;
                this.active = e;
            }

            public void prava(bool a, bool b, bool c, bool d, bool e, bool f)
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
                this.isLogedin = l;
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
                return this.isLogedin;
            }

            public bool returnP1()
            {
                return this.p1;
            }
            public bool returnP2()
            {
                return this.p2;
            }
            public bool returnP3()
            {
                return this.p3;
            }
            public bool returnP4()
            {
                return this.p4;
            }
            public bool returnP5()
            {
                return this.p5;
            }
            public bool returnP6()
            {
                return this.p6;
            }
        }

        public user2 usr3 = new user2();

        string vrijeme;
        int UserId = -1;
        string pocetakId;
        int pID = -1;
        string krajId;
        int kID = -1;
        string ponavljanjeId;
        int poID = -1;
        string prostorija;
        int proID = -1;
        string rezervacija;
        int rID;
        string IDa;
        int IDb;
        int IDc;
        int IDd;
        int IDe;
        int IDf;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string file = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + @"\RoomMNG" + @"\config.txt";
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    cs = @"" + reader.ReadLine();
                }
            }

            listBox1.Visible = false;
            button1.Visible = false;
            button1.Enabled = false;
            button2.Visible = false;
            button2.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Visible = false;
            comboBox2.Enabled = false;
            comboBox2.Visible = false;
            comboBox3.Enabled = false;
            comboBox3.Visible = false;
            comboBox4.Enabled = false;
            comboBox4.Visible = false;
            comboBox5.Enabled = false;
            comboBox5.Visible = false;

            if (usr3.returnP3())
            {
                listBox1.Visible = true;

                
                cnn = new SqlConnection(cs);
                cnn.Open();
                sql = "select * from Rezervacije";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    output = dataReader.GetValue(0) + "\t" + dataReader.GetValue(1) + "\t" + dataReader.GetValue(2).ToString() + "\t" + dataReader.GetValue(3).ToString() + "\t" + dataReader.GetValue(4) + "\t" + dataReader.GetValue(5) + "\t" + dataReader.GetValue(6) + '\n';
                    if (dataReader.GetValue(7).ToString().Contains("True"))
                    {
                        listBox1.Items.Add(output);
                    }
                }
                naredba.Dispose();
                dataReader.Close();

                sql = "select * from Vremena";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    output = dataReader.GetValue(0) + "-" + dataReader.GetDateTime(1).ToString("HH:mm") + '\n';
                    comboBox2.Items.Add(output);
                }
                naredba.Dispose();
                dataReader.Close();
                output = " ";

                sql = "select * from Vremena";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    output = dataReader.GetValue(0) + "-" + dataReader.GetDateTime(1).ToString("HH:mm") + '\n';
                    comboBox3.Items.Add(output);
                }
                naredba.Dispose();
                dataReader.Close();
                output = " ";

                sql = "select * from Prostorija";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    output = dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + '\n';
                    comboBox4.Items.Add(output);
                }
                naredba.Dispose();
                dataReader.Close();
                output = " ";

                sql = "select * from Ponavljanja";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    output = dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + '\n';
                    comboBox5.Items.Add(output);
                }
                naredba.Dispose();
                dataReader.Close();
                output = " ";

                cnn.Close();
            }

            if (usr3.returnP1())
            {
                button1.Visible = true;
                button1.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker1.Visible = true;
                comboBox2.Enabled = true;
                comboBox2.Visible = true;
                comboBox3.Enabled = true;
                comboBox3.Visible = true;
                comboBox4.Enabled = true;
                comboBox4.Visible = true;
                comboBox5.Enabled = true;
                comboBox5.Visible = true;
            }

            if (usr3.returnP2())
            {
                button2.Visible = true;
                button2.Enabled = true;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            krajId = comboBox3.SelectedItem.ToString();
            var ID = krajId.Split('-')[0];
            kID = int.Parse(ID.ToString());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pocetakId = comboBox2.SelectedItem.ToString();
            var ID = pocetakId.Split('-')[0];
            pID = int.Parse(ID.ToString());
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            prostorija = comboBox4.SelectedItem.ToString();
            var ID = prostorija.Split('-')[0];
            proID = int.Parse(ID.ToString());
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ponavljanjeId = comboBox5.SelectedItem.ToString();
            var ID = ponavljanjeId.Split('-')[0];
            poID = int.Parse(ID.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserId = usr3.returnId();
            if (pID <= kID)
            {
                if (UserId == -1 || pID == -1 || kID == -1 || proID == -1 || poID == -1)
                {
                    MessageBox.Show("Prije unosa ispunite sva polja!");
                }
                else {
                    cnn.Open();
                    adapter = new SqlDataAdapter();
                    sql = "insert into Rezervacije(Datum, UserID, PocetakID, KrajID, ProstorijaID, PonavljanjeID, Aktivno)values(CAST(N'" + vrijeme + "' as date)," + UserId + ", " + pID + ", " + kID + "," + proID + "," + poID + ",1  )";
                    naredba = new SqlCommand(sql, cnn);
                    adapter.InsertCommand = new SqlCommand(sql, cnn);
                    adapter.InsertCommand.ExecuteNonQuery();


                    cnn = new SqlConnection(cs);
                    cnn.Open();

                    listBox1.Items.Clear();
                    sql = "select * from Rezervacije";
                    naredba = new SqlCommand(sql, cnn);
                    dataReader = naredba.ExecuteReader();

                    while (dataReader.Read())
                    {
                        output = dataReader.GetValue(0) + "\t" + dataReader.GetValue(1) + "\t" + dataReader.GetValue(2).ToString() + "\t" + dataReader.GetValue(3).ToString() + "\t" + dataReader.GetValue(4) + "\t" + dataReader.GetValue(5) + "\t" + dataReader.GetValue(6) + '\n';
                        if (dataReader.GetValue(7).ToString().Contains("True"))
                        {
                            listBox1.Items.Add(output);
                        }
                    }
                    naredba.Dispose();
                    dataReader.Close();
                    cnn.Close();
                }

            }
            else
            {
                MessageBox.Show("Kraj ne moze biti prije pocetka!");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            vrijeme = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                cnn.Open();
                sql = "update Rezervacije set Aktivno = 0 where ID=" + rID + ";";
                adapter.UpdateCommand = new SqlCommand(sql, cnn);
                adapter.UpdateCommand.ExecuteNonQuery();

                listBox1.Items.Clear();
                sql = "select * from Rezervacije";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    output = dataReader.GetValue(0) + "\t" + dataReader.GetValue(1) + "\t" + dataReader.GetValue(2).ToString() + "\t" + dataReader.GetValue(3).ToString() + "\t" + dataReader.GetValue(4) + "\t" + dataReader.GetValue(5) + "\t" + dataReader.GetValue(6) + '\n';
                    if (dataReader.GetValue(7).ToString().Contains("True"))
                    {
                        listBox1.Items.Add(output);
                    }
                }
                naredba.Dispose();
                dataReader.Close();
                cnn.Close();
                listBox2.Items.Clear();
            }
            catch(Exception ex)
            {
                
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listBox2.Items.Clear();
                rezervacija = listBox1.SelectedItem.ToString();
                var ID = rezervacija.Split('\t')[0];
                var Ida = rezervacija.Split('\t')[1];
                var Idb = rezervacija.Split('\t')[2];
                var Idc = rezervacija.Split('\t')[3];
                var Idd = rezervacija.Split('\t')[4];
                var Ide = rezervacija.Split('\t')[5];
                var Idf = rezervacija.Split('\t')[6];
                rID = int.Parse(ID.ToString());
                IDa = Ida.ToString();
                IDb = int.Parse(Idb.ToString());
                IDc = int.Parse(Idc.ToString());
                IDd = int.Parse(Idd.ToString());
                IDe = int.Parse(Ide.ToString());
                IDf = int.Parse(Idf.ToString());

                cnn.Open();
                sql = "select * from Rezervacije";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    if (dataReader.GetValue(0).ToString() == rID.ToString())
                    {
                        listBox2.Items.Add(IDa);
                    }
                }
                naredba.Dispose();
                dataReader.Close();


                sql = "select * from Users";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    if (dataReader.GetValue(0).ToString() == IDb.ToString())
                    {
                        listBox2.Items.Add(dataReader.GetValue(1).ToString());
                    }
                }
                naredba.Dispose();
                dataReader.Close();

                sql = "select * from Vremena";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    if (dataReader.GetValue(0).ToString() == IDc.ToString())
                    {
                        listBox2.Items.Add(dataReader.GetDateTime(1).ToString("HH:mm"));
                    }
                    if(dataReader.GetValue(0).ToString() == IDd.ToString())
                    {
                        listBox2.Items.Add(dataReader.GetDateTime(1).ToString("HH:mm"));
                    }
                }
                naredba.Dispose();
                dataReader.Close();

                sql = "select * from Prostorija";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    if (dataReader.GetValue(0).ToString() == IDe.ToString())
                    {
                        listBox2.Items.Add(dataReader.GetValue(1).ToString());
                    }
                }
                naredba.Dispose();
                dataReader.Close();

                sql = "select * from Ponavljanja";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    if (dataReader.GetValue(0).ToString() == IDf.ToString())
                    {
                        listBox2.Items.Add(dataReader.GetValue(1).ToString());
                    }
                }
                naredba.Dispose();
                dataReader.Close();
                cnn.Close();
            }
            catch(Exception ex)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.usr2.login(usr3.returnId(), usr3.returnUsr(), usr3.returnPass(), usr3.returnFulln(), usr3.returnAct());
            form2.usr2.prava(usr3.returnP1(), usr3.returnP2(), usr3.returnP3(), usr3.returnP4(), usr3.returnP5(), usr3.returnP6());
            form2.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
