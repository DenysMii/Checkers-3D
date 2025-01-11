using UnityEngine;

[CreateAssetMenu(fileName = "BoardVariant", menuName = "BoardVariant")]
public class BoardVariant : ScriptableObject
{
    public Material blackSquareMaterial;
    public Material whiteSquareMaterial;
    public Material bordersMaterial;
}