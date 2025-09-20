using UnityEngine;

public class TopDownFollowPlayer : MonoBehaviour
{
    #region Singleton

    private static TopDownFollowPlayer m_Instance;
    public static TopDownFollowPlayer Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<TopDownFollowPlayer>();
                if (m_Instance == null)
                {
                    GameObject _obj = new();
                    _obj.name = typeof(TopDownFollowPlayer).Name;
                    m_Instance = _obj.AddComponent<TopDownFollowPlayer>();
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
        TurnCameraDown();
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

    private void TurnCameraDown()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void FollowPlayer()
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
