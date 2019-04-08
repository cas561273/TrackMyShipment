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
            return result
                ? Json(new Request
                {
                    State = RequestState.Success,
                    Msg = "Registered successfully"
                })
                : Json(new Request
                    {
                        State = RequestState.Failed,
                        Msg = "Failed registration"
                    }
                );
        }

        [HttpPost]
        [Route("Auth")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            var token = await _userManage.Login(user);

            return token != null
                ? Json(new Request
                {
                    Data = new
                    {
                        Token = token
                    },
                    State = RequestState.Success,
                    Msg = "User authorized"
                })
                : Json(new Request
                    {
                        State = RequestState.NotAuth,
                        Msg = "Username or password is invalid"
                    }
                );
        }

        [HttpGet]
        [Route("shortInfo")]
        public async Task<RegistrationModel> GetUserInfo()
        {
            return await UserInfo();
        }

    }
}