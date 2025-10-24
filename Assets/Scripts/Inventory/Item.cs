using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour

{
    
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    public void OnPickup(InventoryManager inventory)
    {
        int leftOverItems = inventory.AddItem(itemName, quantity, sprite, itemDescription);
        if (leftOverItems <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            quantity = leftOverItems;
        }
    }
}
