namespace WebDevLab2.Model
{
    public class GameCommentary
    {
        public int Id {  get; set; }
        public Player? Owner { get; set; }
        public Game? Game { get; set; }
        public int Gameplay { get; set; }
        public int Graphics {  get; set; }
        public int Bugs { get; set; }
        public int Plot { get; set; }
        public int AudioQuality { get; set; }
        public bool Recommend { get; set; }
        public string? Commentary { get; set; }
    }
}
