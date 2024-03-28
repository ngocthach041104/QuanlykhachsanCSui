using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlikhachsan
{
    public partial class Form1 : Form
    {   
        function fn = new function();
        String query;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn muốn đóng chương trình", "Thông báo", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {   
            //Truy vấn tên người dùng và pass word để kiểm tra
            query = "select username, pass from employee where username = '" + txtUserName.Text + "' and pass = '" + txtPassword.Text + "'";
            DataSet ds = fn.getData(query);
            //Nếu tồn tại sẽ cho phép đăng nhập 
            if (ds.Tables[0].Rows.Count != 0)
            {
                labelError.Visible = false;
                Dashboard dash = new Dashboard();
                this.Hide();
                dash.Show();
            }
            if(txtUserName.Text == "admin" && txtPassword.Text == "123")
            {
                Dashboard dash = new Dashboard();
                this.Hide();
                dash.Show();
            }
            //Nếu như tên đăng nhập hay mật khẩu bị bỏ trống thì hệ thống sẽ báo lỗi
            else if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                DialogResult dg = MessageBox.Show("Tên đăng nhập hoặc mật khẩu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //Trường hợp sai mật khẩu sẽ báo lỗi và xóa đi mật khẩu để người dùng nhập lại
            else
            {
                labelError.Visible = true;
                txtPassword.Clear();
            } 
                
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
