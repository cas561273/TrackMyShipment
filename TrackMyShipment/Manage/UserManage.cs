using System.Threading.Tasks;
using AutoMapper;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.Utils;
using TrackMyShipment.ViewModel;

namespace TrackMyShipment.Manage
{
    public class UserManage
    {
        private readonly IUserService _userService;

        public UserManage(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> GetByEmail(string email)
        {
            return  await _userService.GetByEmail(email);
        }

        public async Task<User> Get(User user)
        { 
            return await _userService.Get(user);
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var user = await _userService.Get(new User {Email = loginModel.Email, Password = loginModel.Password});
            if (user != null)
            {
                var token = new Token().GetToken(new User {Email = user.Email, Role = user.Role});
                return token;
            }

            return null;
        }

        public async Task<bool> Create(RegistrationModel user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RegistrationModel, User>()).CreateMapper();
            var userModel = mapper.Map<RegistrationModel, User>(user);

            return await _userService.Create(userModel, user.CompanyName);
        }

        public async Task PutCarrier(UserModel carrier)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, User>()).CreateMapper();
            var carrierModel = mapper.Map<UserModel, User>(carrier);
            await _userService.PutCarrier(carrierModel);
        }
    }
}