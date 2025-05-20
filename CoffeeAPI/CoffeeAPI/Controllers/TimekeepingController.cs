﻿using Application.SeedWorks;
using AutoMapper;
using Data.DTO.Shifts;
using Data.DTO.Timekeeping;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimekeepingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public TimekeepingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllTimekeeping")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cate = await _unitOfWork.TimekeepingRepository.GetAllAsync();
                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(TimekeepingCreateUpdateRequest request)
        {
            try
            {
                var i = _mapper.Map<Timekeeping>(request);
                _unitOfWork.TimekeepingRepository.Add(i);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ep = await _unitOfWork.TimekeepingRepository.GetByIdAsync(id);
                _unitOfWork.TimekeepingRepository.Remove(ep);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(TimekeepingCreateUpdateRequest request)
        {
            try
            {
                var cate = await _unitOfWork.TimekeepingRepository.GetByIdAsync(request.TimekeepingID);
                if (cate != null)
                {
                    var i = _mapper.Map(request, cate);
                    await _unitOfWork.CompleteAsync();
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
