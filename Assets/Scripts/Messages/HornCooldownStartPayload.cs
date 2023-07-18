
public class HornCooldownStartPayload
{
    public float DurationSeconds { get; private set; }
    public Player Id { get; private set; }

    public HornCooldownStartPayload(float durationSeconds, Player playerId)
    {
        DurationSeconds = durationSeconds;
        Id = playerId;
    }
}
