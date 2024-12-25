﻿namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var genres = await this.userService.GetAllUsersAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(genres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var genre = await this.userService.GetUserByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(genre);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] UserDTO userDto)
    {
        if ((!ModelState.IsValid) || (userDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.userService.AddUserAsync(userDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetUserByIdAsync), new { id = userDto.UserId }, userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserDTO userDto)
    {
        if (userDto == null)
        {
            return BadRequest();
        }

        if (id != userDto.UserId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.userService.UpdateUserAsync(userDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserByIdAsync(int id)
    {
        await this.userService.DeleteUserByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
