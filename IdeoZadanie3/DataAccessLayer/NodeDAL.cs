using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IdeoZadanie3.Models;
using IdeoZadanie3.ViewModels;

namespace IdeoZadanie3.DataAccessLayer
{
   public class NodeDAL : DbContext
   {
      public DbSet<Node> Nodes { get; set; }

      protected override void OnModelCreating(DbModelBuilder modelbuilder)
      {
         //modelbuilder.Ignore<NodesTreeVM>();
         modelbuilder.Entity<Node>().ToTable("tblNodes");
         base.OnModelCreating(modelbuilder);
      }
   }

}