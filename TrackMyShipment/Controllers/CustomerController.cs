using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyShipment.Manage;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        public CustomerController(UserManage userManage, CarrierManage carrierManage, CustomerManage customerManage)
            : base(userManage, carrierManage, customerManage)
        {
        }

        [HttpPost]
        [Route("PutAddress")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> PutAddress([FromBody] AddressModel address)
        {
            var user = await CurrentUser();
            var userId = user.Id;
            var result = await _customerManage.PutOrUpdate(address, userId);

            return result != null ? Json(new Request
                {
                    State = RequestState.Success,
                    Msg = "Put address successfully"
                })  : Json(new Request
                {
                    State = RequestState.Failed,
                    Msg = "Address can not be added"
                });
        }

        [HttpDelete]
        [Route("DeleteAddress")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> Delete([FromBody] int? id)
        {
            var user = await CurrentUser();
            var userId = user.Id;
            var address =  await _customerManage.DeleteAddress(id, userId);
            return address != null ? Json(new Request
                {
                    State = RequestState.Success,
                    Msg = "Deleted successfully"
                }) : Json(new Request
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
            var user = await CurrentUser();
            var userId = user.Id;
            var myAddress = await _customerManage.MyAddress(userId);

            return myAddress != null  ? Json(new Request
                {
                    Msg = "Received successful",
                    Data = myAddress,
                    State = RequestState.Success
                }) : Json(new Request
                {
                    Msg = "Not Found",
                    State = RequestState.Failed
                });
        }

        [HttpPost]
        [Route("StatusAddress")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> StatusAddress([FromBody] int? id)
        {
            var user = await CurrentUser();
            var userId = user.Id;
            var result =  await _customerManage.StatusAddress(id, userId);
            return result != null ? Json(new Request
                {
                    Msg = "Successfully changed",
                    State = RequestState.Success
                }) : Json(new Request
                {
                    Msg = "Cannot be changed",
                    State = RequestState.Failed
                });
        }

        [HttpPost]
        [Route("Subscribe")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> Subscription([FromBody] int carrierId)
        {
            var result = "exception";
            var user = await CurrentUser();
            if (_carrierManage != null)
            {
                var carrier = await _carrierManage?.GetById(carrierId);

                result = await _customerManage.Subscribe(carrier, user);
                return carrier != null ? Json(new Request
                    {
                        State = RequestState.Success,
                        Msg = result
                    }) : Json(new Request
                    {
                        State = RequestState.Failed,
                        Msg = result
                    });
            }
            return Json(new Request
            {
                State = RequestState.Failed,
                Msg = result
            });
        }
    }
}