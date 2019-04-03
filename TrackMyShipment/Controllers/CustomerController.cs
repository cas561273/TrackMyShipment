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
    public class CustomerController : BaseController
    {
        public CustomerController(UserManage userManage, CarrierManage carrierManage, CustomerManage customerManage)
             : base(userManage, carrierManage, customerManage)
        {
        }


        [HttpPost, Route("PutAddress"), Authorize(Roles = "admin,customer")]
        public IActionResult PutAddress([FromBody]AddressModel address)
        {
            int? userId = CurrentUser()?.Id;
            var result = _customerManage.PutOrUpdate(address, userId);

            if (result != null)
            {
                return Json(new Request
                {
                    State = RequestState.Success,
                    Msg = "Put address successfuly",
                });
            }

            return Json(new Request
            {
                State = RequestState.Failed,
                Msg = "Address can not be added"
            });

        }


        [HttpDelete, Route("DeleteAddress"), Authorize(Roles = "admin,customer")]
        public IActionResult Delete([FromBody]int? id)
        {
            int? userId = CurrentUser()?.Id;
            Address address = _customerManage.DeleteAddress(id, userId);
            if (address != null)
            {
                return Json(new Request
                {
                    Data = address,
                    State = RequestState.Success,
                    Msg = "Deleted successfuly"
                });
            }

            return Json(new Request
            {
                State = RequestState.Failed,
                Msg = "Cannot be deleted"
            });
        }



        [HttpGet, Route("MyAddress"), Authorize(Roles = "admin,customer")]
        public IActionResult MyAddress()
        {
            int? userId = CurrentUser()?.Id;
            IEnumerable<AddressModel> myAddress = _customerManage.MyAddress(userId);

            if (myAddress != null)
            {
                return Json(new Request
                {
                    Msg = "Received successful",
                    Data = myAddress,
                    State = RequestState.Success,
                });
            }

            return Json(new Request
            {
                Msg = "Not Found",
                State = RequestState.Failed,
            });
        }



        [HttpPost, Route("StatusAddress"), Authorize(Roles = "admin,customer")]
        public IActionResult StatusAddress([FromBody]int? id)
        {
            int? userId = CurrentUser()?.Id;
            string result = _customerManage.StatusAddress(id, userId);
            if (result != null)
            {
                return Json(new Request
                {
                    Msg = "Successfully changed",
                    State = RequestState.Success,
                });
            }
            return Json(new Request
            {
                Msg = "Cannot be changed",
                State = RequestState.Failed,
            });
        }



        [HttpPost, Route("Subscribe"), Authorize(Roles = "admin,customer")]
        public IActionResult Subsciption([FromBody]int carrierId)
        {
            string result = string.Empty;
            int? userId = CurrentUser()?.Id;
            Carrier carrier = _carrierManage?.GetById(carrierId);

            if (carrier?.Status != false)
            {
                result = _customerManage.Subscribe(carrierId, userId);
            }
            if (carrier != null)
            {
                return Json(new Request
                {
                    State = RequestState.Success,
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