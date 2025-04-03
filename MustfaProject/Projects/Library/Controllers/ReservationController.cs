using AutoMapper;
using Core.Entites;
using Core.Interfaces;
using Core.Specs;
using Library.DTOS;
using LibraryBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo.Data;
using System.Security.Claims;

namespace Library.Controllers
{
    public class ReservationController : BaseApiController
    {
        private readonly IGeneric<Reservation> _reservationRepo;
        private readonly IMapper _mapper;

        public ReservationController(IGeneric<Reservation> reservationRepo, IMapper mapper)
        {
            _reservationRepo = reservationRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservatoinDTO>>> GetReservations(string? search, string? sort)
        {
            var spec = new ReservationSpec(search, sort);
            var reservations = await _reservationRepo.GetAllWithSpec(spec);

            if (reservations == null || !reservations.Any())
            {
                return NotFound(new { Message = "Reservations not found", StatusCode = 404 });
            }

            // Map Reservation entities to ReservationDTOs
            var reservationDtos = _mapper.Map<IEnumerable<ReservatoinDTO>>(reservations);

            return Ok(reservationDtos);
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ReservatoinDTO>> GetReservation(string userName)
        {
            var spec = new ReservationSpec(userName);
            var reservation = await _reservationRepo.GetWithSpec(spec);

            if (reservation == null)
            {
                return NotFound(new { Message = "Reservation not found", StatusCode = 404 });
            }

            var reservationDto = _mapper.Map<ReservatoinDTO>(reservation);

            return Ok(reservationDto);
        }

        [HttpPost]
        public async Task<ActionResult<ReservatoinDTO>> AddReservation(ReservatoinDTO reservationDto)
        {
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 


            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { Message = "User ID is missing or invalid", StatusCode = 400 });
            }

           
            var createReservation = new Reservation
            {
                ReservationDate = DateTime.Now,
                Status = reservationDto.Status,
                UserReservationName = reservationDto.UserReservationName,
                UserId = userId
                
            };

            // Save the reservation to the repository
            await _reservationRepo.AddAsync(createReservation);

            // Map the created reservation to a DTO
            var createdReservationDto = _mapper.Map<ReservatoinDTO>(createReservation);

            // Return a Created response with the reservation data
            return CreatedAtAction(nameof(GetReservation), new { userName = createdReservationDto.UserReservationName }, createdReservationDto);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(string id)
        {
            await _reservationRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
