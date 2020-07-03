using CustomerSchema.Queries;
using HotChocolate.Types;

namespace CustomerSchema.Types
{
    public class QueryType: ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(t => t.GetCustomers())
                .Type<NonNullType<ListType<NonNullType<CustomerType>>>>();

            descriptor.Field(t => t.GetCustomer(default))
                .Argument("id", a => a.Type<NonNullType<IdType>>())
                .Type<CustomerType>();
        }
    }
}