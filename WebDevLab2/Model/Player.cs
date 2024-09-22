namespace WebDevLab2.Model
{
    public class Player
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public List<GameCommentary>? GameComments { get; set; }
        public List<DeveloperCommentary>? DeveloperComments { get; set; }
    }
}
