using ContractSchema.Models;
using HotChocolate.Types;

namespace ContractSchema.Types
{
    public class LifeInsuranceContractType: ObjectType<LifeInsuranceContract>
    {
        protected override void Configure(
            IObjectTypeDescriptor<LifeInsuranceContract> descriptor)
        {
            descriptor.Interface<ContractType>();
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
            descriptor.Field(t => t.CustomerId).Ignore();
        }
    }
}