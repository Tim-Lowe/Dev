using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyMaas.Models.Interfaces
{
  interface IAudit
  {
    int AuditID { get; set; }
    string AuditNote { get; set; }
  }
}
