using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IInvoiceRepository
    {
        public List<Invoice> GetAllInvoices();
        public Invoice GetInvoiceById(int id);
        public void CreateInvoice(Invoice Invoice);
        public void UpdateInvoice(Invoice Invoice);
        public void DeleteInvoice(Invoice Invoice);
        public List<Invoice> GetInvoiceByUserId(int id);
    }
}
