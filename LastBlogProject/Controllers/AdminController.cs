using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LastBlogProject.Controllers
{
    using Models;
    using Singleton;
    using System.IO;
    using UserAuthentications;

    [UserAuthentication]
    public class AdminController : Controller
    {
        myBlogDbEntities1 db = DBTool.DBInstance;
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        //Yeni Makale Ekleme
        public ActionResult ArticleCreate()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ArticleCreate(Articles item)
        {
            item.FeaturedImage = ImageUpload(item);
            item.CreatedDate = DateTime.Now;
            item.AuthorId = Convert.ToInt32(Session["oturum"]);
            db.Articles.Add(item);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ArticleEdit(int id)
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View(db.Articles.Find(id));
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ArticleEdit(Articles item)
        {
            item.AuthorId = Convert.ToInt32(Session["oturum"]);
            item.CreatedDate = DateTime.Now;
            Articles update = db.Articles.Find(item.ArticleID);
            update.FeaturedImage = ImageUpload(item);
            db.Entry(update).CurrentValues.SetValues(item);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string ImageUpload(Articles item)
        {
            if (item.ImageFile != null)
            { 
                string fileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
                string extension = Path.GetExtension(item.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + extension;
                item.FeaturedImage = "/Content/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Content/images/"), fileName);
                item.ImageFile.SaveAs(fileName); 
            }
            return item.FeaturedImage;
        }

        public ActionResult DeleteArticle(int id)
        {

            db.Articles.Remove(db.Articles.Find(id));

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public JsonResult CommentCreate(string item, int makaleid)
        {
            var uyeId = Session["oturum"];

            if (item!=null)
            {
                db.Comments.Add(new Comments
                {
                    AuthorId = Convert.ToInt32(uyeId),
                    ArticleId = makaleid,
                    Comment = item,
                    CreatedDate = DateTime.Now
                });
                db.SaveChanges();
            }
            
            

            
            //item.AuthorId = Convert.ToInt32(Session["oturum"]);
            //item.ArticleId = id;
            //item.CreatedDate = DateTime.Now;

            return Json(false,JsonRequestBehavior.AllowGet);
        }
    }
}