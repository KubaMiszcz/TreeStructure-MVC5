using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdeoZadanie3.Models;
using IdeoZadanie3.DataAccessLayer;
using System.ComponentModel.DataAnnotations.Schema;
using IdeoZadanie3.Services;

namespace IdeoZadanie3.ViewModels
{
   [NotMapped]
   public class NodesIndexVM
   {
      //public int ID { get; set; }
      public List<Node> NodeList { get; set; } = new List<Node>();
      public List<Node> TreeNodeList { get; set; } = new List<Node>();
      //public Dictionary<Node, Tuple<String,bool>> NodeProperties2 { get; set; }
      public Dictionary<int, AdditionalNodeProperties> AddtnlNodeProps { get; set; } = new Dictionary<int, AdditionalNodeProperties>();
      public class AdditionalNodeProperties
      {
         public String NameToDisplay;
         public int NodeLevel;
         public String RowNameID;
         public String DisableIfLeaf;
      }
      private String _levelMarker = "- - - - - ";

      public NodesIndexVM()
      {
      }
      public NodesIndexVM(NodeDAL db) : this(MyServices.NodesFromDBToList(db))
      {
      }
      public NodesIndexVM(List<Node> nodeList)
      {
         NodeList = nodeList;
         List<Node> rootList = NodeList.Where(x => x.Parent == null).ToList();
         SetNodeLevelAndPrefix(rootList);
      }

      /// <summary>
      /// for recurency
      /// </summary>
      /// <param name="nodeList">list of childrens from upper level</param>
      /// <param name="parentLev"></param>
      /// <param name="sortedList">output sorted list (tree)</param>
      public void SetNodeLevelAndPrefix(List<Node> nodeList)
      {
         foreach (Node node in nodeList)
         {
            AdditionalNodeProperties anp = new AdditionalNodeProperties();
            //node.Childrens = NodeList.Where(x => x.Parent == node).ToList();
            if (node.Parent != null)
            {
               anp.NameToDisplay = " " + node.Name + " (Parent:" + node.Parent.Name + ")";
               AdditionalNodeProperties anpParent = AddtnlNodeProps[node.Parent.ID];
               anp.RowNameID = anpParent.RowNameID + "-" + node.Name +"id"+ node.ID;
               //anp.classJSparamRowNameID = "Fold-" + node.Name + node.ID;
               int parentLev = AddtnlNodeProps[node.Parent.ID].NodeLevel;
               anp.NodeLevel = parentLev + 1;
               for (int i = 0; i < anp.NodeLevel; i++)
               {
                  anp.NameToDisplay = _levelMarker + anp.NameToDisplay;
               }

            }
            else
            {
               anp.NameToDisplay = " " + node.Name + " (Parent: root)";
               anp.RowNameID = "Fold-Root";
               //anp.classJSparamRowNameID = "Fold-" + node.Name + node.ID;
            }
            //anp.DisableIfLeaf = node.IsLeaf ? "disabled" : "";
            AddtnlNodeProps[node.ID] = anp;
            TreeNodeList.Add(node);
            if (!node.IsLeaf)
            {
               SetNodeLevelAndPrefix(node.Childrens);
            }
         }
      }

      //public void UpdateTree()
      //{
      //   List<Node> rootList = NodeList.Where(x => x.Parent == null).ToList();
      //   SetNodeLevelAndPrefix(rootList);
      //}
   }
}
