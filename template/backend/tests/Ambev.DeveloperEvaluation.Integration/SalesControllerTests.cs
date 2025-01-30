using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration
{
    public class SalesControllerTests : IClassFixture<TestApplicationFactory>
    {
        private readonly HttpClient _client;

        public SalesControllerTests(TestApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateSale_ShouldReturn201Created()
        {
            // Arrange
            var sale = new CreateSaleRequest
            {
                SaleDate = DateTime.UtcNow,
                Branch = "Store 1",
                CustomerId = Guid.NewGuid(),
                Items = new List<CreateSaleItemRequest>
                {
                    new CreateSaleItemRequest
                    {
                        ProductName = "Laptop",
                        Quantity = 5,
                        UnitPrice = 20.00m
                    }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/sales", sale);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetSale_ShouldReturn200AndCorrectData()
        {
            // Arrange
            var sale = new CreateSaleRequest
            {
                SaleDate = DateTime.UtcNow,
                Branch = "Store 2",
                CustomerId = Guid.NewGuid(),
                Items = new List<CreateSaleItemRequest>
                {
                    new CreateSaleItemRequest
                    {
                        ProductName = "Phone",
                        Quantity = 3,
                        UnitPrice = 50.00m
                    }
                }
            };

            // Act
            var postResponse = await _client.PostAsJsonAsync("/api/sales", sale);
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var apiResponse = await postResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
            var saleId = apiResponse.Data.Id;

            saleId.Should().NotBeEmpty("O ID da venda não deve estar vazio");

            var response = await _client.GetAsync($"/api/sales/{saleId}");

            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            using (var jsonDoc = JsonDocument.Parse(content))
            {
                var saleData = jsonDoc.RootElement
                    .GetProperty("data")
                    .GetProperty("data");

                var retrievedSaleId = saleData.GetProperty("id").GetString();
                retrievedSaleId.Should().Be(saleId.ToString());

                saleData.GetProperty("branch").GetString().Should().Be("Store 2");

                saleData.GetProperty("saleItems").GetArrayLength().Should().BeGreaterThan(0);

                var firstSaleItem = saleData.GetProperty("saleItems")[0];
                firstSaleItem.GetProperty("productName").GetString().Should().Be("Phone");
                firstSaleItem.GetProperty("quantity").GetInt32().Should().Be(3);
                firstSaleItem.GetProperty("unitPrice").GetDecimal().Should().Be(50.00m);
            }
        }

        [Fact]
        public async Task UpdateSale_ShouldReturn200AndCorrectData()
        {
            // Arrange
            var sale = new CreateSaleRequest
            {
                SaleDate = DateTime.UtcNow,
                Branch = "Store 2",
                CustomerId = Guid.NewGuid(),
                Items = new List<CreateSaleItemRequest>
                {
                    new CreateSaleItemRequest
                    {
                        ProductName = "Phone",
                        Quantity = 3,
                        UnitPrice = 50.00m
                    }
                }
            };

            // Act
            var postResponse = await _client.PostAsJsonAsync("/api/sales", sale);
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var apiResponse = await postResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
            var saleId = apiResponse.Data.Id;

            saleId.Should().NotBeEmpty("O ID da venda não deve estar vazio");

            var updateSaleRequest = new UpdateSaleRequest
            {
                Id = saleId,
                IsCancelled = false,
                Items = new List<UpdateSaleItemRequest>
                {
                    new UpdateSaleItemRequest
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "Smartphone",
                        Quantity = 5,
                        UnitPrice = 60.00m,
                        IsCancelled = false
                    }
                }
            };

            var updateResponse = await _client.PutAsJsonAsync($"/api/sales/{saleId}", updateSaleRequest);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var updateApiResponse = await updateResponse.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateSaleResponse>>();
            updateApiResponse.Should().NotBeNull();
            updateApiResponse.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteSale_ShouldReturn200AndSuccessMessage()
        {
            // Arrange
            var sale = new CreateSaleRequest
            {
                SaleDate = DateTime.UtcNow,
                Branch = "Store 5",
                CustomerId = Guid.NewGuid(),
                Items = new List<CreateSaleItemRequest>
                {
                    new CreateSaleItemRequest
                    {
                        ProductName = "Headphones",
                        Quantity = 1,
                        UnitPrice = 200.00m
                    }
                }
            };

            var postResponse = await _client.PostAsJsonAsync("/api/sales", sale);
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var apiResponse = await postResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
            var saleId = apiResponse.Data.Id;

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/sales/{saleId}");

            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var deleteResponseContent = await deleteResponse.Content.ReadFromJsonAsync<ApiResponse>();
            deleteResponseContent.Success.Should().BeTrue();
        }
    }
}
