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
            return;
        }

        InventoryManager.Instance.AddItem(m_Item, 1);
    }

    #endregion
}
