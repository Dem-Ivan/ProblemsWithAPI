using System;

namespace WebApplicationAPI15_SecondStageTS_.dto
{
    public class AnnouncementDTO
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public UserDTO user { get; set; }
    }
}