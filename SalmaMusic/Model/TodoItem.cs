using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalmaMusic.Model
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public bool Finished { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTask { get; set; }
        public DateTime FinishedTime { get; set; }
        public string? Comment { get; set; }

        public TodoItem()
        {
            
        }
        public TodoItem(string TaskName, DateTime created, string comment = null)
        {
            this.TaskName = TaskName;
            this.CreatedTask = created;
            this.Active = true;
            this.Comment = comment;
        }
    }
}
