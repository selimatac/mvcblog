using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastBlogProject.Singleton
{
    using Models;
    public class DBTool
    {
        public DBTool()
        {

        }

        private static myBlogDbEntities1 _dbInstance;

        public static myBlogDbEntities1 DBInstance
        {
            get {
                if (_dbInstance == null )
                {
                    _dbInstance = new myBlogDbEntities1();
                }

                return _dbInstance;

            }
        }

    }
}