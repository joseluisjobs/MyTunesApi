using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTunes.Core.Enum;
using MyTunes.Core.Interfaces;
using MyTunesAPI.Models;

namespace MyTunesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SongDataTransferObject>> Get()
        {
            var serviceResult = _songService.GetSongs();
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            var song = serviceResult.Result;
            return Ok(serviceResult.Result.Select(s => new SongDataTransferObject
            {
                Id = s.Id,
                AlbumId = s.AlbumId,
                Artist = s.ArtistName,
                Duration = s.Duration,
                Name = s.Name,
                Price = s.Price,
                Popularity = s.Popularity
            }));
        }

        [HttpGet]
        [Route("purchased")]
        public ActionResult<IEnumerable<SongDataTransferObject>> GetPurchased()
        {
            var serviceResult = _songService.GetPurchasedSongs();
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            var song = serviceResult.Result;
            return Ok(serviceResult.Result.Select(s => new SongDataTransferObject
            {
                Id = s.Id,
                AlbumId = s.AlbumId,
                Artist = s.ArtistName,
                Duration = s.Duration,
                Name = s.Name,
                Price = s.Price,
                Popularity = s.Popularity
            }));
        }

        [HttpPut]
        [Route("{songId}/purchase")]
        public ActionResult<SongDataTransferObject> Purchase(int songId)
        {
            var serviceResult = _songService.Purchase(songId);
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            return Ok(serviceResult.Result);
        }

        [HttpGet]
        [Route("{songId}")]
        public ActionResult<SongDataTransferObject> Get(int songId)
        {
            var serviceResult = _songService.GetById(songId);
            if (serviceResult.ResponseCode != ResponseCode.Success)
                return BadRequest(serviceResult.Error);

            var album = serviceResult.Result;
            return Ok(new SongDataTransferObject
            {
                Artist = album.ArtistName,
                Id = album.Id,
                Name = album.Name,
                Price = album.Price,
                AlbumId = album.AlbumId,
                Duration = album.Duration,
                Popularity = album.Popularity
            });
        }
    }
}
