using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Email.Models
{
    public class Student
    {
        [Display(Name ="Id Usuario")]
        public string student_id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [Display(Name ="Apellidos")]
        public string Apellido { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        [Range(11111111,999999999)]
        public int Telefono { get; set; }
        [Required]
        public string Rut { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}