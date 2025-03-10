using System.ComponentModel.DataAnnotations;

namespace CalendarWebApiService.Models
{
    public class Notes
    {
        public Notes()
        {
            
        }
        public Notes(int id, string title, string text, DateTime? date, DateTime createDate)
        {
            Id = id;
            Title = title;
            Text = text;
            Date = date;
            CreateDate = createDate;
        }

        [Key]
        public required int Id { get; init; }
        [Required]
        public required string Title { get; set; }
        public string Text { get; set; }
        public DateTime? Date{ get; set; }
        public DateTime CreateDate{ get; init; }
    }
}
