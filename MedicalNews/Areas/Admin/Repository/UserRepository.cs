using MedicalNews.DAL;
using MedicalNews.Areas.Admin.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace MedicalNews.Areas.Admin.Repository
{
    public class UserRepository : IDisposable
    {
        private bool disposedValue;

        public int CreateUser(UserModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", model.Username),
                new SqlParameter("@Password", model.Password),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@EmailId", model.EmailId),
                new SqlParameter("@MobileNumber", model.MobileNumber),
                new SqlParameter("@Address", model.Address),
                new SqlParameter("RoleId", 1),
                new SqlParameter("@action", 1)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_LoginUser", parameters);
        }

        public int UpdateUser(UserModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginId", model.LoginId),
                new SqlParameter("@Username", model.Username),
                new SqlParameter("@Password", model.Password),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@EmailId", model.EmailId),
                new SqlParameter("@MobileNumber", model.MobileNumber),
                new SqlParameter("@Address", model.Address),
                new SqlParameter("RoleId", 1),
                new SqlParameter("@action", 2)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_LoginUser", parameters);
        }
        
        public int DeleteUser(int id)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@action", 3),
                new SqlParameter("@LoginId", id)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_LoginUser", parameters);
        }

        public List<UserModel> GetAllUsers()
        {
            var dal = new DataAccessLayer();
            var list = new List<UserModel>();

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@action", 4),
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_LoginUser", parameters))
            {
                while (sdr.Read())
                {
                    list.Add(new UserModel()
                    {
                        LoginId = Convert.ToInt32(sdr["LoginId"]),
                        Name= sdr["Name"] as string,
                        EmailId= sdr["EmailId"] as string,
                        MobileNumber = sdr["MobileNumber"] as string,
                        RoleId = Convert.ToInt32(sdr["RoleId"]),
                        RoleName = sdr["RoleName"] as string,
                        Username = sdr["Username"] as string,
                        Password = sdr["Password"] as string,
                        Status = sdr["IsActive"] as string
                    });
                }
            }

            return list;
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var dal = new DataAccessLayer();
            var user = new UserModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@action", 5),
                new SqlParameter("LoginId", id)
            };

            using(var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_LoginUser", parameters))
            {
                while(await sdr.ReadAsync())
                {
                    user.LoginId = Convert.ToInt32(sdr["LoginId"]);
                    user.Name = sdr["Name"] as string;
                    user.EmailId = sdr["EmailId"] as string;
                    user.MobileNumber = sdr["MobileNumber"] as string;
                    user.RoleId = Convert.ToInt32(sdr["RoleId"]);
                    user.Username = sdr["Username"] as string;
                    user.Password = sdr["Password"] as string;
                    user.Address = sdr["Address"] as string;
                }

                return user;
            }
        }

        #region Dispose Pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UserRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
