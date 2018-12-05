using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LastBlogProject.Controllers
{
    using Models;
    using PagedList;
    using Singleton;
    using System.Net;

    public class HomeController : Controller
    {
        myBlogDbEntities1 db = DBTool.DBInstance;
        // GET: Home
        public ActionResult Index(int? Page)
        {
            ViewBag.Categories = db.Categories.ToList();
            int _sayfaNo = Page ?? 1;
            var makaleler = db.Articles.OrderByDescending(a => a.ArticleID).ToPagedList<Articles>(_sayfaNo, 2);

            return View(makaleler);

            
            //return View(db.Articles.OrderByDescending(a => a.CreatedDate).ToList());
        }

        //Kategori Action
        public ActionResult CategoriList()
        {
            return View(db.Categories.ToList());
        }

        ////Arama 
        //public ActionResult _Arama(string searching)
        //{
        //    var makaleler = from s in db.Articles
        //                    select s;

        //    if (!String.IsNullOrEmpty(searching))
        //    {
        //        makaleler = makaleler.Where(s => s.PostContent.Contains(searching));
        //    }
        //    return View(makaleler.ToList());
        //}

        public ActionResult SearchResults(string search)
        {
            List<Articles> result = new List<Articles>();
            if (!string.IsNullOrEmpty(search))
            {
                result = db.Articles.Where(s => s.Title.Contains(search) || s.PostContent.Contains(search)).ToList();
            }
            return View(result);
        }


        public ActionResult MenuCategories()
        {
            var model = db.Categories.ToList();
            return PartialView("~/Views/Home/MenuCategories.cshtml", model);
        }


        public ActionResult MakaleyeGoreYorum(int? id)
        {
            if (id == null)
            {
                return View();

            }
            if (db.Comments.Any(x => x.ArticleId != id))
            {
                ViewData["yorumsuz"] = "Henüz yorum yapılmamış.";
            }
            return PartialView("~/Views/Home/MakaleyeGoreYorum.cshtml", db.Comments.Where(x => x.ArticleId == id).ToList());
        }



        //Kategori bazlı makale listesi

        public ActionResult Category(int? id)
        {
            if (id == null)
            {
                return View(db.Articles.ToList());

            }
            return View(db.Articles.Where(x => x.CategoryId == id).ToList());
        }



        public ActionResult ArticleDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }
        //todo: single page controler(details), kategoriye göre makale listeleme

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Authors item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);

            }



            if (db.Authors.Any(e => e.UserName == item.UserName && e.Password == item.Password))
            {
                Authors girisYapan = db.Authors.Where(x => x.UserName == item.UserName && x.Password == item.Password).FirstOrDefault();

                Session["oturum"] = girisYapan.AuthorID;

                return RedirectToAction("Index", "Home");

            }
            else
            {

                ViewBag.Message = "Böyle bir kullanıcı bulunamadı.";
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("oturum");
            return RedirectToAction("Index", "Home");

        }

        public ActionResult SignUp()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Authors item)
        {
            item.CreatedDate = DateTime.Now;
            item.RoleId = (int)Authors._Role.User;
            db.Authors.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}