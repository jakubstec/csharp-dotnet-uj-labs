namespace BlazorApp.Models
{
    /// <summary>
    /// Class representing single page
    /// </summary>
    public class Page
    {
        public string Url { private get; set; }
        public string Name { private get; set; }

        public string Html
        {
            get
            {
                return "<a href=" + Url + " target=\"_blank\">" + Name + "</a>";
            }
        }
    }
}
