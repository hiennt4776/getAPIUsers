
using getAPIUsers.Models;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace getAPIUsers.Repository
{
    public class UsersRepository
    {
        private readonly string _connectionString;
        private readonly string secretKey = "secretkey";
        public UsersRepository()
        {
            //_connectionString = ConfigurationManager.ConnectionStrings["KClinic2dbConnection"].ConnectionString;
            //EncryptionDecryptionManager.Decrypt(txtSourceDecrypt.Text, txtKeyDecript.Text)
           // _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
           _connectionString = EncryptionDecryptionManager.Decrypt(ConfigurationManager.AppSettings["ConnectionString"], secretKey);
        }


        public DataTable UsersAction(string Action, string UserCode, string Password, int? Idx, string Text, int? User_Id, DateTime? ThoiGianDangNhap, DateTime? ThoiGianKetThuc,
                                            string Ip, string Mac, string UserName, int? BenhVien_Id, int? TamNgung, int? NguoiTao, DateTime? NgayTao, int? NguoiCapNhat,
                                            DateTime? NgayCapNhat, int? Huy, int? Menu_Id, string MenuCode, string MenuName, int? ParentMenu, int? Setting_Id, string SettingCode,
                                            string SettingName, string NoiDung, string Mota, DateTime? Date1, DateTime? Date2, int? Num1, int? Num2, string String1, string String2,
                                            string TieuDe, string NoiDung_text, DateTime? datetime, int? NguoiGui)
        {

            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_001_Users", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Action", SqlDbType.VarChar).Value = Action;
                    if (UserCode != null) command.Parameters.AddWithValue("@UserCode", SqlDbType.VarChar).Value = UserCode;
                    if (Password != null) command.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = Password;
                    if (Idx != null) command.Parameters.AddWithValue("@Idx", SqlDbType.Int).Value = Idx;
                    if (Text != null) command.Parameters.AddWithValue("@Text", SqlDbType.VarChar).Value = Text;

                    if (User_Id != null) command.Parameters.AddWithValue("@User_Id", SqlDbType.Int).Value = User_Id;
                    if (ThoiGianDangNhap.HasValue) command.Parameters.AddWithValue("@ThoiGianDangNhap", SqlDbType.DateTime).Value = ThoiGianDangNhap;
                    if (ThoiGianKetThuc.HasValue) command.Parameters.AddWithValue("@ThoiGianKetThuc", SqlDbType.DateTime).Value = ThoiGianKetThuc;
                    if (Ip != null) command.Parameters.AddWithValue("@Ip", SqlDbType.VarChar).Value = Ip;
                    if (Mac != null) command.Parameters.AddWithValue("@Mac", SqlDbType.VarChar).Value = Mac;


                    if (UserName != null) command.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = UserName;
                    if (BenhVien_Id != null) command.Parameters.AddWithValue("@BenhVien_Id", SqlDbType.Int).Value = BenhVien_Id;
                    if (TamNgung != null) command.Parameters.AddWithValue("@TamNgung", SqlDbType.Int).Value = TamNgung;
                    if (NguoiTao != null) command.Parameters.AddWithValue("@NguoiTao", SqlDbType.Int).Value = NguoiTao;
                    if (NgayTao.HasValue) command.Parameters.AddWithValue("@NgayTao", SqlDbType.DateTime).Value = NgayTao;
                    if (NguoiCapNhat != null) command.Parameters.AddWithValue("@NguoiCapNhat", SqlDbType.Int).Value = NguoiCapNhat;
                    if (NgayCapNhat.HasValue) command.Parameters.AddWithValue("@NgayCapNhat", SqlDbType.DateTime).Value = NgayCapNhat;
                    if (Huy != null) command.Parameters.AddWithValue("@Huy", SqlDbType.Int).Value = Huy;

                    if (Menu_Id != null) command.Parameters.AddWithValue("@Menu_Id", SqlDbType.Int).Value = Menu_Id;
                    if (MenuCode != null) command.Parameters.AddWithValue("@MenuCode", SqlDbType.VarChar).Value = MenuCode;
                    if (MenuName != null) command.Parameters.AddWithValue("@MenuName", SqlDbType.VarChar).Value = MenuName;
                    if (ParentMenu != null) command.Parameters.AddWithValue("@ParentMenu", SqlDbType.Int).Value = ParentMenu;

                    if (Setting_Id != null) command.Parameters.AddWithValue("@Setting_Id", SqlDbType.Int).Value = Setting_Id;
                    if (SettingCode != null) command.Parameters.AddWithValue("@SettingCode", SqlDbType.VarChar).Value = SettingCode;
                    if (SettingName != null) command.Parameters.AddWithValue("@SettingName", SqlDbType.VarChar).Value = SettingName;
                    if (NoiDung != null) command.Parameters.AddWithValue("@NoiDung", SqlDbType.VarChar).Value = NoiDung;
                    if (Mota != null) command.Parameters.AddWithValue("@Mota", SqlDbType.VarChar).Value = Mota;
                    if (Date1.HasValue) command.Parameters.AddWithValue("@Date1", SqlDbType.DateTime).Value = Date1;
                    if (Date2.HasValue) command.Parameters.AddWithValue("@Date2", SqlDbType.DateTime).Value = Date2;
                    if (Num1 != null) command.Parameters.AddWithValue("@Num1", SqlDbType.Int).Value = Num1;
                    if (Num2 != null) command.Parameters.AddWithValue("@Num2", SqlDbType.Int).Value = Num2;
                    if (String1 != null) command.Parameters.AddWithValue("@String1", SqlDbType.VarChar).Value = String1;
                    if (!(String2 == null)) command.Parameters.AddWithValue("@String2", SqlDbType.VarChar).Value = String2;

                    if (TieuDe != null) command.Parameters.AddWithValue("@TieuDe", SqlDbType.VarChar).Value = TieuDe;
                    if (NoiDung_text != null) command.Parameters.AddWithValue("@NoiDung_text", SqlDbType.VarChar).Value = NoiDung_text;
                    if (datetime.HasValue) command.Parameters.AddWithValue("@datetime", SqlDbType.DateTime).Value = datetime;
                    if (NguoiGui != null) command.Parameters.AddWithValue("@NguoiGui", SqlDbType.VarChar).Value = NguoiGui;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();

                    connection.Open();
                    command.ExecuteNonQuery();
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dt);
                    connection.Close();

                }
            }
            return dt;
        }
    }


}