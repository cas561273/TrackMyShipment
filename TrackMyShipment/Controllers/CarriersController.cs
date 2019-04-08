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
    public class CarriersController : BaseController
    {
        public CarriersController(UserManage userManage, CarrierManage carrierManage, CustomerManage customerManage)
            : base(userManage, carrierManage, customerManage)
        {
        }

        [Route("PutCarrier")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddOrUpdate([FromBody] Carrier carrier)
        {
            await _carrierManage.AddOrUpdate(carrier);
            return Json(new Request
            {
                Msg = "Successfully added",
                State = RequestState.Success
            });
        }

        [Route("DeleteCarrier")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var existCarrier = await _carrierManage.Delete(id);
            return existCarrier
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
            var carriers = await _carrierManage.GetAvailable(await user);
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

        [HttpGet("user/{id}")]
        [Authorize(Roles = "admin,carrier")]
        public async Task<IActionResult> ListUsers(int id)
        {
            var myUsers = await _carrierManage.GetMyUsers(id);
            return myUsers != null
                ? Json(new Request
                {
                    Data = myUsers,
                    Msg = "Successfully received",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Not received",
                    State = RequestState.Success
                });
        }

        [HttpPost("ActiveStatus")]
        [Authorize(Roles = "admin,carrier")]
        public async Task<IActionResult> ActiveCarrier([FromBody] int id)
        {
            var user = await CurrentUser();
            var relation = await _customerManage.GetSubscribe(user.Id, id);
            if (relation != null)
                return await _carrierManage.ActiveStatus(id) == true
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

        [Route("Add-UserCarrier")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddUserCarrier([FromBody] UserModel carrier, int carrierId)
        {
            await _userManage.PutCarrier(carrier);
            var currentCarrier = await _userManage.GetByEmail(carrier.Email);
            await _customerManage.Subscribe(new Carrier {Id = carrierId}, currentCarrier);
            return Json(new Request
            {
                Msg = "Successfully added",
                State = RequestState.Success
            });
        }
    }
}