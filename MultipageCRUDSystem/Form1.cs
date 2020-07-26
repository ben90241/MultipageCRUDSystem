using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultipageCRUDSystem
{
    public partial class Form1 : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Benson\source\repos\MultipageCRUDSystem\MultipageCRUDSystem\Database1.mdf;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Student' table. You can move, or remove it, as needed.
            //this.studentTableAdapter.Fill(this.database1DataSet.Student);
            reload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedIndex = -1;
        }
        private void reload()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Student", sqlCon);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //dataGridView1.DataSource = dt;
                dataGridView1.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    int i = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = dr[0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dr[1].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = dr[2].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = dr[3].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = dr[4].ToString();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e) //insert
        {
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("if not exists (select sname from Student where sname=@sname) " +
                "begin insert into Student (sname, sadd, phone, sem, branch) values (@sname, @sadd, @phone, @sem, @branch) end", sqlCon);
                //SqlCommand cmd = new SqlCommand("begin insert into Student (sname, sadd, phone, sem, branch) values (@sname, @sadd, @phone, @sem, @branch) end", sqlCon);

                cmd.Parameters.AddWithValue("@sname", textBox1.Text);
                cmd.Parameters.AddWithValue("@sadd", textBox2.Text);
                cmd.Parameters.AddWithValue("@phone", int.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@sem", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@branch", comboBox1.SelectedItem.ToString());

                int i = cmd.ExecuteNonQuery();

                if(i>=1)
                {
                    MessageBox.Show(i+" Student Registered Successfully : " + textBox1.Text.ToString());
                }
                else
                {
                    MessageBox.Show(" Student is not Registered: ");
                }

                //string str = String.Format("Name: {0}, Address: {1}, Phone: {2}, Semester: {3}, Branch: {4}", textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), textBox4.Text.ToString(), comboBox1.SelectedItem.ToString());
                //MessageBox.Show(str);

                reload();

                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error is "+ ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e) //update
        {
            try 
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("update Student set sadd=@sadd, phone=@phone, sem=@sem, branch=@branch where sname=@sname", sqlCon);

                cmd.Parameters.AddWithValue("@sname", textBox1.Text);
                cmd.Parameters.AddWithValue("@sadd", textBox2.Text);
                cmd.Parameters.AddWithValue("@phone", int.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@sem", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@branch", comboBox1.SelectedItem.ToString());

                int i = cmd.ExecuteNonQuery();

                reload();

                sqlCon.Close();
            }
            catch (Exception ex)
            {

            }

        }

        private void button4_Click(object sender, EventArgs e) //delete
        {
            try
            {
                sqlCon.Open();

                SqlCommand cmd = new SqlCommand("delete Student where sname=@sname", sqlCon);

                cmd.Parameters.AddWithValue("@sname", textBox1.Text);

                int i = cmd.ExecuteNonQuery();

                reload();

                sqlCon.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            reload();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
            catch(Exception ex)
            {

            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                sqlCon.Open();

                SqlCommand cmd = new SqlCommand("select * from Student where sname like '%"+textBox5.Text.ToString()+"%'", sqlCon);

                //SqlCommand cmd = new SqlCommand("select * from Student where sname like '%@sname%'", sqlCon);
                //cmd.Parameters.AddWithValue("@sname", textBox5.Text);

                //int i = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //dataGridView1.DataSource = dt;
                dataGridView1.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    int i = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = dr[0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dr[1].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = dr[2].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = dr[3].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = dr[4].ToString();
                }

                sqlCon.Close();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
