using System;
using Xunit;
using notizenapp.Controllers;
using notizenapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Moq;
using System.Threading.Tasks;

namespace notizenapp.Test
{
    public class Test404
    {
		private readonly ServiceProvider _serviceProvider;
		private readonly notizenappContext _dbContext;

		public Test404()
		{
			var services = new ServiceCollection();

			services.AddDbContext<notizenappContext>(opt => opt.UseInMemoryDatabase());
			_serviceProvider = services.BuildServiceProvider();
			_dbContext = _serviceProvider.GetService<notizenappContext>();
		}

        [Fact]
        public void NonExistentNote()
        {
            var controller = new HomeController(_dbContext);
			var result = (StatusCodeResult) controller.Edit(4);

			Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }


        [Fact]
        public async void TestCreateAsync()
        {
            int initialCount = (int)await _dbContext.Note.CountAsync();

            NewNoteViewModel testNote = new NewNoteViewModel
            {
                Title = "title",
                Text = "text",
                Importance = 1,
                FinishDate = DateTime.Now,
                Finished = false
            };
            var controller = new HomeController(_dbContext);
            var result = controller.Create(testNote) as IActionResult;

            var okResult = Assert.IsType<RedirectToActionResult>(result);
            var res = await _dbContext.Note.CountAsync();
            Assert.Equal((int) res, initialCount + 1);

        }

    }
}
