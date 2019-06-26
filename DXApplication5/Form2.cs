using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thsoft_core;

namespace DXApplication5
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
        }
        
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.DataSource = my_sql.listTable("select Title,Redu,Date from q");
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToXls(@"D:\1.xls");

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
        }
    }
}
