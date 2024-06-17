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
    public partial class Admin : Form
    {
        int d;
        public static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Sklad.accdb;Persist Security Info=False;";
        private OleDbConnection connection;
        public Admin()
        {
            InitializeComponent();
            connection = new OleDbConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sklad gg = new Sklad();
            gg.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from Sotrud";
            command.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            button1.Enabled = true;
            button12.Enabled = true;
            button2.Enabled = false;
            button11.Enabled = true;
            button6.Enabled = false;
            connection.Close();
        }
        private void LoadData()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select * from Sotrud";
            command.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }
        private bool Validation()
        {
            bool resaul = false;

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                            errorProvider1.Clear();
                errorProvider1.SetError(panel10, "Заполните поле");
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
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            connection.Open();
            int i = dataGridView1.CurrentRow.Index;
            string txt = "select * from [Sotrud] where id =" + dataGridView1.Rows[i].Cells[0].Value + "";
            OleDbDataAdapter da = new OleDbDataAdapter(txt, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            label4.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            textBox1.Text = ("" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            textBox2.Text = ("" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            textBox3.Text = ("" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            textBox4.Text = ("" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            connection.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
        private bool IfEmployeeExists(string name, string Mobile)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "select 1 from Sotrud where Фамилия='" + textBox2.Text + "'and Имя='" + textBox1.Text + "'";
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

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sklad ss = new Sklad();
            ss.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
            textBox1.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;

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
                            if (IfEmployeeExists(textBox2.Text, textBox1.Text))
                            {
                                MessageBox.Show("Такой администратор уже зарегистрирован!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                try
                                {
                                    connection.Open();
                                    OleDbCommand cmd = new OleDbCommand("insert into Sotrud (Фамилия,Имя,ИИН,Номер) values ('" + textBox2.Text + "','" + textBox1.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')", connection);
                                    MessageBox.Show("Сотрудник успешно добавлен!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        string msg = "Вы хотите изменить данные?";
                        string caption = "Изменить";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        MessageBoxIcon ico = MessageBoxIcon.Question;
                        DialogResult result;
                        result = MessageBox.Show(this, msg, caption, buttons, ico);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                connection.Open();
                                OleDbCommand cmd = new OleDbCommand("update Sotrud set Фамилия='" + textBox2.Text + "',Имя='" + textBox1.Text + "',ИИН='" + textBox3.Text + "',Номер='" + textBox4.Text + "' where id=" + label4.Text + "", connection);
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
                        MessageBox.Show("Не удалось изменить данные: " + ex.Message.ToString(), "Изменить", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            button1.Enabled = true;
            button12.Enabled = true;
            button2.Enabled = false;
            button11.Enabled = true;
            button6.Enabled = false;
            label4.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            textBox1.Text = ("" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            textBox2.Text = ("" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            textBox3.Text = ("" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            textBox4.Text = ("" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            d = 1;
            
            textBox2.BackColor = Color.Yellow;
            textBox1.BackColor = Color.Yellow;
            textBox3.BackColor = Color.Yellow;
            textBox4.BackColor = Color.Yellow;
            textBox2.Focus();
            button1.Enabled = false;
            button12.Enabled = false;
            button2.Enabled = true;
            button11.Enabled = true;
            button6.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button12.Enabled = false;
            button2.Enabled = true;
            button11.Enabled = true;
            button6.Enabled = true;
            textBox2.BackColor = Color.Yellow;
            textBox1.BackColor = Color.Yellow;
            textBox3.BackColor = Color.Yellow;
            textBox4.BackColor = Color.Yellow;
            textBox2.Focus();
            d = 2;
            connection.Open();
            int i = dataGridView1.CurrentRow.Index;
            string txt = "select * from [Sotrud] where id =" + dataGridView1.Rows[i].Cells[0].Value + "";
            OleDbDataAdapter da = new OleDbDataAdapter(txt, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            label4.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            textBox2.Text = ("" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            textBox1.Text = ("" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            textBox3.Text = ("" + dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            textBox4.Text = ("" + dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            connection.Close();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
            textBox1.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;
            label4.Text = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            try
            {
                string msg = "Вы хотите удалить выбранные записи?";
                string caption = "Удаление";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon ico = MessageBoxIcon.Question;
                DialogResult result;
                result = MessageBox.Show(this, msg, caption, buttons, ico);
                if (result == DialogResult.Yes)
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "delete from Sotrud where ID = " + label4.Text + "";
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
                MessageBox.Show("Не удалось удалить данные: " + ex.Message.ToString(), "Удаление данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            button1.Enabled = true;
            button12.Enabled = true;
            button2.Enabled = false;
            button11.Enabled = true;
            button6.Enabled = false;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
            textBox1.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White; 
            ClearData();
            errorProvider1.Clear();
            button1.Enabled = true;
            button12.Enabled = true;
            button2.Enabled = false;
            button11.Enabled = true;
            button6.Enabled = false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Sklad gg = new Sklad();
            gg.Show();
            this.Hide();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox3.MaxLength = 12;
            if (Char.IsNumber(e.KeyChar)  | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 ss = new Form1();
            ss.Show();
            Form1 frm1 = new Form1();
            frm1.Text = "1";
        }
    }
}
