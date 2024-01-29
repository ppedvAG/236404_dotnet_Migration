using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HalloDotNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var liste = new List<string>();
            var ran = new Random();
            while (true)
            {
                liste.Add(ran.Next(1000, 100000).ToString());
            }
        }
    }
}
