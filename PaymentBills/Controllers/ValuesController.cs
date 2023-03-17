using PaymentBills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Http.Cors;

namespace PaymentBills.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalManagementSystem"].ConnectionString);
        Payment pay = new Payment();

        // GET api/values
        public List<Payment> Get()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("GetPaymentDetails", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<Payment> listPayment = new List<Payment>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        Payment pay = new Payment();
                        pay.PatientBillId = Convert.ToInt32(dt.Rows[i]["PatientBillId"]);
                        pay.PaymentId = Convert.ToInt32(dt.Rows[i]["PaymentId"]);
                        pay.PatientName = dt.Rows[i]["PatientName"].ToString();
                        pay.Age = Convert.ToInt32(dt.Rows[i]["Age"]);
                        pay.Gender = dt.Rows[i]["Gender"].ToString();
                        pay.Address = dt.Rows[i]["Address"].ToString();
                        pay.ContactNumber = dt.Rows[i]["ContactNumber"].ToString();
                        pay.RoomBill = Convert.ToInt32(dt.Rows[i]["RoomBill"]);
                        pay.DoctorBill = Convert.ToInt32(dt.Rows[i]["DoctorBill"]);
                        pay.MedicineBill = Convert.ToInt32(dt.Rows[i]["MedicineBill"]);
                        pay.TotalBill = Convert.ToInt32(dt.Rows[i]["TotalBill"]);
                        pay.Status = dt.Rows[i]["Status"].ToString();

                        listPayment.Add(pay);
                    }
                }
                if (listPayment.Count > 0)
                {
                    return listPayment;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        // GET api/values/5
        public Payment Get(int id)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SearchPayment", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@PaymentId", id);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Payment payment = new Payment();
                if (dt.Rows.Count > 0)
                {
                    payment.PaymentId = Convert.ToInt32(dt.Rows[0]["PaymentId"].ToString());
                    payment.PatientBillId = Convert.ToInt32(dt.Rows[0]["PatientBillId"]);
                    payment.PatientName = dt.Rows[0]["PatientName"].ToString();
                    payment.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                    payment.Gender = dt.Rows[0]["Gender"].ToString();
                    payment.Address = dt.Rows[0]["Address"].ToString();
                    payment.ContactNumber = dt.Rows[0]["ContactNumber"].ToString();
                    payment.TotalBill = Convert.ToInt32(dt.Rows[0]["TotalBill"]);
                    payment.Status = dt.Rows[0]["Status"].ToString();
                }
                if (payment != null)
                {
                    return payment;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/values
        public string Post(Payment payment)
        {
            try
            {
                string msg = "";
                if (payment != null)
                {
                    SqlCommand cmd = new SqlCommand("GeneratePayment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PatientBillId", payment.PatientBillId);
                    cmd.Parameters.AddWithValue("@patientName", payment.PatientName);
                    cmd.Parameters.AddWithValue("@Age", payment.Age);
                    cmd.Parameters.AddWithValue("@Gender", payment.Gender);
                    cmd.Parameters.AddWithValue("@Address", payment.Address);
                    cmd.Parameters.AddWithValue("@ContactNumber", payment.ContactNumber);
                    cmd.Parameters.AddWithValue("@status", payment.Status);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        msg = "Data is inserted";
                    }
                    else
                    {
                        msg = "Error";
                    }
                }
                return msg;
            }
            
             catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/values/5
        public string Put(int id, Payment payment)
        {
            try
            {
                string msg = "";
                if (payment != null)
                {
                    SqlCommand cmd = new SqlCommand("UpdatePaymentDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId);
                    cmd.Parameters.AddWithValue("@PatientName", payment.PatientName);
                    cmd.Parameters.AddWithValue("@Age", payment.Age);
                    cmd.Parameters.AddWithValue("@Gender", payment.Gender);
                    cmd.Parameters.AddWithValue("@Address", payment.Address);
                    cmd.Parameters.AddWithValue("@ContactNumber", payment.ContactNumber);
                    cmd.Parameters.AddWithValue("@Status", payment.Status);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        msg = "Data has been Updated";
                    }
                    else
                    {
                        msg = "Error";
                    }
                }
                return msg;
            }
            

             catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            try
            {
                string msg = "";
                SqlCommand cmd = new SqlCommand("DeletePaymentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentId", id);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Data has been Deleted";
                }
                else
                {
                    msg = "Error";
                }
                return msg;
            }
            

             catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
