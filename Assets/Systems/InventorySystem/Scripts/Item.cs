using UnityEngine;

#region Item Enums

public enum ItemType
{
    Weapon,
    Consumable,
    Material,
    Placeable
}

#endregion

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    #region Private Item Details

    [SerializeField] private int m_ItemID;
    [SerializeField] private string m_ItemName;
    [SerializeField] private Sprite m_ItemSprite;
    [SerializeField] private int m_ItemValue;
    [SerializeField] private ItemType m_ItemType;
    [SerializeField] private string m_ItemDescription;

    #endregion

    #region Public Item Details

    public int ItemID => m_ItemID;
    public string ItemName => m_ItemName;
    public Sprite ItemSprite => m_ItemSprite;
    public int ItemValue => m_ItemValue;
    public ItemType ItemType => m_ItemType;
    public string ItemDescription => m_ItemDescription;

    #endregion

    #region Item Methods

    public string GetItemDetails()
    {
        return $"ID: {m_ItemID}\nName: {m_ItemName}\nDescription: {m_ItemDescription}\nValue: {m_ItemValue}\nType: {m_ItemType}";
    }

    #endregion
}
