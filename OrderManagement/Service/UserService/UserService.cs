using OrderManagement.DTOs;
using OrderManagement.DTOs.Requests;
using OrderManagement.DTOs.Responses;
using OrderManagement.Models;

namespace OrderManagement.Service.UserService
{
    public class UserService:IUserService
    {
        private readonly ApplicationDBContext context;

        public UserService(ApplicationDBContext applicationDBContext)
        {
            context= applicationDBContext;
        }

        public BaseResponse CreateUser(CreateUserRequest request)
        {
            BaseResponse response;
            try
            {
                UserModel newUser = new UserModel();
              //  newUser.UserID = request.UserID;
                newUser.FName = request.FName;
                newUser.LName = request.LName;
                newUser.Email = request.Email;
                newUser.Address = request.Address;
                newUser.PhoneNumber = request.PhoneNumber;

                using (context)
                {
                    context.Add(newUser);
                    context.SaveChanges();

                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new user" }
                };
                return response;
            }
            catch (Exception ex) 
            {
                response = new BaseResponse
                {
                    status_code=StatusCodes.Status500InternalServerError,
                    data = new {message="Internal Server Error : "+ex.Message}
                };
                return response;
            }
        }

        public BaseResponse DeleteUserById(int id)
        {
            BaseResponse response;
            try
            {
               
                using (context)
                {
                    UserModel? studentToDelete = context.Users.Where(user => user.id == id).FirstOrDefault();
                    if (studentToDelete != null)
                    {
                       context.Users.Remove(studentToDelete);
                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "User deleted successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message="No user found" }
                        };
                    }
                }
                
                return response;

            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };
                return response;
            }

        }


        public BaseResponse GetUserById(int id)
        {
            BaseResponse response;
            try
            {
                UserDTO user = new UserDTO();
                using (context)
                {
                    UserModel? filteredUser = context.Users.Where(user => user.id == id).FirstOrDefault();
                    if (filteredUser != null)
                    {
                      //  user.UserID = filteredUser.UserID;
                        user.FName = filteredUser.FName;
                        user.LName = filteredUser.LName;
                        user.Email = filteredUser.Email;
                        user.Address = filteredUser.Address;
                        user.PhoneNumber = filteredUser.PhoneNumber;
                    }
                    else
                    {
                        user = null;
                    }
                }
                if (user != null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { user }
                    };
                }
                else
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "No user found" }
                    };
                }
                return response;

            }
            catch (Exception ex) 
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };
                return response;
            }
     
        }

        public BaseResponse UpdateUserById(int id, UpdateUserRequest request)
        {
            BaseResponse response;
            try
            {
                
                using (context)
                {
                    UserModel? filteredUser = context.Users.Where(user => user.id == id).FirstOrDefault();
                    if (filteredUser != null)
                    {
                        filteredUser.FName = request.FName;
                        filteredUser.LName = request.LName;
                        filteredUser.Email = request.Email;
                        filteredUser.Address = request.Address;
                        filteredUser.PhoneNumber = request.PhoneNumber;

                        context.SaveChanges();
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message="User updated successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message="No user found" }
                        };
                    }
                   
                }
                return response;

            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };
                return response;
            }

        }

        public BaseResponse UserList()
        {
            BaseResponse response;
            try
            {
                List<UserDTO> users = new List<UserDTO>();
                using (context)
                {
                    context.Users.ToList().ForEach(user => users.Add(new UserDTO
                    {
                        id = user.id,
                        FName = user.FName,
                        LName = user.LName,
                        Email = user.Email,
                        Address = user.Address,
                        PhoneNumber = user.PhoneNumber,
                    }));

                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { users }
                };
                return response;
            }
            catch(Exception ex) {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };
                return response;
            }
        }
    }
}
