using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyShipment.Manage;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierController : BaseController
    {
        public CarrierController(ObjectiveManage objectiveManage,UserManage userManage, CarrierManage carrierManage, AddressManage addressManage,CompanyManage companyManage,SubscriptionManage subscriptionManage)
            : base(objectiveManage,userManage, carrierManage, addressManage,companyManage,subscriptionManage)
        {
        }

        [Route("PutCarrier")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutCarrier([FromBody] Carrier carrier)
        {
            var result = await _carrierManage.AddOrUpdateCarrier(carrier);
            return result
                ? Json(new Request
                {
                    Msg = "Successfully added",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Failed to add",
                    State = RequestState.Failed
                });
        }


        [Route("DeleteCarrier")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCarrier([FromBody] int id)
        {
            var existedCarrier = await _carrierManage.DeleteCarrier(id);
            return existedCarrier
                ? Json(new Request
                {
                    Msg = "Successfully deleted",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Not Found",
                    State = RequestState.Failed
                });
        }

        [Route("GetCarriers")]
        [HttpGet]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> AvailableCarriers()
        {
            var user = CurrentUser();
            var carriers = await _carrierManage.GetAvailableCarriers(await user);
            return carriers != null
                ? Json(new Request
                {
                    Data = carriers,
                    Msg = "Successfully received",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Not received",
                    State = RequestState.Success
                });
        }

        [Route("carrier/{id}")]
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetCarrierById(int id)
        {
            var carrier = await _carrierManage.GetByIdCarrier(id);
            return carrier != null
                ? Json(new Request
                {
                    Data = carrier,
                    Msg = "Receive successfully",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Failed to receive",
                    State = RequestState.Failed
                });
        }

        [Route("MyCarriers")]
        [HttpGet]
        [Authorize(Roles = "admin,customer,carrier")]
        public async Task<IActionResult> MyCarriers()
        {
            var user = CurrentUser();
            var myCarriers = await _carrierManage.GetMyCarriers(await user);
            return myCarriers != null
                ? Json(new Request
                {
                    Data = myCarriers,
                    Msg = "Successfully received",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Not received",
                    State = RequestState.Success
                });
        }
    

    [HttpPost("ChangeStatusCarrier")]
        [Authorize(Roles = "admin,carrier")]
        public async Task<IActionResult> ChangeStatusCarrier([FromBody] int id)
        {
            var user = await CurrentUser();
            var relation = await _subscriptionManage.GetSubscribe(user.Id, id);
            if (relation != null || user.Role.Name.Equals("admin"))
                return await _carrierManage.ChangeStatusCarrier(id) == true
                    ? Json(new Request
                    {
                        Msg = "Status activated",
                        State = RequestState.Success
                    })
                    : Json(new Request
                    {
                        Msg = "Status unactivated",
                        State = RequestState.Success
                    });

            return Json(new Request
            {
                Msg = "Cannot be activated",
                State = RequestState.Failed
            });
        }
    }
}