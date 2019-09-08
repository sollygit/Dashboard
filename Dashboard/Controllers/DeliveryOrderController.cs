using Dashboard.Models;
using Dashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dashboard.Service.Controllers
{
    [Route("api/omdb/100/[controller]")]
    public class DeliveryOrderController : Controller
    {
        private readonly IDeliveryOrderRepository _deliveryOrderRepository;

        public DeliveryOrderController(IDeliveryOrderRepository deliveryOrderRepository)
        {
            _deliveryOrderRepository = deliveryOrderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody] DeliveryOrderRequest request)
        {
            if (request == null)
            {
                return BadRequest(new DeliveryOrderResponse
                {
                    TransactionId = string.Empty,
                    ServiceResult = new ServiceResult
                    {
                        Code = 2,
                        Message = "Request could not be parsed",
                        DeliveryOrderId = null
                    }
                });
            }
            var response = new DeliveryOrderResponse
            {
                TransactionId = request.TransactionId
            };
            try
            {
                await _deliveryOrderRepository.Upsert(request.DeliveryOrder);
            }
            catch (Exception ex)
            {
                response.ServiceResult = new ServiceResult
                {
                    Code = 1,
                    Message = ex.Message,
                    DeliveryOrderId = request.DeliveryOrder.DeliveryOrderId
                };
                return StatusCode(500, response);
            }
            response.ServiceResult = new ServiceResult
            {
                Code = 0,
                Message = "Success",
                DeliveryOrderId = request.DeliveryOrder.DeliveryOrderId
            };
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> CleanUp()
        {
            await _deliveryOrderRepository.CleanUp();
            return Ok();
        }
    }
}