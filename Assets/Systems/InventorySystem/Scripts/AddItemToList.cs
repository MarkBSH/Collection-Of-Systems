using UnityEngine;

public class AddItemToList : MonoBehaviour
{
    [SerializeField] private Item m_item;

    public void AddItem()
    {
        if (m_item == null)
        {
            Debug.LogWarning("Item is null, cannot add to inventory.");
            return;
        }

        Debug.Log($"Adding item to inventory: {m_item.ItemName}");
        InventoryManager.Instance.AddItem(m_item, 1);
    }
}
