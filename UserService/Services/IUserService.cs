using UserService.DTOs.Requests;
using UserService.DTOs.Responses;

namespace UserService.Services
{
    public interface IUserService
    {
        BaseResponseDTOs CreateUser(CreateUserRequestDTOs request);
        BaseResponseDTOs UserList();
        BaseResponseDTOs GetUserById(int id);
        BaseResponseDTOs UpdateUserById(int id, UpdateUserRequestDTOs request);
        BaseResponseDTOs DeleteUserById(int id);
    }
}
