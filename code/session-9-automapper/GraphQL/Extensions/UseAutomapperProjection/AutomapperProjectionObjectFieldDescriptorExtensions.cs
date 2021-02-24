using HotChocolate.Internal;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using System;

namespace ConferencePlanner.GraphQL.Extensions
{
    public static class AutomapperProjectionObjectFieldDescriptorExtensions
    {
        private static readonly Type _middlewareDefinition = typeof(QueryableAutomapperProjectionMiddleware<,>);

        public static IObjectFieldDescriptor UseAutoMapperProjection<T>(
            this IObjectFieldDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            return UseAutoMapperProjection(descriptor, typeof(T));
        }

        public static IObjectFieldDescriptor UseAutoMapperProjection(
            this IObjectFieldDescriptor descriptor,
            Type objectType)
        {
            FieldMiddleware placeholder = next => context => default;

            descriptor
                .Use(placeholder)
                .Extend()
                .OnBeforeCreate(
                    (context, definition) =>
                    {
                        if (definition.ResultType is null ||
                            !context.TypeInspector.TryCreateTypeInfo(
                                definition.ResultType,
                                out ITypeInfo? typeInfo))
                        {
                            Type resultType = definition.ResolverType ?? typeof(object);
                            throw new ArgumentException(
                                $"Cannot handle the specified type `{resultType.FullName}`.",
                                nameof(descriptor));
                        }

                        Type selectionType = typeInfo.NamedType;
                        definition.ResultType = objectType;
                        definition.Type = context.TypeInspector.GetTypeRef(objectType);

                        ILazyTypeConfiguration lazyConfiguration =
                            LazyTypeConfigurationBuilder
                                .New<ObjectFieldDefinition>()
                                .Definition(definition)
                                .Configure(
                                    (_, __) =>
                                    {
                                        CompileMiddleware(
                                            selectionType,
                                            objectType,
                                            definition,
                                            placeholder,
                                            _middlewareDefinition);
                                    })
                                .On(ApplyConfigurationOn.Completion)
                                .Build();

                        definition.Configurations.Add(lazyConfiguration);
                    });

            return descriptor;
        }

        private static void CompileMiddleware(
            Type type,
            Type projectionType,
            ObjectFieldDefinition definition,
            FieldMiddleware placeholder,
            Type middlewareDefinition)
        {
            Type middlewareType = middlewareDefinition.MakeGenericType(type, projectionType);
            FieldMiddleware middleware = FieldClassMiddlewareFactory.Create(middlewareType);

            var index = definition.MiddlewareComponents.IndexOf(placeholder);

            definition.MiddlewareComponents[index] = middleware;
        }
    }
}
