using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferencePlanner.GraphQL.Extensions
{
    public class QueryableAutomapperProjectionMiddleware<TSource, TProjection>
    {
        private readonly FieldDelegate _next;

        public QueryableAutomapperProjectionMiddleware(
            FieldDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            await _next(context);

            IQueryable<TSource>? source = null;

            if (context.Result is IQueryable<TSource> q)
            {
                source = q;
            }
            else if (context.Result is IEnumerable<TSource> e)
            {
                source = e.AsQueryable();
            }

            if (source is not null)
            {
                var mapper = context.Service<IMapper>();
                
                context.Result = source.ProjectTo<TProjection>(mapper.ConfigurationProvider);
            }
        }
    }
}
