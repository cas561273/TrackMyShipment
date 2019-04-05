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
        public ActionResult AddOrUpdate([FromBody] Carrier carrier)
        {
            _carrierManage.AddOrUpdate(carrier);
            return Json(new Request
            {
                Msg = "Successfully added",
                State = RequestState.Success
            });
        }


        [Route("DeleteCarrier")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public ActionResult Delete([FromBody] int id)
        {
            var existCarrier = _carrierManage.Delete(id);
            if (existCarrier)
                return Json(new Request
                {
                    Msg = "Successfully deleted",
                    State = RequestState.Success
                });
            return Json(new Request
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
            var user =  CurrentUser();
            var carriers = await _carrierManage.GetAvailable(user);
            if (carriers != null)
                return Json(new Request
                {
                    Data = carriers,
                    Msg = "Successfully received",
                    State = RequestState.Success
                });
            return Json(new Request
            {
                Msg = "Not received",
                State = RequestState.Success
            });
        }


        [Route("MyCarriers")]
        [HttpGet]
        [Authorize(Roles = "admin,customer,carrier")]
        public IActionResult MyCarriers()
        {
            var user =  CurrentUser();
            var myCarriers = _carrierManage.GetMyCarriers(user);
            if (myCarriers != null)
                return Json(new Request
                {
                    Data = myCarriers,
                    Msg = "Successfully received",
                    State = RequestState.Success
                });
            return Json(new Request
            {
                Msg = "Not received",
                State = RequestState.Success
            });
        }


        [HttpGet("user/{id}")]
        [Authorize(Roles = "admin,carrier")]
        public ActionResult ListUsers(int id)
        {
            var myUsers = _carrierManage.GetMyUsers(id);
            if (myUsers != null)
                return Json(new Request
                {
                    Data = myUsers,
                    Msg = "Successfully received",
                    State = RequestState.Success
                });
            return Json(new Request
            {
                Msg = "Not received",
                State = RequestState.Success
            });
        }


        [HttpPost("Active")]
        [Authorize(Roles = "admin,carrier")]
        public IActionResult ActiveCarrier([FromBody] int id)
        {
            var user =  CurrentUser();
            var relation =   _customerManage.GetSubscribe(user.Id, id);
            if (relation != null)
            {
                if (_carrierManage.Active(id)==true)
                    return Json(new Request
                    {
                        Msg = "Subscribe",
                        State = RequestState.Success
                    });
                return Json(new Request
                {
                    Msg = "UnSubscribe",
                    State = RequestState.Success
                });
            }

            return Json(new Request
            {
                Msg = "Cannot be activated",
                State = RequestState.Failed
            });
        }

        [Route("Add-UserCarrier")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult AddUserCarrier([FromBody] UserModel carrier, int carrierId)
        {
            _userManage.PutCarrier(carrier);
            var currentCarrier = _userManage.GetByEmail(carrier.Email);
             _customerManage.Subscribe(carrierId, currentCarrier.Id);
            return Json(new Request
            {
                Msg = "Successfully added",
                State = RequestState.Success
            });
        }
    }
}