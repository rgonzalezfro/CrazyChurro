using UnityEngine;

public class PedestrianRandomizer : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    void Start()
    {
        int index = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
}
