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

        public form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            denglu();
        }

        private void denglu()
        {
            List<GroupBox> gbxs = new List<GroupBox>();
            gbxs.Add(groupBox1);
            gbxs.Add(groupBox2);

            string num = "";
            int year = (int)numericUpDown1.Value;
            string name = textBox1.Text;

            if (name == "")
            {
                foreach (Control c in gbxs[i].Controls)
                {
                    if (c is RadioButton && (c as RadioButton).Checked)
                    {
                        num = c.Text;
                        //MessageBox.Show("checked : " + c.Text);
                    }
                }
            }
            else
            {
                num = name;
            }

            string url = string.Format("http://172.17.5.144:9001/download/index.html?u={0}&y={1}&ip=172.17.5.144&port=9001", num, year);

            // MessageBox.Show("url : " + url);
            if (checkBox1.Checked)
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
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static bool isConnected()
        {
            try
            {
                Ping p = new Ping();//172.17.5.3     ,,www.baidu.com
                PingReply reply = p.Send("172.17.5.3", 1000);//第一个参数为ip地址，第二个参数为ping的时间
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("网络连通");
                    return true;
                }
                else
                    Console.WriteLine("失败");
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isConnected())
            {
                button2.ForeColor = Color.Green;
                msglabel.Text = "网络连接成功!,可以开始登陆.";
                msglabel.ForeColor = Color.Green;
            }
            else
            {
                button2.ForeColor = Color.Red;
                msglabel.Text = "网络连接失败,请检查网络!!!";
                msglabel.ForeColor = Color.Red;
                //MessageBox.Show("无法连接,请检查网络!", "连接网站失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}