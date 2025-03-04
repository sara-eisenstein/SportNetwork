using Common.Dto;
using Repositorys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IFollowerService
    {
        List<FollowerDto> GetFollowersByUserId(int userId);
    }
}
