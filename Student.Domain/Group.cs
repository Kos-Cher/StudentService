using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Student.Domain
{
    [Microsoft.EntityFrameworkCore.Index(nameof(Name))]
    public partial class Group
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Group()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
