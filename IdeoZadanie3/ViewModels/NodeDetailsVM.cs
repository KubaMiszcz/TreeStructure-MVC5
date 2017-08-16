using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IdeoZadanie3.Models;

namespace IdeoZadanie3.ViewModels
{
   [NotMapped]
   public class NodeDetailsVM : Node
   {
      public Node node { get; set; }
      [Display(Name = "Parent Name")]
      public String ParentName { get { return this.Parent == null ? "" : this.Parent.Name; } }
      [Display(Name = "Number of childrens")]
      public int NumChildrens { get { return node.Childrens.Count; } }
      [Display(Name = "Childrens Names")]
      public String ChildrensString
      {
         get
         {
            String str = "";
            foreach (Node n in node.Childrens)
            {
               str += (n.Name + ", ");
            }
            return str;
         }
      }
      public new String IsLeaf { get { return (node.IsLeaf ? YesNoEnum.Yes : YesNoEnum.No).ToString(); } }
      public enum YesNoEnum { No = 0, Yes = 1 }
      public NodeDetailsVM() { }
      public NodeDetailsVM(Node _n)
      {
         node = _n;
         ID = node.ID;
         Name = node.Name;
         Note = node.Note;
         Childrens = node.Childrens;
      }
   }
}