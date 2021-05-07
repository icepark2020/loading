using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace 快捷登陆财政账务系统
{
    public partial class form1 : Form
    {
        public int tabControl = 0;

        public form1()
        {
            InitializeComponent();

            init();
        }

        public void AddLab_Gb(GroupBox gb, Dictionary<string, string> dict)
        {
            for (int i = 0; i < dict.Count; i++)
            {
                Label label = new Label();
                label.Name = gb.Name + "lab" + i;  //label的Name
                label.Text = dict.ElementAt(i).Value; //文本
                label.Size = new Size(110, 15);//label大小
                label.Location = new Point(25, 25 + 35 * i);//label坐标
                gb.Controls.Add(label);

                RadioButton RadioButton = new RadioButton();
                RadioButton.Name = gb.Name + "rb" + i;  //label的Name
                RadioButton.Text = dict.ElementAt(i).Key; //文本
                RadioButton.Size = new Size(120, 20);//label大小
                RadioButton.Location = new Point(135, 21 + 35 * i);//label坐标
                gb.Controls.Add(RadioButton);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked) { isConnected(); } else { denglu(); }
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

        private void denglu()
        {
            string RbName = "";
            string textBoxName = textBox1.Text;
            int year = (int)numericUpDown1.Value;

            //Console.WriteLine("name:" + name);

            List<GroupBox> gbxs = new List<GroupBox> { groupBox1, groupBox2, groupBox3 };

            if (textBoxName == "")
            {
                foreach (Control ctr in gbxs[tabControl].Controls)
                {
                    if (ctr is RadioButton && (ctr as RadioButton).Checked)
                    {
                        RbName = ctr.Text;
                        Console.WriteLine("RadioButton:{0}", ctr.Text);
                    }
                }
            }
            else
            {
                RbName = textBox1.Text;
            }
            if (RbName == "")
            {
                MessageBox.Show("请选择要登陆的账号!!!", "*** 提示 ***", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string url = string.Format("http://172.17.5.144:9001/download/index.html?u={0}&y={1}&ip=172.17.5.144&port=9001", RbName, year);
            string url1 = string.Format("rwzwstart://u={0}&y={1}&ip=172.17.5.144&port=9001", RbName, year);

            if (checkBox1.Checked)
            {
                System.Diagnostics.Process.Start("iexplore.exe", url);
            }
            else
            {
                System.Diagnostics.Process.Start(url1);
            }
        }

        private Dictionary<string, string> DictName_Gh()
        {
            var dict = new Dictionary<string, string>
            {
                ["124005604"] = "出纳/审核:张严今",
                ["124005603"] = "会计/主管:和  洁",
            };

            return dict;
        }

        private Dictionary<string, string> DictName_Ksy()
        {
            var dict = new Dictionary<string, string>
            {
                ["124005002"] = "出纳:司朝琳",
                ["124005602"] = "会计:雷  星",
                ["124005001"] = "审核:袁  婧",
                ["124005601"] = "主管:张天福",
            };

            return dict;
        }

        private Dictionary<string, string> DictName_Zx()
        {
            var dict = new Dictionary<string, string>
            {
                ["124038604"] = "出纳:付惠英",
                ["124038602"] = "会计:和  洁",
                ["124038603"] = "审核:杨  洁",
                ["124038601"] = "主管:李国辉",
            };

            return dict;
        }

        private void init()
        {
            //List<GroupBox> gbs = new List<GroupBox> { groupBox1, groupBox2, groupBox3 };

            AddLab_Gb(groupBox1, DictName_Ksy());
            AddLab_Gb(groupBox2, DictName_Zx());
            AddLab_Gb(groupBox3, DictName_Gh());
            numericUpDown1.Value = DateTime.Now.Year;
        }

        private void isConnected()
        {
           /* try
            {*/
                Ping p = new Ping();//172.17.5.3     ,,www.baidu.com
                PingReply reply = p.Send("www.baidu.com", 1000);//第一个参数为ip地址，第二个参数为ping的时间
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
           /* }
            catch
            {
                Console.WriteLine("测试中......");
                MessageBox.Show("参数错误，请重新检查。", "*** 警告 ***", MessageBoxButtons.OK, MessageBoxIcon.Error);
                msglabel.Text = "测试中......";
                msglabel.ForeColor = Color.Red;
            }*/
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tab = tabControl1.SelectedTab.Name;

            Console.WriteLine("tab:" + tab);
            switch (tab)
            {
                case "tabPage1":
                    tabControl = 0;

                    break;

                case "tabPage2":
                    tabControl = 1;

                    break;

                case "tabPage3":
                    tabControl = 2;

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
    }
}