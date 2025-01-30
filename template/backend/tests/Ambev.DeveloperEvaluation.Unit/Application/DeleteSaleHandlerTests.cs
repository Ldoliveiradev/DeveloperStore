using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    /// <summary>
    /// Unit tests for <see cref="DeleteSaleHandler"/>.
    /// </summary>
    public class DeleteSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<DeleteSaleHandler> _logger;
        private readonly DeleteSaleHandler _handler;

        public DeleteSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _logger = Substitute.For<ILogger<DeleteSaleHandler>>();

            _handler = new DeleteSaleHandler(
                _saleRepository,
                _logger
            );
        }

        /// <summary>
        /// Tests that a valid sale deletion request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid sale data When deleting sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = DeleteSaleHandlerTestData.GenerateValidCommand();

            _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);

            // When
            var deleteSaleResponse = await _handler.Handle(command, CancellationToken.None);

            // Then
            deleteSaleResponse.Should().NotBeNull();
            deleteSaleResponse.Success.Should().BeTrue();
            await _saleRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid sale deletion request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid sale data When deleting sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = DeleteSaleHandlerTestData.GenerateInvalidCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();

            _logger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString().Contains("Received request to delete Sale with ID")),
                null,
                Arg.Any<Func<object, Exception, string>>()
            );

            _logger.Received(1).Log(
                LogLevel.Warning,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString().Contains("Validation failed for DeleteSaleCommand")),
                null,
                Arg.Any<Func<object, Exception, string>>()
            );
        }

        /// <summary>
        /// Tests that the logger logs key events during sale deletion.
        /// </summary>
        [Fact(DisplayName = "Given valid sale When deleting Then logs events correctly")]
        public async Task Handle_ValidRequest_LogsEvents()
        {
            // Given
            var command = DeleteSaleHandlerTestData.GenerateValidCommand();

            _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);

            // When
            await _handler.Handle(command, CancellationToken.None);

            // Then
            _logger.ReceivedWithAnyArgs().Log(
                Arg.Any<LogLevel>(),
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception, string>>()
            );
        }

        /// <summary>
        /// Tests that the handler throws an exception when the sale is not found.
        /// </summary>
        [Fact(DisplayName = "Given non-existing sale When deleting Then throws key not found exception")]
        public async Task Handle_NonExistingSale_ThrowsKeyNotFoundException()
        {
            // Given
            var command = DeleteSaleHandlerTestData.GenerateValidCommand();

            _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(false);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();

            _logger.Received(1).Log(
                LogLevel.Error,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString().Contains($"Sale with ID {command.Id} not found")),
                null,
                Arg.Any<Func<object, Exception, string>>()
            );
        }
    }
}
