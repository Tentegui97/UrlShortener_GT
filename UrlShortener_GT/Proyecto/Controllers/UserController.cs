using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener_GT.Proyecto.Data;
using UrlShortener_GT.Proyecto.Entities;
using UrlShortener_GT.Proyecto.Models;
using UrlShortener_GT.Proyecto.Services;


namespace UrlShortener_GT.Proyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAll());
        }

        [HttpPost]
        public IActionResult CreateUser(CreateAndUpdateUserDto dto)
        {
            try
            {
                _userService.CreateUser(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Created("Created", dto);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult UpdateUser(CreateAndUpdateUserDto dto, int userId)
        {
            _userService.Update(dto, userId);
            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteUser(int id)
        {
            User? user = _userService.GetUser(id);
            if (user is null)
            {
                return BadRequest("El cliente que intenta eliminar no existe");
            }

            if (user.Username != "Admin")
            {
                _userService.DeleteUser(id);
            }
            return NoContent();
        }
    }
}
