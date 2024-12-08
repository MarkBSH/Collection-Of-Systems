using UnityEngine;

public class AddItemToList : MonoBehaviour
{
    #region Unity Methods



    #endregion

    #region Components



    #endregion

    #region ItemAdder

    [SerializeField] private Item m_Item;

    public void AddItem()
    {
        if (m_Item == null)
        {
            DebugWarning("Item is null, cannot add to inventory.");
            return;
        }

        InventoryManager.Instance.AddItem(m_Item, 1);
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
