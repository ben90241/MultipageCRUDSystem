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
    public partial class Login : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Benson\source\repos\MultipageCRUDSystem\MultipageCRUDSystem\Database1.mdf;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Student where sname=@sname and sadd=@sadd", sqlCon);
                
                cmd.Parameters.AddWithValue("@sname", textBox1.Text);
                cmd.Parameters.AddWithValue("@sadd", textBox2.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count>=1)
                {
                    MDIParent1 cs = new MDIParent1();
                    cs.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
