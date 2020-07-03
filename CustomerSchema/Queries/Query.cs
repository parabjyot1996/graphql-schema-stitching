using System.Collections.Generic;
using System.Linq;
using CustomerSchema.Model;
using CustomerSchema.Repository;

namespace CustomerSchema.Queries
{
    public class Query
    {
        private readonly CustomerRepository _repository;

        public Query(CustomerRepository repository)
        {
            _repository = repository;
        }

        public Customer GetCustomer(string id)
        {
            return _repository.Customers
                        .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _repository.Customers;
        }

        public IEnumerable<Consultant> GetConsultants()
        {
            return _repository.Consultants;
        }

        public Consultant GetConsultant(string id)
        {
            return _repository.Consultants
                        .FirstOrDefault(t => t.Id == id);
        }
    }
}