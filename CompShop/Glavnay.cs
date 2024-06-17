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
    public partial class Glavnay : Form
    {
        public static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Sklad.accdb;Persist Security Info=False;";
        private OleDbConnection connection;
        public Glavnay()
        {
            InitializeComponent();
            connection = new OleDbConnection(connectionString);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Admin aa = new Admin();
            aa.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Glavnay_Load(object sender, EventArgs e)
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
            Fiicombo();
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
            OleDbCommand command = new OleDbCommand("select Фамилия from Admin", connection);
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
        private bool Validation()
        {
            bool resaul = false;

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(panel7, "Заполните поле");
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(panel8, "Заполните поле");
            }

            else if (string.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(panel9, "Заполните поле");
            }
            else if (string.IsNullOrEmpty(textBox9.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(panel15, "Заполните поле");
            }
            else if (string.IsNullOrEmpty(textBox4.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(panel10, "Заполните поле");
            }
            else if (string.IsNullOrEmpty(textBox5.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(panel11, "Заполните поле");
            }
            else if (string.IsNullOrEmpty(textBox6.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(panel12, "Заполните поле");
            }
            else if (string.IsNullOrEmpty(textBox7.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(panel13, "Заполните поле");
            }
            else if (string.IsNullOrEmpty(comboBox1.Text))
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

        private void button11_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    string msg = "Принять товар?";
                    string caption = "Сообщение";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    MessageBoxIcon ico = MessageBoxIcon.Question;
                    DialogResult result;
                    result = MessageBox.Show(this, msg, caption, buttons, ico);
                    if (result == DialogResult.Yes)
                    {


                        if (IfEmployeeExists(textBox4.Text))
                        {
                            MessageBox.Show("Такой товар уже на складе!");
                           

                        }
                        else
                        {
                            try
                            {
                                int a = Convert.ToInt32(textBox5.Text);
                                int b = Convert.ToInt32(textBox6.Text);
                                int c = Convert.ToInt32(textBox5.Text) * Convert.ToInt32(textBox6.Text);
                                textBox8.Text = c.ToString();
                                connection.Open();                              
                                OleDbCommand cmd = new OleDbCommand("insert into Prinat (Фамилия,Имя,Поставщик,Телефон,Название,Количество,Цена,Описание,Принял,Дата,Итого,Статус) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox9.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "','" + textBox8.Text + "','НЕОПЛАЧЕНО')", connection);
                                MessageBox.Show("Товар успешно принят", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private bool IfEmployeeExists(string name)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select 1 from Prinat where Название='" + textBox4.Text + "'";
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
        private void ClearData()
        {
          
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox9.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox6.Clear();
            textBox7.Clear();
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox8.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ClearData();
            errorProvider1.Clear();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
           
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sklad ss = new Sklad();
            ss.Show();
            this.Hide();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            textBox9.MaxLength = 11;
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

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Otpravit ot = new Otpravit();
            ot.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Otpravlennye oo = new Otpravlennye();
            oo.Show();
            this.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Pastavshik pp = new Pastavshik();
            pp.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
