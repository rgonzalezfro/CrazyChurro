using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPositions;
    
    void Start()
    {
        var player1 = GameManager.Instance.GetPlayer(Player.One);
        player1.transform.position = spawnPositions[0].position;

        if (GameManager.Instance.IsMultiplayer())
        {
            var player2 = GameManager.Instance.GetPlayer(Player.Two);
            player2.transform.position = spawnPositions[1].position;
        }
    }
}
