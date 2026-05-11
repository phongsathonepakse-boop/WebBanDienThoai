using System;
using System.Collections.Generic;

namespace WebBanDienThoai.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}