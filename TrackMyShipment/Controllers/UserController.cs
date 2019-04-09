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
    public class UserController : BaseController
    {
        public UserController(UserManage userManage, CarrierManage carrierManage, AddressManage addressManage,CompanyManage companyManage,SubscriptionManage subscriptionManage)
            : base(userManage, carrierManage, addressManage,companyManage, subscriptionManage)
        {
        }

        [Route("AddUserCarrier")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddUserCarrier([FromBody] UserModel carrier, int carrierId)
        {
            var currentCarrier = await _userManage.PutUserCarrier(carrier);
            bool result = await _subscriptionManage.Subscribe(new Carrier { Id = carrierId }, currentCarrier);
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

        [Route("EditUserCarrier")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditUserCarrier([FromBody] EditUserModel carrier)
        {
            var currentCarrier = await _userManage.EditUserCarrier(carrier);
            return currentCarrier!=null
                ? Json(new Request
                {
                    Msg = "Successfully edited",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Failed to edit",
                    State = RequestState.Failed
                });
        }

        [Route("GetUsersCarrier")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsersCarrier()
        {
            var carrierUsers = await _userManage.GetCarrierUsers();
            return carrierUsers != null
                ? Json(new Request
                {
                    Data = carrierUsers,
                    Msg = "Receive successfully",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Failed to receive",
                    State = RequestState.Failed
                });
        }

        [HttpPost]
        [Route("Subscribe")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> Subscription([FromBody] int carrierId)
        {
            var user = await CurrentUser();
            var carrier = await _carrierManage?.GetByIdCarrier(carrierId);
            var subscriptionStatus = await _subscriptionManage.Subscribe(carrier, user);

            if (subscriptionStatus)
                return Json(new Request
                {
                    State = RequestState.Success,
                    Msg = "Successfully to subscribe"
                });
            return Json(new Request
            {
                State = RequestState.Failed,
                Msg = "Failed to subscribe"
            });
        }


        [HttpGet("Users/{carrierId}")]
        [Authorize(Roles = "admin,carrier")]
        public async Task<IActionResult> ListUsersOfCarrier(int carrierId)
        {
            var myUsers = await _userManage.GetMyUsers(carrierId);
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
    }
}