using Microsoft.Identity.Client;

namespace MedicalNews.Models
{
    public class ArticleSD
    {
        public int ArticleId { get; set; }
        public string? ArticleTitle { get; set; }
        public string? ArticleDescription { get; set;}
        public DateTime? PublishedDate { get; set; }
        public string? ArticleCategoryName { get; set; }
        public string? ArticleSubCategoryName { get; set; }
        public string? WebUrl { get; set; }

    }
}
