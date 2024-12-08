using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    Material,
    Placeable
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private int m_ItemID;
    [SerializeField] private string m_ItemName;
    [SerializeField] private Sprite m_ItemSprite;
    [SerializeField] private int m_ItemValue;
    [SerializeField] private ItemType m_ItemType;

    public int ItemID => m_ItemID;
    public string ItemName => m_ItemName;
    public Sprite ItemSprite => m_ItemSprite;
    public int ItemValue => m_ItemValue;
    public ItemType ItemType => m_ItemType;
}
