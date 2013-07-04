using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DoubleFish.Model;
using DoubleFish.BLL;

using DoubleFish;
using DoubleFish.Http;

namespace DoubleFish.Mvc.Controllers
{
    public class UserController : Controller
    {

        //
        // GET: /User/

        public ActionResult Index()
        {
			var service = this.HttpContext.Cache.GetInstance<UserBLL>();
			var model = service.List().Results;
            return View(model);
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(long id)
        {
			var service = this.HttpContext.Cache.GetInstance<UserBLL>();
			var model = service.Get(id);
            return View(model);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {

            return View();
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(int id)
		{
			var service = this.HttpContext.Cache.GetInstance<UserBLL>();
			var model = service.Get(id);
			return View(model);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
			var model = new UserInfo();
			model.Id = id;
			model.Name = collection["Name"];
			model.FullName = collection["FullName"];
			model.Sex = collection["Sex"].ToInt32(0);
			model.Birthday = collection["Birthday"].ToDateTime(DateTime.Now);
			model.Tel = collection["Tel"];
			model.Mobile = collection["Mobile"];
			model.Mail = collection["Mail"];
			model.Region = collection["Region"].ToInt64(0);
			model.Status = collection["Status"].ToInt64(0);

			var service = this.HttpContext.Cache.GetInstance<UserBLL>();

            try
            {
                // TODO: Add update logic here
				model = service.Save(model);
                //return RedirectToAction("Index");
				return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
