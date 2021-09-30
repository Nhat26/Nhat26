using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab02_04
{

    public partial class Form1 : Form
    {
        DataTable dataTable;
        int SoThuTu;
        public Form1()
        {
            InitializeComponent();
            SoThuTu = 0;
            textBox5.ReadOnly = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox5.Text = "0";
            dataTable = new DataTable();
            dataTable.Columns.Add("So Thu Tu");
            dataTable.Columns.Add("So Tai Khoan");
            dataTable.Columns.Add("Ten Khach Hang");
            dataTable.Columns.Add("Dia Chi");
            dataTable.Columns.Add("So Tien");
            FillDataToDataGridView(dataTable);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public void FillDataToDataGridView(DataTable dt)
        {
            dataGridView1.DataSource = dt;
        }
        public bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && string.IsNullOrWhiteSpace(textBox2.Text) && string.IsNullOrWhiteSpace(textBox3.Text)&& string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Vui long nhap thong tin day du");
                return false;
            }
            else
            {
                float a;
                if (float.TryParse(textBox4.Text, out a) && float.TryParse(textBox1.Text, out a) && !float.TryParse(textBox2.Text, out a) && !float.TryParse(textBox3.Text, out a) )
                {
                    
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
           

        }
      
        public int FindID(string SOTK)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][0] != null && dataTable.Rows[i].ItemArray[1].ToString().CompareTo(SOTK) == 0)
                {
                    return i;
                }
            }
            return -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (!ValidateData())
            {
                MessageBox.Show("error");
            }
            else
            {

                int index = FindID(textBox1.Text);

                if (index == -1)
                {
                    SoThuTu++;
                    MessageBox.Show("Them Thanh Cong");
                    dataTable.Rows.Add(new string[] { SoThuTu.ToString(), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text });
                    FillDataToDataGridView(dataTable);

                }
                else
                {
                    dataTable.Rows[index].SetField("So Thu Tu", SoThuTu.ToString());
                    dataTable.Rows[index].SetField("So Tai Khoan", textBox1.Text);
                    dataTable.Rows[index].SetField("Ten Khach Hang", textBox2.Text);
                    dataTable.Rows[index].SetField("Dia Chi", textBox3.Text);
                    dataTable.Rows[index].SetField("So Tien", textBox4.Text);

                }
                TongTien(dataTable);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataTable.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                FillDataToDataGridView(dataTable);
                MessageBox.Show("Xoa Thanh Cong");
                TongTien(dataTable);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count >0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
            
        }
        public void TongTien(DataTable dt)
        {
            int tien = 0;
            foreach (DataRow item in dataTable.Rows)
            {
                tien += int.Parse( item.ItemArray[4].ToString());
            }
            textBox5.Text = tien.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
