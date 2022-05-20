using System;

namespace AspNetCoreWebApiProjManager.Entities
{
    public class StudentModel
    {
        public string ID_REG { get; set; }
        public string NAME { get; set; }
        public DateTime DOB { get; set; }
        public string POB { get; set; }
        public double CGPA { get; set; }
    }
}
