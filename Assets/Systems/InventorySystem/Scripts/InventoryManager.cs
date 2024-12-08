using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class InventoryManager : MonoBehaviour
{
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

    [SerializeField] private List<Item> m_Items = new();
    [SerializeField] private List<int> m_ItemAmounts = new();
    private GameObject m_InventoryUI;
    private List<Image> m_InventoryImage = new();
    private List<TextMeshProUGUI> m_InventoryText = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_InventoryUI = GameObject.Find("InventoryUI");
        for (int i = 0; i < m_InventoryUI.transform.childCount; i++)
        {
            m_InventoryImage.Add(m_InventoryUI.transform.GetChild(i).GetChild(0).GetComponent<Image>());
            m_InventoryText.Add(m_InventoryUI.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>());
        }
        UpdateInventoryUI();
    }

    public void AddItem(Item _item, int _amount)
    {
        if (m_Items.Contains(_item))
        {
            int _index = m_Items.IndexOf(_item);
            m_ItemAmounts[_index] += _amount;
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
        if (m_Items.Contains(_item))
        {
            int _index = m_Items.IndexOf(_item);
            m_ItemAmounts[_index] -= _amount;
            if (m_ItemAmounts[_index] <= 0)
            {
                m_Items.RemoveAt(_index);
                m_ItemAmounts.RemoveAt(_index);
            }
        }
        else
        {
            Debug.LogWarning("Item not found in inventory.");
        }
        UpdateInventoryUI();
    }

    public bool HasItem(Item _item, int _amount)
    {
        if (m_Items.Contains(_item))
        {
            int _index = m_Items.IndexOf(_item);
            return m_ItemAmounts[_index] >= _amount;
        }
        return false;
    }

    public int GetItemAmount(Item _item)
    {
        if (m_Items.Contains(_item))
        {
            int _index = m_Items.IndexOf(_item);
            return m_ItemAmounts[_index];
        }
        return 0;
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
}