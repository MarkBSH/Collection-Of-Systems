using UnityEngine;

public class AddItemToList : MonoBehaviour
{
    [SerializeField] private Item _item;
    public void AddItem()
    {
        Debug.Log("Adding item to inventory: " + _item.ItemName);
        InventoryManager.Instance.AddItem(_item, 1);
    }
}
