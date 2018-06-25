using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfileMgrApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMgrApp.Models
{
    public class SeedEmployeeProfiles
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EmployeeProfileDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<EmployeeProfileDbContext>>()))
            {
                // Look for any movies.
                if (context.EmployeeProfiles.Any())
                {
                    return;   // DB has been seeded
                }

                context.EmployeeProfiles.AddRange(
                     new EmployeeProfile
                     {
                         FirstName = "Armen",
                         LastName = "Romo",
                         Title = "Software Engineer",
                         Department = "Engineering"
                     },

                     new EmployeeProfile
                     {
                         FirstName = "Corinne",
                         LastName = "Horn",
                         Title = "Business Analyst",
                         Department = "Human Resources"
                     },

                     new EmployeeProfile
                     {
                         FirstName = "Dan",
                         LastName = "Drayton",
                         Title = "System Administrator",
                         Department = "IT"
                     },

                   new EmployeeProfile
                   {
                       FirstName = "Joann",
                       LastName = "Chambers",
                       Title = "Senior Accoutant",
                       Department = "Finance"
                   }
                );
                context.SaveChanges();
            }
        }
    }
}
