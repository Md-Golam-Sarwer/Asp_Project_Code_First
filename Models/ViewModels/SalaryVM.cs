using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EmployeeInformationSystem.Models;

namespace EmployeeInformationSystem.Models.ViewModels
{
    public class SalaryVM
    {
        public int SalaryID { get; set; }
        public int? EmployeeID { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HouseRent { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalSalary { get { return (BasicSalary + HouseRent); } }
        public bool IsActive { get; set; }

        //public string EmployeeName { get; set; }

        public virtual Employee Employee { get; set; }
    }
}