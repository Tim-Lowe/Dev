using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyMaas
{
  public static class Globals
  {
    public static string ConnectionString =>
      System.Configuration.ConfigurationManager.ConnectionStrings["RouteContext"].ConnectionString;
  }
}