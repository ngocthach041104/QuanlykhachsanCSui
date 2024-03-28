using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlikhachsan.All_User_Control
{

    public partial class UC_Employee : UserControl
    {
        function fn = new function();
        String query;
        public UC_Employee()
        {
            InitializeComponent();
        }

        private void UC_Employee_Load(object sender, EventArgs e)
        {

        }

        public void getMaxID()
        {   //Thiết lập ID tăng dần cho nhân viên
            query = "select max(eid) from employee";
            DataSet ds = fn.getData(query);
            if (ds.Tables[0].Rows[0][0].ToString() != "")
            {
                Int64 num = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                labelToSET.Text = (num + 1).ToString();
            }
        }
        //Đăng ký nhân viên
        private void btnRegis_Click(object sender, EventArgs e)
        {   
            if(txtName.Text != "" && txtMobile.Text != "" && txtGender.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {   
                //Thêm vào bảng dữ liệu thông tin nhân viên
                String name = txtName.Text;
                String mobile = txtMobile.Text;
                String email = txtEmail.Text;
                String gender = txtGender.Text;
                String username = txtUsername.Text;
                String password = txtPassword.Text;
                query = "insert into employee (ename, mobile, gender, emailid, username, pass) values ('" + name + "'," + mobile + ",'" + gender + "','" + email + "','" + username + "','" + password + "')";
                fn.setData(query, "Đăng ký thành viên thành công!");
                //Xóa toàn bộ thông tin đã nhập trước đó sau khi đăng ký thành công 
                clearAll();
                //ID thiết lập tăng dần
                getMaxID();
            }
            
        }

        //Xóa thông tin
        public void clearAll()
        {
            txtName.Clear();
            txtMobile.Clear();
            txtGender.SelectedIndex = -1;
            txtUsername.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
        }
        //Xem thông tin nhân viên
        private void tabEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabEmployee.SelectedIndex == 1)
            {
                setEmployee(guna2DataGridView1);
                /*if(MessageBox.Show("Bạn có chắc không", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = "delete from employee where eid = " + txtID.Text + "";
                    fn.setData(query, "Thông tin nhân viên đã được xóa");
                    tabEmployee_SelectedIndexChanged(this, null);
                }*/
            }
            else if(tabEmployee.SelectedIndex == 2)
            {
                setEmployee(guna2DataGridView2);
            }
        }

        public void setEmployee(DataGridView dgv)
        {  
            query = "select * from employee";
            DataSet ds = fn.getData(query);
            dgv.DataSource = ds.Tables[0];
        }
        //Xóa thông tin nhân viên
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc chắn xóa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) ;
                {
                    query = "delete from employee where eid = " + txtID.Text + "";
                    fn.setData(query, "Thông tin nhân viên đã được xóa");
                    tabEmployee_SelectedIndexChanged(this, null);

                    // Kiểm tra số lượng nhân viên sau khi xóa
                    query = "SELECT COUNT(*) FROM employee";
                    DataSet dsCount = fn.getData(query);
                    int employeeCount = Convert.ToInt32(dsCount.Tables[0].Rows[0][0]) -  1 ;
                }
            }
            //Thông báo lỗi khi chưa điền ID cần xóa
            else if (txtID.Text == "")
            {
                MessageBox.Show("Chưa điền ID nhân viên cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UC_Employee_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void labelToSET_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
