using AutoMapper;
using TrackMyShipment.Core.Interfaces;
using TrackMyShipment.Repository.Models;
using TrackMyShipment.Repository.ViewModel;
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
        public User GetByEmail(string email)
        {
            return _userService.GetByEmail(email);
        }

        public User Get(User user)
        {
            return _userService.Get(user);
        }

        public string Login(LoginModel user)
        {
            User _user = _userService.Get(new User { Email = user.Email, Password = user.Password });
            if (_user != null)
            {
                string token = new Token().GetToken(new User { Email = _user.Email, Role = _user.Role });
                return token;
            }
            return null;
        }

            public bool Create(RegistrationModel user)
        {
            return _userService.Create((new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone
            }), user.CompanyName);
        }


        public void PutCarrier(UserModel carrier)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, User>()).CreateMapper();
            User carrierModel = mapper.Map<UserModel, User>(carrier);

            _userService.PutCarrier(carrierModel);
        }
    }

}

