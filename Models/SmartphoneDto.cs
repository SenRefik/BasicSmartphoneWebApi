using System.ComponentModel.DataAnnotations;

namespace FirstWeekProject.Models
{
    public class SmartphoneDto
    {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public bool? StockStatus { get; set; }

    }
}
