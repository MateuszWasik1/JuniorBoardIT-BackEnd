using JuniorBoardIT.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorBoardIT.Core.Entities
{
    public class Reports
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RID { get; set; }
        public Guid RGID { get; set; }
        public Guid RJOGID { get; set; }
        public Guid? RReporterGID { get; set; }
        public Guid? RSupportGID { get; set; }
        public DateTime RDate { get; set; }
        public string? RReasons { get; set; }
        public string? RText { get; set; }
        public ReportsStatusEnum? RStatus { get; set; }
    }
}