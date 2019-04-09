using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TrackMyShipment.Core.Helper;
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

        public async Task<string> Login(LoginModel loginModel)
        {

            var user = await _userService.GetUser(new User {Email = loginModel.Email, Password = PasswordHelper.CalculateHashedPassword(loginModel.Email, loginModel.Password)});
            if (user == null) return null;
            var token = new Token().GetToken(user);
            return token;
        }

        public async Task<User> CreateUser(RegistrationModel user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RegistrationModel, User>()).CreateMapper();
            var userModel = mapper.Map<RegistrationModel, User>(user);
            userModel.Password = PasswordHelper.CalculateHashedPassword(user.Email, user.Password);
            var registeredUser =  await _userService.CreateUser(userModel);
            if (registeredUser != null) return registeredUser;
             return null;
        }

        public async Task PutUserCarrier(UserModel carrier)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, User>()).CreateMapper();
            var carrierModel = mapper.Map<UserModel, User>(carrier);
            carrierModel.Password = PasswordHelper.CalculateHashedPassword(carrierModel.Email, carrierModel.Password);
            await _userService.PutUserCarrier(carrierModel);
        }

        public async Task<IEnumerable<UserModel>> GetMyUsers(int carrierId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>()).CreateMapper();
            var people = await _userService.GetMyUsers(carrierId);
            IEnumerable<UserModel> peopleShort = mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(people);
            return peopleShort;
        }

        public async Task<User> GetByEmailUser(string email)
        {
            return await _userService.GetUserByEmail(email);
        }

        public async Task<User> GetUser(User user)
        {
            user.Password = PasswordHelper.CalculateHashedPassword(user.Email, user.Password);
            return await _userService.GetUser(user);
        }
    }
}