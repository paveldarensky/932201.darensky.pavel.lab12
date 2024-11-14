using System.ComponentModel.DataAnnotations;

namespace LW_12_WebApplication.Models
{
    public enum OperationType
    {
        [Display(Name = "+")]
        Add,
        [Display(Name = "-")]
        Subtract,
        [Display(Name = "*")]
        Multiply,
        [Display(Name = "/")]
        Divide
    }
}

