using MedicalNews.Areas.Admin.Models;
using MedicalNews.DAL;
using System.Data.SqlClient;

namespace MedicalNews.Areas.Admin.Repository
{
    public class CategoryRepository : IDisposable
    {
        private bool disposedValue;

        #region Article Category
        public int CreateArticleCategory(ArticleCategoryModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@Action", 1),
                    new SqlParameter("@ArticleCategoryName" ,model.Category),
                    new SqlParameter("@SNo", model.Sno),
                    new SqlParameter("@ShowOnMenu", model.ShowOnMainMenu),
                    new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_ArticleCategory", parameters);
        }

        public int UpdateArticleCategory(ArticleCategoryModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@Action", 2),
                    new SqlParameter("@ArticleCategoryID", model.Id),
                    new SqlParameter("@ArticleCategoryName" ,model.Category),
                    new SqlParameter("@SNo", model.Sno),
                    new SqlParameter("@ShowOnMenu", model.ShowOnMainMenu),
                    new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_ArticleCategory", parameters);
        }

        public int DeleteArticleCategory(int id)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 3),
                new SqlParameter("@ArticleCategoryID", id)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_ArticleCategory", parameters);
        }

        public List<ArticleCategoryModel> GetAllArticleCategories()
        {
            var dal = new DataAccessLayer();

            var list = new List<ArticleCategoryModel>();

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Action", 4) };

            using (SqlDataReader sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_ArticleCategory", parameters))
            {
                while (sdr.Read())
                {
                    list.Add(new ArticleCategoryModel()
                    {
                        Id = Convert.ToInt32(sdr["ArticleCategoryID"].ToString()),
                        Sno = Convert.ToInt32(sdr["SNo"].ToString()),
                        Category = sdr["ArticleCategoryName"] as string,
                        ShowOnHomePage = Convert.ToBoolean(sdr["ShowOnHomePage"]),
                        ShowOnMainMenu = Convert.ToBoolean(sdr["ShowOnMenu"]),
                    });
                }
            }

            return list;
        }

        public async Task<ArticleCategoryModel> GetArticleCategoryById(int id)
        {
            var dal = new DataAccessLayer();
            var data = new ArticleCategoryModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 5),
                new SqlParameter("@ArticleCategoryId", id)
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_ArticleCategory", parameters))
            {
                while ( await sdr.ReadAsync())
                {
                    data.Id = Convert.ToInt32(sdr["ArticleCategoryID"].ToString());
                    data.Sno = Convert.ToInt32(sdr["SNo"].ToString());
                    data.Category = sdr["ArticleCategoryName"] as string;
                    data.ShowOnHomePage = Convert.ToBoolean(sdr["ShowOnHomePage"]);
                    data.ShowOnMainMenu = Convert.ToBoolean(sdr["ShowOnMenu"]);
                }

                return data;
            }
        }
        #endregion

        #region Event Category
        public int CreateEventCategory(EventCategoryModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@Action", 1),
                    new SqlParameter("@EventCategoryName" ,model.Category),
                    new SqlParameter("@ShowOnMenu", model.ShowOnMainMenu),
                    new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_EventCategory", parameters);
        }

        public int UpdateEventCategory(EventCategoryModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@Action", 2),
                    new SqlParameter("@EventCategoryId", model.Id),
                    new SqlParameter("@EventCategoryName" ,model.Category),
                    new SqlParameter("@ShowOnMenu", model.ShowOnMainMenu),
                    new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_EventCategory", parameters);
        }

        public int DeleteEventCategory(int id)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 3),
                new SqlParameter("@EventCategoryId", id)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_EventCategory", parameters);
        }

        public List<EventCategoryModel> GetAllEventCategories()
        {
            var dal = new DataAccessLayer();

            var list = new List<EventCategoryModel>();

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Action", 4) };

            using (SqlDataReader sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_EventCategory", parameters))
            {
                while (sdr.Read())
                {
                    list.Add(new EventCategoryModel()
                    {
                        Id = Convert.ToInt32(sdr["EventCategoryId"].ToString()),
                        Category = sdr["EventCategoryName"] as string,
                        ShowOnHomePage = Convert.ToBoolean(sdr["ShowOnHomePage"]),
                        ShowOnMainMenu = Convert.ToBoolean(sdr["ShowOnMenu"]),
                    });
                }
            }

            return list;
        }

        public async Task<EventCategoryModel> GetEventCategoryById(int id)
        {
            var dal = new DataAccessLayer();
            var data = new EventCategoryModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 5),
                new SqlParameter("@EventCategoryId", id)
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_EventCategory", parameters))
            {
                while (await sdr.ReadAsync())
                {
                    data.Id = Convert.ToInt32(sdr["EventCategoryID"].ToString());
                    data.Category = sdr["EventCategoryName"] as string;
                    data.ShowOnHomePage = Convert.ToBoolean(sdr["ShowOnHomePage"]);
                    data.ShowOnMainMenu = Convert.ToBoolean(sdr["ShowOnMenu"]);
                }

                return data;
            }
        }
        #endregion

        #region Interview Category

        public int CreateInterviewCategory(InterviewCategoryModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 1),
                new SqlParameter("@Sno", model.Sno),
                new SqlParameter("@InterViewCategoryName", model.InterviewCategory)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_InterViewCategory", parameters);
        }

        public int UpdateInterviewCategory(InterviewCategoryModel model)
        {
            var dal = new DataAccessLayer();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 2),
                new SqlParameter("@InterViewCategoryId", model.Id),
                new SqlParameter("@InterViewCategoryName", model.InterviewCategory),
                new SqlParameter("@Sno", model.Sno)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_InterViewCategory", parameters);
        }

        public int DeleteInterviewCategory(int id)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 3),
                new SqlParameter("@InterViewCategoryId", id),
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_InterViewCategory", parameters);
        }

        public List<InterviewCategoryModel> GetAllInterviewCategories()
        {
            var dal = new DataAccessLayer();

            List<InterviewCategoryModel> list = new List<InterviewCategoryModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 4)
            };

            using (SqlDataReader sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_InterViewCategory", parameters))
            {
                while (sdr.Read())
                {
                    list.Add(new InterviewCategoryModel()
                    {
                        Id = Convert.ToInt32(sdr["InterViewCategoryId"]),
                        InterviewCategory = Convert.ToString(sdr["InterViewCategoryName"]),
                        Sno = Convert.ToInt32(sdr["Sno"])
                    });

                }
            }

            return list;
        }

        public async Task<InterviewCategoryModel> GetInterviewCategoryById(int id)
        {
            var dal = new DataAccessLayer();
            var data = new InterviewCategoryModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 5),
                new SqlParameter("@InterViewCategoryId", id)
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_InterViewCategory", parameters))
            {
                while (await sdr.ReadAsync())
                {
                    data.Id = Convert.ToInt32(sdr["InterViewCategoryID"].ToString());
                    data.Sno = Convert.ToInt32(sdr["SNo"].ToString());
                    data.InterviewCategory = sdr["InterViewCategoryName"] as string;
                }

                return data;
            }
        }
        #endregion

        #region Article SubCategory
        public int CreateArticleSubCategory(ArticleSubCategoryModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@Action", 1),
                    new SqlParameter("@ArticleSubCategoryName" ,model.ArticleSubCategoryName),
                    new SqlParameter("@ArticleCategoryID", model.ArticleCategoryID),
                    new SqlParameter("@SNo", model.Sno),
                    new SqlParameter("@ShowOnMenu", model.ShowOnMainMenu),
                    new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_ArticleSubCategory", parameters);
        }

        public int UpdateArticleSubCategory(ArticleSubCategoryModel model)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@Action", 2),
                    new SqlParameter("@ArticleSubCategoryId", model.Id),
                    new SqlParameter("@ArticleSubCategoryName" ,model.ArticleSubCategoryName),
                    new SqlParameter("@SNo", model.Sno),
                    new SqlParameter("@ShowOnMenu", model.ShowOnMainMenu),
                    new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_ArticleSubCategory", parameters);
        }
        
        public int DeleteArticleSubCategory(int id)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 3),
                new SqlParameter("@ArticleSubCategoryId", id)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_ArticleSubCategory", parameters);
        }

        public List<ArticleSubCategoryModel> GetAllArticleSubCategories(int id)
        {
            var dal = new DataAccessLayer();

            var list = new List<ArticleSubCategoryModel>();

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Action", 4), new SqlParameter("@ArticleCategoryID", id) };

            using (SqlDataReader sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_ArticleSubCategory", parameters))
            {
                while (sdr.Read())
                {
                    list.Add(new ArticleSubCategoryModel()
                    {
                        Id = Convert.ToInt32(sdr["ArticleSubCategoryId"].ToString()),
                        Sno = Convert.ToInt32(sdr["SNo"].ToString()),
                        Category = sdr["ArticleCategoryName"] as string,
                        ShowOnHomePage = Convert.ToBoolean(sdr["ShowOnHomePage"]),
                        ShowOnMainMenu = Convert.ToBoolean(sdr["ShowOnMenu"]),
                        ArticleCategoryID = Convert.ToInt32(sdr["ArticleCategoryID"]),
                        ArticleSubCategoryName = sdr["ArticleSubCategoryName"].ToString()
                    });
                }
            }

            return list;
        }

        public async Task<ArticleSubCategoryModel> GetArticleSubCategoryById(int id)
        {
            var dal = new DataAccessLayer();
            var data = new ArticleSubCategoryModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 5),
                new SqlParameter("@ArticleSubCategoryId", id)
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_ArticleSubCategory", parameters))
            {
                while (await sdr.ReadAsync())
                {
                    data.Id = Convert.ToInt32(sdr["ArticleSubCategoryId"].ToString());
                    data.Sno = Convert.ToInt32(sdr["SNo"].ToString());
                    data.ArticleSubCategoryName = sdr["ArticleSubCategoryName"] as string;
                    data.ShowOnHomePage = Convert.ToBoolean(sdr["ShowOnHomePage"]);
                    data.ShowOnMainMenu = Convert.ToBoolean(sdr["ShowOnMenu"]);
                    data.ArticleCategoryID = Convert.ToInt32(sdr["ArticleCategoryID"]);
                }

                return data;
            }
        }
        #endregion

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
        // ~CategoryRepository()
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
    }
}
