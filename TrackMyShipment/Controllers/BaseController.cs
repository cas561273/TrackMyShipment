using System.Security.Claims;
using System.Threading.Tasks;
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
        protected readonly UserManage _userManage;
        protected readonly CompanyManage _companyManage;
        protected readonly SubscriptionManage _subscriptionManage;
        protected readonly ObjectiveManage _objectiveManage;

        protected BaseController(ObjectiveManage objectiveManage,UserManage userManage, CarrierManage carrierManage, CompanyManage companyManage, SubscriptionManage subscriptionManage)
        {
            _userManage = userManage;
            _carrierManage = carrierManage;
            _companyManage = companyManage;
            _subscriptionManage = subscriptionManage;
            _objectiveManage = objectiveManage;
        }
 
        protected async Task<User> CurrentUser()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null) return null;
            return  await _userManage.GetByEmailUser(claimsIdentity.Name);
        }
  
        protected async Task<InfoUserModel> UserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null) return null;
            var user =  await _userManage.GetByEmailUser(claimsIdentity.Name);
 
            return new InfoUserModel {FirstName = user.FirstName, LastName = user.LastName,
                Phone = user.Phone,CompanyName = user.Company.Name,Email = user.Email,Role = user.Role.Name};
        }
    }
}