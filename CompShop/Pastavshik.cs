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
    public partial class Pastavshik : Form
    {
        public static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Sklad.accdb;Persist Security Info=False;";
        private OleDbConnection connection;
        public Pastavshik()
        {
            InitializeComponent();
            connection = new OleDbConnection(connectionString);
        }

        private void Pastavshik_Load(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from Prinat";
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
            string query = "select * from Prinat";
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
            string txt = "select * from [Prinat] where id =" + dataGridView1.Rows[i].Cells[0].Value + "";
            OleDbDataAdapter da = new OleDbDataAdapter(txt, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            label4.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            label16.Text = (dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            label17.Text = (dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            label18.Text = (dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            label19.Text = (dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            label20.Text = (dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            label21.Text = (dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
            label22.Text = (dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
            label23.Text = (dataGridView1.SelectedRows[0].Cells[8].Value.ToString());
            label24.Text = (dataGridView1.SelectedRows[0].Cells[9].Value.ToString());
            label25.Text = (dataGridView1.SelectedRows[0].Cells[10].Value.ToString());
            label26.Text = (dataGridView1.SelectedRows[0].Cells[11].Value.ToString());
            label27.Text = (dataGridView1.SelectedRows[0].Cells[12].Value.ToString());         
            errorProvider1.Clear();
            connection.Close();
            if (label27.Text == "ОПЛАЧЕНО") label27.ForeColor = Color.Green;
            if (label27.Text == "НЕОПЛАЧЕНО") label27.ForeColor = Color.Red;
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
                        if (label27.Text == "ОПЛАЧЕНО")
                        {
                            MessageBox.Show("Заказ уже оплачен!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ClearData();

                        }
                        else
                        {
                            try
                            {
                                connection.Open();
                                OleDbCommand cmd = new OleDbCommand("update Prinat set Статус='ОПЛАЧЕНО' where id=" + label4.Text + "", connection);
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
        private bool Validation()
        {
            bool resaul = false;

           
           if (string.IsNullOrEmpty(label16.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(label16, "Заполните поле");
            }

            else if (string.IsNullOrEmpty(label17.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(label17, "Заполните поле");
            }
            else if (string.IsNullOrEmpty(label27.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(label27, "Заполните поле");
            }
            else
            {
                errorProvider1.Clear();
                resaul = true;
            }
            return resaul;
        }
        private void ClearData()
        {
            label4.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";
            label22.Text = "";
            label23.Text = "";
            label24.Text = "";
            label25.Text = "";
            label26.Text = "";
            label27.Text = "";
            errorProvider1.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ClearData();
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
                        string query = "delete from Prinat where ID = " + label4.Text + "";
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("=======ТОО'АГРОСОЮЗ'=======", new Font("Century Gothis", 20, FontStyle.Bold), Brushes.Red, new Point(180));
            e.Graphics.DrawString("Номер заказа:" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 100));
            e.Graphics.DrawString("Фамилия:" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 140));
            e.Graphics.DrawString("Имя:" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 180));
            e.Graphics.DrawString("Поставщик:" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 220));
            e.Graphics.DrawString("Телефон:" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 260));
            e.Graphics.DrawString("Название товара:" + dataGridView1.SelectedRows[0].Cells[5].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 300));
            e.Graphics.DrawString("Количество(Kg):" + dataGridView1.SelectedRows[0].Cells[6].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 340));
            e.Graphics.DrawString("Цена:" + dataGridView1.SelectedRows[0].Cells[7].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 380));
            e.Graphics.DrawString("Описание:" + dataGridView1.SelectedRows[0].Cells[8].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 420));
            e.Graphics.DrawString("Принял:" + dataGridView1.SelectedRows[0].Cells[9].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 460));
            e.Graphics.DrawString("Дата принятия:" + dataGridView1.SelectedRows[0].Cells[10].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 500));
            e.Graphics.DrawString("Итого:" + dataGridView1.SelectedRows[0].Cells[11].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 540));
            e.Graphics.DrawString("Статус:" + dataGridView1.SelectedRows[0].Cells[12].Value.ToString(), new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(10, 580));         
            e.Graphics.DrawString("Подпись", new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(300, 700));
            e.Graphics.DrawString("Спасибо за сотрудничество!", new Font("Century Gothis", 20, FontStyle.Regular), Brushes.Blue, new Point(400, 850));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin aa = new Admin();
            aa.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Otpravit oo = new Otpravit();
            oo.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sklad ss = new Sklad();
            ss.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Admin aa = new Admin();
            aa.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Otpravlennye oo = new Otpravlennye();
            oo.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
