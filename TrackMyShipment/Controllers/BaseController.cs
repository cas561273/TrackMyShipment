using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrackMyShipment.Manage;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        protected readonly UserManage _userManage;
        protected readonly CustomerManage _customerManage;
        protected readonly CarrierManage _carrierManage;


        protected BaseController(UserManage userManage, CarrierManage carrierManage, CustomerManage customerManage)
        {
            _userManage = userManage;
            _customerManage = customerManage;
            _carrierManage = carrierManage;
        }

        [HttpGet, Route("fullInfo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public User CurrentUser()
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return null;
            }
            return _userManage.GetByEmail(claimsIdentity.Name);

        }

        [HttpGet, Route("shortInfo")]
        public RegistrationModel UserInfo()
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return null;
            }
            User user = _userManage.GetByEmail(claimsIdentity.Name);
            return new RegistrationModel { FirstName = user.FirstName, LastName = user.LastName, Phone = user.Phone };
        }
    }
}


