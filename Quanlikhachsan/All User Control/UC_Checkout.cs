using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlikhachsan.All_User_Control
{
   

    public partial class UC_Checkout : UserControl
    {
        function fn = new function();
        String query;

        public UC_Checkout()
        {
            InitializeComponent();
        }

        //Truy vấn ra thông tin của khách hàng, load ra danh sách khi mà tình trạng checkout là NO
        private void UC_Checkout_Load(object sender, EventArgs e)
        {
            query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where chekout = 'NO'";
            DataSet db = fn.getData(query);
            guna2DataGridView1.DataSource = db.Tables[0];


        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            // Truy vấn ra thông tin khách hàng có tên cần tìm với tình trạng checkout là NO
            query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where cname like '" + txtName.Text + "%' and chekout = 'NO'";

            //Lấy dữ liệu
            DataSet ds = fn.getData(query);

            // Kiểm tra 
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                // Lấy ra giá trị phần tử đã chọn
                int selectedIndex = guna2DataGridView1.SelectedRows[0].Index;

                //Lọc dữ liệu dựa trên phần tử đã chọn
                DataRow selectedRow = ds.Tables[0].Rows[selectedIndex];
                DataTable filteredTable = ds.Tables[0].Clone();
                filteredTable.ImportRow(selectedRow);

               
                guna2DataGridView1.DataSource = filteredTable;
            }
            else
            {
                //Nếu như không có dữ liệu nào được chọn thì không hiển thị
                guna2DataGridView1.DataSource = ds.Tables[0];
            }
        }

        int id;
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.RowIndex].Value != null)
            {   
                id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtCName.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtRoom.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            }
        }
        public void clearAll()
        {
            txtCheckOutDate.ResetText();
            txtRoom.Clear();
            txtCName.Clear();
            txtName.Clear();
        }
        //Xử lý sự kiện khi bấm vào nút thanh toán
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (txtCName.Text != "")
            {   //Hỏi lại khách hàng một lần nữa
                if (MessageBox.Show("Bạn có chắc chắn không ?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {   
                    //Cập nhật thông tin chekout từ NO thành YES và cập nhật lại phòng đã đặt từ booked YES thành NO với số phòng tương ứng 
                    String cdate = txtCheckOutDate.Text;
                    query = "update customer set chekout = 'YES', checkout = '" + cdate + "' where cid = " + id + "; update rooms set booked = 'NO' where roomNo = '" + txtRoom.Text + "'";
                    fn.setData(query, "Thanh toán thành công");
                    //Thông báo cho khách hàng đã thanh toán thành công
                    UC_Checkout_Load(this, null);
                    clearAll();
                }
            }
            else
            {
                MessageBox.Show("Không có khách hàng", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Tạo một instance của form hóa đơn
            PaymentReceiptForm receiptForm = new PaymentReceiptForm(/* Truyền các thông tin cần thiết */);

            // Hiển thị form hóa đơn
            receiptForm.ShowDialog(); // Sử dụng Show() nếu bạn muốn form hóa đơn không phải là cửa sổ modal
        }

        private void txtCName_TextChanged(object sender, EventArgs e)
        {

        }
    }

    }
