using System.Linq;
using CustomerSchema.Model;
using CustomerSchema.Repository;
using HotChocolate;

namespace CustomerSchema.Resolvers
{
    public class CustomerResolver
    {
        private readonly CustomerRepository _repository;

        public CustomerResolver(CustomerRepository repository)
        {
            _repository = repository;
        }

        public Consultant GetConsultant(string consultantId)
        {
            if (consultantId != null)
            {
                return _repository.Consultants.FirstOrDefault(
                            t => t.Id == consultantId);
            }
            return null;
        }
    }
}