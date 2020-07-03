using CustomerSchema.Model;
using HotChocolate.Types;

namespace CustomerSchema.Types
{
    public class ConsultantType: ObjectType<Consultant>
    {
        protected override void Configure(IObjectTypeDescriptor<Consultant> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();

            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
        }
    }
}