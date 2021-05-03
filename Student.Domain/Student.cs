using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Student.Domain
{
    [Microsoft.EntityFrameworkCore.Index(nameof(StudentId), IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(Gender))]
    [Microsoft.EntityFrameworkCore.Index(nameof(FirstName))]
    [Microsoft.EntityFrameworkCore.Index(nameof(LastName))]
    [Microsoft.EntityFrameworkCore.Index(nameof(MiddleName))]
    public partial class Student
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "The Gender must be 1 characters.")]
        [RegularExpression("M|F", ErrorMessage = "The Gender must be either 'M' or 'F' only.")]
        public string Gender { get; set; }
        [MaxLength(40)]
        public string FirstName { get; set; }
        [MaxLength(40)]
        public string LastName { get; set; }
        [MaxLength(60)]
        public string MiddleName { get; set; }
        [MinLength(6)]
        [MaxLength(16)]
        public string StudentId { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public Student()
        {
            this.Id = Guid.NewGuid();
        }

    }
}

