using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CraftingRecipe : ScriptableObject
{
    public Resource targetResource;
    public List<Resource> requiredResources;
}
