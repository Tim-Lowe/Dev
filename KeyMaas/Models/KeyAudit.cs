using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyMaas.Models.Interfaces;

namespace KeyMaas.Models
{
  [Table("KeyAudit")]
  public class KeyAudit : IAudit
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AuditID { get; set; }
    public string AuditNote { get; set; }

    public int ID { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int PersonID { get; set; }

    public KeyState State { get; set; }

    public bool Illuminated { get; set; }

    public DateTime Updated { get; set; }
  }
}