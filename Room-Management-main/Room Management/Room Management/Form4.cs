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
    public partial class Form4 : Form
    {

        public SqlConnection cnn;
        public SqlDataReader dataReader;
        public SqlCommand naredba;
        public SqlDataAdapter adapter;

        public string output = "";
        public string cs;
        public string sql;

        public class user3
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

        public user3 usr4 = new user3();

        bool Fullname = false;
        string lista;
        bool p1 = false;
        bool p2 = false;
        bool p3 = false;
        bool p4 = false;
        bool p5 = false;
        bool p6 = false;
        int uID;
        string un;
        string fn;
        

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string file = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + @"\RoomMNG" + @"\config.txt";
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    cs = @"" + reader.ReadLine();
                }
            }

            button1.Enabled = false;
            button1.Visible = false;
            button2.Enabled = false;
            button2.Visible = false;
            button3.Enabled = false;
            button3.Visible = false;
            if (usr4.returnP4() || usr4.returnP5() || usr4.returnP6())
            {
                
                cnn = new SqlConnection(cs);
                cnn.Open();
                sql = "select * from Users";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    output = dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + '\n';
                    if (dataReader.GetValue(4).ToString().Contains("True"))
                    {
                        listBox1.Items.Add(output);
                    }
                }
                naredba.Dispose();
                dataReader.Close();
                cnn.Close();
            }

            if (usr4.returnP5())
            {
                button2.Enabled = true;
                button2.Visible = true;
            }

            if (usr4.returnP4())
            {
                button1.Enabled = true;
                button1.Visible = true;
            }

            if (usr4.returnP6())
            {
                button3.Enabled = true;
                button3.Visible = true;
            }
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

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fullname = true;
            
            cnn = new SqlConnection(cs);
            cnn.Open();
            sql = "select * from Users";
            naredba = new SqlCommand(sql, cnn);
            dataReader = naredba.ExecuteReader();

            while (dataReader.Read())
            {
                if (textBox1.Text.ToString() == dataReader.GetValue(1).ToString())
                {
                    Fullname = false;
                }
                
            }
            naredba.Dispose();
            dataReader.Close();
            cnn.Close();

            try
            {

                if (Fullname == true)
                {
                    if (textBox1.Text.ToString() != "" || textBox2.Text.ToString() != "" || textBox3.Text.ToString() != "") {
                        cnn.Open();
                        adapter = new SqlDataAdapter();
                        sql = "insert into Users(Username, Password, FullName, Active) values ('" + textBox1.Text.ToString() + "','" + CreateMD5(textBox2.Text.ToString()) + "','" + textBox3.Text.ToString() + "',1  ) DECLARE @UserID INT; set @UserID = SCOPE_IDENTITY(); ";
                        naredba = new SqlCommand(sql, cnn);

                        adapter = new SqlDataAdapter();

                        if (checkBox1.Checked)
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 1 ,1  );";
                        }
                        else
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 1 ,0  );";
                        }
                        if (checkBox2.Checked)
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 2 ,1  );";
                        }
                        else
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 2 ,0  );";
                        }
                        if (checkBox3.Checked)
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 3 ,1  );";
                        }
                        else
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 3 ,0  );";
                        }
                        if (checkBox4.Checked)
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 4 ,1  );";
                        }
                        else
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 4 ,0  );";
                        }
                        if (checkBox5.Checked)
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 5 ,1  );";
                        }
                        else
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 5 ,0  );";
                        }
                        if (checkBox6.Checked)
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 6 ,1  );";
                        }
                        else
                        {
                            sql += "insert into UserPrava(UserID, PravoID, Aktivno) values (@UserID, 6 ,0  );";
                        }

                        naredba = new SqlCommand(sql, cnn);
                        adapter.InsertCommand = new SqlCommand(sql, cnn);
                        adapter.InsertCommand.ExecuteNonQuery();

                        listBox1.Items.Clear();
                        sql = "select * from Users";
                        naredba = new SqlCommand(sql, cnn);
                        dataReader = naredba.ExecuteReader();

                        while (dataReader.Read())
                        {
                            output = dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + '\n';
                            if (dataReader.GetValue(4).ToString().Contains("True"))
                            {
                                listBox1.Items.Add(output);
                            }
                        }
                        naredba.Dispose();
                        dataReader.Close();
                        cnn.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ispunite sve podatke prije unosa!");
                    }
                }
                else
                {
                    MessageBox.Show("Taj korisnik vec postoji!");
                    Fullname = true;
                }
            }catch
            {

            }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lista = listBox1.SelectedItem.ToString();
                var ID = lista.Split('-')[0];
                var usrn = lista.Split('-')[1];
                var fullN = lista.Split('-')[3];
                uID = int.Parse(ID.ToString());
                un = usrn.ToString();
                fn = fullN.ToString();

                p1 = false;
                p2 = false;
                p3 = false;
                p4 = false;
                p5 = false;
                p6 = false;

                textBox1.Text = un;
                textBox3.Text = fn;
                textBox2.Text = "";

                cnn.Open();
                sql = "select * from UserPrava";
                naredba = new SqlCommand(sql, cnn);
                dataReader = naredba.ExecuteReader();

                while (dataReader.Read())
                {
                    if(dataReader.GetValue(1).ToString() == ID.ToString())
                    {
                        if (dataReader.GetValue(2).ToString().Contains("1") && dataReader.GetValue(3).ToString().Contains("True"))
                        {
                            p1 = true;
                        }

                        if (dataReader.GetValue(2).ToString().Contains("2") && dataReader.GetValue(3).ToString().Contains("True"))
                        {
                            p2 = true;
                        }

                        if (dataReader.GetValue(2).ToString().Contains("3") && dataReader.GetValue(3).ToString().Contains("True"))
                        {
                            p3 = true;
                        }

                        if (dataReader.GetValue(2).ToString().Contains("4") && dataReader.GetValue(3).ToString().Contains("True"))
                        {
                            p4 = true;
                        }

                        if (dataReader.GetValue(2).ToString().Contains("5") && dataReader.GetValue(3).ToString().Contains("True"))
                        {
                            p5 = true;
                        }

                        if (dataReader.GetValue(2).ToString().Contains("6") && dataReader.GetValue(3).ToString().Contains("True"))
                        {
                            p6 = true;
                        }
                       
                    }
                }
                naredba.Dispose();
                dataReader.Close();
                cnn.Close();

                if (p1)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                if (p2)
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
                if (p3)
                {
                    checkBox3.Checked = true;
                }
                else
                {
                    checkBox3.Checked = false;
                }
                if (p4)
                {
                    checkBox4.Checked = true;
                }
                else
                {
                    checkBox4.Checked = false;
                }
                if (p5)
                {
                    checkBox5.Checked = true;
                }
                else
                {
                    checkBox5.Checked = false;
                }
                if (p6)
                {
                    checkBox6.Checked = true;
                }
                else
                {
                    checkBox6.Checked = false;
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.ToString() != "" || textBox2.Text.ToString() != "" || textBox3.Text.ToString() != "") {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    cnn.Open();
                    sql = "update Users set Username='" + textBox1.Text.ToString() + "',FullName='" + textBox3.Text.ToString() + "' where ID=" + uID + ";";
                    adapter.UpdateCommand = new SqlCommand(sql, cnn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    cnn.Close();

                    cnn.Open();
                    if (p1 == true && checkBox1.Checked == false)
                    {
                        sql += "update UserPrava set Aktivno=0 where UserID=" + uID + " and PravoID=1;";
                    }
                    else if (p1 == false && checkBox1.Checked == true)
                    {
                        sql += "update UserPrava set Aktivno=1 where UserID=" + uID + " and PravoID=1;";
                    }
                    if (p2 == true && checkBox2.Checked == false)
                    {
                        sql += "update UserPrava set Aktivno=0 where UserID=" + uID + " and PravoID=2;";
                    }
                    else if (p2 == false && checkBox2.Checked == true)
                    {
                        sql += "update UserPrava set Aktivno=1 where UserID=" + uID + " and PravoID=2;";
                    }
                    if (p3 == true && checkBox3.Checked == false)
                    {
                        sql += "update UserPrava set Aktivno=0 where UserID=" + uID + " and PravoID=3;";
                    }
                    else if (p3 == false && checkBox3.Checked == true)
                    {
                        sql += "update UserPrava set Aktivno=1 where UserID=" + uID + " and PravoID=3;";
                    }
                    if (p4 == true && checkBox4.Checked == false)
                    {
                        sql += "update UserPrava set Aktivno=0 where UserID=" + uID + " and PravoID=4;";
                    }
                    else if (p4 == false && checkBox4.Checked == true)
                    {
                        sql += "update UserPrava set Aktivno=1 where UserID=" + uID + " and PravoID=4;";
                    }
                    if (p5 == true && checkBox5.Checked == false)
                    {
                        sql += "update UserPrava set Aktivno=0 where UserID=" + uID + " and PravoID=5;";
                    }
                    else if (p5 == false && checkBox5.Checked == true)
                    {
                        sql += "update UserPrava set Aktivno=1 where UserID=" + uID + " and PravoID=5;";
                    }
                    if (p6 == true && checkBox6.Checked == false)
                    {
                        sql += "update UserPrava set Aktivno=0 where UserID=" + uID + " and PravoID=6;";
                    }
                    else if (p6 == false && checkBox6.Checked == true)
                    {
                        sql += "update UserPrava set Aktivno=1 where UserID=" + uID + " and PravoID=6;";
                    }
                    adapter.UpdateCommand = new SqlCommand(sql, cnn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    cnn.Close();

                    listBox1.Items.Clear();
                    cnn.Open();
                    sql = "select * from Users";
                    naredba = new SqlCommand(sql, cnn);
                    dataReader = naredba.ExecuteReader();

                    while (dataReader.Read())
                    {
                        output = dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + '\n';
                        if (dataReader.GetValue(4).ToString().Contains("True"))
                        {
                            listBox1.Items.Add(output);
                        }
                    }
                    naredba.Dispose();
                    dataReader.Close();
                    cnn.Close();
                }
                else
                {
                    MessageBox.Show("Ispunite sve podatke prije izmjene!");
                }
                
            }
            catch(Exception ex)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (usr4.returnId() == uID)
                {
                    MessageBox.Show("User kojeg zelite obrisati trenutno se korisit!");
                }
                else
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    cnn.Open();
                    sql = "update Users set Active = 0 where ID=" + uID + ";";
                    adapter.UpdateCommand = new SqlCommand(sql, cnn);
                    adapter.UpdateCommand.ExecuteNonQuery();

                    listBox1.Items.Clear();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    checkBox5.Checked = false;
                    checkBox6.Checked = false;

                    sql = "select * from Users";
                    naredba = new SqlCommand(sql, cnn);
                    dataReader = naredba.ExecuteReader();

                    while (dataReader.Read())
                    {
                        output = dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + '\n';
                        if (dataReader.GetValue(4).ToString().Contains("True"))
                        {
                            listBox1.Items.Add(output);
                        }
                    }
                    naredba.Dispose();
                    dataReader.Close();
                    cnn.Close();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.usr2.login(usr4.returnId(), usr4.returnUsr(), usr4.returnPass(), usr4.returnFulln(), usr4.returnAct());
            form2.usr2.prava(usr4.returnP1(), usr4.returnP2(), usr4.returnP3(), usr4.returnP4(), usr4.returnP5(), usr4.returnP6());
            form2.Show();
        }
    }
}
