namespace WebDevLab2.Model
{
    public class Game
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public int Mark {  get; set; }
        public Developer? Developer { get; set; }
        public List<GameCommentary>? Comments { get; set; }
        public int AmmountOfPlayers { get; set; }

    }
}
