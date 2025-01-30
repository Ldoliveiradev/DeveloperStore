using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Application.SaleItems.DeleteSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.DeleteSaleItem;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleItemsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SaleItemsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        //[HttpPost]
        //[ProducesResponseType(typeof(ApiResponseWithData<CreateSaleItemResponse>), StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> CreateSale([FromBody] CreateSaleItemRequest request, CancellationToken cancellationToken)
        //{
        //    var validator = new CreateSaleItemRequestValidator();
        //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors);

        //    var command = _mapper.Map<CreateSaleItemCommand>(request);
        //    var response = await _mediator.Send(command, cancellationToken);

        //    return Created(string.Empty, new ApiResponseWithData<CreateSaleItemResponse>
        //    {
        //        Success = true,
        //        Message = "Sale item created successfully",
        //        Data = _mapper.Map<CreateSaleItemResponse>(response)
        //    });
        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
        //{
        //    var request = new DeleteSaleItemRequest { Id = id };
        //    var validator = new DeleteSaleItemRequestValidator();
        //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors);

        //    var command = _mapper.Map<DeleteSaleItemCommand>(request.Id);
        //    await _mediator.Send(command, cancellationToken);

        //    return Ok(new ApiResponse
        //    {
        //        Success = true,
        //        Message = "Sale item deleted successfully"
        //    });
        //}
    }
}
