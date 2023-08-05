using System.Data.SqlClient;
using MedicalNews.Areas.Admin.Repository;
using MedicalNews.DAL;
using MedicalNews.Models;

namespace MedicalNews.Repositories
{
    public class ArticleRepository
    {
        public async Task<List<ArticleSD>> GetAllArticles()
        {
            var dal = new DataAccessLayer();

            var list = new List<ArticleSD>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", 4)
            };
            
            using(var sdr = dal.SelectRecordBydataReader("usp_Ad_Managa_Article", parameters))
            {
                while(await sdr.ReadAsync())
                {
                    list.Add(new ArticleSD
                    {
                        ArticleId = Convert.ToInt32(sdr["ArticleId"]),
                        ArticleTitle = sdr["ArticleTitle"].ToString(),
                        PublishedDate = Convert.ToDateTime(sdr["CreatedDate"]),
                        ArticleCategoryName = sdr["ArticleCategoryName"].ToString(),
                        ArticleSubCategoryName = sdr["ArticleSubCategoryName"].ToString(),
                        WebUrl = sdr["WebUrl"].ToString()
                    });
                }
            }

            return list;
        }
    }
}
