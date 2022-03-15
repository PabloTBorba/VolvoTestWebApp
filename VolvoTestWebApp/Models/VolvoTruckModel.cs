using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolvoTestWebApp.Models
{
    /**
     * Basic object model for the Volvo Truck
     * Also contain some non-mapped elements required for the create/edit forms
     */
    [Table("VolvoTruck")]
    public class VolvoTruckModel
    {
        [Key]
        public int Id { get; set; }

        public string? Description { get; set; }

        public string Model { get; set; } = "FH";

        [Display(Name = "Fabrication Year")]
        public int FabricationYear { get; set; } = DateTime.Now.Year;

        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }

        [NotMapped]
        public bool IsEdit { get; set; }

        [NotMapped]
        public List<SelectListItem> ModelTypes { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Selected = true,
                Text = "FH Model",
                Value = "FH"
            },
            new SelectListItem
            {
                Selected = false,
                Text = "FM Model",
                Value = "FM"
            },
            new SelectListItem
            {
                Selected = false,
                Text = "VNL Model",
                Value = "VNL"
            },
            new SelectListItem
            {
                Selected = false,
                Text = "VNR Model",
                Value = "VNR"
            }
        };

        [NotMapped]
        public List<SelectListItem> ModelYears { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Selected = true,
                Text = DateTime.Now.Year.ToString(),
                Value = DateTime.Now.Year.ToString()
            },
            new SelectListItem
            {
                Selected = false,
                Text = DateTime.Now.AddYears(1).Year.ToString(),
                Value = DateTime.Now.AddYears(1).Year.ToString()
            }
        };
    }
}
