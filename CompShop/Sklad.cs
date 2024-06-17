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
    
    public partial class Sklad : Form
    {
        int k;
        int d;
        public static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Sklad.accdb;Persist Security Info=False;";
        private OleDbConnection connection;
        public Sklad()
        {
            InitializeComponent();
            connection = new OleDbConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Admin aa = new Admin();
            aa.Show();
            this.Hide();
        }

        //Кнопка "Выход"
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Sklad_Load(object sender, EventArgs e)
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
            label4.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            textBox4.Text = ("" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            textBox5.Text = ("" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            textBox6.Text = ("" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            textBox7.Text = ("" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            comboBox1.Text = ("" + dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            dateTimePicker1.Text = ("" + dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
            textBox8.Text = ("" + dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
            connection.Close();
            Fiicombo();
            fiicombo1();
            textBox4.Focus();
            try
            {
                connection.Open();
                {
                    OleDbCommand command1 = new OleDbCommand();
                    command.Connection = connection;
                    string query1 = "select * from Prinat Where Название='" + textBox10.Text + "'";
                    command.CommandText = query1;
                    OleDbDataAdapter da1 = new OleDbDataAdapter(query1, connection);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    OleDbDataAdapter adapter1 = new OleDbDataAdapter("SELECT * FROM Prinat WHERE Название   LIKE '%" + textBox10.Text + "%'", connection);

                    DataSet ds1 = new DataSet();
                    adapter1.Fill(ds1);
                    this.dataGridView1.AutoGenerateColumns = true;
                    this.dataGridView1.DataSource = ds1.Tables[0];
                    label4.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    textBox4.Text = ("" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    textBox5.Text = ("" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                    textBox6.Text = ("" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                    textBox7.Text = ("" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    comboBox1.Text = ("" + dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                    dateTimePicker1.Text = ("" + dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
                    textBox8.Text = ("" + dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
                    if (dt.Rows.Count > 0)
                    { }
                    else if (textBox10.Text == "")
                    { MessageBox.Show("Введите название товара!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    else
                    { }
                    connection.Close();
                    connection.Close();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Error" + ex);
            }
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


        private void Fiicombo()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("select Фамилия from Sotrud", connection);
            OleDbDataReader rtr;
            rtr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Фамилия", typeof(string));
            dt.Load(rtr);
            comboBox1.ValueMember = "Фамилия";
            comboBox1.DataSource = dt;
            comboBox1.Text = "";
            connection.Close();
        }

        private void fiicombo1()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("select Фамилия from Sotrud", connection);
            OleDbDataReader rtr;
            rtr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Фамилия", typeof(string));
            dt.Load(rtr);
            comboBox2.ValueMember = "Фамилия";
            comboBox2.DataSource = dt;
            comboBox2.Text = "";
            connection.Close();
        }

        //
        private bool Validation()
        {
            bool resaul = false;

            if (string.IsNullOrEmpty(textBox4.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox1, "Заполните поле");
            }
            else
            {
                errorProvider1.Clear();
                resaul = true;
            }
            return resaul;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        Bitmap bmp;
        
        //Печать
        private void button12_Click(object sender, EventArgs e)
        {
            
        }

        //отображение табличного документа
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        //Отображение данных из таблицы базы данных при двойном щелчке
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            connection.Open();
            int i = dataGridView1.CurrentRow.Index;
            string txt = "select * from [Prinat] where id =" + dataGridView1.Rows[i].Cells[0].Value + "";
            OleDbDataAdapter da = new OleDbDataAdapter(txt, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            label4.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            textBox4.Text = ("" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            textBox5.Text = ("" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            textBox6.Text = ("" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            textBox7.Text = ("" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            comboBox1.Text = ("" + dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            dateTimePicker1.Text = ("" + dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
            textBox8.Text = ("" + dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
            errorProvider1.Clear();
            connection.Close();
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }


        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        //Кнопка "Изменить"
        private void button8_Click(object sender, EventArgs e)
        {
            d = 2;
            textBox4.BackColor = Color.Yellow;
            textBox5.BackColor = Color.Yellow;
            textBox6.BackColor = Color.Yellow;
            textBox7.BackColor = Color.Yellow;
            comboBox1.BackColor = Color.Yellow;
            textBox4.Focus();
            button1.Enabled = false;
            button8.Enabled = false;
            button2.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;

            connection.Open();
            int i = dataGridView1.CurrentRow.Index;
            string txt = "select * from [Prinat] where id =" + dataGridView1.Rows[i].Cells[0].Value + "";
            OleDbDataAdapter da = new OleDbDataAdapter(txt, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            label4.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            textBox4.Text = ("" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            textBox5.Text = ("" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            textBox6.Text = ("" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            textBox7.Text = ("" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            comboBox1.Text = ("" + dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            dateTimePicker1.Text = ("" + dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
            textBox8.Text = ("" + dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
            errorProvider1.Clear();
            connection.Close();
        }

        //Очистка полей
        private void ClearData()
        {
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox6.Clear();
            textBox7.Clear();
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox8.Clear();
        }

        //Кнопка "Удалить"
        private void button9_Click(object sender, EventArgs e)
        {
            textBox4.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox6.BackColor = Color.White;
            textBox7.BackColor = Color.White;
            comboBox1.BackColor = Color.White;
            textBox4.Focus();
            button1.Enabled = true;
            button8.Enabled = true;
            button2.Enabled = false;
            button9.Enabled = true;
            button10.Enabled = true;

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

        //Кнопка "Отменить"
        private void button10_Click(object sender, EventArgs e)
        {
            ClearData();
            errorProvider1.Clear();
            textBox4.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox6.BackColor = Color.White;
            textBox7.BackColor = Color.White;
            comboBox1.BackColor = Color.White;
            textBox4.Focus();
            button1.Enabled = true;
            button8.Enabled = true;
            button2.Enabled = false;
            button9.Enabled = true;
            button10.Enabled = true;

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text == "")
            {
                try
                {
                    connection.Open();
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Prinat WHERE Название   LIKE '%" + textBox10.Text + "%'", connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        this.dataGridView1.AutoGenerateColumns = true;
                        this.dataGridView1.DataSource = ds.Tables[0];
                        connection.Close();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show("Error" + ex);
                }
            }
        }

        //Поиск по названию
        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Admin aa = new Admin();
            aa.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Sklad ss = new Sklad();
            ss.Show();
            this.Hide();
        }
        
        //Кнопка "Добавить"
        private void button1_Click_1(object sender, EventArgs e)
        {
            ClearData();
            d = 1;
            textBox4.BackColor = Color.Yellow;
            textBox5.BackColor = Color.Yellow;
            textBox6.BackColor = Color.Yellow;
            textBox7.BackColor = Color.Yellow;
            comboBox1.BackColor = Color.Yellow;
            textBox4.Focus();
            button1.Enabled = false;
            button8.Enabled = false;
            button2.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
        }

        private bool IfEmployeeExists(string name, string Mobile)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select 1 from Prinat where Название='" + textBox4.Text + "'and Количество='" + textBox5.Text + "'and Цена='" + textBox6.Text + "'and Описание='" + textBox7.Text + "'";
            command.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(query, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            connection.Close();

            if (dt.Rows.Count > 0)

            { return true; }

            else
            { return false; }
        }

        //Переход на форму "Склад"
        private void button3_Click_2(object sender, EventArgs e)
        {
            Sklad ss = new Sklad();
            ss.Show();
            this.Hide();
        }

        //Переход на форму "Сотрудники"
        private void button4_Click_2(object sender, EventArgs e)
        {
            Admin aa = new Admin();
            aa.Show();
            this.Hide();

        }

        //Кнопка "Сохранить"
        private void button2_Click_2(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(textBox5.Text);
            int b = Convert.ToInt32(textBox6.Text);
            int c = Convert.ToInt32(textBox5.Text) * Convert.ToInt32(textBox6.Text);
            textBox8.Text = c.ToString();
            textBox4.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox6.BackColor = Color.White;
            textBox7.BackColor = Color.White;
            comboBox1.BackColor = Color.White;
            textBox4.Focus();
            button1.Enabled = true;
            button8.Enabled = true;
            button2.Enabled = false;
            button9.Enabled = true;
            button10.Enabled = true;

            if (d == 1)
            {
                if (Validation())
                {
                    try
                    {
                        string msg = "Вы хотите добавить данные?";
                        string caption = "Добавить";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        MessageBoxIcon ico = MessageBoxIcon.Question;
                        DialogResult result;
                        result = MessageBox.Show(this, msg, caption, buttons, ico);
                        if (result == DialogResult.Yes)
                        {
                            if (IfEmployeeExists(textBox4.Text, textBox5.Text))
                            {
                                MessageBox.Show("Такое наименование уже имеется в базе данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else
                            {
                                try
                                {
                                    connection.Open();
                                    OleDbCommand cmd = new OleDbCommand("insert into Prinat (Название,Количество,Цена,Описание,Принял,Дата,Итого) values ('" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + textBox8.Text + "')", connection);
                                    MessageBox.Show("Наименование введено в базу данных!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Не удалось добавить данные: " + ex.Message.ToString(), "Добавить", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            if (d == 2)
            {

                if (Validation())
                {
                    try
                    {
                        string msg = "Вы хотите сохранить данные?";
                        string caption = "Сохранить";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        MessageBoxIcon ico = MessageBoxIcon.Question;
                        DialogResult result;
                        result = MessageBox.Show(this, msg, caption, buttons, ico);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                connection.Open();
                                OleDbCommand cmd = new OleDbCommand("update Prinat set Название='" + textBox4.Text + "', Количество='" + textBox5.Text + "',Цена='" + textBox6.Text + "',Описание='" + textBox7.Text + "', Принял='" + comboBox1.Text + "',Дата='" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "', Итого='" + textBox8.Text + "' where id=" + label4.Text + "", connection);
                                MessageBox.Show("Данные успешно сохранены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        else
                        {
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось сохранить данные: " + ex.Message.ToString(), "Сохранить", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }


        //Отображение на полях записи таблицы
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label4.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            textBox4.Text = ("" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            textBox5.Text = ("" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            textBox6.Text = ("" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            textBox7.Text = ("" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            comboBox1.Text = ("" + dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            dateTimePicker1.Text = ("" + dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
            textBox8.Text = ("" + dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Вывод данных сотрудника, принявшего товар
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        //Отмена поиска
        private void button6_Click_1(object sender, EventArgs e)
        {
            connection.Close();
            textBox10.Text = "";
        }

        //Вызов формы "О магазине"
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 ss = new Form1();
            ss.Show();
            Form1 frm1 = new Form1();
            frm1.Text = "1";
        }

        //вызов формы "Об авторе"
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2 ss2 = new Form2();
            ss2.Show();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
