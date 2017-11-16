using lvwei8.Picture.Models;
using lvwei8.Picture.Models.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace lvwei8.Picture.Service.Image
{
    public class ImageManager
    {
        /// <summary>
        ///添加
        /// </summary>
        /// <param name="model"></param>
        public void Add(ImageInfoDbModel model)
        {
            using (var dbcontext = new ImageMySqlEntities())
            {
                var result = dbcontext.Set<ImageInfoDbModel>().Add(model);
                dbcontext.SaveChanges();
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        public void Update(ImageInfoDbModel model)
        {
            using (var dbcontext = new ImageMySqlEntities())
            {
                dbcontext.Entry<ImageInfoDbModel>(model).State = EntityState.Modified;
                dbcontext.SaveChanges();
            }

        }

    }
}