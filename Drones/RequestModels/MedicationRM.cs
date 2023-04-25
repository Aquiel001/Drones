using Drones.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Drones.RequestModels
{
    public class MedicationRM
    {
        [RegularExpression(@"\w*\d*[-_\w]*", ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; }
        [Range(0,(double)decimal.MaxValue)]
        public decimal Weight { get; set; }
        [RegularExpression(@"[A-Z\d-]*", ErrorMessage = "Characters are not allowed.")]
        public string Code { get; set; }
        public string Image { get; set; }
    }
}
