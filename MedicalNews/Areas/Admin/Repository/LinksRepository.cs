using MedicalNews.DAL;
using MedicalNews.Areas.Admin.Models;
using System.Data.SqlClient;

namespace MedicalNews.Areas.Admin.Repository
{
    public class LinksRepository : IDisposable
    {
        public int CreateLink(LinksModel model)
        {
            var dal = new DataAccessLayer();

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 1),
                new SqlParameter("@WebTitle", model.WebTitle),
                new SqlParameter("@WebUrl", model.WebUrl)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_ImportentLink", parameters);
        }

        public int UpdateLink(LinksModel model)
        {
            var dal = new DataAccessLayer();

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 2),
                new SqlParameter("@ImportentLinlId", model.ImportantLinkId),
                new SqlParameter("@WebTitle", model.WebTitle),
                new SqlParameter("@WebUrl", model.WebUrl)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_ImportentLink", parameters);
        }

        public int DeleteLink(int id)
        {
            var dal = new DataAccessLayer();

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 3),
                new SqlParameter("@ImportentLinlId", id)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_ImportentLink", parameters);
        }

        public List<LinksModel> GetAllLinks()
        {
            var dal = new DataAccessLayer();
            var list = new List<LinksModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 4)
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_ImportentLink", parameters))
            {
                while (sdr.Read())
                {
                    list.Add(new LinksModel()
                    {
                        ImportantLinkId = Convert.ToInt32(sdr["ImportentLinlId"]),
                        WebTitle = sdr["WebTitle"].ToString(),
                        WebUrl = sdr["WebUrl"].ToString(),
                        CreatedDate = Convert.ToDateTime(sdr["CreatedDate"])
                    });
                }
            }

            return list;
        }

        public async Task<LinksModel> GetLinkbyID(int id)
        {
            var dal = new DataAccessLayer();
            LinksModel link = new LinksModel(); ;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 5),
                new SqlParameter("@ImportentLinlId", id)
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_ImportentLink", parameters))
            {
                while (await sdr.ReadAsync())
                {
                    link.ImportantLinkId = Convert.ToInt32(sdr["ImportentLinlId"]);
                    link.WebTitle = sdr["WebTitle"].ToString();
                    link.WebUrl = sdr["WebUrl"].ToString();
                }

                return link;
            }
        }

        public LinksPaginatedModel GetPaginatedList(int page, int pageSize)
        {
            var dal = new DataAccessLayer();
            LinksPaginatedModel model;
            var list = new List<LinksModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@limit", pageSize),
                new SqlParameter("@page_no", page)
            };

            /*using (var dataset = dal.SelectRecordByDataSet("usp_Paginate_ImportantLinks", parameters))
            {
                var dt = dataset.Tables[0];
                var dt2 = dataset.Tables[1];

                for (var i = 0; i < dt2.Rows.Count; i++)
                {
                    list.Add(new LinksModel()
                    {
                        ImportantLinkId = Convert.ToInt32(dt2.Rows[i]["ImportentLinlId"]),
                        WebTitle = (dt2.Rows[i]["WebTitle"]).ToString(),
                        WebUrl = (dt2.Rows[i]["WebUrl"]).ToString(),
                        CreatedDate = Convert.ToDateTime(dt2.Rows[i]["CreatedDate"])
                    });
                }

                model = new LinksPaginatedModel()
                {
                    PageNo = Convert.ToInt32(dt.Rows[0][0]),
                    TotalPage = Convert.ToInt32(dt.Rows[0][1]),
                    Totallimit = Convert.ToInt32(dt.Rows[0][2]),
                    Links= list
                };

            }*/

            using (SqlDataReader sdr = dal.SelectRecordBydataReader("usp_Paginate_ImportantLinks", parameters))
            {
                model = new LinksPaginatedModel();
                while(sdr.Read())
                {
                        model.PageNo = Convert.ToInt32(sdr["page_no"]);
                        model.TotalPage = Convert.ToInt32(sdr["total_page"]);
                        model.Totallimit =  Convert.ToInt32(sdr["limit"]);
                }

                if (sdr.NextResult())
                {
                    while (sdr.Read())
                    {
                    list.Add(new LinksModel()
                    {
                        ImportantLinkId = Convert.ToInt32(sdr["ImportentLinlId"]),
                        WebTitle = sdr["WebTitle"].ToString(),
                        WebUrl = sdr["WebUrl"].ToString(),
                        CreatedDate = Convert.ToDateTime(sdr["CreatedDate"]),
                    });
                    }
                model.Links = list;
                }
            }

            return model;
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
        // ~LinksRepository()
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
