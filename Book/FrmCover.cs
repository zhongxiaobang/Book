using Book.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;

namespace Book
{
    public partial class FrmCover : Form
    {
        private BookInfo book;
        public FrmCover(BookInfo book)
        {
            InitializeComponent();
            this.book = book;
        }

        private async void FrmCover_Load(object sender, EventArgs e)
        {
            this.Text = book.Name;

            HttpClient client = new HttpClient();
            Stream stream = await client.GetStreamAsync(book.ImagePath);
            pictureBox1.Image = Image.FromStream(stream);
        }
    }
}
