using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        UserDto GetUserByEmail(string email);
        userPublicDto GetPublicUderDetails(int userId);
        string getUserName(int userId);

    }
}
