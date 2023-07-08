
public class HornCooldownStartPayload
{
    public float DurationSeconds { get; private set; }
    public bool Player2 { get; private set; }

    public HornCooldownStartPayload(float durationSeconds, bool player2 = false)
    {
        DurationSeconds = durationSeconds;
        Player2 = player2;
    }
}
