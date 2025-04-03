using AutoMapper;
using Core.Entites;
using Core.Interfaces;
using Core.Specs;
using Library.DTOS;
using LibraryBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.api.Attributes;

namespace Library.Controllers
{
    public class NotificationController : BaseApiController
    {
        private readonly IGeneric<Notification> _notificationRepo;
        private readonly IMapper _mapper;

        public NotificationController(IGeneric<Notification> notificationRepo, IMapper mapper)
        {
            _notificationRepo = notificationRepo;
            _mapper = mapper;
        }
        //[CacheAttributes(30)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetNotifications(string? sort)
        {
            var spec = new NotificationSpec(sort);
            var notifications = await _notificationRepo.GetAllWithSpec(spec);

            if (notifications == null || !notifications.Any())
            {
                return NotFound(new { Message = "Notifications not found", StatusCode = 404 });
            }

            var notificationDtos = _mapper.Map<IEnumerable<NotificationDTO>>(notifications);

            return Ok(notificationDtos);
        }
       // [CacheAttributes(30)]
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDTO>> GetNotification(string id)
        {
            var spec = new NotificationSpec(id);
            var notification = await _notificationRepo.GetWithSpec(spec);

            if (notification == null)
            {
                return NotFound(new { Message = "Notification not found", StatusCode = 404 });
            }

            var notificationDto = _mapper.Map<NotificationDTO>(notification);

            return Ok(notificationDto);
        }

        [HttpPost]
        public async Task<ActionResult<NotificationDTO>> AddNotification(NotificationDTO notificationDto)
        {
            var createdNotification = new Notification
            {
                CreatedAt = DateTime.UtcNow,
                Message = notificationDto.Message,
                Type = notificationDto.Type,
                IsRead = notificationDto.IsRead,
            };



            await _notificationRepo.AddAsync(createdNotification);


            var createdNotificationDto = _mapper.Map<NotificationDTO>(createdNotification);

            return CreatedAtAction(nameof(GetNotification), new { id = createdNotification.Id }, createdNotificationDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNotification(string id)
        {
            await _notificationRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
