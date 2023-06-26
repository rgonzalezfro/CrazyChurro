using UnityEngine;

public class HornSoundPayload
{
    public Vector3 PlayerPosition { get; private set; }

    public HornSoundPayload(Vector3 playerPosition)
    {
        PlayerPosition = playerPosition;
    }
}
