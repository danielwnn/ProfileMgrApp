using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileMgrApp.Controllers;
using ProfileMgrApp.Data;
using ProfileMgrApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ProfileMgrApp.Tests
{
    public class EmployeeProfileControllerTest
    {
        private EmployeeProfileController _controller;
        private EmployeeProfileRepository _repository;

        public EmployeeProfileControllerTest()
        {
            Initialize();
        }

        private void Initialize()
        {
            // Create InMemory Database for testing
            var options = new DbContextOptionsBuilder<EmployeeProfileDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeProfileDb")
                .Options;
            var context = new EmployeeProfileDbContext(options);            

            // Seed data
            SeedEmployeeProfiles.Initialize(context);

            // Create the repository
            _repository = new EmployeeProfileRepository(context);

            // create the controller 
            _controller = new EmployeeProfileController(new HostingEnvironment(), _repository);
        }

        [Fact]
        public void TestIndex()
        {
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<EmployeeProfile>>(viewResult.ViewData.Model);
            Assert.Equal(4, model.Count());
        }

        [Fact]
        public void TestCreateGet()
        {
            var result = _controller.Create();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async void TestCreatePost()
        {
            var profile = new EmployeeProfile
            {
                FirstName = "Daniel",
                LastName = "Wang",
                Title = "CSA",
                Department = "Technical Sales"
            };

            // test create
            var result = await _controller.Create(profile, null);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void TestEditGet()
        {
            // test not found
            var result1 = await _controller.Edit(null);
            Assert.IsType<NotFoundResult>(result1);

            // test found
            var result2 = await _controller.Edit(1);
            var viewResult = Assert.IsType<ViewResult>(result2);
            EmployeeProfile model = Assert.IsAssignableFrom<EmployeeProfile>(viewResult.ViewData.Model);
            Assert.Equal(1, model.ID);
        }

        [Fact]
        public async void TestEditPost()
        {
            var profile = new EmployeeProfile
            {
                ID = 3,
                FirstName = "Daniel",
                LastName = "Weber",
                Title = "CSA",
                Department = "Technical Sales"
            };

            // test ID mismatch
            var result = await _controller.Edit(4, profile, null);
            Assert.IsType<NotFoundResult>(result);

            // test the change of last name
            result = await _controller.Edit(3, profile, null);
            Assert.IsType<RedirectToActionResult>(result);

            // check if the last names match
            EmployeeProfile profile2 = await _repository.ReadAsync(3);
            Assert.Equal("Weber", profile2.LastName);
        }

        [Fact]
        public async void TestDeleteGet()
        {
            // test not found
            var result = await _controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);

            // test found
            var result2 = await _controller.Delete(2);
            var viewResult = Assert.IsType<ViewResult>(result2);
            EmployeeProfile model = Assert.IsAssignableFrom<EmployeeProfile>(viewResult.ViewData.Model);
            Assert.Equal(2, model.ID);
        }

        [Fact]        
        public async void TestDeletePost()
        {
            // test delete
            var result = await _controller.DeleteConfirmed(4);
            Assert.IsType<RedirectToActionResult>(result);

            // check if the profile is really deleted
            EmployeeProfile profile = await _repository.ReadAsync(4);
            Assert.Null(profile);
        }
    }
}
