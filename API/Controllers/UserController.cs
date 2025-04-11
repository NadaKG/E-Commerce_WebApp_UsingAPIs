using Application.DTOs;
using Application.Service;
using Application.Service_Interface;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var addedUser = await _userService.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = addedUser.Id }, addedUser);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPut("updateuser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            var updatedUser = _userService.Update(user);
            return Ok(updatedUser);
        }

        [HttpDelete("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound("User not found.");

            _userService.Delete(user);
            return NoContent();
        }


        [HttpPost("update-address/{userId}")]
        public async Task<IActionResult> UpdateAddress(int userId, [FromBody] CheckOutDTO checkoutDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _userService.UpdateUserAddressAsync(userId, checkoutDetails);
            if (!result)
            {
                return NotFound("User not found or address update failed.");
            }

            return Ok("Address updated successfully.");
        }


        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _userService.GetUserProfileAsync(id);
            if (profile == null) return NotFound("User not found.");
            return Ok(profile);
        }

        [HttpPut("editProfile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UserProfileDTO profile)
        {
            try
            {
                bool updated = await _userService.UpdateUserProfileAsync(id, profile);
                if (!updated) return NotFound("User not found.");
                return Ok("Profile updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("all-users")]
        public async Task<IActionResult> AllUsers()
        {
            var users = await _userService.GetAllUserDetailsAsync();
            if (users == null)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }
    }
}

