﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNT.Core.Repository;
using CFEntity.Models;
using TNT.Core.UnitOfWork;

using Autofac;

namespace Nailhub.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<COUNTRY> rpoCountry;
        //private IUnitOfWork uow;
        private IUnitOfWorkAsync uow; 
        public HomeController(IRepository<COUNTRY> _rpoCountry, IUnitOfWorkAsync _uow)
        {
            this.rpoCountry = _rpoCountry;
            this.uow = _uow;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //var country = new COUNTRY
            //{
            //    NAME = "China",
            //    NATIVELANGUAGE = "Chinese"
            //};

            //uow.BeginTransaction();
            //rpoCountry.Insert(country);

            //uow.SaveChanges();
            //uow.Commit();

            ////update

            //var china = rpoCountry.Query().Get().Where(c => c.NAME.ToLower().Equals("china")).SingleOrDefault();

            //china.NAME = "China new name";

            //rpoCountry.Update(china);

            //uow.SaveChanges();

            //Find by primary key
            var country1 = rpoCountry.Find(1);

            //Get current IRepository from container

            var crpoCountry = TNT.App.Scope.Resolve<IRepository<COUNTRY>>();

            var country2 = crpoCountry.Find(2);

            ViewBag.Country = string.Format(@"<br\> {0}-{1}", country1.NAME, country2.NAME);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}