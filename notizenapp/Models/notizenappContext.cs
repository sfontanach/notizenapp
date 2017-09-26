using System;
using Microsoft.EntityFrameworkCore;

namespace notizenapp.Models
{
    public class notizenappContext: DbContext
    {
		public notizenappContext(DbContextOptions<notizenappContext> options)
			: base(options)
		{
		}
    }
}
