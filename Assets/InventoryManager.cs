using UnityEngine;
using UnityEngine.InputSystem; // new Input System

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryMenu; // assign InventoryMenu
    private PlayerControls controls;
    private bool isOpen = false;
    public ItemSlot[] itemSlot;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.OpenInventory.performed += ctx => ToggleInventory();
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Player.OpenInventory.performed -= ctx => ToggleInventory();
        controls.Disable();
    }

    private void ToggleInventory()
    {
        isOpen = !isOpen;
        if (inventoryMenu != null)
            inventoryMenu.SetActive(isOpen);

        // Optional: show cursor when inventory is open
        Cursor.visible = isOpen;
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for(int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if(leftOverItems > 0)
                {
                    leftOverItems=AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                }
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
