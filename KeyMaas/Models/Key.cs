using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyMaas.Models
{
  [Table("[Key]")]
  public class Key
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int PersonID { get; set; }

    public KeyState State { get; set; }

    public bool Illuminated { get; set; }

    public DateTime Updated { get; set; }
  }
}