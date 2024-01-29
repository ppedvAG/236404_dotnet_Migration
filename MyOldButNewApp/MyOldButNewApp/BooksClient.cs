using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            var url = $"https://www.googleapis.com/books/v1/volumes?q={textBox1.Text}&maxResults=40";

            var http = new HttpClient();

            var json = await http.GetStringAsync(url);

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<BooksResult>(json);

            dataGridView1.DataSource = result.items.Select(x => x.volumeInfo).ToList();
           
        }
    }
}
