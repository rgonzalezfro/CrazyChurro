public class PlayerScorePayload
{
    public int Score { get; private set; }
    public Player Id { get; private set; }

    public PlayerScorePayload(int score, Player playerId)
    {
        Score = score;
        Id = playerId;
    }
}
