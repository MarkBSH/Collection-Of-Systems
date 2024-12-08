using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ShowcaseObjectChanger : MonoBehaviour
{
    private static ShowcaseObjectChanger m_Instance;
    public static ShowcaseObjectChanger Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<ShowcaseObjectChanger>();
                if (m_Instance == null)
                {
                    GameObject _obj = new GameObject(typeof(ShowcaseObjectChanger).Name);
                    m_Instance = _obj.AddComponent<ShowcaseObjectChanger>();
                }
            }
            return m_Instance;
        }
    }

    [SerializeField] private List<GameObject> m_Objects = new();
    [SerializeField] private List<GameObject> m_ShowObjectObjects = new();
    private int m_CurrentObjectIndex = 0;
    private int m_pastObjectIndex = 0;
    private int CurrentObjectIndex
    {
        get => m_CurrentObjectIndex;
        set
        {
            m_CurrentObjectIndex = (value + m_Objects.Count) % m_Objects.Count;
            SetObjects();
        }
    }
    private Transform m_SpawnPoint;
    private GameObject m_SpawnedObject;

    private void Awake()
    {
        m_SpawnPoint = GameObject.Find("SpawnPoint").transform;

        foreach (GameObject _obj in m_ShowObjectObjects)
        {
            _obj.SetActive(false);
        }

        SetObjects();
    }

    public void ChangeObjectUp(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            CurrentObjectIndex--;
        }
    }

    public void ChangeObjectDown(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            CurrentObjectIndex++;
        }
    }

    private void SetObjects()
    {
        Destroy(m_SpawnedObject);

        m_SpawnedObject = Instantiate(m_Objects[m_CurrentObjectIndex], m_SpawnPoint);
        ShowcaseColorChanger.Instance.SetColors();

        m_ShowObjectObjects[m_pastObjectIndex].SetActive(false);
        m_ShowObjectObjects[m_CurrentObjectIndex].SetActive(true);
        m_pastObjectIndex = m_CurrentObjectIndex;
    }
}
