using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener_GT.Proyecto.Data;
using UrlShortener_GT.Proyecto.Entities;
using UrlShortener_GT.Proyecto.Models;
using UrlShortener_GT.Proyecto.Services;

namespace UrlShortener_GT.Proyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UrlControllers : ControllerBase
    {
        private readonly UrlShortenerService _UrlShortenerService;
        private readonly UrlShortenerContext _UrlShortenerContext;
        public UrlControllers(UrlShortenerService urlShortener, UrlShortenerContext urlShortenerContext)
        {
            _UrlShortenerService = urlShortener;
            _UrlShortenerContext = urlShortenerContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            return Ok(_UrlShortenerService.GetAllByUser(userId));
        }


        [HttpGet("{shortUrl}")]
        public RedirectResult GetLongUrl(string shortUrl)
        {
            Url urlDest = _UrlShortenerService.UrlRedirector(shortUrl);
            return Redirect(urlDest.NormalUrl);
        }

        [HttpDelete]

        public IActionResult DeleteUrl(int id)
        {
            return Ok(_UrlShortenerService.DeleteUrl(id));
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult UpdateUrl(CreateAndUpdateUrlDto dto, int urlId)
        {
            _UrlShortenerService.Update(dto, urlId);
            return NoContent();
        }
        [HttpPost]
        public IActionResult CreateUrl(CreateAndUpdateUrlDto dto)
        {
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            _UrlShortenerService.Create(dto, userId);
            return Created("Created", dto);
        }

    }
}

