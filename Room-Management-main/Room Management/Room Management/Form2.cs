using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Room_Management
{
    public partial class Form2 : Form
    {

        public class user1
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

        public user1 usr2 = new user1();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text = usr2.returnUsr().ToString();
            button1.Visible = false;
            button1.Enabled = false;
            button2.Visible = false;
            button2.Enabled = false;

            if(usr2.returnP1() || usr2.returnP2() || usr2.returnP3())
            {
                button1.Visible = true;
                button1.Enabled = true;
            }

            if(usr2.returnP4() || usr2.returnP5() || usr2.returnP6())
            {
                button2.Visible = true;
                button2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.usr3.login(usr2.returnId(), usr2.returnUsr(), usr2.returnPass(), usr2.returnFulln(), usr2.returnAct());
            form3.usr3.prava(usr2.returnP1(), usr2.returnP2(), usr2.returnP3(), usr2.returnP4(), usr2.returnP5(), usr2.returnP6());
            form3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            this.Hide();
            form4.usr4.login(usr2.returnId(), usr2.returnUsr(), usr2.returnPass(), usr2.returnFulln(), usr2.returnAct());
            form4.usr4.prava(usr2.returnP1(), usr2.returnP2(), usr2.returnP3(), usr2.returnP4(), usr2.returnP5(), usr2.returnP6());
            form4.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
