using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackMyShipment.Core.Helper;
using TrackMyShipment.Manage;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.Utils;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(UserManage userManage, CarrierManage carrierManage, AddressManage addressManage,CompanyManage companyManage,SubscriptionManage subscriptionManage)
            : base(userManage, carrierManage, addressManage,companyManage,subscriptionManage)
        {
        }

        [HttpPut]
        [Route("Register")]
        public async Task<IActionResult> Registration([FromBody] RegistrationModel user)
        {
            User existedUser = await _userManage.CreateUser(user);
            await _companyManage.AddCompanyToUser(existedUser, user.CompanyName);

            var token = new Token().GetToken(existedUser);
            if (token != null)
                return Json(new Request
                {
                    Data = new {Token = token},
                    State = RequestState.Success,
                    Msg = "Registered successfully"
                });
            else
                return Json(new Request
                    {
                        State = RequestState.Failed,
                        Msg = "Failed registration"
                    }
                );
        }

        [HttpPost]
        [Route("Login")]
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