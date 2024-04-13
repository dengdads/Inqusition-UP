using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasaBlazorApp1.Data
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public partial class UserLoginData
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请输入您的用户名")]
        public string account { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "请输入您的密码")]
        public string password { get; set; }

        public UserLoginData(string Account, string Password)
        {
            account = Account;
            password = Password;
        }
    }
}