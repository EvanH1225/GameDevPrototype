using UnityEngine;

[CreateAssetMenu(fileName = "NewResource", menuName = "Resources/Resource")]
public class Resource : ScriptableObject
{
    public string resourceName;
    public Sprite icon;
    public enum ResourceType { RawMaterial, RefinedMaterial, Consumable, Tool }
    public ResourceType type;
}
