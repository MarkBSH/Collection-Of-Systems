using UnityEngine;

public class AddItemToList : MonoBehaviour
{
    [SerializeField] private Item m_item;
    public void AddItem()
    {
        Debug.Log("Adding item to inventory: " + m_item.ItemName);
        InventoryManager.Instance.AddItem(m_item, 1);
    }
}
