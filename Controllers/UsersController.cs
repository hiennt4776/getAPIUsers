
using getAPIUsers.Authorize;

using getAPIUsers.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace getAPIUsers.Controllers
{
    [BearerAuthorization]
    public class UsersController : ApiController
    {
        private readonly UsersRepository usersRepository;
        public UsersController()
        {
            usersRepository = new UsersRepository();
        }
        // GET: Users

      


        [Route("api/UsersAction")]



        public IHttpActionResult GetUsers(string Action = null, string UserCode = null, string Password = null, int? Idx = null, string Text = null, int? User_Id = null, DateTime? ThoiGianDangNhap = null, DateTime? ThoiGianKetThuc = null,
                                            string Ip = null, string Mac = null, string UserName = null, int? BenhVien_Id = null, int? TamNgung = null, int? NguoiTao = null, DateTime? NgayTao = null, int? NguoiCapNhat = null,
                                            DateTime? NgayCapNhat = null, int? Huy = null, int? Menu_Id = null, string MenuCode = null, string MenuName = null, int? ParentMenu = null, int? Setting_Id = null, string SettingCode = null,
                                            string SettingName = null, string NoiDung = null, string Mota = null, DateTime? Date1 = null, DateTime? Date2 = null, int? Num1 = null, int? Num2 = null, string String1 = null, string String2 = null,
                                            string TieuDe = null, string NoiDung_text = null, DateTime? datetime = null, int? NguoiGui = null)
          
            {
                    DataTable userDataTable = usersRepository.UsersAction(Action, UserCode, Password, Idx, Text, User_Id, ThoiGianDangNhap, ThoiGianKetThuc,
                                             Ip, Mac, UserName, BenhVien_Id, TamNgung, NguoiTao, NgayTao, NguoiCapNhat,
                                             NgayCapNhat, Huy, Menu_Id, MenuCode, MenuName, ParentMenu, Setting_Id, SettingCode,
                                             SettingName, NoiDung, Mota, Date1, Date2, Num1, Num2, String1, String2,
                                             TieuDe, NoiDung_text, datetime, NguoiGui);

                    return Ok(userDataTable);
            }
    }
}
