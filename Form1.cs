using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace 快捷登陆财政账务系统
{
    public partial class form1 : Form
    {
        public int i = 0;
        public int ceshi;

        public form1()
        {
            InitializeComponent();
            numericUpDown1.Value = DateTime.Now.Year;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                isConnected();
            }
            else
            {
                denglu();
            }
        }

        private void denglu()
        {
            List<GroupBox> gbxs = new List<GroupBox>();
            gbxs.Add(groupBox1);
            gbxs.Add(groupBox2);
            gbxs.Add(groupBox3);
            string num = "";
            int year = (int)numericUpDown1.Value;
            string name = textBox1.Text;
            //Console.WriteLine("name:" + name);
            if (name == "" || name == "9999")
            {
                foreach (Control c in gbxs[i].Controls)
                {
                    if (c is RadioButton && (c as RadioButton).Checked)
                    {
                        num = c.Text;
                        //MessageBox.Show("checked : " + c.Text);
                    }
                }
                if (name == "9999")
                {
                    ceshi = 1;
                }
                else
                {
                    ceshi = 0;
                }
            }
            else
            {
                num = name;
            }

            //string url = string.Format("http://172.17.5.144:9001/download/index.html?u={0}&y={1}&ip=172.17.5.144&port=9001", num, year);
            string url = string.Format("rwzwstart://u={0}&y={1}&ip=172.17.5.144&port=9001", num, year);

            if (ceshi == 1)

            {
                TabPage tab = tabControl1.SelectedTab;
                MessageBox.Show(string.Format("单位:{0}\n\r \n\r账号 :{1}  年度 :{2}", tab.Text, num, year));
            }
            else if (checkBox1.Checked)
            {
                System.Diagnostics.Process.Start("iexplore.exe", url);
            }
            else
            {
                System.Diagnostics.Process.Start(url);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tab = tabControl1.SelectedTab.Name;

            Console.WriteLine("tab:" + tab);
            switch (tab)
            {
                case "tabPage1":
                    i = 0;

                    break;

                case "tabPage2":
                    i = 1;

                    break;

                case "tabPage3":
                    i = 2;

                    break;

                    /*  default:
                          i = 0;

                          break;*/
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void isConnected()
        {
            try
            {
                Ping p = new Ping();//172.17.5.3     ,,www.baidu.com
                PingReply reply = p.Send("172.17.5.3", 1000);//第一个参数为ip地址，第二个参数为ping的时间
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("网络连通");
                    button1.ForeColor = Color.Green;
                    msglabel.Text = "网络连接成功!,正在登陆...";
                    msglabel.ForeColor = Color.Green;

                    denglu();
                   
                }
                else
                {
                    Console.WriteLine("失败");
                    button1.ForeColor = Color.Red;
                    msglabel.Text = "网络连接失败,请检查网络!!!";
                    msglabel.ForeColor = Color.Red;
                    //MessageBox.Show("无法连接,请检查网络!", "连接网站失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                Console.WriteLine("测试中......");

                msglabel.Text = "测试中......";
                msglabel.ForeColor = Color.Red;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox2.Text = "开启『自动登陆』";
            }
            else
            {
                button1.ForeColor = Color.Black;
                msglabel.Text = "";
                checkBox2.Text = "关闭『自动登陆』";
            }
        }
    }
}