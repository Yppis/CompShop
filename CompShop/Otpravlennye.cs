using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompShop
{
    public partial class Otpravlennye : Form
    {
        public static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Sklad.accdb;Persist Security Info=False;";
        private OleDbConnection connection;
        public Otpravlennye()
        {
            InitializeComponent();
            connection = new OleDbConnection(connectionString);
        }

        private void Otpravlennye_Load(object sender, EventArgs e)
        {

            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from Otpravit";
            command.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
            
        }
        private void LoadData()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from Otpravit";
            command.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            connection.Open();
            int i = dataGridView1.CurrentRow.Index;
            string txt = "select * from [Otpravit] where id =" + dataGridView1.Rows[i].Cells[0].Value + "";
            OleDbDataAdapter da = new OleDbDataAdapter(txt, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            label32.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            label4.Text = (dataGridView1.SelectedRows[0].Cells[1].Value.ToString());      
            label20.Text = (dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            label21.Text = (dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            label22.Text = (dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            label23.Text = (dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            label24.Text = (dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
            label26.Text = (dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
            label27.Text = (dataGridView1.SelectedRows[0].Cells[8].Value.ToString());
            label28.Text = (dataGridView1.SelectedRows[0].Cells[9].Value.ToString());
            label29.Text = (dataGridView1.SelectedRows[0].Cells[10].Value.ToString());
            label17.Text = (dataGridView1.SelectedRows[0].Cells[11].Value.ToString());
            label5.Text = (dataGridView1.SelectedRows[0].Cells[12].Value.ToString());
            label31.Text = (dataGridView1.SelectedRows[0].Cells[13].Value.ToString());
            errorProvider1.Clear();
            connection.Close();
            if (label31.Text == "ОПЛАЧЕНО") label31.ForeColor = Color.Green;          
            if (label31.Text == "НЕОПЛАЧЕНО") label31.ForeColor = Color.Red;




        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label31_TextAlignChanged(object sender, EventArgs e)
        {
            
        }
        private bool Validation()
        {
            bool resaul = false;

            if (string.IsNullOrEmpty(label4.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(label4, "Заполните поле");
            }
            else if (string.IsNullOrEmpty(label20.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(label20, "Заполните поле");
            }

            else if (string.IsNullOrEmpty(label21.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(label21, "Заполните поле");
            }
            else if (string.IsNullOrEmpty(label31.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(label31, "Заполните поле");
            }
            else
            {
                errorProvider1.Clear();
                resaul = true;
            }
            return resaul;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    string msg = "Подвердить расчет?";
                    string caption = "Подвердить";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    MessageBoxIcon ico = MessageBoxIcon.Question;
                    DialogResult result;
                    result = MessageBox.Show(this, msg, caption, buttons, ico);
                    if (result == DialogResult.Yes)
                    {
                        if (label31.Text=="ОПЛАЧЕНО")
                        {
                            MessageBox.Show("Заказ уже оплачен!","Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ClearData();

                        }
                        else
                        {
                            try
                            {
                                connection.Open();
                                OleDbCommand cmd = new OleDbCommand("update Otpravit set Статус='ОПЛАЧЕНО' where id=" + label32.Text + "", connection);
                                MessageBox.Show("Расчет выполнен успешно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmd.ExecuteNonQuery();
                                connection.Close();
                                ClearData();
                                LoadData();
                            }
                            catch (Exception ex)
                            {
                                connection.Close();
                                MessageBox.Show("Error" + ex);
                            }
                        }
                    }
                    else
                    {
                        return;

                    }

                       
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось изменить данные: " + ex.Message.ToString(), "Изменить", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void ClearData()
        {
            label32.Text = "";
            label4.Text = "";
            label20.Text = "";
            label21.Text = "";
            label22.Text = "";
            label23.Text = "";
            label24.Text = "";
            label26.Text = "";
            label27.Text = "";
            label28.Text = "";
            label29.Text = "";
            label17.Text = "";
            label5.Text = "";
            label31.Text = "";
            errorProvider1.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    string msg = "Вы хотите удалить данные?";
                    string caption = "Удалить";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    MessageBoxIcon ico = MessageBoxIcon.Question;
                    DialogResult result;
                    result = MessageBox.Show(this, msg, caption, buttons, ico);
                    if (result == DialogResult.Yes)
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = "delete from Otpravit where ID = " + label32.Text + "";
                        command.CommandText = query;
                        MessageBox.Show("Данные удалены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        command.ExecuteNonQuery();
                        connection.Close();
                        ClearData();
                        LoadData();
                    }
                    else if (result == DialogResult.No)
                    {
                        ClearData();

                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось удалить данные: " + ex.Message.ToString(), "Удалить", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();

                }
                ClearData();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("=======ТОО'АГРОСОЮЗ'=======", new Font("Century Gothis", 20, FontStyle.Bold), Brushes.Red, new Point(180));
            e.Graphics.DrawString("Номер заказа:" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 100));
            e.Graphics.DrawString("Получатель:" + dataGridView1.SelectedRows[0].Cells[6].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 140));
            e.Graphics.DrawString("Фамилия:" + dataGridView1.SelectedRows[0].Cells[7].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 180));
            e.Graphics.DrawString("Имя:" + dataGridView1.SelectedRows[0].Cells[8].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 220));
            e.Graphics.DrawString("Адрес:" + dataGridView1.SelectedRows[0].Cells[9].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 260));
            e.Graphics.DrawString("Телефон:" + dataGridView1.SelectedRows[0].Cells[5].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 300));
            e.Graphics.DrawString("Название товара:" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 340));
            e.Graphics.DrawString("Цена:" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 380));
            e.Graphics.DrawString("Описание:" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 420));
            e.Graphics.DrawString("Отправить(Kg):" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 460));
            e.Graphics.DrawString("Итого:" + dataGridView1.SelectedRows[0].Cells[5].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 500));
            e.Graphics.DrawString("Отправитель:" + dataGridView1.SelectedRows[0].Cells[11].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 540));
            e.Graphics.DrawString("Дата отправки:" + dataGridView1.SelectedRows[0].Cells[12].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 580));
            e.Graphics.DrawString("Статус :" + dataGridView1.SelectedRows[0].Cells[13].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 620));
            e.Graphics.DrawString("Подпись", new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(300, 700));
            e.Graphics.DrawString("Спасибо за покупку!", new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(450, 850));


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Glavnay g = new Glavnay();
            g.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Otpravit o = new Otpravit();
            o.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sklad s = new Sklad();
            s.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Admin a = new Admin();
            a.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Pastavshik p = new Pastavshik();
            p.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
