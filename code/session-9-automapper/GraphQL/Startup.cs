using AutoMapper;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Speakers; 
using GrowFlow.Domain.Dto.Mappings;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConferencePlanner.GraphQL
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<ApplicationDbContext>(

                 options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ContosoUniversity1;Trusted_Connection=True;MultipleActiveResultSets=true")
                  )

            ;

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });


            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    //.AddTypeExtension<AttendeeQueries>()
                    .AddTypeExtension<SpeakerQueries>()
                    //.AddTypeExtension<SessionQueries>()
                    //.AddTypeExtension<TrackQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    //.AddTypeExtension<AttendeeMutations>()
                    //.AddTypeExtension<SessionMutations>()
                    .AddTypeExtension<SpeakerMutations>()
                    //.AddTypeExtension<TrackMutations>()
                //.AddSubscriptionType(d => d.Name("Subscription"))
                //    .AddTypeExtension<AttendeeSubscriptions>()
                //    .AddTypeExtension<SessionSubscriptions>()
                //.AddType<AttendeeType>()
                //.AddType<SessionType>()
                //.AddType<SpeakerType>()
                //.AddType<TrackType>()
                //.EnableRelaySupport()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .SetPagingOptions(new PagingOptions()
                {
                    MaxPageSize = 500,
                    DefaultPageSize = 100,
                    IncludeTotalCount = true
                });
            ////.AddInMemorySubscriptions()
            //.AddDataLoader<SpeakerByIdDataLoader>()
            //.AddDataLoader<SessionByIdDataLoader>()
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            //app.UseCors(CorsExtensions.CorsPolicyName);
            //app.UseRequestValidator();
            //app.UseAuthentication();
            //app.UseCsvExporter();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapGraphQL(path: "/graphql");
            });
        }
    }
}
