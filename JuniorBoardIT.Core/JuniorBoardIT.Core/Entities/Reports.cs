using JuniorBoardIT.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organiser.Cores.Entities
{
    public class Reports
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RID { get; set; }
        public Guid RGID { get; set; }
        public int RUID { get; set; }
        public string? RAUIDS { get; set; }
        public DateTime RDate { get; set; }
        public string? RReasons { get; set; }
        public string? RText { get; set; }
        public ReportsStatusEnum? RStatus { get; set; }
    }
}