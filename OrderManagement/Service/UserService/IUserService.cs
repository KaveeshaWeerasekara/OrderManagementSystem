using OrderManagement.DTOs.Requests;
using OrderManagement.DTOs.Responses;

namespace OrderManagement.Service.UserService
{
    public interface IUserService
    {
        BaseResponse CreateUser(CreateUserRequest request);
        BaseResponse UserList();
        BaseResponse GetUserById(int id);
        BaseResponse UpdateUserById(int id, UpdateUserRequest request);
        BaseResponse DeleteUserById(int id);

    }
}
