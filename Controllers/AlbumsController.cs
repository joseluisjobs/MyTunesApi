using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyTunes.Core.Enum;
using MyTunes.Core.Interfaces;
using MyTunesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyTunesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumDto>> Get()
        {
            var serviceResult = _albumService.GetAlbums();
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            var albums = serviceResult.Result;
            return Ok(albums.Select(a => new AlbumDto
            {
                Artist = a.Artist,
                Date = a.Date,
                Description = a.Description,
                Genre = a.Genre,
                Id = a.Id,
                Img = a.Img,
                Name = a.Name,
                Price = a.Price,
                Rating = a.Rating
            }));
        }

        [HttpGet]
        [Route("{albumId}")]
        public ActionResult<AlbumDataTransferObject> Get(int albumId)
        {
            var serviceResult = _albumService.GetById(albumId);
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            var album = serviceResult.Result;
            return Ok(new AlbumDataTransferObject
            {
                Artist = album.ArtistName,
                Date = album.Date,
                Description = album.Description,
                Genre = album.Genres,
                Id = album.Id,
                Image = album.Image,
                Name = album.Name,
                Price = album.Price,
                Score = album.Score
            });
        }

        [HttpGet]
        [Route("purchased")]
        public ActionResult<IEnumerable<AlbumDataTransferObject>> GetPurchased()
        {
            var serviceResult = _albumService.GetPurchasedAlbums();
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            var albums = serviceResult.Result;
            return Ok(albums.Select(a => new AlbumDataTransferObject
            {
                Artist = a.ArtistName,
                Date = a.Date,
                Description = a.Description,
                Genre = a.Genres,
                Id = a.Id,
                Image = a.Image,
                Name = a.Name,
                Price = a.Price,
                Score = a.Score
            }));
        }

        [HttpGet]
        [Route("{albumId}/duration")]
        public ActionResult<int> GetDuration(int albumId)
        {
            var serviceResult = _albumService.GetAlbumDuration(albumId);
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            return serviceResult.Result;
        }

        [HttpPut]
        [Route("{albumId}/purchase")]
        public ActionResult<AlbumDataTransferObject> Purchase(int albumId)
        {
            var serviceResult = _albumService.Purchase(albumId);
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            return Ok(serviceResult.Result);
        }

        [HttpPut]
        [Route("{albumId}/{rating}")]
        public ActionResult<AlbumDataTransferObject> Rating(int albumId, int rating)
        {
            var serviceResult = _albumService.SetRating(albumId, rating);
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            return Ok(serviceResult.Result);
        }
    }
}
