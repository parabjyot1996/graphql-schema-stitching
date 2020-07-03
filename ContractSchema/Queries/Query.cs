using System.Collections.Generic;
using System.Linq;
using ContractSchema.Models;
using ContractSchema.Repository;
using HotChocolate.Types.Relay;

namespace ContractSchema.Queries
{
    public class Query
    {
        private readonly IdSerializer _idSerializer = new IdSerializer();
        private readonly ContractRepository _contractRepo;

        public Query(ContractRepository contractRepo)
        {
            _contractRepo = contractRepo;
        }

        public IContract GetContract(string contractId)
        {
            // IdValue value = _idSerializer.Deserialize(contractId);

            // if (value == nameof(LifeInsuranceContract))
            // {
            //     return _contractRepo.Contracts
            //         .OfType<LifeInsuranceContract>()
            //         .FirstOrDefault(t => t.Id.Equals(value.Value));
            // }
            // else
            // {
            //     return _contractRepo.Contracts
            //         .OfType<SomeOtherContract>()
            //         .FirstOrDefault(t => t.Id.Equals(value.Value));
            // }

            return _contractRepo.Contracts
                    .OfType<IContract>()
                    .FirstOrDefault(t => t.Id == contractId);
        }

        public IEnumerable<IContract> GetContracts(string customerId)
        {
            // IdValue value = _idSerializer.Deserialize(customerId);

            return _contractRepo.Contracts
                .Where(t => t.CustomerId == customerId);
        }
    }
}