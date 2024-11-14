using System.ComponentModel.DataAnnotations;

namespace LW_12_WebApplication.Models
{
    public class CalcModel
    {
        [Required(ErrorMessage = "error")]
        public double Num1 { get; set; }

        [Required(ErrorMessage = "error")]
        public double Num2 { get; set; }

        [Required(ErrorMessage = "error")]
        public OperationType Operation { get; set; }
    }
}
