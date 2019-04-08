using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyShipment.Manage;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        protected readonly CarrierManage _carrierManage;
        protected readonly CustomerManage _customerManage;
        protected readonly UserManage _userManage;
    
        protected BaseController(UserManage userManage, CarrierManage carrierManage, CustomerManage customerManage)
        {
            _userManage = userManage;
            _customerManage = customerManage;
            _carrierManage = carrierManage;
        }

        [HttpGet]
        [Route("fullInfo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
         public async Task<User> CurrentUser()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null) return null;
            return  await _userManage.GetByEmail(claimsIdentity.Name);
        }

        [HttpGet]
        [Route("shortInfo")]
        protected virtual  async Task<RegistrationModel> UserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null) return null;
            var user =  await _userManage.GetByEmail(claimsIdentity.Name);
            return new RegistrationModel {FirstName = user.FirstName, LastName = user.LastName, Phone = user.Phone};
        }
    }
}