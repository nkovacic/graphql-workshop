using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConferencePlanner.GraphQL;
using ConferencePlanner.GraphQL.Attendees;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using ConferencePlanner.GraphQL.Sessions;
using ConferencePlanner.GraphQL.Speakers;
using ConferencePlanner.GraphQL.Tracks;
using ConferencePlanner.GraphQL.Types;
using GrowFlow.Domain.Dto.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
                 //options => options.UseSqlite("Data Source=conferences.db"))
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
                    .AddTypeExtension<AttendeeQueries>()
                    .AddTypeExtension<SpeakerQueries>()
                    .AddTypeExtension<SessionQueries>()
                    .AddTypeExtension<TrackQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<AttendeeMutations>()
                    .AddTypeExtension<SessionMutations>()
                    .AddTypeExtension<SpeakerMutations>()
                    .AddTypeExtension<TrackMutations>()
                //.AddSubscriptionType(d => d.Name("Subscription"))
                //    .AddTypeExtension<AttendeeSubscriptions>()
                //    .AddTypeExtension<SessionSubscriptions>()
                .AddType<AttendeeType>()
                .AddType<SessionType>()
                .AddType<SpeakerType>()
                .AddType<TrackType>()
                //.EnableRelaySupport()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
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
