using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required ")]

        public string Name { get; set; }

        [Column("NationalId", TypeName = "bigint")]
        public long NationalId { get; set; }

        [DataType(DataType.Date ) ]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true )]
        public DateTime BirthBade { get; set; }

        [Required(ErrorMessage = "Age is Required")]
      // [Range(18, 100, ErrorMessage = "Age must be between 18-100 in years.")]
        public int Age { get; set; } 

        [Required(ErrorMessage = "you must select any option ")]

        public string Account { get; set; }

        [Required(ErrorMessage = "you must select any option ")]

        public string LineOfBusiness { get; set; }

        [Required(ErrorMessage = "you must select any option")]

        public string Language { get; set; }

        [Required(ErrorMessage = "you must select any option ")]

        public string Languagelevel { get; set; }

      
   
  
    }
}
