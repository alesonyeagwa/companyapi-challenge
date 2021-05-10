using CompanyAPI.Controllers;
using CompanyAPI.Models.Requests;
using CompanyAPI.Models.Schemas;
using CompanyAPI.Services;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static CompanyAPI.Models.Requests.RegisterRequest;

namespace CompanyAPI.Test
{
    public class RegisterControllerTests
    {
        private readonly Mock<IUserService> repositoryMock;
        private readonly RegisterController controller;

        public RegisterControllerTests()
        {
            repositoryMock = new Mock<IUserService>();
            controller = new RegisterController(repositoryMock.Object);
        }

        private RegisterRequest FakeRegisterRequestObject()
        {
            return new RegisterRequest()
            {
                Username = "Harry",
                Password = "Password123"
            };
        }

        private User FakerUserObject()
        {
            return new User()
            {
                Id = 1,
                UserName = "James",
                Password = "Password123"
            };
        }

        [Fact]
        public async Task Should_Return_200_When_User_Registers()
        {
            // Arrange
            var user = FakerUserObject();
            var registerRequest = new RegisterRequest()
            {
                Username = user.UserName,
                Password = user.Password
            };
            repositoryMock.Setup(r => r.AddUser(registerRequest)).Returns(Task.FromResult(user));

            // Act
            var result = await controller.register(registerRequest);

            //Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task Should_Return_422_When_User_Registers_Existing_Username()
        {
            // Arrange
            var user = FakerUserObject();
            var registerRequest = new RegisterRequest()
            {
                Username = user.UserName,
                Password = user.Password
            };
            repositoryMock.Setup(r => r.UsernameExists(It.IsAny<string>())).Returns(true);
            repositoryMock.Setup(r => r.AddUser(registerRequest)).Returns(Task.FromResult(user));

            // Act
            var result = await controller.register(registerRequest);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result.Result);
        }

        [Fact]
        public async Task Should_Return_422_When_User_Registers_With_Missing_Attribute()
        {
            // Arrange
            var user = FakerUserObject();
            var registerRequest = new RegisterRequest()
            {
                Username = user.UserName
            };
            RegisterRequestValidator validator = new RegisterRequestValidator();

            // Act
            var result = validator.TestValidate(registerRequest);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Password);
            result.ShouldNotHaveValidationErrorFor(r => r.Username);
        }
    }
}
