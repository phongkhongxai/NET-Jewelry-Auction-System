using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class InvoiceDAO
    {
        public static List<Invoice> GetAllInvoices()
        {
            var list = new List<Invoice>();
            try
            {
                using var db = new GroupProjectPRN221();
                list = db.Invoices.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static Invoice GetInvoiceById(int id)
        {
            using var db = new GroupProjectPRN221();
            return db.Invoices.SingleOrDefault(a => a.Id == id);
        }

        public static void CreateInvoice(Invoice Invoice)
        {
            try
            {
                using var db = new GroupProjectPRN221();
                db.Invoices.Add(Invoice);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateInvoice(Invoice Invoice)
        {
            try
            {
                using var db = new GroupProjectPRN221();
                db.Entry<Invoice>(Invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteInvoice(Invoice Invoice)
        {
            try
            {
                using var db = new GroupProjectPRN221();
                var c = db.Invoices.FirstOrDefault(a => a.Id == Invoice.Id);
                if (c != null)
                {
                    c.IsDelete = true;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Invoice not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
