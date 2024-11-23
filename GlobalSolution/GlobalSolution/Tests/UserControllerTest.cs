using Microsoft.AspNetCore.Mvc;
using Moq;
using GlobalSolution.Controllers;
using GlobalSolution.Models;
using GlobalSolution.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GlobalSolution.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<dbContext> _contextMock;
        private readonly Mock<DbSet<User>> _userSetMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _contextMock = new Mock<dbContext>();
            _userSetMock = new Mock<DbSet<User>>();
            _contextMock.Setup(c => c.Users).Returns(_userSetMock.Object);
            _controller = new UserController(_contextMock.Object);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnOkWithUsers()
        {
            var users = new List<User>
            {
                new User { Id = 1, Nome = "User 1", Email = "user1@example.com" },
                new User { Id = 2, Nome = "User 2", Email = "user2@example.com" }
            };
            _userSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            _userSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            _userSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            _userSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var result = await _controller.GetUsers();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(2, returnedUsers.Count);
        }

        [Fact]
        public async Task GetUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            _userSetMock.Setup(c => c.FindAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var result = await _controller.GetUser(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUser_ShouldReturnOkWithUser_WhenUserExists()
        {
            var user = new User { Id = 1, Nome = "User 1", Email = "user1@example.com" };
            _userSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync(user);

            var result = await _controller.GetUser(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(user.Id, returnedUser.Id);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnCreatedAtAction_WhenUserIsCreated()
        {
            var newUser = new User { Nome = "New User", Email = "newuser@example.com" };
            _userSetMock.Setup(c => c.Add(It.IsAny<User>())).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.CreateUser(newUser);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetUser", createdAtActionResult.ActionName);
            var returnedUser = Assert.IsType<User>(createdAtActionResult.Value);
            Assert.Equal(newUser.Nome, returnedUser.Nome);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnNoContent_WhenUserIsUpdated()
        {
            var userToUpdate = new User { Id = 1, Nome = "Updated User", Email = "updateduser@example.com" };
            _userSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync(userToUpdate);
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.UpdateUser(1, userToUpdate);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            var userToUpdate = new User { Id = 1, Nome = "Updated User", Email = "updateduser@example.com" };

            var result = await _controller.UpdateUser(2, userToUpdate);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNoContent_WhenUserIsDeleted()
        {
            var userToDelete = new User { Id = 1, Nome = "User 1", Email = "user1@example.com" };
            _userSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync(userToDelete);
            _userSetMock.Setup(c => c.Remove(It.IsAny<User>())).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.DeleteUser(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            _userSetMock.Setup(c => c.FindAsync(1)).ReturnsAsync((User)null);

            var result = await _controller.DeleteUser(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}

