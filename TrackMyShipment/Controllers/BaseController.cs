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
        protected readonly AddressManage _addressManage;
        protected readonly SubscriptionManage _subscriptionManage;

        protected BaseController(UserManage userManage, CarrierManage carrierManage,AddressManage addressManage, CompanyManage companyManage, SubscriptionManage subscriptionManage)
        {
            _userManage = userManage;
            _carrierManage = carrierManage;
            _addressManage = addressManage;
            _companyManage = companyManage;
            _subscriptionManage = subscriptionManage;
        }
 
        protected async Task<User> CurrentUser()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null) return null;
            return  await _userManage.GetByEmailUser(claimsIdentity.Name);
        }
  
        protected async Task<RegistrationModel> UserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null) return null;
            var user =  await _userManage.GetByEmailUser(claimsIdentity.Name);
            return new RegistrationModel {FirstName = user.FirstName, LastName = user.LastName,
                Phone = user.Phone,CompanyName = user.Company.Name,Email = user.Email};
        }
    }
}