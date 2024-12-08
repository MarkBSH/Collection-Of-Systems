using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    #region Singleton

    private static InventoryManager m_Instance;
    public static InventoryManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<InventoryManager>();
                if (m_Instance == null)
                {
                    GameObject _obj = new();
                    _obj.name = typeof(InventoryManager).Name;
                    m_Instance = _obj.AddComponent<InventoryManager>();
                }
            }
            return m_Instance;
        }
    }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeInventoryUI();
        UpdateInventoryUI();
    }

    #endregion

    #region Components



    #endregion

    #region Inventory

    [SerializeField] private List<Item> m_Items = new();
    [SerializeField] private List<int> m_ItemAmounts = new();

    public void AddItem(Item _item, int _amount)
    {
        int _Index = m_Items.IndexOf(_item);
        if (_Index >= 0)
        {
            m_ItemAmounts[_Index] += _amount;
        }
        else
        {
            m_Items.Add(_item);
            m_ItemAmounts.Add(_amount);
        }
        UpdateInventoryUI();
    }

    public void RemoveItem(Item _item, int _amount)
    {
        int _Index = m_Items.IndexOf(_item);
        if (_Index < 0)
        {
            DebugWarning("Item not found in inventory.");
            return;
        }

        m_ItemAmounts[_Index] -= _amount;
        if (m_ItemAmounts[_Index] <= 0)
        {
            m_Items.RemoveAt(_Index);
            m_ItemAmounts.RemoveAt(_Index);
        }
        UpdateInventoryUI();
    }

    public bool HasItem(Item _item, int _amount)
    {
        int _Index = m_Items.IndexOf(_item);
        return _Index >= 0 && m_ItemAmounts[_Index] >= _amount;
    }

    public int GetItemAmount(Item _item)
    {
        int _Index = m_Items.IndexOf(_item);
        return _Index >= 0 ? m_ItemAmounts[_Index] : 0;
    }

    #endregion

    #region InventoryUI

    private GameObject m_InventoryUI;
    private List<Image> m_InventoryImage = new();
    private List<TextMeshProUGUI> m_InventoryText = new();

    private void InitializeInventoryUI()
    {
        m_InventoryUI = GameObject.Find("InventoryUI");
        if (m_InventoryUI == null)
        {
            DebugWarning("InventoryUI GameObject not found.");
            return;
        }
        for (int i = 0; i < m_InventoryUI.transform.childCount; i++)
        {
            Transform child = m_InventoryUI.transform.GetChild(i);
            m_InventoryImage.Add(child.GetChild(0).GetComponent<Image>());
            m_InventoryText.Add(child.GetChild(1).GetComponent<TextMeshProUGUI>());
        }
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < m_InventoryImage.Count; i++)
        {
            if (i < m_Items.Count)
            {
                m_InventoryImage[i].sprite = m_Items[i].ItemSprite;
                m_InventoryText[i].text = m_ItemAmounts[i].ToString();
                m_InventoryImage[i].gameObject.SetActive(true);
                m_InventoryText[i].gameObject.SetActive(true);
            }
            else
            {
                m_InventoryImage[i].gameObject.SetActive(false);
                m_InventoryText[i].gameObject.SetActive(false);
            }
        }
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
