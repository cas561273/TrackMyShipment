using System.Linq;
using TrackMyShipment.Core.Utils;
using TrackMyShipment.Repository.Interfaces;
using TrackMyShipment.Repository.Models;

namespace TrackMyShipment.Repository.Implemetations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public ApplicationContext Context;
        public UserRepository(ApplicationContext context) : base(context)
        {
            Context = context;
        }

        public User UserExists(User userExist)
        {
            string encryptPass = Encrypt.Sha256(userExist.Email, userExist.Password);
            User user = Context.Users.SingleOrDefault(u => u.Email == userExist.Email && u.Password == encryptPass);
            if (user != null)
            {
                Context.Entry(user).Reference("Role").Load();
                Context.Entry(user).Reference("Company").Load();
                Context.Entry(user).Reference("Subscription").Load();
            }

            return user;
        }

        public User GetByEmail(string email)
        {
            User user = Context.Users.SingleOrDefault(_ => _.Email.Equals(email));
            if (user != null)
            {
                Context.Entry(user).Reference("Role").Load();
                Context.Entry(user).Reference("Company").Load();
                Context.Entry(user).Reference("Subscription").Load();

            }

            return user;
        }

        public int GetRoleId(string role)
        {
            return Context.Roles.SingleOrDefault(r => r.Role.Equals(role)).Id;
        }

        public int GetSubscribeId(string subscribe)
        {
            return Context.Subscriptions.SingleOrDefault(s => s.Status.Equals(subscribe)).Id;
        }

        public void PutCompany(string companyName, string email)
        {
            User currentUser = GetByEmail(email);
            Company company = Context.Company.SingleOrDefault(c => c.Name.Equals(companyName));
            if (company != null)
            {
                currentUser.CompanyId = company.Id;
            }
            else
            {
                Context.Company.Add(new Company { Name = companyName });
                Context.SaveChanges();
                company = Context.Company.SingleOrDefault(c => c.Name.Equals(companyName));
                Context.Users.SingleOrDefault(u => u.Email == email).CompanyId = company.Id;

            }
        }
    }
}

