using System.Data.SqlClient;
using MedicalNews.DAL;

using MedicalNews.Areas.Admin.Models;

namespace MedicalNews.Areas.Admin.Repository
{
    public class MagazineRepository: IDisposable
    {
        private bool disposedValue;

        public int CreateMagazine(MagazineModel model)
        {
            var dal = new DataAccessLayer();

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@action", 1),
                new SqlParameter("@title", model.Title),
                new SqlParameter("@coverimage", model.ImageUrl),
                new SqlParameter("@shortdec", model.ShortDescription),
                new SqlParameter("@issue_date", model.IssueDate)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Magazine", parameters);
        }

        public int UpdateMagazine(MagazineModel model)
        {
            var dal = new DataAccessLayer();

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@action", 2),
                new SqlParameter("@mag_id", model.Id),
                new SqlParameter("@title", model.Title),
                new SqlParameter("@coverimage", model.ImageUrl),
                new SqlParameter("@shortdec", model.ShortDescription),
                new SqlParameter("@issue_date", model.IssueDate)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Magazine", parameters);
        }

        public int DeleteMagazine(int id)
        {
            var dal = new DataAccessLayer();
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@action", 3),
                new SqlParameter("@mag_id", id)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Magazine", parameters);
        }

        public async Task<List<MagazineModel>> GetAllMagazines()
        {
            var dal  = new DataAccessLayer();
            var list = new List<MagazineModel>();
            var parameter = new SqlParameter[] { new SqlParameter("@action", 4) };

            var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_Magazine", parameter);

            while(await sdr.ReadAsync())
            {
                list.Add(new MagazineModel
                {
                    Id = Convert.ToInt32(sdr["mag_id"]),
                    Title = sdr["title"].ToString(),
                    ImageUrl = sdr["coverimage"].ToString(),
                    ShortDescription = sdr["shortdec"].ToString(),
                    IssueDate = Convert.ToDateTime(sdr["issue_date"])
                });
            }

            return list;
        }

        public async Task<MagazineModel> GetMagazineById(int id)
        {
            var dal = new DataAccessLayer();
            var data = new MagazineModel();

            var parameter = new SqlParameter[]
            {
                new SqlParameter("@action", 5),
                new SqlParameter("@mag_id", id)
            };

            var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_Magazine", parameter);

            while(await sdr.ReadAsync())
            {
                data.Id = Convert.ToInt32(sdr["mag_id"]);
                data.Title = sdr["title"].ToString();
                data.ImageUrl = sdr["coverimage"].ToString();
                data.ShortDescription = sdr["shortdec"].ToString();
                data.IssueDate = Convert.ToDateTime(sdr["issue_date"]);
            }

            return data;
        }


        #region Disposable Pattern
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
        // ~MagazineRepository()
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
