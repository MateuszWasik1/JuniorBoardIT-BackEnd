using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JuniorBoardIT.Core.Entities
{
    public class JobOffers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JOID { get; set; }
        public Guid JOGID { get; set; }
    }
}
