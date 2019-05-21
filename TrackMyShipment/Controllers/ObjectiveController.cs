using System.Collections.Generic;
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
    public class ObjectiveController : BaseController
    {
        public ObjectiveController(ObjectiveManage objectiveManage,UserManage userManage, CarrierManage carrierManage, AddressManage addressManage, CompanyManage companyManage, SubscriptionManage subscriptionManage)
            : base(objectiveManage,userManage, carrierManage, addressManage, companyManage, subscriptionManage)
        {
        }


        [HttpPut]
        [Route("Add-Task")]
        [Authorize(Roles = "carrier")]
        public async Task<IActionResult> AddTask([FromBody]Objective task)
        {
            User user = await CurrentUser();
            int userId = user.Id;
            bool result = await ObjectiveManage.AddTask(userId, task);

            return result 
                ? Json(new Request
                {
                    Msg = "Added successful",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Error, can not add!",
                    State = RequestState.Failed
                });
        }

        [HttpGet]
        [Route("GetMyTask")]
        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> GetMyTask()
        {
            User user = await CurrentUser();
            int userId = user.Id;
            var tasks = await ObjectiveManage.GetMyTask(userId);

            return tasks!=null
                ? Json(new Request
                {
                    Data = tasks,
                    Msg = "Received successful",
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