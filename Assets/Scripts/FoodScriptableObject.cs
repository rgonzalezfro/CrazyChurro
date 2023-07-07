using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "ScriptableObjects/Food")]
public class FoodScriptableObject : ScriptableObject
{
    public Sprite Image;
    public int Price;
}
