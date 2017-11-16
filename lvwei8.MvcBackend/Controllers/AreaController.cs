using lvwei8.Model.Models;
using lvwei8.Service.Area;
using lvwei8.Service.Area.DTO;
using lvwei8.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lvwei8.MvcBackend.Common;
using lvwei8.MvcBackend.Models;
using System.Text;

namespace lvwei8.MvcBackend.Controllers
{
    public class AreaController : Controller
    {
        // GET: Area
        #region 服务

        /// <summary>
        /// 通用仓储
        /// </summary>
        public ICommonRepository CommonRepository { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public IRepository<AreaDbModel> Repository { get; set; }
        /// <summary>
        /// [ReadOnly]区域
        /// </summary>
        public IReadOnlyRepository<AreaDbModel> ReadOnlyRepository { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public lvwei8MySqlEntities db { get; set; }
        /// <summary>
        /// 行政区服务
        /// </summary>
        public IAreaService AreaService { get; set; }


        #endregion

        #region 辅助
        /// <summary>
        /// 模型克隆
        /// </summary>
        /// <param name="updateareamodel"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private AreaDbModel CloneAreaModel(AreaDbModel updateareamodel, AreaDbModel model)
        {
            //updateareamodel.Code = model.Code;
            updateareamodel.Name = model.Name;
            updateareamodel.FirstLetter = model.FirstLetter;
            updateareamodel.IsCityBlock = model.IsCityBlock;
            updateareamodel.Name = model.Name;
            updateareamodel.FullName = model.FullName;
            updateareamodel.Longitude = model.Longitude;
            updateareamodel.Latitude = model.Latitude;
            updateareamodel.BaiduName = model.BaiduName;
            updateareamodel.BaiduFullName = model.BaiduFullName;
            return updateareamodel;
        }

        /// <summary>
        /// 视图模型转数据模型
        /// </summary>
        /// <param name="model">视图模型</param>
        /// <returns></returns>
        private AreaDbModel ConvertViewToDB(AreaViewModel model)
        {

            var area = new AreaDbModel()
            {
                Code = model.Code,
                FirstLetter = model.FirstLetter,
                IsCityBlock = model.IsCityBlock,
                Name = model.Name,
                BaiduFullName = model.BaiduFullName,
                BaiduName = model.BaiduName,
                FullName = model.FullName,
                CityLevel = model.CityLevel,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
            };
            return area;
        }
        #endregion


        #region 查
        [Authorize(Roles = "Admin,MasterStation")]
        [keywordFilter]
        public ActionResult Index(ResourceQueryPegeViewModel querymodel)
        { // 页信息
            var pageSize = querymodel.PageSize.HasValue ? querymodel.PageSize.Value : 10;
            var pageNo = querymodel.PageNo.HasValue ? querymodel.PageNo.Value : 1;
            var page = new PageModel() { PageSize = pageSize, PageNo = pageNo };

            var skipPageCount = (page.PageNo - 1) * page.PageSize;
            StringBuilder keyCondtion = new StringBuilder();

            // 拼接关键字查询
            if (!String.IsNullOrWhiteSpace(querymodel.KeyWords))
            {
                keyCondtion.Append("and (");
                keyCondtion.AppendFormat(" locate('{0}', Name) != 0", querymodel.KeyWords);
                keyCondtion.Append(")");
            }

            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat("select count(Code) from area where 1=1 {0}", keyCondtion.ToString());

            var total = CommonRepository.SelectQuery<int>(sb1.ToString()).First();
            page.RecordCount = total;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select * from area where 1=1 {0}", keyCondtion.ToString());
            sb.AppendFormat("order by Code desc limit {0},{1}", skipPageCount, page.PageSize);
            var models = CommonRepository.SelectQuery<AreaDbModel>(sb.ToString()).ToList();

            ViewBag.PageCount = page.PageCount;
            ViewBag.PageNo = page.PageNo;
            ViewBag.KeyWords = querymodel.KeyWords;
            ViewBag.RecordCount = page.RecordCount;
            ViewBag.PageSize = page.PageSize;

            return View(models);
        }


        [Authorize(Roles = "Admin,MasterStation")]
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = ReadOnlyRepository.Query().Where(e => e.Code == id).FirstOrDefault();
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(model);
        }

        #endregion


        #region 删
        [Authorize(Roles = "Admin,MasterStation")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = ReadOnlyRepository.Query().Where(e => e.Code == id).FirstOrDefault();
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,MasterStation")]
        public ActionResult DeleteConfirm(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Delete", new { ErrorMsg = "区域码不能删除", id = id });
            //  return AreaDeleteChange(id);
        }
        #endregion


        #region 增

        [Authorize(Roles = "Admin,MasterStation")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,MasterStation")]
        public ActionResult Create(AreaDbModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Code))
            {
                return RedirectToAction("Create", new { ErrorMsg = "名称或者区域码不为空" });
            }
            var code = Repository.Add(model).Code;
          
            return RedirectToAction("Index", new { Message = "已添加" });
        }
        #endregion


        #region 改


        [HttpGet]
        [Authorize(Roles = "Admin,MasterStation")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = ReadOnlyRepository.Query().Where(e => e.Code == id).FirstOrDefault();
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,MasterStation")]
        public ActionResult Edit(AreaDbModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Code))
            {
                return RedirectToAction("Edit", new { ErrorMsg = "名称或者区域码不为空" });
            }

            return AreaEditChange(model);
        }



        #endregion

        #region 资源变更

        #region 更新
        /// <summary>
        ///变更
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="Pics">图片集</param>
        private ActionResult AreaEditChange(AreaDbModel model)
        {
            var updatemodel = ReadOnlyRepository.GetForUpdate(e => e.Code == model.Code);
            if (updatemodel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        
            updatemodel = CloneAreaModel(updatemodel, model);

            Repository.Update(updatemodel);
            //需要级联改动(市辖区、县)
            var isedit = string.IsNullOrEmpty(updatemodel.CityLevel);
            if (isedit)
            {
               
                return RedirectToAction("Index", new { Message = "已编辑" });
            }
            var isprovince = updatemodel.CityLevel == "1";
            var iscity = updatemodel.CityLevel == "2";
            if (isprovince)
            {
               
                var cityscodes = AreaService.GetCitysByProvinceCode(updatemodel.Code).Select(e => e.Code).ToList();
                if (cityscodes.Count == 0) return RedirectToAction("Index", new { Message = "已编辑" });
                foreach (var citycode in cityscodes)
                {
                    var updatecitymodel = ReadOnlyRepository.GetForUpdate(e => e.Code == citycode);
                    updatecitymodel.FullName = updatemodel.FullName + "," + updatecitymodel.Name;

                    Repository.Update(updatecitymodel);
               
                    var countryscode = AreaService.GetCountyByCityCode(updatecitymodel.Code).Select(e => e.Code).ToList();
                    if (countryscode.Count > 0)
                    {
                        foreach (var countrycode in countryscode)
                        {
                            var updatecountrymodel = ReadOnlyRepository.GetForUpdate(e => e.Code == countrycode);
                            updatecountrymodel.FullName = updatecitymodel.FullName + "," + updatecountrymodel.Name;
                            Repository.Update(updatecountrymodel);
                           
                        }
                    }
                }
            }
            else if (iscity)
            {
              
                var countryscode = AreaService.GetCountyByCityCode(updatemodel.Code).Select(e => e.Code).ToList();
                if (countryscode.Count == 0) return RedirectToAction("Index", new { Message = "已编辑" });
                foreach (var countrycode in countryscode)
                {
                    var updatecountrymodel = ReadOnlyRepository.GetForUpdate(e => e.Code == countrycode);
                    updatecountrymodel.FullName = updatemodel.FullName + "," + updatecountrymodel.Name;
                    Repository.Update(updatecountrymodel);
                  
                }
            }

            return RedirectToAction("Index", new { Message = "已编辑" });
        }
        #endregion

        #region 删除
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private ActionResult AreaDeleteChange(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deletemodel = ReadOnlyRepository.Get(e => e.Code == code);
            if (deletemodel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

 
            Repository.Delete(e => e.Code == deletemodel.Code);
            var isprovince = deletemodel.CityLevel == "1";
            var iscity = deletemodel.CityLevel == "2";
            if (isprovince)
            {
             
                var cityscodes = AreaService.GetCitysByProvinceCode(deletemodel.Code).Select(e => e.Code).ToList();
                if (cityscodes.Count == 0) return RedirectToAction("Index", new { Message = "已删除" });
                foreach (var citycode in cityscodes)
                {
                    Repository.Delete(e => e.Code == citycode);
                
                    var countryscode = AreaService.GetCountyByCityCode(citycode).Select(e => e.Code).ToList();
                    if (countryscode.Count > 0)
                    {
                        foreach (var countrycode in countryscode)
                        {
                            Repository.Delete(e => e.Code == countrycode);
                       
                        }
                    }
                }
            }
            else if (iscity)
            {
              
                var countryscode = AreaService.GetCountyByCityCode(deletemodel.Code).Select(e => e.Code).ToList();
                if (countryscode.Count == 0) return RedirectToAction("Index", new { Message = "已删除" });
                foreach (var countrycode in countryscode)
                {
                    Repository.Delete(e => e.Code == countrycode);
               
                }
            }
      
            return RedirectToAction("Index", new { Message = "已删除" });
        }
        #endregion

        #endregion
    }
}