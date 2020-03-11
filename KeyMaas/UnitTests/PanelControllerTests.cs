using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using KeyMaas.Context;
using KeyMaas.Controllers;
using KeyMaas.Models;
using NUnit.Framework;

namespace KeyMaas.UnitTests
{
  public class PanelControllerTests
  {
    private List<int> keys;

    [OneTimeSetUp]
    public void FixtureSetup()
    {
      keys = new List<int>();
    }

    [Test]
    public void Create_Returns_CorrectParameterData()
    {
      var panelController = new PanelController();

      var key = new Key
      {
        Description = "TestDescription1",
        Name = "TestName1",
        Illuminated = true,
        State = KeyState.In,
        PersonID = 0
      };

      panelController.Create(null, key);

      Key returnKey;
      KeyAudit returnKeyAudit;
      using (var context = new KeyContext())
      {
        returnKey = context.Keys.FirstOrDefault(r => r.Name.Equals("TestName1"));
        returnKeyAudit = context.KeyAudits.FirstOrDefault(r => r.Name.Equals("TestName1"));
      }

      Assert.That(returnKey != null, "No object returned");

      keys.Add(returnKey.ID);

      bool valuesMatch = key.Description == returnKey.Description;
      Assert.IsTrue(valuesMatch, "Description of return object did not match persisted object");

      valuesMatch = key.Illuminated == returnKey.Illuminated;
      Assert.IsTrue(valuesMatch, "Illuminated of return object did not match persisted object");

      valuesMatch = key.PersonID == returnKey.PersonID;
      Assert.IsTrue(valuesMatch, "PersonID of return object did not match persisted object");

      valuesMatch = key.State == returnKey.State;
      Assert.IsTrue(valuesMatch, "State of return object did not match persisted object");

      Assert.That(returnKeyAudit != null, "No audit object returned");

      valuesMatch = returnKeyAudit.AuditNote.Equals("INSERT");
      Assert.IsTrue(valuesMatch, "No audit note was attached to audit record");

      valuesMatch = key.Description == returnKeyAudit.Description;
      Assert.IsTrue(valuesMatch, "Description of return audit object did not match object");

      valuesMatch = key.Illuminated == returnKeyAudit.Illuminated;
      Assert.IsTrue(valuesMatch, "Illuminated of return audit object did not match object");

      valuesMatch = key.PersonID == returnKeyAudit.PersonID;
      Assert.IsTrue(valuesMatch, "PersonID of return audit object did not match object");

      valuesMatch = key.State == returnKeyAudit.State;
      Assert.IsTrue(valuesMatch, "State of return audit object did not match  object");
    }

    [Test]
    public void Edit_Returns_CorrectParameterData()
    {
      var panelController = new PanelController();

      var key = new Key
      {
        Description = "TestDescription2",
        Name = "TestName2",
        Illuminated = true,
        State = KeyState.In,
        PersonID = 0
      };

      panelController.Create(null, key);

      using (var context = new KeyContext())
      {
        key = context.Keys.FirstOrDefault(r => r.Name.Equals("TestName2"));
      }

      Assert.That(key != null, "No object returned");

      keys.Add(key.ID);

      key.Name = "UpdatedName";
      key.Description = "UpdatedDescription";

      //Update existing key
      panelController.Edit(key.ID, key);

      Key returnKey;
      KeyAudit returnKeyAudit;
      using (var context = new KeyContext())
      {
        returnKey = context.Keys.FirstOrDefault(r => r.ID == key.ID);
        returnKeyAudit = context.KeyAudits.FirstOrDefault(r => r.ID == key.ID && r.AuditNote.Equals("UPDATED"));
      }

      Assert.That(returnKey != null, "No object returned");

      bool valuesMatch = key.Description == returnKey.Description;
      Assert.IsTrue(valuesMatch, "Description of return object did not match persisted object");

      valuesMatch = key.Illuminated == returnKey.Illuminated;
      Assert.IsTrue(valuesMatch, "Illuminated of return object did not match persisted object");

      valuesMatch = key.PersonID == returnKey.PersonID;
      Assert.IsTrue(valuesMatch, "PersonID of return object did not match persisted object");

      valuesMatch = key.State == returnKey.State;
      Assert.IsTrue(valuesMatch, "State of return object did not match persisted object");

      Assert.That(returnKeyAudit != null, "No audit object returned");

      valuesMatch = returnKeyAudit.AuditNote.Equals("UPDATED");
      Assert.IsTrue(valuesMatch, "No audit note was attached to audit record");

      valuesMatch = key.Description == returnKeyAudit.Description;
      Assert.IsTrue(valuesMatch, "Description of return audit object did not match object");

      valuesMatch = key.Illuminated == returnKeyAudit.Illuminated;
      Assert.IsTrue(valuesMatch, "Illuminated of return audit object did not match object");

      valuesMatch = key.PersonID == returnKeyAudit.PersonID;
      Assert.IsTrue(valuesMatch, "PersonID of return audit object did not match object");

      valuesMatch = key.State == returnKeyAudit.State;
      Assert.IsTrue(valuesMatch, "State of return audit object did not match  object");
    }

    [OneTimeTearDown]
    public void FixtureTearDown()
    {
      using (var context = new KeyContext())
      {
        foreach (var id in keys)
        {
          var key = context.Keys.FirstOrDefault(k => k.ID == id);
          context.Keys.Remove(key);
          context.SaveChanges();

          var keyAudits = context.KeyAudits.Where(ke => ke.ID == id);
          context.KeyAudits.RemoveRange(keyAudits);
          context.SaveChanges();
        }
      }
    }
  }
}