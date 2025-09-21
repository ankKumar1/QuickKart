using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Repository;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : Controller
    {
        private readonly UserRepository repository;

        public UserServiceController(UserRepository _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public JsonResult GetAllUsersDetails()
        {
            List<User> listOfUsers = repository.GetAllUsers();
            return Json(listOfUsers);
        }

        [HttpGet("{emailId}")]
        public JsonResult GetUserByEmail(string emailId)
        {
            List<User> listOfUsers = repository.GetAllUsers();
            User user = listOfUsers.Find(p => p.EmailId == emailId);
            return Json(user);
        }

        [HttpPost]
        public JsonResult AddNewUser(User user)
        {
            return Json(repository.AddNewUser(user));
        }

        [HttpPut]
        public JsonResult UpdateUser(User user)
        {
            int result = repository.UpdateUserDetails(user);
            return Json(result);
        }

        [HttpDelete]
        public JsonResult DeleteUser(string emaild)
        {
            bool result = repository.DeleteUser(emaild);
            return Json(result);
        }
    }
}
