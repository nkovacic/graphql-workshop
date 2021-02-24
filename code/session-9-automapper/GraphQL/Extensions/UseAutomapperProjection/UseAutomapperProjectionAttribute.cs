using System;
using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace ConferencePlanner.GraphQL.Extensions
{
    public class UseAutomapperProjectionAttribute : ObjectFieldDescriptorAttribute
    {
        private readonly Type _projectToObjectType;

        public UseAutomapperProjectionAttribute(Type projectToObjectType)
        {
            _projectToObjectType = projectToObjectType;
        }

        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseAutoMapperProjection(_projectToObjectType);
        }
    }
}
