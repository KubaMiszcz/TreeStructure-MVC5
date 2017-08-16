using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdeoZadanie3.Models
{
   public class Node
   {
      [Key]
      public int ID { get; set; }
      [Required]
      [StringLength(255, MinimumLength = 2)]
      public String Name { get; set; } = "";
      public String Note { get; set; } = "";
      //[ForeignKey("ID")]
      public Node Parent { get; set; }
      public List<Node> Childrens { get; set; } = new List<Node>();
      //[NotMapped]
      public bool IsLeaf { get { return Childrens.Count > 0 ? false : true; } }
      public btnProp DisableIfLeaf
      {
         get
         {
            btnProp b = new btnProp();
            if (IsLeaf)
            {
               b.State = btnDisable.disabled.ToString();
               b.Color = btnDiColor.darkgray.ToString();
            }
            else
            {
               b.State = "";
               b.Color = "";
            }
            return b;
         }
      }
      public btnProp DisableIfNotLeaf
      {
         get
         {
            btnProp b = new btnProp();
            if (IsLeaf)
            {
               b.State = "";
               b.Color = "";
            }
            else
            {
               b.State = btnDisable.disabled.ToString();
               b.Color = btnDiColor.darkgray.ToString();
            }
            return b;
         }
      }
      public struct btnProp
      {
         public String State;
         public String Color;
      }
      private enum btnDisable { disabled };
      private enum btnDiColor { darkgray };



      //cstr n methods
      public Node() { }
      public Node(int id, List<Node> nodeList) : this()
      {
         if (id > 0)
         {
            Node node = nodeList.Find(x => x.ID == id);
            ID = node.ID;
            Name = node.Name;
            Note = node.Note;
            Childrens = node.Childrens;
         }
         else
         {  //new node
            Name = "";
            Note = "";
         }
      }
      public Node(Node node, List<Node> nodeList) : this(node.ID, nodeList) { }
   }
}
