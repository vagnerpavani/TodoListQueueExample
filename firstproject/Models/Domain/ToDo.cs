using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstproject.Models.Domain
{
    public class ToDo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }

        public long UserId {get; set;}

        public User? User;

        public bool IsNotified {get; set;}

        public ToDo(string description){
            Description = description;
            IsDone = false;
            IsNotified = false;
        }
    }
}
