using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowcaseColorChanger : MonoBehaviour
{
    private static ShowcaseColorChanger m_Instance;
    public static ShowcaseColorChanger Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<ShowcaseColorChanger>();
                if (m_Instance == null)
                {
                    GameObject _obj = new();
                    _obj.name = typeof(ShowcaseColorChanger).Name;
                    m_Instance = _obj.AddComponent<ShowcaseColorChanger>();
                }
            }
            return m_Instance;
        }
    }

    [SerializeField] private List<Material> m_Colors = new();
    [SerializeField] private List<GameObject> m_ShowColorObjects = new();
    private int m_CurrentColorIndex = 0;
    private int m_pastColorIndex = 0;

    private void Awake()
    {
        foreach (GameObject _obj in m_ShowColorObjects)
        {
            _obj.SetActive(false);
        }
        m_ShowColorObjects[m_CurrentColorIndex].SetActive(true);
    }

    public void ChangeColorLeft(InputAction.CallbackContext _context)
    {

        if (_context.performed)
        {
            m_CurrentColorIndex--;
            if (m_CurrentColorIndex < 0)
            {
                m_CurrentColorIndex = m_Colors.Count - 1;
            }
            SetColors();
        }
    }
    public void ChangeColorRight(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            m_CurrentColorIndex++;
            if (m_CurrentColorIndex >= m_Colors.Count)
            {
                m_CurrentColorIndex = 0;
            }
            SetColors();
        }
    }

    public void SetColors()
    {
        GameObject[] changeableObjects = GameObject.FindGameObjectsWithTag("ChangeableColors");

        foreach (GameObject _obj in changeableObjects)
        {
            Renderer _renderer = _obj.GetComponent<Renderer>();
            _renderer.material = m_Colors[m_CurrentColorIndex];
        }

        m_ShowColorObjects[m_pastColorIndex].SetActive(false);
        m_ShowColorObjects[m_CurrentColorIndex].SetActive(true);
        m_pastColorIndex = m_CurrentColorIndex;
    }
}
