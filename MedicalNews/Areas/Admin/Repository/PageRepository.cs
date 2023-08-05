using MedicalNews.Areas.Admin.Models;
using MedicalNews.DAL;
using System.Data.SqlClient;

namespace MedicalNews.Areas.Admin.Repository
{
    public class PageRepository : IDisposable
    {
        public int CreatePage(PageModel model)
        {
            var dal = new DataAccessLayer();
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@action", 1),
                new SqlParameter("@mag_id", model.MagId),
                new SqlParameter("@pdfurl", model.PdfUrl),
                new SqlParameter("@isFree", model.IsFree),
                new SqlParameter("@pageno", model.PageNo),
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Page", parameter);
        }

        public int UpdatePage(PageModel model)
        {
            var dal = new DataAccessLayer();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@action", 2),
                new SqlParameter("@page_id", model.Id),
                new SqlParameter("@mag_id", model.MagId),
                new SqlParameter("@isFree", model.IsFree),
                new SqlParameter("@pdfurl", model.PdfUrl),
                new SqlParameter("@pageno", model.PageNo)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Page", parameters);
        }

        public int DeletePage(int id)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@action", 3),
                new SqlParameter("@page_id", id)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Page", parameters);
        }

        public async Task<List<PageModel>> GetAllPages(int magId)
        {
            var dal = new DataAccessLayer();
            var list = new List<PageModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@action", 4),
                new SqlParameter("@mag_id", magId)
            };

            var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_Page", parameters); ;

            while(await sdr.ReadAsync())
            {
                list.Add(new PageModel
                {
                    Id = Convert.ToInt32(sdr["page_id"]),
                    MagId = Convert.ToInt32(sdr["mag_id"]),
                    IsFree = Convert.ToBoolean(sdr["isFree"]),
                    PageNo = Convert.ToInt32(sdr["pageno"]),
                    PdfUrl = sdr["pdfurl"].ToString()
                });
            }

            return list;
        }

        public async Task<PageModel> GetPageById(int id)
        {
            var dal = new DataAccessLayer();
            var data = new PageModel();

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@action", 5),
                new SqlParameter("@page_id", id)
            };

            var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_Page", parameters);

            while(await sdr.ReadAsync())
            {
                data.Id = Convert.ToInt32(sdr["page_id"]);
                data.MagId = Convert.ToInt32(sdr["mag_id"]);
                data.IsFree = Convert.ToBoolean(sdr["isFree"]);
                data.PageNo = Convert.ToInt32(sdr["pageno"]);
                data.PdfUrl = sdr["pdfurl"].ToString();
            }

            return data;
        }
        #region disposable pattern
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
        // ~PageRepository()
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
