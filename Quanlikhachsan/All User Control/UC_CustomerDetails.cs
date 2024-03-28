using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlikhachsan.All_User_Control
{
    public partial class UC_CustomerDetails : UserControl
    {
        function fn = new function();
        String query;
        public UC_CustomerDetails()
        {
            InitializeComponent();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Hàm getRecrod(query) sẽ được gọi để lấy và hiển thị các bản ghi.
            //Kết quả truy vấn sẽ bao gồm các cột như cid, cname, mobile, nationality, gender, dob, idproof, address, checkin, roomNo, roomType, bed, và price.
            if (guna2ComboBox1.SelectedIndex ==0)
            {
                //Trong trường hợp này, truy vấn sẽ lấy tất cả các bản ghi từ bảng customer và bảng rooms, kết nối chúng lại bằng khóa ngoại roomid.
                
                query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid";
                getRecrod(query);
            }
            else if(guna2ComboBox1.SelectedIndex == 1)
            {
                //Trong trường hợp này, truy vấn sẽ lấy tất cả các bản ghi từ bảng customer và bảng rooms, kết nối chúng lại bằng khóa ngoại roomid
                //, nhưng chỉ lấy các bản ghi mà checkout là null, tức là những khách hàng đang có phòng.
                query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout is null";
                getRecrod(query);
            }

            else if(guna2ComboBox1.SelectedIndex==2)
            {
                //Trong trường hợp này, truy vấn sẽ lấy tất cả các bản ghi từ bảng customer và bảng rooms,
                //kết nối chúng lại bằng khóa ngoại roomid, nhưng chỉ lấy các bản ghi mà checkout không phải là null,
                //tức là những khách hàng đã trả phòng.
                query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout is not null";
                getRecrod(query);
            }
        }

        private void getRecrod(String query)
        {
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0]; 
        }
    }
}
