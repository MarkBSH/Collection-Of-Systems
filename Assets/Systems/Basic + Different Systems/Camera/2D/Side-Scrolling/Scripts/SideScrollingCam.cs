using UnityEngine;

public class SideScrollingCam : MonoBehaviour
{
    #region Singleton

    private static SideScrollingCam m_Instance;
    public static SideScrollingCam Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<SideScrollingCam>();
                if (m_Instance == null)
                {
                    GameObject _obj = new();
                    _obj.name = typeof(SideScrollingCam).Name;
                    m_Instance = _obj.AddComponent<SideScrollingCam>();
                }
            }
            return m_Instance;
        }
    }

    #endregion

    #region Unity Methods

    private void Start()
    {
        GetPlayer();
    }

    private void Update()
    {
        FollowPlayer();
    }

    #endregion

    #region Components

    GameObject m_Player;

    private void GetPlayer()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        if (m_Player == null)
        {
            DebugWarning("Player not found");
        }
    }

    #endregion

    #region Camera

    [SerializeField] private Vector3 m_Offset;

    public void FollowPlayer()
    {
        transform.position = m_Player.transform.position + m_Offset;
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
