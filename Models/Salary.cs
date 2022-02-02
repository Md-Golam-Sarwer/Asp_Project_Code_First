using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeInformationSystem.Models
{
    public class Salary
    {
        [Key]
        public int SalaryID { get; set; }
        public int? EmployeeID { get; set; }
        //public string EmployeeName { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HouseRent { get; set; }
        public decimal TotalSalary { get { return (BasicSalary + HouseRent); } }
        public bool IsActive { get; set; }

        public virtual Employee Employee { get; set; }
    }
}