using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Unit tests for <see cref="GetSaleHandler"/>.
    /// </summary>
    public class GetSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSaleHandler> _logger;
        private readonly GetSaleHandler _handler;

        public GetSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<GetSaleHandler>>();

            _handler = new GetSaleHandler(
                _saleRepository,
                _mapper,
                _logger
            );
        }

        /// <summary>
        /// Tests that a valid sale retrieval request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid sale data When retrieving sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = GetSaleHandlerTestData.GenerateValidCommand();
            var sale = new Sale(DateTime.UtcNow, "Branch A", Guid.NewGuid()) { Id = command.Id };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
            _mapper.Map<GetSaleResult>(sale).Returns(new GetSaleResult { Id = sale.Id });

            // When
            var getSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            getSaleResult.Should().NotBeNull();
            getSaleResult.Id.Should().Be(sale.Id);
            await _saleRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid sale retrieval request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid sale data When retrieving sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = GetSaleHandlerTestData.GenerateInvalidCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();

            _logger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString()!.Contains("Handling GetSaleCommand for SaleId")),
                null,
                Arg.Any<Func<object, Exception?, string>>()
            );

            _logger.Received(1).Log(
                LogLevel.Warning,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString()!.Contains("Validation failed")),
                null,
                Arg.Any<Func<object, Exception?, string>>()
            );
        }

        /// <summary>
        /// Tests that the logger logs key events during sale retrieval.
        /// </summary>
        [Fact(DisplayName = "Given valid sale When retrieving Then logs events correctly")]
        public async Task Handle_ValidRequest_LogsEvents()
        {
            // Given
            var command = GetSaleHandlerTestData.GenerateValidCommand();
            var sale = new Sale(DateTime.UtcNow, "Branch A", Guid.NewGuid()) { Id = command.Id };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
            _mapper.Map<GetSaleResult>(sale).Returns(new GetSaleResult { Id = sale.Id });

            // When
            await _handler.Handle(command, CancellationToken.None);

            // Then
            _logger.ReceivedWithAnyArgs().Log(
                Arg.Any<LogLevel>(),
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception?, string>>()
            );
        }

        /// <summary>
        /// Tests that the handler throws an exception when the sale is not found.
        /// </summary>
        [Fact(DisplayName = "Given non-existing sale When retrieving Then throws key not found exception")]
        public async Task Handle_NonExistingSale_ThrowsKeyNotFoundException()
        {
            // Given
            var command = GetSaleHandlerTestData.GenerateValidCommand();

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Sale?)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();

            _logger.Received(1).Log(
                LogLevel.Error,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString()!.Contains($"Sale with ID {command.Id} not found")),
                null,
                Arg.Any<Func<object, Exception?, string>>()
            );
        }
    }
}
