using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.DTOs.Requests;
using OrderManagement.DTOs.Responses;
using OrderManagement.Service.UserService;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService) 
        { 
        this.userService = userService;
        }

        [HttpPost("save")]
        public BaseResponse CreateUser(CreateUserRequest request)
        {
            return userService.CreateUser(request);
        }
        [HttpGet("list")]
        public BaseResponse UserList()
        {
            return userService.UserList();
        }

        [HttpGet("{id}")]
        public BaseResponse GetUserById(int id)
        {
            return userService.GetUserById(id);
        }

        [HttpPut("{id}")]
        public BaseResponse UpdateUserById(int id,UpdateUserRequest request)
        {
            return userService.UpdateUserById(id,request);
        }
        [HttpDelete("{id}")]
        public BaseResponse DeleteUserById(int id)
        {
            return userService.DeleteUserById(id);
        }
    }
}
