using System;

namespace ConsoleApp.Domain.Dtos
{
    public class UserInputLogDto
    {
        public int? UserInputLogId { get; set; }
        public string UserInput { get; set; }
        public decimal? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
