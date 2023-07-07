using UnityEditor.Animations;
using UnityEngine;

public class PedestrianRandomizer : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private AnimatorController[] animators;

    void Awake()
    {
        int index = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[index];

        if (animators.Length > index)
        {
            GetComponent<Animator>().runtimeAnimatorController = animators[index];
        }
    }
}
