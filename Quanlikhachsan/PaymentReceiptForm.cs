using DevExpress.Xpo.DB.Helpers;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Quanlikhachsan
{
    public partial class PaymentReceiptForm : Form
    {
        function fn = new function();
        String query;
        

        // Constructor tùy chỉnh nhận thông tin từ Form UC_Checkout
        public PaymentReceiptForm()
        {
            InitializeComponent();
        }

            private void PaymentReceiptForm_Load(object sender, EventArgs e)
            {
            //// TODO: This line of code loads data into the 'qLKSDataSet1.rooms' table. You can move, or remove it, as needed.
            //this.roomsTableAdapter.Fill(this.qLKSDataSet1.rooms);
            //// TODO: This line of code loads data into the 'qLKSDataSet1.customer' table. You can move, or remove it, as needed.
            //this.customerTableAdapter.Fill(this.qLKSDataSet1.customer);
            //================================================================//

            //Câu truy vấn id, tên, số điện thoại, thông tin phòng và loại phòng từ bảng khách hàng và bảng rooms
            string query = "SELECT customer.cid, customer.cname, customer.mobile, rooms.roomNo, rooms.roomType " +
                   "FROM customer INNER JOIN rooms ON customer.roomid = rooms.roomid";
            //Lấy dữ liệu từ query
            DataSet ds = fn.getData(query);

            if (ds != null && ds.Tables.Count > 0)
            {
                guna2DataGridView1.DataSource = ds.Tables[0];
            }
        }
        
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Truy vấn tình trạng checkin, checkout, tên khách hàng, giá phòng, loại phòng từ bảng customer và room 
            string query = "SELECT customer.checkin,customer.checkout, customer.cname, rooms.price, rooms.roomType " +
               "FROM customer INNER JOIN rooms ON customer.roomid = rooms.roomid";
            DataSet ds = fn.getData(query);

            if (ds != null && ds.Tables.Count > 0)
            {
                guna2DataGridView1.DataSource = ds.Tables[0];
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
