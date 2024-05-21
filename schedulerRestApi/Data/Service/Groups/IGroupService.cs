using schedulerRestApi.Data.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace schedulerRestApi.Data.Service.Groups
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDTO>> GetAllGroupsAsync();
        Task<GroupDTO> GetGroupByIdAsync(int id);
        Task<GroupDTO> CreateGroupAsync(GroupDTO groupDTO);
        Task<GroupDTO> UpdateGroupAsync(int id, GroupDTO groupDTO);
        Task<bool> DeleteGroupAsync(int id);
    }
}
