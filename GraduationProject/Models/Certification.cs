using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Models
{
    public class Certification
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public DateTimeOffset DateEarned { get; set; }
        public string Name { get; set; }
        public string Organizaion { get; set; }
        public CV Cv { get; set; }


    }
}
