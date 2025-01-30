using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Common.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    /// <summary>
    /// Unit tests for <see cref="UpdateSaleHandler"/>.
    /// </summary>
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSaleHandler> _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<UpdateSaleHandler>>();
            _eventDispatcher = Substitute.For<IEventDispatcher>();

            _handler = new UpdateSaleHandler(
                _saleRepository,
                _mapper,
                _logger,
                _eventDispatcher
            );
        }

        /// <summary>
        /// Tests that a valid sale update request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid sale data When updating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();
            var existingSale = new Sale(DateTime.UtcNow, "Branch A", Guid.NewGuid()) { Id = command.Id };
            var updatedSale = new Sale(DateTime.UtcNow, "Branch A", Guid.NewGuid()) { Id = command.Id };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingSale);
            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(updatedSale);
            _mapper.Map<UpdateSaleResult>(updatedSale).Returns(new UpdateSaleResult { Id = updatedSale.Id });

            // When
            var updateSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            updateSaleResult.Should().NotBeNull();
            updateSaleResult.Id.Should().Be(updatedSale.Id);
            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid sale update request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid sale data When updating sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = UpdateSaleHandlerTestData.GenerateInvalidCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();

            _logger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString().Contains("Processing UpdateSaleCommand for SaleId")),
                null,
                Arg.Any<Func<object, Exception, string>>()
            );

            _logger.Received(1).Log(
                LogLevel.Warning,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString().Contains("Validation failed for UpdateSaleCommand")),
                null,
                Arg.Any<Func<object, Exception, string>>()
            );
        }

        /// <summary>
        /// Tests that the logger logs key events during sale update.
        /// </summary>
        [Fact(DisplayName = "Given valid sale When updating Then logs events correctly")]
        public async Task Handle_ValidRequest_LogsEvents()
        {
            // Given
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();
            var existingSale = new Sale(DateTime.UtcNow, "Branch A", Guid.NewGuid()) { Id = command.Id };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingSale);
            _mapper.Map<UpdateSaleResult>(existingSale).Returns(new UpdateSaleResult { Id = existingSale.Id });

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
        [Fact(DisplayName = "Given non-existing sale When updating Then throws invalid operation exception")]
        public async Task Handle_NonExistingSale_ThrowsInvalidOperationException()
        {
            // Given
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Sale)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<InvalidOperationException>();

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
