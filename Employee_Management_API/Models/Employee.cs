﻿using System.ComponentModel.DataAnnotations;

namespace Employee_Management_API.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        public string? First_Name { get; set; }
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        public string? Last_Name { get; set; }
        public string? Email_Id { get; set; }
        public string? Mobile_Number { get; set; }
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        public string? Address { get; set; }
        public int? Zip_Code { get; set; }
    }
}
