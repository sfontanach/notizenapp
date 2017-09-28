using System;
using Microsoft.EntityFrameworkCore;

namespace notizenapp.Models
{
    // This represents the connection to the Database
    public class notizenappContext: DbContext
    {
		public notizenappContext(DbContextOptions<notizenappContext> options)
			: base(options)
		{
		}

        public DbSet<notizenapp.Models.Note> Note { get; set; }
    }
}
