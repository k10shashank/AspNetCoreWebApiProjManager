using AspNetCoreWebApiProjManager.Entities;
using AspNetCoreWebApiProjManager.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreWebApiProjManager.Controller
{
    public class StudentController : ApiBaseController  
    {
        private readonly IEnumerable<StudentModel> Students = new List<StudentModel>()
        {
            new() { ID_REG = "16BCE1237", NAME = "Shashank Keshari", DOB = new (1998, 4, 10), POB = "Varanasi", CGPA = 8.8 },
            new() { ID_REG = "16BCE1249", NAME = "Suryanshu Singh", DOB = new (1998, 1, 26), POB = "Gazipur", CGPA = 9.1 },
            new() { ID_REG = "16BCE1305", NAME = "Mayank Singh", DOB = new (1998, 7, 30), POB = "Prayagraj", CGPA = 9.6 },
            new() { ID_REG = "16BCE1032", NAME = "Aditya Das", DOB = new (1998, 12, 26), POB = "Bhilai", CGPA = 8.2 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<StudentModel>> Get()
        {
            return Ok(Students);
        }

        [HttpGet("Csv")]
        public FileStreamResult DownloadCsv()
        {
            return CsvFileHandler.DownloadCsv(Students.ToList().ToDataTable(), "StudentsDownload", (x) => Functions.GetFileColumnName(x));
        }
    }
}
