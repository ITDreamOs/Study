using lvwei8.Picture.Models;
using lvwei8.Picture.Models.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace lvwei8.Picture.Service.ImageService
{
    public class ImageServiceManager
    {

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public ImageServiceDbModel Get(int serviceId)
        {
            using (var dbcontext = new ImageMySqlEntities())
            {
                return dbcontext.Set<ImageServiceDbModel>().AsNoTracking().Where(e => e.ServerId == serviceId).FirstOrDefault();
            }

        }

        /// <summary>
        /// 根据域名获取服务器
        /// </summary>
        /// <param name="serviceUrl">域名地址</param>
        /// <returns></returns>
        public ImageServiceDbModel GetByName(string serviceUrl)
        {
            using (var dbcontext = new ImageMySqlEntities())
            {
                return dbcontext.Set<ImageServiceDbModel>().AsNoTracking().Where(e => e.ServerUrl == serviceUrl).FirstOrDefault();
            }

        }

        /// <summary>
        ///添加
        /// </summary>
        /// <param name="model"></param>
        public void Add(ImageServiceDbModel model)
        {
            using (var dbcontext = new ImageMySqlEntities())
            {
                var result = dbcontext.Set<ImageServiceDbModel>().Add(model);
                dbcontext.SaveChanges();
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        public void Update(ImageServiceDbModel model)
        {
            using (var dbcontext = new ImageMySqlEntities())
            {
                dbcontext.Entry<ImageServiceDbModel>(model).State = EntityState.Modified;
                dbcontext.SaveChanges();
            }

        }

        /// <summary>
        /// 查询可用图片服务器
        /// </summary>
        /// <returns></returns>
        public List<ImageServiceDbModel> Query()
        {
            var result = new List<ImageServiceDbModel>();
            using (var dbcontext = new ImageMySqlEntities())
            {
                var dbs = dbcontext.Set<ImageServiceDbModel>().AsNoTracking().Where(e => e.FlgUsable == true && e.CurPicAmount < e.MaxPicAmount).ToList();
                if (dbs == null)
                {
                    return result;
                }
                return dbs;
            }
        }


     
    }
}