using GlobalSolution.Controllers;
using GlobalSolution.Models;
using GlobalSolution.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GlobalSolution.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<AuthService> _authServiceMock;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<AuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            var loginRequest = new Login { Email = "test@example.com", Password = "wrongpassword" };
            _authServiceMock.Setup(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var result = await _authController.Login(loginRequest);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Credenciais inválidas.", unauthorizedResult.Value);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            var loginRequest = new Login { Email = "test@example.com", Password = "correctpassword" };
            var user = new User { Id = 1, Nome = "Test User", Email = "test@example.com" };

            _authServiceMock.Setup(service => service.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var result = await _authController.Login(loginRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;
            Assert.Equal("Autenticação realizada com sucesso!", response.Message);
            Assert.Equal(user.Email, response.User.Email);
        }

        [Fact]
        public async Task Register_ShouldReturnConflict_WhenEmailAlreadyExists()
        {
            var registerRequest = new Register { Name = "Test User", Email = "existing@example.com", Password = "password123" };

            _authServiceMock.Setup(service => service.Register(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await _authController.Register(registerRequest);

            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal("E-mail já está em uso.", conflictResult.Value);
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            var registerRequest = new Register { Name = "Test User", Email = "newuser@example.com", Password = "password123" };

            _authServiceMock.Setup(service => service.Register(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await _authController.Register(registerRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Usuário registrado com sucesso.", okResult.Value);
        }
    }
}
