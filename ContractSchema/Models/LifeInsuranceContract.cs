namespace ContractSchema.Models
{
    public class LifeInsuranceContract: IContract
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public double Premium { get; set; }
    }
}