using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab15.Models;

namespace Lab15.Controllers
{
    public class CategoriesController : Controller
    {
        private NORTHWNDEntities _contexto;
        #region ["Contexto"]
        public NORTHWNDEntities Contexto
        {
            set { _contexto = value; }
            get 
            {
                if(_contexto == null)
                    _contexto = new NORTHWNDEntities();
                return _contexto;
            }
        }
        #endregion
        // GET: Categories
        public ActionResult Index()
        {
            return View(Contexto.Categories.ToList());
        }

        public ActionResult Details(int? id)
        {
            var prductosPorCategoria = from p in Contexto.Products
                                       orderby p.ProductName ascending
                                       where p.CategoryID == id
                                       select p;
            return View(prductosPorCategoria.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Categories nuevaCategoria)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Contexto.Categories.Add(nuevaCategoria);
                    Contexto.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(nuevaCategoria);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories CategoriesEditar = Contexto.Categories.Find(id);

            if(CategoriesEditar == null)
            {
                return HttpNotFound();
            }
            return View(CategoriesEditar);
        }

        //Post: Categories / Edit / 5
        [HttpPost]
        public ActionResult Edit(Categories CategoriasEditar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contexto.Entry(CategoriasEditar).State = System.Data.Entity.EntityState.Modified;
                    Contexto.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(CategoriasEditar);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete (int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categories CategoriaEliminar = Contexto.Categories.Find(id);

            if(CategoriaEliminar == null)
                return HttpNotFound();

            return View(CategoriaEliminar);
        }

        [HttpPost]
        public ActionResult Delete(int? id, Categories categoria1)
        {
            try
            {
                Categories CategoriesEliminar = new Categories();
                if(ModelState.IsValid)
                {
                    if(id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    CategoriesEliminar = Contexto.Categories.Find(id);

                    if(CategoriesEliminar == null)
                    {
                        return HttpNotFound();
                    }
                    Contexto.Categories.Remove(CategoriesEliminar);
                    Contexto.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(CategoriesEliminar);
            }
            catch
            {
                return View();
            }
        }

    }
}