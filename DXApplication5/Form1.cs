using NSoup.Nodes;
using System;
using System.Data;
using System.Net;
using System.Text;
using System.Windows.Forms;
using thsoft_core;
using System.Threading;

namespace DXApplication5
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        static int i = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelControl1.Text = "开始写入";
            WebClient webClient = new WebClient();
            webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Safari/537.36");
            String HtmlString = Encoding.UTF8.GetString(webClient.DownloadData("https://www.baidu.com/s?cl=3&tn=baidutop10&fr=top1000&wd=%E5%B1%A0%E5%91%A6%E5%91%A6%E5%9B%A2%E9%98%9F%E6%96%B0%E7%AA%81%E7%A0%B4&rsv_idx=2&rsv_dl=fyb_n_homepage"));
            Document doc = NSoup.NSoupClient.Parse(HtmlString);
            string[] str = new string[doc.Select("td>span>a[href]").Count];
            string[] str1 = new string[doc.Select("td.opr-toplist1-right").Count];
            for (int i = 0; i < doc.Select("td>span>a[href]").Count; i++)
            {
                str[i] = doc.Select("td>span>a[href]")[i].Attr("title");
            }
            for (int i = 0; i < doc.Select("td.opr-toplist1-right").Count; i++)
            {
                str1[i] = doc.Select("td.opr-toplist1-right").Text.Split(' ')[i];
            }
            for (int i = 0; i < str.Length; i++)
            {//insert into q (Title,Redu) SELECT '" + str[i] + "','" + str1[i] + "','' FROM q where '','','' not in (SELECT Title,Redu FROM q)
                my_sql.updateSql("INSERT INTO `q` (`Title`, `ReDu`,`Date`) VALUES ('" + str[i] + "', '" + str1[i] + "','" + DateTime.Now.ToString() + "')");
            }
            labelControl1.Text = "写入完毕";
            timer1.Stop();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                //ThreadPool.SetMaxThreads(300, 300);
            labelControl1.Text = "启动";
            timer1.Start();
            //for (;;)
            //{
            //ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadMethod), i);
            // }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
