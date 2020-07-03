using CustomerSchema.Model;
using CustomerSchema.Resolvers;
using HotChocolate.Types;

namespace CustomerSchema.Types
{
    public class CustomerType: ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.ConsultantId)
                .Name("consultant")
                .Resolver(
                    resolve => resolve.Service<CustomerResolver>().GetConsultant(resolve.Parent<Customer>().ConsultantId)
                ).Type<ConsultantType>();
        }
    }
}