using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using schedulerRestApi.Data.DTOs;
using schedulerRestApi.Data.Repository;

namespace schedulerRestApi.Data.Service.Groups
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<GroupDTO>> GetAllGroupsAsync()
        {
            return await _groupRepository.GetAllGroupsAsync();
        }

        public async Task<GroupDTO> GetGroupByIdAsync(int id)
        {
            return await _groupRepository.GetGroupByIdAsync(id);
        }

        public async Task<GroupDTO> CreateGroupAsync(GroupDTO groupDTO)
        {
            // Perform any additional validation or business logic here
            return await _groupRepository.CreateGroupAsync(groupDTO);
        }

        public async Task<GroupDTO> UpdateGroupAsync(int id, GroupDTO groupDTO)
        {
            // Perform any additional validation or business logic here
            return await _groupRepository.UpdateGroupAsync(id, groupDTO);
        }

        public async Task<bool> DeleteGroupAsync(int id)
        {
            // Perform any additional validation or business logic here
            return await _groupRepository.DeleteGroupAsync(id);
        }
    }
}
