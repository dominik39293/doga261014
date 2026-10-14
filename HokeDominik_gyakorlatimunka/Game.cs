namespace HokeDominik_gyakorlatimunka;

public class Game
{
    public string name { get; set; }
    public string type { get; set; }
    public string platform { get; set; }
    public int releaseDate { get; set; }
    public double score { get; set; }
    public override string ToString()
    {
        return $"{name} | {type} | {platform} | {releaseDate} | {score}";
    }
}