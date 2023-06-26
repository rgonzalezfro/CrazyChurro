
public class HornCooldownStartPayload
{
    public float DurationSeconds { get; private set; }

    public HornCooldownStartPayload(float durationSeconds)
    {
        DurationSeconds = durationSeconds;
    }
}
