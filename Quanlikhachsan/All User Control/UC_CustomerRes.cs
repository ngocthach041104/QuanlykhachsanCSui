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

namespace Quanlikhachsan.All_User_Control
{
    public partial class UC_CustomerRes : UserControl
    {
        function fn = new function();
        String query;

        public UC_CustomerRes()
        {
            InitializeComponent();
        }
        //Cài đặt thuộc tính cho combobox
        public void setComboBox(String query, ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query);
            while(sdr.Read())
            {
                for(int i = 0; i < sdr.FieldCount; i++)
                {
                    combo.Items.Add(sdr.GetString(i));
                }
            }

            sdr.Close();
        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBed_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomType.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
        }

        //Thực hiện truy vấn số phòng với điều kiện loại giường, loại phòng và tình trạng đặt phòng cho trước
        //Ở đây tình trạng đặt phòng là NO
        private void txtRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomNo.Items.Clear();
            query = "select roomNo from rooms where bed = '" + txtBed.Text + "' and roomType = '" + txtRoomType.Text + "'and booked = 'NO'";
            setComboBox(query, txtRoomNo);
        }

        int rid;
        //Sau khi đã có dữ liệu thì hệ thống sẽ tự động truy vấn ra giá phòng đã được chọn
        private void txtRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "select price, roomid from rooms where roomNo = '" + txtRoomNo.Text + "'";
            DataSet ds = fn.getData(query);
            txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][1].ToString());

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {   
            //Khi tất cả các thuộc tính trên đều đã được điền thông tin thì lúc này khách hàng được thông báo đăng ký
            //thành công, và lúc này tình trạng đặt phòng từ NO sẽ trở thành YES
            if(txtName.Text != "" && guna2TextBox2.Text != "" && txtQuoctich.Text != "" && txtGender.Text != "" 
                && txtDob.Text != "" && txtIDProof.Text != "" && txtAddress.Text != ""
                && txtCheckin.Text != "" && txtCheckin.Text != "" && txtPrice.Text != "")
            {
                String name = txtName.Text;
                Int64 mobile = Int64.Parse(guna2TextBox2.Text);
                String national = txtQuoctich.Text;
                String gender = txtGender.Text;
                String dob = txtDob.Text;
                String Idproof = txtIDProof.Text;
                String address = txtAddress.Text;
                String checkin = txtCheckin.Text;

                query = "insert into customer (cname, mobile, nationality, gender, dob, idproof, address, checkin, roomid) values('" + name + "', " + mobile + ",'" + national + "','" + gender + "','" + dob + "','" + address + "','" + Idproof + "','" + checkin + "'," + rid + "" +
                    ") update rooms set booked = 'YES' where roomNo = '" + txtRoomNo.Text + "'";
                fn.setData(query, "Số phòng" + txtRoomNo.Text + "Đăng ký khách hàng thành công");
                //Sau khi đăng ký thành công thì phương thức clearAll được gọi ra để xóa hết thông tin nhằm tạo đăng ký mới cho khách hàng
                clearAll();
            }
            //Nếu thông tin chưa nhập đủ thì hệ thống yêu cầu nhập đủ
            else
            {
                MessageBox.Show("Xin vui lòng nhập đủ thông tin", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void clearAll()
        {   
            //Xóa toàn bộ thông tin đã điền
            txtName.Clear();
            guna2TextBox2.Clear(); //txtContact
            txtQuoctich.Clear();
            txtGender.SelectedIndex = -1;
            txtDob.ResetText();
            txtIDProof.Clear();
            txtAddress.Clear();
            txtCheckin.ResetText();
            txtBed.SelectedIndex = -1;
            txtRoomType.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();


        }
        private void UC_CustomerRes_Load(object sender, EventArgs e)
        {

        }
    }
}
