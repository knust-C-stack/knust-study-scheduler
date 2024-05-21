using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using schedulerRestApi.Data.DTOs;
using schedulerRestApi.Models;

namespace schedulerRestApi.Data.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GroupDTO> CreateGroupAsync(GroupDTO groupDTO)
        {
            var group = new Group
            {
                Name = groupDTO.Name,
                Description = groupDTO.Description,
                CreatedBy = groupDTO.CreatedBy,
            };

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return new GroupDTO
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                CreatedBy = group.CreatedBy,
            };
        }

        public async Task<GroupDTO> GetGroupByIdAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id);

            return group != null ? new GroupDTO
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                CreatedBy = group.CreatedBy,
            } : null;
        }

        public async Task<IEnumerable<GroupDTO>> GetAllGroupsAsync()
        {
            return await _context.Groups
                .Select(group => new GroupDTO
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    CreatedBy = group.CreatedBy,
                })
                .ToListAsync();
        }

        public async Task<GroupDTO> UpdateGroupAsync(int id, GroupDTO groupDTO)
        {
            var group = await _context.Groups.FindAsync(id);

            if (group == null)
                return null;

            group.Name = groupDTO.Name;
            group.Description = groupDTO.Description;

            await _context.SaveChangesAsync();

            return new GroupDTO
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                CreatedBy = group.CreatedBy,
            };
        }

        public async Task<bool> DeleteGroupAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id);

            if (group == null)
                return false;

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
