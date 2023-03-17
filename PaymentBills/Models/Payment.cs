using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentBills.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int PatientBillId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public int RoomBill { get; set; }
        public int DoctorBill { get; set; }
        public int MedicineBill { get; set; }
        public int TotalBill { get; set; }
        public string Status { get; set; }

        //public virtual Bill Bill { get; set; }
    }
}