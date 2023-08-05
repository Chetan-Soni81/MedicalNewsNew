using MedicalNews.DAL;
using MedicalNews.Areas.Admin.Models;
using System.Data.SqlClient;

namespace MedicalNews.Areas.Admin.Repository
{
    public class InterviewRepository : IDisposable
    {
        public int CreateInterview(InterviewModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 1),
                new SqlParameter("@InterviewTitle", model.InterviewTitle),
                new SqlParameter("@InterViewCategoryId", model.InterViewCategoryId),
                new SqlParameter("@VideoUrl", model.VideoUrl),
                new SqlParameter("@InterviewDescription", model.InterviewDescription),
                new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_InterView", parameters);
        }

        public int UpdateInterview(InterviewModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 2),
                new SqlParameter("@InterviewId", model.InterviewId),
                new SqlParameter("@InterviewTitle", model.InterviewTitle),
                new SqlParameter("@InterViewCategoryId", model.InterViewCategoryId),
                new SqlParameter("@VideoUrl", model.VideoUrl),
                new SqlParameter("@InterviewDescription", model.InterviewDescription),
                new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_InterView", parameters);
        }

        public int DeleteInterview(int id)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 3),
                new SqlParameter("@InterviewId", id)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_InterView", parameters);
        }

        public List<InterviewModel> GetAllInterviews()
        {
            var list = new List<InterviewModel>();

            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 4),
            };

            using (SqlDataReader sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_Interview", parameters))
            {
                while (sdr.Read())
                {
                    list.Add(new InterviewModel
                    {
                        InterviewId = Convert.ToInt32(sdr["InterviewId"]),
                        InterviewTitle = sdr["InterviewTitle"].ToString(),
                        InterViewCategoryName = sdr["InterViewCategoryName"].ToString(),
                        VideoUrl = sdr["VideoUrl"].ToString(),
                        InterviewDescription = sdr["InterviewDescription"].ToString(),
                        ShowOnHomePage = Convert.ToBoolean(sdr["ShowOnHomePage"]),
                        CreatedDate = Convert.ToDateTime(sdr["CreatedDate"])
                    }); 
                }
            }

            return list;
        }

        public async Task<InterviewModel> GetInterviewById(int id)
        {
            var dal = new DataAccessLayer();
            var interview = new InterviewModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 5),
                new SqlParameter("@InterviewId", id)
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_InterView", parameters))
            {
                while ( await sdr.ReadAsync())
                {
                    interview.InterviewId = Convert.ToInt32(sdr["InterviewId"]);
                    interview.InterviewTitle = sdr["InterviewTitle"].ToString();
                    interview.InterViewCategoryId = Convert.ToInt32(sdr["InterViewCategoryId"]);
                    interview.InterViewCategoryName = sdr["InterViewCategoryName"].ToString();
                    interview.VideoUrl = sdr["VideoUrl"].ToString();
                    interview.InterviewDescription = sdr["InterviewDescription"].ToString();
                    interview.ShowOnHomePage = Convert.ToBoolean(sdr["ShowOnHomePage"]);
                    interview.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"]);
                }

                return interview;
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
        // ~InterviewRepository()
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
