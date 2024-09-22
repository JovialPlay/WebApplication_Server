namespace WebDevLab2.Model
{
    public class Developer
    {
        public int Id { get; set; }
        public required string CompanyName { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public int Rating { get; set; }
        public List<Game>? CreatedGames { get; set; }
        public List<DeveloperCommentary>? DeveloperCommentaries { get; set; }
    }
}
