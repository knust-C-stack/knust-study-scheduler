using Microsoft.AspNetCore.Mvc;
using schedulerRestApi.Data.DTOs;
using schedulerRestApi.Data.Service.Groups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace schedulerRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDTO>> GetGroup(int id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        // POST: api/Groups
        [HttpPost]
        public async Task<ActionResult<GroupDTO>> PostGroup(GroupDTO groupDTO)
        {
            var createdGroup = await _groupService.CreateGroupAsync(groupDTO);
            return CreatedAtAction(nameof(GetGroup), new { id = createdGroup.Id }, createdGroup);
        }

        // PUT: api/Groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, GroupDTO groupDTO)
        {
            if (id != groupDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedGroup = await _groupService.UpdateGroupAsync(id, groupDTO);
                return Ok(updatedGroup);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var result = await _groupService.DeleteGroupAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
