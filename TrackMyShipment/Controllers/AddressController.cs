using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyShipment.Manage;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Controllers
{
    [Route("api")]
    [ApiController]
    public class AddressController : BaseController
    {
        public AddressController(ObjectiveManage objectiveManage,UserManage userManage, CarrierManage carrierManage, AddressManage addressManage, CompanyManage companyManage, SubscriptionManage subscriptionManage)
            : base(objectiveManage,userManage, carrierManage, addressManage, companyManage, subscriptionManage)
        {
        }

        [HttpPost]
        [Route("PutAddress")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> PutAddress([FromBody] AddressModel address)
        {
            User user = await CurrentUser();
            int userId = user.Id;
            bool? result = await _addressManage.PutOrUpdateAddress(address, userId);
            if (result == true)
            return Json(new Request
                {
                    State = RequestState.Success,
                    Msg = "Put address successfully"
                });
            return Json(new Request
            {
                State = RequestState.Failed,
                Msg = "Address can not be added"
            });
        }

        [HttpDelete]
        [Route("DeleteAddress")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> DeleteAddress([FromBody] int? id)
        {
            User user = await CurrentUser();
            int userId = user.Id;
            bool? result = await _addressManage.DeleteAddress(id, userId);
            if (result == true)
                return Json(new Request
                {
                    State = RequestState.Success,
                    Msg = "Deleted successfully"
                });

            return Json(new Request
            {
                State = RequestState.Failed,
                Msg = "Cannot be deleted"
            });
        }

        [HttpGet]
        [Route("MyAddress")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> MyAddress()
        {
            User user = await CurrentUser();
            int userId = user.Id;
            IEnumerable<AddressModel> myAddress = await _addressManage.MyAddress(userId);

            return myAddress != null
                ? Json(new Request
                {
                    Msg = "Received successful",
                    Data = myAddress,
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Not Found",
                    State = RequestState.Failed
                });
        }

        [HttpGet]
        [Route("MyActiveAddress")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> MyActiveAddress()
        {
            User user = await CurrentUser();
            AddressModel myAddress = await _addressManage.MyActiveAddress(user);
            
            return myAddress != null
                ? Json(new Request
                {
                    Msg = "Received successful",
                    Data = myAddress,
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Not Found",
                    State = RequestState.Failed
                });
        }

        [HttpPost]
        [Route("ChangeStatusAddress")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> ChangeStatusAddress([FromBody] int? id)
        {
            User user = await CurrentUser();
            int userId = user.Id;
            bool? result = await _addressManage.ChangeStatusAddress(id, userId);
            return result == true
                ? Json(new Request
                {
                    Msg = "Successfully changed",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Cannot be changed",
                    State = RequestState.Failed
                });
        }
    }
}