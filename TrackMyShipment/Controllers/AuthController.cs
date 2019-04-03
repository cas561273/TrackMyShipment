using Microsoft.AspNetCore.Mvc;
using TrackMyShipment.Core.ViewModel;
using TrackMyShipment.Manage;
using TrackMyShipment.Repository.ViewModel;
using TrackMyShipment.ViewModel;


namespace TrackMyShipment.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(UserManage userManage, CarrierManage carrierManage, CustomerManage customerManage)
            : base(userManage, carrierManage, customerManage)
        {
        }

        [HttpPut, Route("Register")]
        public IActionResult Add([FromBody]RegistrationModel user)
        {
            bool result = _userManage.Create(user);
            if (result == true)
            {
                return Json(new Request
                {
                    State = RequestState.Success,
                    Msg = "Registered successfuly"
                });
            }

            return Json(new Request
            {
                State = RequestState.Failed,
                Msg = "Failed registration"
            }
            );

        }


        [HttpPost, Route("Auth")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            string token = _userManage.Login(user);

            if (token != null)
            {
                return Json(new Request
                {
                    Data = new
                    {
                        Token = token
                    },
                    State = RequestState.Success,
                    Msg = "User authorized",
                });
            }

            return Json(new Request
            {
                State = RequestState.Failed,
                Msg = "Username or password is invalid"
            }
            );
        }
    }
}