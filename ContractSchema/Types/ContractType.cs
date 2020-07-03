using HotChocolate.Types;

namespace ContractSchema.Types
{
    public class ContractType: InterfaceType
    {
        protected override void Configure(IInterfaceTypeDescriptor descriptor)
        {
            descriptor.Name("Contract");
            descriptor.Field("id").Type<NonNullType<IdType>>();
        }
    }
}