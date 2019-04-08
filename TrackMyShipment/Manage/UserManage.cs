using System.Collections.Generic;
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

        public async Task<User> GetByEmailUser(string email)
        {
            return  await _userService.GetUserByEmail(email);
        }

        public async Task<User> GetUser(User user)
        { 
            return await _userService.GetUser(user);
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var user = await _userService.GetUser(new User {Email = loginModel.Email, Password = loginModel.Password});
            if (user == null) return null;
            var token = new Token().GetToken(user);
            return token;
        }

        public async Task<User> CreateUser(RegistrationModel user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RegistrationModel, User>()).CreateMapper();
            var userModel = mapper.Map<RegistrationModel, User>(user);
            var registeredUser =  await _userService.Create(userModel);
            if (registeredUser != null) return registeredUser;
             return null;
        }

        public async Task PutUserCarrier(UserModel carrier)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, User>()).CreateMapper();
            var carrierModel = mapper.Map<UserModel, User>(carrier);
            await _userService.PutCarrier(carrierModel);
        }

        public async Task<IEnumerable<UserModel>> GetMyUsers(int carrierId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>()).CreateMapper();
            var people = await _userService.GetMyUsers(carrierId);
            IEnumerable<UserModel> peopleShort = mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(people);
            return peopleShort;
        }
    }
}