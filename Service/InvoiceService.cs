using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceService()
        {
            _invoiceRepository = new InvoiceRepository();
        }
        public void CreateInvoice(Invoice invoice)
        {
            _invoiceRepository.CreateInvoice(invoice);
        }

        public void DeleteInvoice(Invoice invoice)
        {
            _invoiceRepository.DeleteInvoice(invoice);
        }

        public List<Invoice> GetAllInvoices()
        {
            return _invoiceRepository.GetAllInvoices();
        }

        public Invoice GetInvoiceById(int id)
        {
            return _invoiceRepository.GetInvoiceById(id);
        }

		public List<Invoice> GetInvoiceByUserId(int id)
		{
			return _invoiceRepository.GetInvoiceByUserId(id);
		}

		public void UpdateInvoice(Invoice invoice)
        {
            _invoiceRepository.UpdateInvoice(invoice);
        }
    }
}
