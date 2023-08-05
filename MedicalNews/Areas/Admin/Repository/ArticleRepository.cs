using MedicalNews.Areas.Admin.Models;
using MedicalNews.DAL;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace MedicalNews.Areas.Admin.Repository
{
    public class ArticleRepository : IDisposable
    {

        public int CreateAritcle(ArticleModel model)
        {
            var dal = new DataAccessLayer();
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 1),
                new SqlParameter("@ArticleTitle", model.ArticleTitle),
                new SqlParameter("@ArticleCategoryId", model.ArticleCategoryId),
                new SqlParameter("@ArticleSubCategoryId", model.ArticleSubCategoryId),
                new SqlParameter("@ArticleShortDiscription", model.ArticleShortDiscription),
                new SqlParameter("@WebUrl", model.WebUrl),
                new SqlParameter("@Description1", model.Description1),
                new SqlParameter("@Description2", model.Description2),
                new SqlParameter("@Image1", model.ImageUrl1),
                new SqlParameter("@Image2", model.ImageUrl2),
                new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage),
                new SqlParameter("@ShowOnBanner", model.ShowOnBanner),
                new SqlParameter("@IsBrakeing", model.IsBrakeing)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Article", parameters);
        }
        
        public int UpdateAritcle(ArticleModel model)
        {
            var dal = new DataAccessLayer();
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 2),
                new SqlParameter("@ArticleId", model.ArticleId),
                new SqlParameter("@ArticleTitle", model.ArticleTitle),
                new SqlParameter("@ArticleCategoryId", model.ArticleCategoryId),
                new SqlParameter("@ArticleSubCategoryId", model.ArticleSubCategoryId),
                new SqlParameter("@ArticleShortDescription", model.ArticleShortDiscription),
                new SqlParameter("@WebUrl", model.WebUrl),
                new SqlParameter("@Description1", model.Description1),
                new SqlParameter("@Description2", model.Description2),
                new SqlParameter("@Image1", model.ImageUrl1),
                new SqlParameter("@Image2", model.ImageUrl2),
                new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage),
                new SqlParameter("@ShowOnBanner", model.ShowOnBanner),
                new SqlParameter("@IsBreaking", model.IsBrakeing)
            };

            return dal.ExecuteNonQuery("ups_Ad_Manage_Article", parameters);
        }

        public int DeleteArticle(int id)
        {
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Action", 3),
                new SqlParameter("@ArticleId", id)
            };

            return dal.ExecuteNonQuery("usp_Ad_Manage_Article", parameters);
        }

        public List<ArticleModel> GetAllArticles()
        {
            var list = new List<ArticleModel>();
            var dal = new DataAccessLayer();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 4)
            };

            using (var sdr = dal.SelectRecordBydataReader("usp_Ad_Manage_Article", parameters))
            {
                while(sdr.Read())
                {
                    list.Add(new ArticleModel()
                    {
                        ArticleId = Convert.ToInt32(sdr["ArticleId"]),
                        ArticleTitle = sdr["ArticleTitle"].ToString(),
                        ArticleCategoryId = Convert.ToInt32(sdr["ArticleCategoryId"]),
                        ArticleCategoryName = sdr["ArticleCategoryName"].ToString(),
                        ArticleSubCategoryId = Convert.ToInt32(sdr["ArticleSubCategoryId"]),
                        ArticleSubCategoryName = sdr["ArticleSubCategoryName"].ToString(),
                        WebUrl = sdr["WebUrl"].ToString(),
                        CreatedDate = Convert.ToDateTime(sdr["CreatedDate"])
                    });
                }

                return list;
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
        // ~ArttcleRepository()
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
