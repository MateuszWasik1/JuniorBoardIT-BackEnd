using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using JuniorBoardIT.Core.Models.Companies.Enums;

namespace JuniorBoardIT.Core.Entities
{
    public class Companies
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CID { get; set; }
        public Guid CGID { get; set; }
        public string? CName { get; set; }
        public IndustryEnum CIndustry { get; set; }
        public string? CDescription { get; set; }
        public string? CEmail { get; set; }
        public string? CAddress { get; set; }
        public string? CCity { get; set; }
        public string? CCountry { get; set; }
        public string? CPostalCode { get; set; }
        public string? CPhoneNumber { get; set; }
        public string? CNIP { get; set; }
        public string? CRegon { get; set; }
        public string? CKRS { get; set; }
        public string? CLI { get; set; }
        public int CFoundedYear { get; set; }
        public CompanyEmpNoEnum CEmployeesNo { get; set; }
        public DateTime? CCreatedAt { get; set; }
        public DateTime? CUpdatedAt { get; set; }
    }
}