using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Herintaa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(search.Text))
                    dataGridView.DataSource = dataprodukBindingSource;
                else
                {
                    var query = from o in this.db_produk.data_produk
                                where o.NamaBarang.Contains(search.Text) || o.JumlahBarang == search.Text || o.AsalPabrik == search.Text || o.Deskripsi.Contains(search.Text)
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    dataprodukBindingSource.RemoveCurrent();
            }
        }

        private void button2_Click(object sender, EventArgs e) //Button New
        {
            try
            {
                panel.Enabled = true;
                namaBarang.Focus();
                this.db_produk.data_produk.Adddata_produkRow(this.db_produk.data_produk.Newdata_produkRow());
                dataprodukBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataprodukBindingSource.ResetBindings(false);
            }
        }

        private void button3_Click(object sender, EventArgs e) //button edit
        {
            panel.Enabled = true;
            namaBarang.Focus();
        }

        private void button4_Click(object sender, EventArgs e) //Button Cancel
        {
            panel.Enabled = false;
            dataprodukBindingSource.ResetBindings(false);
        }

        private void button5_Click(object sender, EventArgs e) //Button save
        {
            try
            {
                dataprodukBindingSource.EndEdit();
                data_produkTableAdapter.Update(this.db_produk.data_produk);
                panel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataprodukBindingSource.ResetBindings(false);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'db_produk.data_produk' table. You can move, or remove it, as needed.
            this.data_produkTableAdapter.Fill(this.db_produk.data_produk);
            dataprodukBindingSource.DataSource = this.db_produk.data_produk;

        }

        private void browse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
