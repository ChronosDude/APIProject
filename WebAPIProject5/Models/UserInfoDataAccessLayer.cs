using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject5.Models
{
    public class UserInfoDataAccessLayer
    {
        //string constr = @"workstation id=AWProyecto.mssql.somee.com;packet size=4096;user id=JimmiAWS2;pwd=JimmiAWS2;data source=AWProyecto.mssql.somee.com;persist security info=False;initial catalog=AWProyecto";
        string constr = @"Data Source=JIMMY-LAP\SQLEXPRESS;Initial Catalog=AWProjecto;Integrated Security=True";

        public UserSend GetLoginUser(User login)
        {
            var userInfo = new UserSend();
            using(SqlConnection con = new SqlConnection(constr))
            {
                string sql = string.Format(@"Select a.*,b.Role from Users a
                                            inner join Roles b on a.IdRole=b.Id
                                            where a.UserName='{0}' and Password='{1}'",login.UserName,login.Password);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while(rd.Read())
                {
                    userInfo.Id = Convert.ToInt32(rd["Id"]);
                    userInfo.UserName = rd["UserName"].ToString();
                    userInfo.Email = rd["Email"].ToString();
                    userInfo.Name = rd["Name"].ToString();
                    userInfo.LastName = rd["LastName"].ToString();
                    userInfo.Password = rd["Password"].ToString();
                    userInfo.Rolename = rd["Role"].ToString();
                }
                return userInfo;
            }
        }
    }
}
