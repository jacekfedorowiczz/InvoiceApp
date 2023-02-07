﻿namespace InvoiceApp.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public long? TaxNumber { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
    }
}
