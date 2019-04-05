using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackMyShipment.Manage;
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

        [HttpPut]
        [Route("Register")]
        public async Task<IActionResult> Add([FromBody] RegistrationModel user)
        {
            var result = await _userManage.Create(user);
            if (result)
                return Json(new Request
                {
                    State = RequestState.Success,
                    Msg = "Registered successfully"
                });

            return Json(new Request
                {
                    State = RequestState.Failed,
                    Msg = "Failed registration"
                }
            );
        }


        [HttpPost]
        [Route("Auth")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            var token =  _userManage.Login(user);

            if (token != null)
                return Json(new Request
                {
                    Data = new
                    {
                        Token = token
                    },
                    State = RequestState.Success,
                    Msg = "User authorized"
                });

            return Json(new Request
                {
                    State = RequestState.Failed,
                    Msg = "Username or password is invalid"
                }
            );
        }
    }
}