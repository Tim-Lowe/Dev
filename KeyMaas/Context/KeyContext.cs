using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Audit.EntityFramework;
using AutoMapper;
using KeyMaas.Models;

namespace KeyMaas.Context
{
  public class KeyContext : DbContext
  {
    public DbSet<Key> Keys { get; set; }
    public DbSet<KeyAudit> KeyAudits { get; set; }

    public KeyContext() : base("Name=RouteContext") { }

    public override int SaveChanges()
    {
      foreach (var entry in ChangeTracker.Entries())
      {
        if (!(entry.Entity is Key))
          continue;

        if (entry.State == EntityState.Unchanged)
          continue;
        
        string auditNote = "";
        if (entry.State == EntityState.Added)
          auditNote = "INSERT";

        if (entry.State == EntityState.Modified)
          auditNote = "UPDATED";

        if (entry.State == EntityState.Deleted)
          auditNote = "DELETED";

        base.SaveChanges(); //Persist Key 
        var keyAudit = MappKeyToKeyAudit((Key)entry.Entity);
        keyAudit.AuditNote = auditNote;
        KeyAudits.Add(keyAudit);
      }

      return base.SaveChanges();
    }
    
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      Database.SetInitializer<KeyContext>(null);
      base.OnModelCreating(modelBuilder);
    }

    private KeyAudit MappKeyToKeyAudit(Key key)
    {
      var config = new MapperConfiguration(cfg => {
        cfg.CreateMap<Key, KeyAudit>();
      });
      IMapper iMapper = config.CreateMapper();

      return iMapper.Map<Key, KeyAudit>(key);
    }
  }
}