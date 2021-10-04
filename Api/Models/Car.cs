using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    [Index(nameof(RegNumber), IsUnique = true)]
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required, Range(0, 100, ErrorMessage = "Fuel must be between 0 and 100.")]
        public double Fueld { get; set; }

        [Required, RegularExpression(@"\d{4} [А-Я]{2}-\d", ErrorMessage = "Invalid registration number.")]
        public string RegNumber { get; set; }

        [MaxLength(2048, ErrorMessage = "URL too long.")]
        public string ImageUrl { get; set; }

        [Required]
        public int ProviderId { get; set; }

        [ForeignKey(nameof(ProviderId))]
        public Provider Provider { get; set; }
    }
}