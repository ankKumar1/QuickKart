using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository
    {
        private readonly UserDBContext dbContext;

        public UserRepository(UserDBContext context)
        {
            dbContext = context;
        }

        public List<User> GetAllUsers()
        {
            List<User> listOfUsers = dbContext.Users.ToList();
            return listOfUsers;
        }

        public bool AddNewUser(User User)
        {
            bool status;
            try
            {
                dbContext.Users.Add(User);
                dbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public int UpdateUserDetails(User User)
        {
            int status = -1;
            User userObj = dbContext.Users.Find(User.EmailId);
            try
            {
                if (userObj != null)
                {
                    userObj.RoleName = User.EmailId;
                    userObj.UserPassword = User.UserPassword;
                    userObj.Address = User.Address;
                    userObj.DateOfBirth = User.DateOfBirth;
                    userObj.Gender = User.Gender;
                    dbContext.Users.Update(userObj);
                    dbContext.SaveChanges();
                    status = 1;
                }
            }
            catch (Exception)
            {
                status = -99;
            }
            return status;
        }

        public bool DeleteUser(string emailId)
        {
            bool status = false;
            User userObj = dbContext.Users.Find(emailId);
            try
            {
                if (userObj != null)
                {
                    dbContext.Users.Remove(userObj);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
    }
}
