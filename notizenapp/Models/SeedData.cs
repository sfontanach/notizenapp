using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace notizenapp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new notizenappContext(
                serviceProvider.GetRequiredService<DbContextOptions<notizenappContext>>()))
            {
                
                // Look for any notes
                if (context.Note.Any())
                {
                    return;   // DB has been seeded
                }

                context.Note.AddRange(
                     new Note
                     {  
                        Title = "Important",
                        Text = "This is an important description",
                        Importance = 5,
                        FinishDate = DateTime.Parse("2017-10-6"),
                        Finished = true
                     },
					new Note
					{
						Title = "Not Important",
						Text = "This is a description",
						Importance = 1,
						FinishDate = DateTime.Parse("2017-11-9")
					}
                    
                );
                context.SaveChanges();
            }
        }
    }
}