using MedicalNews.DAL;
using MedicalNews.Areas.Admin.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MedicalNews.Areas.Admin.Repository
{
    public class EventRepository : IDisposable
    {
        public int CreateEvent(EventModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 1),
                new SqlParameter("@EventTitle", model.EventTitle),
                new SqlParameter("@EventCategoryId", model.EventCategoryId),
                new SqlParameter("@ShortDescription", model.ShortDescription),
                new SqlParameter("@CoverImage", model.ImageUrl),
                new SqlParameter("@EventDate", model.EventDate),
                new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Event", parameters);
        }

        public int UpdateEvent(EventUpdateModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 2),
                new SqlParameter("@EventId", model.EventId),
                new SqlParameter("@EventTitle", model.EventTitle),
                new SqlParameter("@EventCategoryId", model.EventCategoryId),
                new SqlParameter("@ShortDescription", model.ShortDescription),
                new SqlParameter("@CoverImage", model.ImageUrl),
                new SqlParameter("@EventDate", model.EventDate),
                new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Event", parameters);
        }

        public int DeleteEvent(int id)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 3),
                new SqlParameter("@EventId", id)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Event", parameters);
        }

        public List<EventModel> GetAllEvents()
        {
            var list = new List<EventModel>();

            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Action", 4) };

            using(var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_Event", parameters))
            {
                while (sdr.Read())
                {
                    list.Add(new EventModel()
                    {
                        EventId = Convert.ToInt32(sdr["EventId"]),
                        EventTitle = sdr["EventTitle"].ToString(),
                        EventCategoryId  = Convert.ToInt32(sdr["EventCategoryId"]),
                        EventCategoryName = sdr["EventCategoryName"].ToString(),
                        ShortDescription = sdr["ShortDescription"].ToString(),
                        ShowOnHomePage = Convert.ToBoolean(sdr["ShowOnHomePage"]),
                        ImageUrl = sdr["CoverImage"].ToString(),
                        EventDate = Convert.ToDateTime(sdr["EventDate"]),
                        CreatedDate = Convert.ToDateTime(sdr["CreatedDate"]),
                    });
                }
            }

            return list;
        }

        public async Task<EventModel> GetEventById(int id)
        {
            var dal = new DataAccessLayer();
            var data = new EventModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 5),
                new SqlParameter("@EventId", id)
            };

            using(var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_Event", parameters))
            {
                while(await sdr.ReadAsync())
                {

                    data.EventId = Convert.ToInt32(sdr["EventId"]);
                    data.EventTitle = sdr["EventTitle"].ToString();
                    data.EventCategoryId = Convert.ToInt32(sdr["EventCategoryId"]);
                    data.ShortDescription = sdr["ShortDescription"].ToString();
                    data.ShowOnHomePage = Convert.ToBoolean(sdr["ShowOnHomePage"]);
                    data.ImageUrl = sdr["CoverImage"].ToString();
                    data.EventDate = Convert.ToDateTime(sdr["EventDate"]);
                    data.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"]);
                }

                return data;
            }
        }

        #region Disposable pattern
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
        // ~EventRepository()
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
