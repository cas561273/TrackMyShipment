﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyShipment.Manage;
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


        [HttpGet("user/{id}")]
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