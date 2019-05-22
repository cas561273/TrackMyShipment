using System.Data.SqlClient;
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
            bool result = await _objectiveManage.AddTask(userId, task);

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
        [Authorize(Roles = "customer,carrier")]
        public async Task<IActionResult> GetMyTask()
        {
            User user = await CurrentUser();
            int userId = user.Id;
            var tasks = await _objectiveManage.GetMyTask(userId);

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

        [HttpPost]
        [Route("ChangeStatusTask")]
        [Authorize(Roles = "carrier")]
        public async Task<IActionResult> ChangeStatusTask([FromBody]int objectiveId)
        {
            User user = await CurrentUser();
            int userId = user.Id;
            bool result = await _objectiveManage.ChangeStatusTask(userId, objectiveId);
            return result
                ? Json(new Request
                {
                    Msg = "Changed successful",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Can not changed!",
                    State = RequestState.Failed
                });
        }

        [HttpPost]
        [Route("TakeTask")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> TakeTask([FromBody]int objectiveId)
        {
            User user = await CurrentUser();
            int userId = user.Id;
            bool result = await _objectiveManage.TakeTask(userId, objectiveId);
            return result
                ? Json(new Request
                {
                    Msg = "Task take success!",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Can not take this task!",
                    State = RequestState.Failed
                });
        }

        [HttpPost]
        [Route("CloseTask")]
        [Authorize(Roles = "carrier")]
        public async Task<IActionResult> ResolveTask([FromBody]int idTask)
        {
            bool result = await _objectiveManage.ResolveTask(idTask);
            return result
                ? Json(new Request
                {
                    Msg = "Task take success!",
                    State = RequestState.Success
                })
                : Json(new Request
                {
                    Msg = "Can not take this task!",
                    State = RequestState.Failed
                });
        }

    }
}