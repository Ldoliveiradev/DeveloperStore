using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
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

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Unit tests for <see cref="CreateSaleHandler"/>.
    /// </summary>
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<CreateSaleHandler>>();
            _eventDispatcher = Substitute.For<IEventDispatcher>();

            _handler = new CreateSaleHandler(
                _saleRepository,
                _mapper,
                _logger,
                _eventDispatcher
            );
        }

        /// <summary>
        /// Tests that a valid sale creation request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            var mappedSale = new Sale(command.SaleDate, command.Branch, command.CustomerId);
            _mapper.Map<Sale>(command).Returns(mappedSale);
            var createdSale = new Sale(command.SaleDate, command.Branch, command.CustomerId) { Id = Guid.NewGuid() };
            var result = new CreateSaleResult { Id = createdSale.Id };

            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(createdSale);
            _mapper.Map<CreateSaleResult>(createdSale).Returns(result);

            // When
            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createSaleResult.Should().NotBeNull();
            createSaleResult.Id.Should().Be(createdSale.Id);
            await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
            _eventDispatcher.Received(1).Publish(Arg.Any<SaleCreatedEvent>());
        }

        /// <summary>
        /// Tests that an invalid sale creation request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateInvalidCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();

            _logger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString()!.Contains("Processing CreateSaleCommand for CustomerId")),
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
        /// Tests that the logger logs key events during sale creation.
        /// </summary>
        [Fact(DisplayName = "Given valid sale When handling Then logs events correctly")]
        public async Task Handle_ValidRequest_LogsEvents()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            var mappedSale = new Sale(command.SaleDate, command.Branch, command.CustomerId);

            _mapper.Map<Sale>(command).Returns(mappedSale);
            var createdSale = new Sale(command.SaleDate, command.Branch, command.CustomerId) { Id = Guid.NewGuid() };

            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(createdSale);
            _mapper.Map<CreateSaleResult>(createdSale).Returns(new CreateSaleResult { Id = createdSale.Id });

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
        /// Tests that the mapper is called with the correct command.
        /// </summary>
        [Fact(DisplayName = "Given valid command When handling Then maps command to sale entity")]
        public async Task Handle_ValidRequest_MapsCommandToSale()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            var sale = new Sale(command.SaleDate, command.Branch, command.CustomerId);

            _mapper.Map<Sale>(Arg.Any<CreateSaleCommand>()).Returns(sale);
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

            // When
            await _handler.Handle(command, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
                c.SaleDate == command.SaleDate &&
                c.Branch == command.Branch &&
                c.CustomerId == command.CustomerId &&
                c.Items.Count == command.Items.Count
            ));
        }
    }
}
