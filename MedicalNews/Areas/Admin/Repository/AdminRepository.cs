using MedicalNews.DAL;
using MedicalNews.Areas.Admin.Models;
using System.Data.SqlClient;

namespace MedicalNews.Areas.Admin.Repository
{
    public class AdminRepository : IDisposable
    {

        public AdminLoginModel LoginAdmin(string username, string password)
        {
            var dal = new DataAccessLayer();
            var usermodel = new AdminLoginModel();
            try
            {
                SqlParameter[] parameter = new SqlParameter[]
                {
                  new SqlParameter("@Action","1"),
                  new SqlParameter("@UserName",username),
                  new SqlParameter("@Password",password),
                  new SqlParameter("@MachineIp",""),
                };
                using (SqlDataReader sdr = dal.SelectRecordBydataReader("Usp_Ad_AdminLogin", parameter))
                {
                    while (sdr.Read())
                    {
                        usermodel.Id = Convert.ToInt32(sdr["LoginId"]);
                        usermodel.Name = sdr["Name"] as string;
                        usermodel.Username = sdr["Username"] as string;
                        usermodel.RoleId = Convert.ToInt32(sdr["RoleId"]);
                    }
                    return usermodel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #region Disposable Pattern
        private bool disposedValue;

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
        // ~AdminRepository()
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
