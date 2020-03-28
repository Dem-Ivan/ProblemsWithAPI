using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationAPI15_SecondStageTS_.dto;
using WebApplicationAPI15_SecondStageTS_.Models;

namespace WebApplicationAPI15_SecondStageTS_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        ApplicationContext _db;
        private readonly IMapper _mapper;

        public AnnouncementController(ApplicationContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            if (!_db.Announcements.Any())
            {
                _db.Announcements.Add(
                    new Announcement
                    {
                        OrderNumber = 1,
                        user = new User {Name = "Tom"},
                        Text = "пойду добровольно в армию!",
                        CreationDate = DateTime.Now,
                        Rating = 3
                    });
                _db.SaveChanges();
            }
        }

        //GET api/announcements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementDTO>>> Get()
        {
            var query = _db.Announcements;
            var announcements = await _mapper.ProjectTo<AnnouncementDTO>(query).ToListAsync();
            return announcements;
        }

        //GET api/announcement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementDTO>> Get(int id)
        {
            var query = _db.Announcements.Where(an => an.Id == id);
            var announcement = await _mapper.ProjectTo<AnnouncementDTO>(query).FirstOrDefaultAsync();
            
            if (announcement == null)
            {
                return NotFound();
            }
            return Ok(announcement);
        }

        //POST api/announcement
        [HttpPost]
        public async Task<ActionResult<Announcement>> Post(Announcement announcement)
        {
            if (announcement == null)
            {
                return BadRequest();
            }

            announcement.OrderNumber = _db.Announcements.Max(e => e.OrderNumber) + 1;
            announcement.CreationDate = DateTime.Now;
            _db.Announcements.Add(announcement);
            await _db.SaveChangesAsync();
            return Ok(announcement);
        }

        //PUT api/announcement
        [HttpPut]
        public async Task<ActionResult<Announcement>> Put(Announcement announcement)
        {
            if (announcement == null)
            {
                return BadRequest();
            }

            if (!_db.Announcements.Any(an => an.Id == announcement.Id))
            {
                return NotFound();
            }

            _db.Update(announcement);
            await _db.SaveChangesAsync();
            return Ok(announcement);
        }

        //DELETE api/announcement/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Announcement>> DeleteAnnouncement(int id)
        {
            Announcement announcement = _db.Announcements.Include(an => an.user).FirstOrDefault(an => an.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            _db.Announcements.Remove(announcement);
            await _db.SaveChangesAsync();
            return Ok(announcement);
        }

        //DELETE api/user/5
        //[HttpDelete]
        //public async Task<ActionResult<User>> DeleteUser(int id)
        //{
        //    User user = db.Users.FirstOrDefault(u => u.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Users.Remove(user);
        //    await db.SaveChangesAsync();
        //    return Ok(user);
        //}
    }
}