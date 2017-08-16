using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IdeoZadanie3.Models;

namespace IdeoZadanie3.ViewModels
{
   [NotMapped]
   public class NodeCreateVM:Node
   {
      [Display(Name = "Parent Name")]
      public String ParentName { get; set; } = "";
      public List<String> PotentialParentsNames { get; set; } = new List<String>();
      public NodeCreateVM() { }
      public NodeCreateVM(Node node, List<Node> nodeList):base(node,nodeList)
      {
         foreach (Node item in nodeList)
         {
            PotentialParentsNames.Add(item.Name);
         }
         PotentialParentsNames.Add("");//for add as root
         PotentialParentsNames.Sort();
      }
   }
}