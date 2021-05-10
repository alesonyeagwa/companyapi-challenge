using CompanyAPI.Controllers;
using CompanyAPI.Models.Repositories;
using CompanyAPI.Models.Requests;
using CompanyAPI.Models.Responses;
using CompanyAPI.Models.Schemas;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static CompanyAPI.Models.Requests.LoginRequest;

namespace CompanyAPI.Test
{
    public class LoginControllerTests
    {
        private readonly Mock<IAuthenticationService> repositoryMock;
        private readonly LoginController controller;

        public LoginControllerTests()
        {
            repositoryMock = new Mock<IAuthenticationService>();
            controller = new LoginController(repositoryMock.Object);
        }

        private LoginRequest FakeLoginRequestObject()
        {
            return new LoginRequest()
            {
                username = "Harry",
                password = "Password123"
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
        public async Task Should_Return_200_When_User_Logs_In()
        {
            // Arrange
            var user = FakerUserObject();
            var loginRequest = FakeLoginRequestObject();
            var loginResponse = new LoginResponse(user, "token");
            repositoryMock.Setup(r => r.Authenticsate(loginRequest)).Returns(loginResponse);

            // Act
            var result = controller.login(loginRequest);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Should_Return_404_When_UserLogin_Fails()
        {
            // Arrange
            var user = FakerUserObject();
            var loginRequest = FakeLoginRequestObject();
            var loginResponse = new LoginResponse(user, "token");
            repositoryMock.Setup(r => r.Authenticsate(loginRequest)).Returns<LoginResponse>(null);

            // Act
            var result = controller.login(loginRequest);

            //Assert
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task Should_Return_422_When_User_Logs_in_With_Missing_Attribute()
        {
            // Arrange
            var user = FakerUserObject();
            var loginRequest = new LoginRequest()
            {
                username = user.UserName
            };
            LoginRequestValidator validator = new LoginRequestValidator();

            // Act
            var result = validator.TestValidate(loginRequest);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.password);
            result.ShouldNotHaveValidationErrorFor(r => r.username);
        }
    }
}
