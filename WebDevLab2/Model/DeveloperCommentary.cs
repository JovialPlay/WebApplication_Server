namespace WebDevLab2.Model
{
    public class DeveloperCommentary
    {
        public int Id { get; set; }

        public Player? Owner { get; set; }
        public Developer? Developer { get; set; }

        public int CommunityInteraction { get; set; }
        public int GameQuality { get; set; }
        public int Monetisation { get; set; }
        public bool Recommend { get; set; }
        public string? Commentary { get; set; }
    }
}
