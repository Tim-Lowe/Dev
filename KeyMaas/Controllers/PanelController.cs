using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using KeyMaas.Context;
using KeyMaas.Models;

namespace KeyMaas.Controllers
{
  public class PanelController : Controller
  {
    public ActionResult Index()
    {
      using (var context = new KeyContext())
      {
        return View(context.Keys.ToList());
      }
    }

    public ActionResult Edit(int? id)
    {
      using (var context = new KeyContext())
      {
        return View(context.Keys.FirstOrDefault(k => k.ID == id));
      }
    }

    [HttpPost]
    public ActionResult Edit(int id, Key editKey)
    {
      try
      {
        editKey.Updated = DateTime.Now;
        
        using (var context = new KeyContext())
        {
          context.Entry(editKey).State = EntityState.Modified;
          context.SaveChanges();
        }
        return RedirectToAction("Index");
      }
      catch(Exception e)
      {
        return View("Error", e);
      }
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(int? id, Key newKey)
    {
      try
      {
        newKey.Updated = DateTime.Now;
        
        using (var context = new KeyContext())
        {
          context.Keys.Add(newKey);
          context.SaveChanges();
        }
        return RedirectToAction("Index");
      }
      catch (Exception e)
      {
        return View("Error", e);
      }
    }

    public ActionResult Delete(int? id)
    {
      using (var context = new KeyContext())
      {
        return View(context.Keys.FirstOrDefault(k => k.ID == id));
      }
    }

    [HttpPost]
    public ActionResult Delete(int id, Key deleteKey)
    {
      try
      {
        using (var context = new KeyContext())
        {
          Key key = context.Keys.FirstOrDefault(k => k.ID == id);
          context.Keys.Remove(key);
          context.SaveChanges();
        }
        return RedirectToAction("Index");
      }
      catch (Exception e)
      {
        return View("Error", e);
      }
    }
  }
}