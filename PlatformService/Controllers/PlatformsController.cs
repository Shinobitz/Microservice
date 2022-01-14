using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            Console.WriteLine("--> Gettig all platforms");
            var platformsFromData = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformsFromData));
        }

        [HttpGet("{id}", Name ="GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformFromData = _repository.GetPlatformById(id);
            if (platformFromData != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformFromData));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto newPlatform)
        {
            var platformForRepo = _mapper.Map<Platform>(newPlatform);
            _repository.CreatePlatform(platformForRepo);
            _repository.SaveChanges();
            var platformForRead = _mapper.Map<PlatformReadDto>(platformForRepo);
            return CreatedAtRoute(nameof(GetPlatformById), new {id = platformForRead.Id} , platformForRead);
        }
    }
}