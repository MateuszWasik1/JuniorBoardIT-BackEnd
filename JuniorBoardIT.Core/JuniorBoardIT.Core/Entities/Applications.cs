using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorBoardIT.Core.Entities
{
    public class Applications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AID { get; set; }
        public Guid AGID { get; set; }
        public Guid? AUGID { get; set; }
        public Guid AJOGID { get; set; }
        public DateTime AApplicationDate { get; set; }
    }
}