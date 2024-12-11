using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    #region Singleton

    private static ThirdPersonCam m_Instance;
    public static ThirdPersonCam Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<ThirdPersonCam>();
                if (m_Instance == null)
                {
                    GameObject _obj = new();
                    _obj.name = typeof(ThirdPersonCam).Name;
                    m_Instance = _obj.AddComponent<ThirdPersonCam>();
                }
            }
            return m_Instance;
        }
    }

    #endregion

    #region Unity Methods



    #endregion

    #region Components



    #endregion

    #region Camera Movement



    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
