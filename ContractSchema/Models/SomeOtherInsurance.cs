using System;

namespace ContractSchema.Models
{
    public class SomeOtherContract: IContract
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}