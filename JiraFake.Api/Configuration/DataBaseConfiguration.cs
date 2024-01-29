using JiraFake.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace JiraFake.Api.Configuration
{
    public static class DataBaseConfiguration
    {
        public static void MigrationInitialisation(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider
                                 .GetService<JiraFakeContext>();

                context.Database.Migrate();
            }
        }
    }
}
