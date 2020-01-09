using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Moi nhap Username")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Moi nhap Password")]
        public string PassWord { set; get; }

        public bool RememberMe { set; get; }

    }
}