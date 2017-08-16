using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdeoZadanie3.Models;

namespace IdeoZadanie3.ViewModels
{
   public class NodeDeleteVM:NodeDetailsVM
   {
      public NodeDeleteVM() { }
      public NodeDeleteVM(Node node):base(node) { }
   }
}