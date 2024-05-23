using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using schedulerRestApi.Data.DTOs;
using schedulerRestApi.Models;
using Microsoft.Data.SqlClient;


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

            int AffectedRows = await  _context.Database.ExecuteSqlAsync($"Exec uspCreateGroup {groupDTO.Name},{groupDTO.Description},{groupDTO.CreatedBy}");
            if(AffectedRows > 0){
                return groupDTO;
            }
           
             return new GroupDTO();
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
            // Prepare the SQL command with parameters
            var commandText = "EXEC uspUpdateGroup @id, @name, @description, @createdBy";
            
            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(commandText,
                new SqlParameter("@id", id),
                new SqlParameter("@name", groupDTO.Name),
                new SqlParameter("@description", groupDTO.Description),
                new SqlParameter("@createdBy", groupDTO.CreatedBy));

            // Optionally, retrieve the updated group from the database to return the updated data
            var updatedGroup = await _context.Groups.FindAsync(id);

            if (updatedGroup == null)
                return null;

            return new GroupDTO
            {
                Id = updatedGroup.Id,
                Name = updatedGroup.Name,
                Description = updatedGroup.Description,
                CreatedBy = updatedGroup.CreatedBy,
            };
        }


        public async Task<bool> DeleteGroupAsync(int id)
        {
            var commandText = "EXEC uspDeleteGroup @id";
            
            // Execute the stored procedure
            var affectedRows = await _context.Database.ExecuteSqlRawAsync(commandText,
                new SqlParameter("@id", id));

            // Check if any rows were affected (i.e., a group was deleted)
            return affectedRows > 0;
        }

    }
}
