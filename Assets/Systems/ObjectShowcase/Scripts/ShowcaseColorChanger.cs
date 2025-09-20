using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowcaseColorChanger : MonoBehaviour
{
    #region Singleton

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
                    GameObject _obj = new(typeof(ShowcaseColorChanger).Name);
                    m_Instance = _obj.AddComponent<ShowcaseColorChanger>();
                }
            }
            return m_Instance;
        }
    }

    #endregion

    #region Unity Methods

    private void Start()
    {
        StartShowcaseObject();
    }

    #endregion

    #region Components



    #endregion

    #region ColorChanger

    [SerializeField] private List<Material> m_ColorMaterials = new();
    [SerializeField] private List<GameObject> m_ColorShowcaseObjects = new();
    private int m_CurrentObjectIndex = 0;
    private int m_PreviousColorIndex = 0;
    private int CurrentColorIndex
    {
        get => m_CurrentObjectIndex;
        set
        {
            m_CurrentObjectIndex = (value + m_ColorMaterials.Count) % m_ColorMaterials.Count;
            SetColors();
        }
    }

    private void StartShowcaseObject()
    {
        // UI Setup
        foreach (GameObject obj in m_ColorShowcaseObjects)
        {
            obj.SetActive(false);
        }
        m_ColorShowcaseObjects[CurrentColorIndex].SetActive(true);
    }

    public void ChangeColorLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CurrentColorIndex--;
        }
    }

    public void ChangeColorRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CurrentColorIndex++;
        }
    }

    public void SetColors()
    {
        GameObject[] changeableObjects = GameObject.FindGameObjectsWithTag("ChangeableColors");

        foreach (GameObject obj in changeableObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            renderer.material = m_ColorMaterials[CurrentColorIndex];
        }

        m_ColorShowcaseObjects[m_PreviousColorIndex].SetActive(false);
        m_ColorShowcaseObjects[CurrentColorIndex].SetActive(true);
        m_PreviousColorIndex = CurrentColorIndex;
    }

    #endregion
}
