using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdeoZadanie3.DataAccessLayer;
using IdeoZadanie3.Models;
using IdeoZadanie3.ViewModels;
using IdeoZadanie3.Services;

namespace IdeoZadanie3.Controllers
{
   public class NodesController : Controller
   {
      private NodeDAL db = new NodeDAL();

      // GET: Nodes
      public ActionResult Index(int? id)
      {
         NodesIndexVM vm = new NodesIndexVM(db);
         if (id != null)
         {
            Node n = vm.NodeList.FindAll(x => x.ID == id).First();
            n.Childrens=n.Childrens.OrderBy(x => x.Name).ToList();
            List<Node> rootList = vm.NodeList.Where(x => x.Parent == null).ToList();
            vm.TreeNodeList = new List<Node>();
            vm.SetNodeLevelAndPrefix(rootList);
         }
         return View(vm);
      }


      //POST: UnfoldNodes
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Index(NodesIndexVM vm, int? id)
      //public ActionResult Edit2([Bind(Include = "ID,Name,Note")] Node node)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }

         List<Node> allNodes = MyServices.NodesFromDBToList(db);
         Node node = allNodes.Find(x => x.ID == id);

         if (node == null)
         {
            return HttpNotFound();
         }

         node.Childrens.Sort();
         db.Entry(node).State = EntityState.Modified;
         db.SaveChanges();
         //return RedirectToAction("Index");
         vm = new NodesIndexVM(allNodes);

         //return RedirectToAction("Index");
         return View(vm);
      }


      // GET: Nodes/Details/5
      public ActionResult Details(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }

         //List<Node> allNodes = MyServices.NodesFromDBToList(db);
         //Node node = new Node((int)id, allNodes);
         //Node node = db.Nodes.Find(id);
         //node = db.Nodes.Include(n => n.Parent).SingleOrDefault(x=>x.ID=id??0);
         //node = db.Nodes.Include(n=>n.Parent).Find(id);
         Node node = db.Nodes.Include(n => n.Parent).ToList().Where(x => x.ID == id).First();

         if (node == null)
         {
            return HttpNotFound();
         }
         NodeDetailsVM vmDetails = new NodeDetailsVM(node);
         return View(vmDetails);
      }

      // GET: Nodes/Create
      public ActionResult Create()
      {
         List<Node> allNodes = MyServices.NodesFromDBToList(db);
         Node node = new Node();
         NodeCreateVM vm = new NodeCreateVM(node, allNodes);
         return View(vm);
      }

      // POST: Nodes/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create(NodeCreateVM vmCreate)
      {
         if (ModelState.IsValid)
         {
            List<Node> allNodes = MyServices.NodesFromDBToList(db);
            if (MyServices.NameAlreadyExists(vmCreate.Name, allNodes))
            {
               ViewBag.Message = "Node with this name already exist.";
               return View("Error");
            }
            Node node = new Node();
            node.Name = vmCreate.Name;
            node.Note = vmCreate.Note;
            node.Parent = allNodes.Find(x => x.Name == vmCreate.ParentName);
            db.Nodes.Add(node);
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(vmCreate);
      }

      // GET: Nodes/Edit/5
      public ActionResult Edit(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         List<Node> allNodes = MyServices.NodesFromDBToList(db);
         Node node = allNodes.Find(x => x.ID == id);
         if (node == null)
         {
            return HttpNotFound();
         }
         NodeEditVM vmEdit = new NodeEditVM(node, allNodes);
         return View(vmEdit);
      }

      // POST: Nodes/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(NodeEditVM vm)
      //public ActionResult Edit2([Bind(Include = "ID,Name,Note")] Node node)
      {
         if (ModelState.IsValid)
         {
            List<Node> allNodes = MyServices.NodesFromDBToList(db);
            Node node = allNodes.Find(x => x.ID == vm.ID);
            node.Name = vm.Name;
            node.Note = vm.Note;
            node.Parent = allNodes.Find(x => x.Name == vm.ParentName);//i cant choose node as itself because of GET
            if (vm.CheckIfNewParentIsntMyChildren(node, node.Parent))
            {
               ViewBag.Message = "This node new parent is already his children, grandchildren or descendant.";
               return View("Error");
            }
            db.Entry(node).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(vm);
      }


      // GET: Nodes/Delete/5
      public ActionResult Delete3(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Node node = db.Nodes.Find(id);
         if (node == null)
         {
            return HttpNotFound();
         }
         return View(node);
      }

      public ActionResult Delete(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Node node = db.Nodes.Include(n => n.Parent).ToList().Where(x => x.ID == id).First();
         if (node == null)
         {
            return HttpNotFound();
         }
         NodeDeleteVM vm = new NodeDeleteVM(node);
         return View(vm);
      }

      // POST: Nodes/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         Node node = db.Nodes.Include(n => n.Parent).ToList().Where(x => x.ID == id).First();
         if (!node.IsLeaf)
         {
            ViewBag.Message = "Node has childrens, delete them first.";
            return View("Error");
         }
         db.Nodes.Remove(node);
         db.SaveChanges();
         return RedirectToAction("Index");
      }

      public ActionResult About()
      {
         return View();
      }

      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            db.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
