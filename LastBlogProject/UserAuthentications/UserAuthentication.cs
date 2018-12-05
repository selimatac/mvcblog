using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LastBlogProject.UserAuthentications
{
    public class UserAuthentication: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["oturum"] != null)
            {
                return true;
            }
            else
            {
                //eger session null ise giriş yapılamamıs demektir. Bu durumda kullanıcı aynı sayfaya yönlendirilecek ve yetikelendirme metodum false deger döndürecektir.
                httpContext.Response.Redirect("/Home/Login");
                return false;
            }
        }
    }
}