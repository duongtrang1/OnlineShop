﻿using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 1)//khai báo tham số mặc định = 1,pageSize = 1 là 1 trang chứa 1 User
        {
            //Khởi tạo đối tượng dao
            var dao = new UserDao();
            //gán model, truyền vào page, pageSize
            var model = dao.ListAllPaging(page, pageSize); 
            return View(model);//Truyền model vào View
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user) 
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var encryptedMD5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMD5Pas;

                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm User thất bại");
                }
            }
            return View("Index");
        }


        public ActionResult Edit(int id)//Truyền vào id của user
        {
            var user = new UserDao().ViewDetail(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //Kiểm tra xem người dùng có muốn đổi mật khẩu không
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMD5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMD5Pas;
                }

                var result = dao.Update(user);
                if (result)
                {                    
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật User thất bại");
                }
            }
            return View("Index");
        }


        //Xóa
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}