using CompanyAPI.Controllers;
using CompanyAPI.Models.Repositories;
using CompanyAPI.Models.Requests;
using CompanyAPI.Models.Schemas;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static CompanyAPI.Models.Requests.CreateCompanyRequest;
using static CompanyAPI.Models.Requests.UpdateCompanyRequest;

namespace CompanyAPI.Test
{
    public class CompaniesControllerTests
    {
        private readonly Mock<ICompanyService> repositoryMock;
        private readonly CompaniesController controller;

        public CompaniesControllerTests()
        {
            repositoryMock = new Mock<ICompanyService>();
            controller = new CompaniesController(repositoryMock.Object);
        }

        private IEnumerable<Company> GetFakeCompaniesLists()
        {
            return new List<Company>
            {
                new Company()
                {
                    Id = 1,
                    Name = "Apple inc",
                    Ticker = "NASDAQ",
                    Exchange = "AAPL",
                    ISIN = "US0378331005",
                    Website = "http://www.apple.com"
                },
                new Company()
                {
                    Id = 2,
                    Name = "British Airways Plc",
                    Ticker = "Pink Sheets",
                    Exchange = "BAIRY",
                    ISIN = "US1104193065"
                },
            };
        }

        private CreateCompanyRequest FakeCreateRequestObject()
        {
            return new CreateCompanyRequest()
            {
                Name = "Heineken NV",
                Ticker = "Euronext Amsterdam",
                Exchange = "HEIA",
                ISIN = "NL0000009165"
            };
        }

        private UpdateCompanyRequest FakeUpdateRequestObject()
        {
            return new UpdateCompanyRequest()
            {
                Name = "Heineken NV",
                Ticker = "Euronext Amsterdam",
                Exchange = "HEIA",
                ISIN = "NL0000009165"
            };
        }

        private CreateCompanyRequest FakeCreateRequestObjectWithInvalidISIN()
        {
            return new CreateCompanyRequest()
            {
                Name = "Heineken NV",
                Ticker = "Euronext Amsterdam",
                Exchange = "HEIA",
                ISIN = "N30000009165"
            };
        }
        
        private CreateCompanyRequest FakeCreateRequestObjectWithMissingAttribute()
        {
            return new CreateCompanyRequest()
            {
                Name = "Heineken NV",
                Ticker = "Euronext Amsterdam",
                ISIN = "NL0000009165"
            };
        }

        private UpdateCompanyRequest FakeUpdateRequestObjectWithInvalidISIN()
        {
            return new UpdateCompanyRequest()
            {
                Name = "Heineken NV",
                Ticker = "Euronext Amsterdam",
                Exchange = "HEIA",
                ISIN = "N30000009165"
            };
        }

        [Fact]
        public async Task Should_Return_200_When_ID_Is_Found()
        {
            // Arrange
            long id = 1;
            repositoryMock.Setup(r => r.GetCompany(id)).ReturnsAsync(GetFakeCompaniesLists().Single(c => c.Id.Equals(id)));

            // Act
            var result = await controller.GetCompany(id);

            //Assert
            Assert.Equal(id, result.Value.Id);
        }

        [Fact]
        public async Task Should_Return_404_When_ID_Not_Found()
        {
            // Arrange
            long id = 10;
            repositoryMock.Setup(r => r.GetCompany(id)).Returns(Task.FromResult<Company>(null));

            // Act
            var result = await controller.GetCompany(id);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Should_Return_200_When_ISIN_Is_Found()
        {
            // Arrange
            Company company = new Company();
            repositoryMock.Setup(r => r.GetCompanyByISIN("UD324354534534")).Returns(Task.FromResult(company));

            // Act
            var result = await controller.GetCompanyByISIN("UD324354534534");

            //Assert
            Assert.Equal(company, result.Value);
        }

        [Fact]
        public async Task Should_Return_404_When_ISIN_Not_Found()
        {
            // Arrange
            repositoryMock.Setup(r => r.GetCompanyByISIN("UD324354534534")).Returns(Task.FromResult<Company>(null));

            // Act
            var result = await controller.GetCompanyByISIN("UD324354534534");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Should_Return_201_When_Creating()
        {
            // Arrange
            var company = GetFakeCompaniesLists().First();
            var companyReq = new CreateCompanyRequest()
            {
                Name = company.Name,
                Ticker = company.Ticker,
                ISIN = company.ISIN,
                Exchange = company.Exchange,
                Website = company.Website
            };
            repositoryMock.Setup(r => r.CompanyISINExists(companyReq.ISIN)).Returns(false);
            repositoryMock.Setup(r => r.AddCompany(companyReq)).Returns(Task.FromResult<Company>(company));

            //Act
            var result = await controller.PostCompany(companyReq); 

            //Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task Should_Return_422_When_Creating_With_Invalid_ISIN()
        {
            /// Arrange

            //Act
            var createCompanyRequestValidator = new CreateCompanyRequestValidator();
            var result = createCompanyRequestValidator.TestValidate(FakeCreateRequestObjectWithInvalidISIN());

            //Assert
            result.ShouldNotHaveValidationErrorFor(company => company.Exchange);
            result.ShouldHaveValidationErrorFor(company => company.ISIN);
        }
        
        [Fact]
        public async Task Should_Return_422_When_Creating_With_Missing_Atrribute()
        {
            /// Arrange

            //Act
            var createCompanyRequestValidator = new CreateCompanyRequestValidator();
            var result = createCompanyRequestValidator.TestValidate(FakeCreateRequestObjectWithMissingAttribute());

            //Assert
            result.ShouldHaveValidationErrorFor(company => company.Exchange);
            result.ShouldNotHaveValidationErrorFor(company => company.ISIN);
        }

        [Fact]
        public async Task Should_Return_422_When_Creating_With_Existing_ISIN()
        {
            // Arrange
            var company = GetFakeCompaniesLists().First();
            var companyReq = new CreateCompanyRequest()
            {
                Name = company.Name,
                Ticker = company.Ticker,
                ISIN = company.ISIN,
                Exchange = company.Exchange,
                Website = company.Website
            };
            repositoryMock.Setup(r => r.CompanyISINExists(companyReq.ISIN)).Returns(true);
            repositoryMock.Setup(r => r.AddCompany(companyReq)).Returns(Task.FromResult<Company>(company));

            //Act
            var result = await controller.PostCompany(companyReq);

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result.Result);
        }

        [Fact]
        public async Task Should_Return_200_When_Updating()
        {
            long id = 1;
            repositoryMock.Setup(r => r.GetCompany(id)).Returns(Task.FromResult(GetFakeCompaniesLists().First()));
            repositoryMock.Setup(r => r.UpdateCompany(id, It.IsAny<UpdateCompanyRequest>()))
                 .Returns(Task.FromResult(GetFakeCompaniesLists().First()));

            var result = await controller.PutCompany(id, FakeUpdateRequestObject());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Should_Return_404_When_ID_Not_Found_During_Update()
        {
            // Arrange
            long id = 10;
            repositoryMock.Setup(r => r.GetCompany(id)).Returns(Task.FromResult<Company>(null));

            // Act
            var result = await controller.PutCompany(id, FakeUpdateRequestObject());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Should_Return_422_When_Updating_With_Existing_ISIN()
        {
            // Arrange
            long id = 1;
            repositoryMock.Setup(r => r.GetCompany(id)).Returns(Task.FromResult(GetFakeCompaniesLists().Last()));
            repositoryMock.Setup(r => r.GetCompanyByISIN(It.IsAny<string>())).Returns(Task.FromResult(GetFakeCompaniesLists().First()));
            repositoryMock.Setup(r => r.UpdateCompany(id, It.IsAny<UpdateCompanyRequest>()))
                 .Returns(Task.FromResult(GetFakeCompaniesLists().First()));

            //Act
            var result = await controller.PutCompany(id, FakeUpdateRequestObject());

            //Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        public async Task Should_Return_422_When_Updating_With_Invalid_ISIN()
        {
            /// Arrange

            //Act
            var updateCompanyRequestValidator = new UpdateCompanyRequestValidator();
            var result = updateCompanyRequestValidator.TestValidate(FakeUpdateRequestObjectWithInvalidISIN());

            //Assert
            result.ShouldNotHaveValidationErrorFor(company => company.Exchange);
            result.ShouldHaveValidationErrorFor(company => company.ISIN);
        }

        [Fact]
        public async Task Should_Return_204_When_Deleting()
        {
            long id = 1;
            repositoryMock.Setup(r => r.CompanyExists(id)).Returns(true);
            repositoryMock.Setup(r => r.DeleteCompany(id))
                 .Returns(Task.FromResult(true));

            var result = await controller.DeleteCompany(id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Should_Return_404_When_ID_Not_Found_During_Delete()
        {
            // Arrange
            long id = 10;
            repositoryMock.Setup(r => r.CompanyExists(id)).Returns(false);
            repositoryMock.Setup(r => r.DeleteCompany(id))
                 .Returns(Task.FromResult(true));

            // Act
            var result = await controller.DeleteCompany(id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
