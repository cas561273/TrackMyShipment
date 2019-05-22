using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrackMyShipment.Manage;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(ObjectiveManage objectiveManage,UserManage userManage, CarrierManage carrierManage, AddressManage addressManage, CompanyManage companyManage, SubscriptionManage subscriptionManage)
            : base(objectiveManage,userManage, carrierManage, addressManage, companyManage, subscriptionManage)
        {
        }

        [Route("AddUserCarrier/{id}")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddUserCarrier(int id,[FromBody] UserModel carrier)
        {
            User currentCarrier = await _userManage.PutUserCarrier(carrier);
            bool result = await _subscriptionManage.Subscribe(new Carrier { Id = id }, currentCarrier);
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
            User currentCarrier = await _userManage.EditUserCarrier(carrier);
            return currentCarrier != null
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
        [HttpGet]
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

        //carriers  carrier-id
        [Route("usersOfCarrier/{id}")]
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsersCarrierById(int id)
        {
            var carrierUsers = await _userManage.GetCarrierUsersByIdAsync(id);
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


        //customer  carrier-id
        [HttpGet("Users/{id}")]
        [Authorize(Roles = "admin,carrier")]
        public async Task<IActionResult> ListUsersOfCarrier(int id)
        {
            var myUsers = await _userManage.GetMyUsers(id);
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


        [HttpPost]
        [Route("Subscribe")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> Subscription([FromBody] int carrierId)
        {
            User user = await CurrentUser();
            Carrier carrier = await _carrierManage?.GetByIdCarrier(carrierId);
            bool subscriptionStatus = await _subscriptionManage.Subscribe(carrier, user);

            if (subscriptionStatus)
            {
                return Json(new Request
                {
                    State = RequestState.Success,
                    Msg = "Successfully to Subscribe"
                });
            }
            else
            {
                return Json(new Request
                {
                    State = RequestState.Failed,
                    Msg = "Successfully to Unsubscribe"
                });
            }
        }

        [HttpGet]
        [Route("GetWorkUsers")]
        [Authorize(Roles = "carrier,admin")]
        public async Task<IActionResult> GetWorkUsers()
        {
            User user = await CurrentUser();
            int userId = user.Id;
            var result = await _userManage.GetWorkUsers(userId);
            return result!=null
                ? Json(new Request
                {
                    Data = result,
                    Msg = "Received successfully!",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Can not received!",
                    State = RequestState.Failed
                });
        }
    }
}