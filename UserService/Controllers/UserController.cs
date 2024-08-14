using Microsoft.AspNetCore.Mvc;
using UserService.DTOs.Requests;
using UserService.DTOs.Responses;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("SaveUser")]
        public BaseResponseDTOs CreateUser(CreateUserRequestDTOs request)
        {
            return userService.CreateUser(request);
        }
        [HttpGet("GetList")]
        public BaseResponseDTOs UserList()
        {
            return userService.UserList();
        }

        [HttpGet("GetUserById/{id}")]
        public BaseResponseDTOs GetUserById(int id)
        {
            return userService.GetUserById(id);
        }

        [HttpPut("UpdateUserById/{id}")]
        public BaseResponseDTOs UpdateUserById(int id, UpdateUserRequestDTOs request)
        {
            return userService.UpdateUserById(id, request);
        }
        [HttpDelete("DeleteUserById/{id}")]
        public BaseResponseDTOs DeleteUserById(int id)
        {
            return userService.DeleteUserById(id);
        }
    }
}
