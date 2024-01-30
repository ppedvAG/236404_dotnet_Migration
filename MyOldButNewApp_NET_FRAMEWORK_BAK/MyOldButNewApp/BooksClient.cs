using MyOldButNewApp.GoogleBooksClient;
using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace MyOldButNewApp
{
    public partial class BooksClient : UserControl
    {
        public BooksClient()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var bm = new BooksManager();

            dataGridView1.DataSource = (await bm.GetVolumeInfos(textBox1.Text)).ToList();
        }
    }
}
