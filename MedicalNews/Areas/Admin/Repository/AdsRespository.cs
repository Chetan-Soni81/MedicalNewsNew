using MedicalNews.DAL;
using MedicalNews.Areas.Admin.Models;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;

namespace MedicalNews.Areas.Admin.Repository
{
    public class AdsRespository : IDisposable
    {

        public int CreateAds(AdsModel model)
        {
            var dal = new DataAccessLayer();

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 1),
                new SqlParameter("@Title", model.Title),
                new SqlParameter("@Weburl", model.WebUrl),
                new SqlParameter("@Position", model.Position),
                new SqlParameter("@AdsImage", model.ImageUrl),
                new SqlParameter("@ShowFrom", model.ShowFrom),
                new SqlParameter("@ShowTo", model.ShowTo)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Ads", parameters);
        }
        public int UpdateAds(AdsUpdateModel model)
        {
            var dal = new DataAccessLayer();

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 2),
                new SqlParameter("@AdsId", model.AdsId),
                new SqlParameter("@Title", model.Title),
                new SqlParameter("@Weburl", model.WebUrl),
                new SqlParameter("@Position", model.Position),
                new SqlParameter("@AdsImage", model.ImageUrl),
                new SqlParameter("@ShowFrom", model.ShowFrom),
                new SqlParameter("@ShowTo", model.ShowTo)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Ads", parameters);
        }
        
        public int DeleteAds(int id)
        {
            var dal = new DataAccessLayer();

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 3),
                new SqlParameter("@AdsId", id),
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Ads", parameters);
        }

        public List<AdsModel> GetAllAds()
        {
            var list = new List<AdsModel>();

            var dal = new DataAccessLayer();

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 4)
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_Ads", parameters))
            {
                while(sdr.Read())
                {
                    list.Add(new AdsModel()
                    {
                        AdsId = Convert.ToInt32(sdr["AdsId"]),
                        Title = sdr["Title"].ToString(),
                        Position = Convert.ToInt32(sdr["Position"]),
                        ImageUrl = sdr["AdsImage"].ToString(),
                        WebUrl = sdr["Weburl"].ToString(),
                        ShowFrom = Convert.ToDateTime(sdr["ShowFrom"]),
                        ShowTo = Convert.ToDateTime(sdr["ShowTo"]),
                        CreatedDate = Convert.ToDateTime(sdr["CreatedDate"])
                    });
                }
            }

            return list;
        }

        public async Task<AdsModel> GetAdsById(int id)
        {
            var dal = new DataAccessLayer();
            AdsModel ads = new AdsModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 5),
                new SqlParameter("@AdsId", id)
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_Ads", parameters))
            {
                while(await sdr.ReadAsync())
                {
                    ads.AdsId = Convert.ToInt32(sdr["AdsId"]);
                    ads.Title = sdr["Title"].ToString();
                    ads.Position = Convert.ToInt32(sdr["Position"]);
                    ads.ImageUrl = sdr["AdsImage"].ToString();
                    ads.WebUrl = sdr["Weburl"].ToString();
                    ads.ShowFrom = Convert.ToDateTime(sdr["ShowFrom"]);
                    ads.ShowTo = Convert.ToDateTime(sdr["ShowTo"]);
                }

                return ads;
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
        // ~AdsRespository()
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
