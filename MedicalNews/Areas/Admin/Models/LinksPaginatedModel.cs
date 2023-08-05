namespace MedicalNews.Areas.Admin.Models
{
    public class LinksPaginatedModel
    {
        public int PageNo { get; set; } 
        public int TotalPage { get; set; }
        public int Totallimit { get; set; }
        public List<LinksModel>? Links { get; set; }
    }
}
