using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Todo
    {
        [Key]
        public int ID { get; set; }
        public int UserId { get; set; }
        public virtual User? UserName { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ShortDesc { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Status Status { get; set; }
    }
    public enum Status : int
    {
        [Display(Name = "Chưa start")]
        NotStarted = 0,
        [Display(Name = "Đang làm")]
        Doing = 1,
        [Display(Name = "Đã xong")]
        Done = 2,
        [Display(Name = "Đã hủy")]
        Canceled = 3
    }
}
