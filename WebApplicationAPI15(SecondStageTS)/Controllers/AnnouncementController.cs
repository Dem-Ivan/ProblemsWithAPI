using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationAPI15_SecondStageTS_.Models;

namespace WebApplicationAPI15_SecondStageTS_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        ApplicationContext db;
        public AnnouncementController(ApplicationContext context)
        {
             db = context;

            if (!db.Announcements.Any())
            {
                db.Announcements.Add(
                    new Announcement
                    {
                        OrderNumber = 1,
                        user = new User { Name = "Tom" },
                        Text = "пойду добровольно в армию!",
                        CreationDate = DateTime.Now,
                        Rating = 3
                    });
                db.SaveChanges();
            }
        }

        //GET api/announcements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> Get()
        {
            return await db.Announcements.ToListAsync();
        }

        //GET api/announcement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> Get(int id)
        {
            Announcement announcement = await db.Announcements.FirstOrDefaultAsync(an => an.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }
            //return new ObjectResult(announcement);
            return Ok(announcement);
        }

        //POST api/announcement
        public async Task<ActionResult<Announcement>> Post(Announcement announcement)
        {
            if (announcement == null)
            {
                return BadRequest();
            }

            announcement.OrderNumber = db.Announcements.Max(e => e.OrderNumber) + 1;
            announcement.CreationDate = DateTime.Now;
            db.Announcements.Add(announcement);
            await db.SaveChangesAsync();
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
            if (!db.Announcements.Any(an => an.Id == announcement.Id))
            {
                return NotFound();
            }

            db.Update(announcement);
            await db.SaveChangesAsync();
            return Ok(announcement);
        }

        //DELETE api/announcement/5
        [HttpDelete]
        public async Task<ActionResult<Announcement>> DeleteAnnouncement(int id)
        {
            Announcement announcement = db.Announcements.FirstOrDefault(an => an.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            db.Announcements.Remove(announcement);
            await db.SaveChangesAsync();
            return Ok(announcement);
        }

        //DELETE api/user/5
        [HttpDelete]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            User user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}