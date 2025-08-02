using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JuniorBoardIT.Core.Entities
{
    public class FavoriteJobOffers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FJOID { get; set; }
        public Guid FJOGID { get; set; }
        public Guid FJOUGID { get; set; }
        public Guid FJOJOGID { get; set; }
    }
}