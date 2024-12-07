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
                    GameObject _obj = new();
                    _obj.name = typeof(ShowcaseObjectChanger).Name;
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
    private Transform m_SpawnPoint;
    private GameObject m_SpawnedObject;

    private void Awake()
    {
        m_SpawnPoint = GameObject.Find("SpawnPoint").transform;

        foreach (GameObject _obj in m_ShowObjectObjects)
        {
            if (_obj != null)
            {
                _obj.SetActive(false);
            }
        }
        if (m_ShowObjectObjects[m_CurrentObjectIndex] != null)
        {
            m_ShowObjectObjects[m_CurrentObjectIndex].SetActive(true);
        }
    }

    public void ChangeObjectUp(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            m_CurrentObjectIndex--;
            if (m_CurrentObjectIndex < 0)
            {
                m_CurrentObjectIndex = m_Objects.Count - 1;
            }
            SetObjects();
        }
    }
    public void ChangeObjectDown(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {
            m_CurrentObjectIndex++;
            if (m_CurrentObjectIndex >= m_Objects.Count)
            {
                m_CurrentObjectIndex = 0;
            }
            SetObjects();
        }
    }

    private void SetObjects()
    {
        if (m_SpawnedObject != null)
        {
            Destroy(m_SpawnedObject);
        }
        if (m_Objects[m_CurrentObjectIndex] != null)
        {
            m_SpawnedObject = Instantiate(m_Objects[m_CurrentObjectIndex], m_SpawnPoint);
            ShowcaseColorChanger.Instance.SetColors();
        }



        if (m_ShowObjectObjects[m_pastObjectIndex] != null)
        {
            m_ShowObjectObjects[m_pastObjectIndex].SetActive(false);
        }
        if (m_ShowObjectObjects[m_CurrentObjectIndex] != null)
        {
            m_ShowObjectObjects[m_CurrentObjectIndex].SetActive(true);
        }
        m_pastObjectIndex = m_CurrentObjectIndex;
    }

}
