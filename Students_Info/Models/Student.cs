using System.ComponentModel.DataAnnotations;

namespace Students_Info.Models
{
    public class Student
    {
        [Key]
        public int StdId { get; set; }
        public string StdName { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }
        public int  StdAge { get; set; }
        public string HomeAddress { get; set; }
        public DateTime RegistrationDate  { get; set; }

        public bool IsDeleted { get; set; }


    }
}
