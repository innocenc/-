using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebHelper.GetCsvString(@"{accesstokenUrl:'https://oapi.dingtalk.com/gettoken?corpid=ding32b764261f7b682b35c2f4657eb6378f&corpsecret=1-MI996IRufZTudBOfFWg0MPuJgj1nPaywXEUxRDIPvaAPYLBykesScLq-8imwsn',cardDataUrl:'https://oapi.dingtalk.com/attendance/list',SavePath:'c:/updd.txt',postdata:{userId:'',from:'-1',to:'0'}}"); 
        }
    }
}
