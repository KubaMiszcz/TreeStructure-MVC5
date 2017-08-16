using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdeoZadanie3.Models;
using IdeoZadanie3.DataAccessLayer;

namespace IdeoZadanie3.Services
{
   public static class MyServices
   {
      public static List<Node> NodesFromDBToList(NodeDAL db)
      {
         return db.Nodes.ToList();
      }
      public static bool NameAlreadyExists(String checkedName, List<Node> nodeList)
      {
         return (nodeList.Find(x => x.Name == checkedName) != null) ? true : false;
      }

      public static List<Node> PopulateChildrens( Node parent, List<Node> nodeList)
      {
         return nodeList.Where(x => x.Parent == parent).ToList();
      }
   }
}