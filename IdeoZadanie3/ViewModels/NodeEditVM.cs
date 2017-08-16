using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IdeoZadanie3.Models;
using IdeoZadanie3.Services;

namespace IdeoZadanie3.ViewModels
{
   [NotMapped]
   public class NodeEditVM : NodeCreateVM
   {
      public NodeEditVM() : base() { }
      public NodeEditVM(Node node, List<Node> nodeList) : base(node, nodeList)
      {
         PotentialParentsNames.RemoveAll(x => x == node.Name);//remove itself from list
         //MyServices.PopulateChildrens(node,nodeList);
      }

      public bool CheckIfNewParentIsntMyChildren(Node node, Node newParent)
      {
         Node parent = newParent;
         while (parent != null)
         {
            if (parent.Parent != null && node.Name == parent.Parent.Name) return true;
            parent = parent.Parent;
         }
         return false;
      }
   }
}

//public int ID { get; set; }
//[Required]
//[StringLength(255, MinimumLength = 2)]
//public String Name { get; set; } = "";
//public String Note { get; set; } = "";
//[Display(Name = "Parent Name")]
//public String ParentName { get; set; } = "";
////public List<Node> PotentialParents { get; set; } = new List<Node>();
//public List<String> PotentialParentsNames { get; set; } = new List<String>();
//public NodeEditVM() { }
//public NodeEditVM(Node node, List<Node> nodeList)
//{
//   ID = node.ID;
//   Name = node.Name;
//   Note = node.Note;
//   ParentName = node.Parent != null ? node.Parent.Name : "this node is root";
//   nodeList.RemoveAll(x => x.ID == ID);//remove itself
//   foreach (Node item in nodeList)
//   {
//      PotentialParentsNames.Add(item.Name);
//   }
//   PotentialParentsNames.Add("");//for add as root
//   PotentialParentsNames.Sort();
//}