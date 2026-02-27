using System.ComponentModel.DataAnnotations;

namespace TestDeploy.Models
{
    public class TestData
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}