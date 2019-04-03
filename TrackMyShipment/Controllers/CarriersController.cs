using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrackMyShipment.Core.ViewModel;
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
        [HttpPut, Authorize(Roles = "admin")]
        public ActionResult AddOrUpdate([FromBody]Carrier carrier)
        {
            _carrierManage.AddOrUpdate(carrier);
            return Json(new Request
            {
                Msg = "Successfully added",
                State = RequestState.Success,
            });
        }


        [Route("DeleteCarrier")]
        [HttpDelete, Authorize(Roles = "admin")]
        public ActionResult Delete([FromBody]int id)
        {
            bool existCarrier = _carrierManage.Delete(id);
            if (existCarrier)
            {
                return Json(new Request
                {
                    Msg = "Successfully deleted",
                    State = RequestState.Success,
                });
            }
            return Json(new Request
            {
                Msg = "Not Found",
                State = RequestState.Failed,
            });
        }


        [Route("GetCarriers")]
        [HttpGet, Authorize(Roles = "admin,customer")]
        public ActionResult AvailableCarriers()
        {
            User user = CurrentUser();
            IEnumerable<Carrier> carriers = _carrierManage.GetAvailable(user);
            if (carriers != null)
            {
                return Json(new Request
                {
                    Data = carriers,
                    Msg = "Successfuly received",
                    State = RequestState.Success,
                });
            }
            return Json(new Request
            {
                Msg = "Not received",
                State = RequestState.Success,
            });
        }


        [Route("MyCarriers")]
        [HttpGet, Authorize(Roles = "admin,customer,carrier")]
        public ActionResult MyCarriers()
        {
            User user = CurrentUser();
            IEnumerable<Carrier> myCarriers = _carrierManage.GetMyCarriers(user);
            if (myCarriers != null)
            {
                return Json(new Request
                {
                    Data = myCarriers,
                    Msg = "Successfuly received",
                    State = RequestState.Success,
                });
            }
            return Json(new Request
            {
                Msg = "Not received",
                State = RequestState.Success,
            });
        }



        [HttpGet("user/{id}"), Authorize(Roles = "admin,carrier")]
        public ActionResult ListUsers(int id)
        {
            IEnumerable<UserModel> myUsers = _carrierManage.GetMyUsers(id);
            if (myUsers != null)
            {
                return Json(new Request
                {
                    Data = myUsers,
                    Msg = "Successfuly received",
                    State = RequestState.Success,
                });
            }
            return Json(new Request
            {
                Msg = "Not received",
                State = RequestState.Success,
            });
        }


        [HttpPost("Active"), Authorize(Roles = "admin,carrier")]
        public ActionResult ActiveCarrier([FromBody]int id)
        {
            User user = CurrentUser();
            Supplies relation = _customerManage.GetSubscribe(user.Id, id);
            if (relation != null)
            {
               if( _carrierManage.Active(id))
                {
                    return Json(new Request
                    {
                        Msg =  "Subscribe",
                        State = RequestState.Success,
                    });
                }
                return Json(new Request
                {
                    Msg = "UnSubscribe",
                    State = RequestState.Success,
                });
            }
            return Json(new Request
            {
                Msg = "Cannot be activated",
                State = RequestState.Failed,
            });

        }


        [Route("Add-UserCarrier")]
        [HttpPut, Authorize(Roles = "admin")]
        public ActionResult AddUserCarrier([FromBody]UserModel carrier,int carrierId)
        {
            _userManage.PutCarrier(carrier);
            var currentCarrier = _userManage.GetByEmail(carrier.Email);
            _customerManage.Subscribe(carrierId, currentCarrier.Id);
            return Json(new Request
            {
                Msg = "Successfuly added",
                State = RequestState.Success,
            });
        }
    }
}
