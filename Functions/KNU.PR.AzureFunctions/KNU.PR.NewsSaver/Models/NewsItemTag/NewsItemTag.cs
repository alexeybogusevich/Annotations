
namespace KNU.PR.NewsManager.Models.NewsItemTag
{
    public class NewsItemTag
    {
        public string Name { get; set; }
        public int OccurencesCount { get; set; }
        public NewsItemTag(string name, int count)
        {
            this.Name = name;
            this.OccurencesCount = count;
        }
    }
}
