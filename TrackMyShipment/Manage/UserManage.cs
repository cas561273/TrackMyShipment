using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TrackMyShipment.Core.Helper;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Extensions;
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
            var user = await _userService.GetUserAsync(new User
            {
                Email = loginModel.Email,
                Password = PasswordHelper.CalculateHashedPassword(loginModel.Email, loginModel.Password)
            });
            if (user == null) return null;
            var token = new Token().GetToken(user);
            return token;
        }

        public async Task<User> CreateUser(RegistrationModel user)
        {
            if (user == null) return null;
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RegistrationModel, User>()).CreateMapper();
                var userModel = mapper.Map<RegistrationModel, User>(user);
                userModel.Password = PasswordHelper.CalculateHashedPassword(user.Email, user.Password);
                var registeredUser = await _userService.CreateUserAsync(userModel);
                return registeredUser;
        }

        public async Task<User> PutUserCarrier(UserModel carrier)
        {
            if (carrier == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, User>()).CreateMapper();
            var carrierModel = mapper.Map<UserModel, User>(carrier);
            carrierModel.Password =
                PasswordHelper.CalculateHashedPassword(carrierModel.Email, carrierModel.Password);
            return await _userService.PutUserCarrierAsync(carrierModel);
        }

        public async Task<User> EditUserCarrier(EditUserModel carrier)
        {
            if (carrier == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EditUserModel, User>()).CreateMapper();
            var carrierModel = mapper.Map<EditUserModel, User>(carrier);
            return await _userService.EditUserCarrierAsync(carrierModel);
        }

        public async Task<IEnumerable<UserModel>> GetMyUsers(int? carrierId)
        {
            if (carrierId==null) return null;
            var people = await _userService.GetMyUsersAsync(carrierId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>()).CreateMapper();
            var peopleShort = mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(people);
            return peopleShort;
        }

        public async Task<IEnumerable<EditUserModel>> GetCarrierUsers()
        {
            var carrierUsers = await _userService.GetCarrierUsersAsync();
            if (carrierUsers == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, EditUserModel>()).CreateMapper();
            IEnumerable<EditUserModel> userCarrierShortModel = mapper.Map<IEnumerable<User>, IEnumerable<EditUserModel>>(carrierUsers);
            return userCarrierShortModel;
        }

        public async Task<User> GetByEmailUser(string email)
        {
            return await _userService.GetUserByEmailAsync(email);
        }

        public async Task<User> GetUser(User user)
        {
            user.Password = PasswordHelper.CalculateHashedPassword(user.Email, user.Password);
            return await _userService.GetUserAsync(user);
        }
    }
}