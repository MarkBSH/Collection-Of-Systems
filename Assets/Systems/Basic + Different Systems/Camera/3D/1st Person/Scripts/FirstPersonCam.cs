using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    #region Singleton

    private static FirstPersonCam m_Instance;
    public static FirstPersonCam Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<FirstPersonCam>();
                if (m_Instance == null)
                {
                    GameObject _obj = new();
                    _obj.name = typeof(FirstPersonCam).Name;
                    m_Instance = _obj.AddComponent<FirstPersonCam>();
                }
            }
            return m_Instance;
        }
    }

    #endregion

    #region Unity Methods

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GetPlayer();
    }

    private void Update()
    {
        MoveCamera();
    }

    #endregion

    #region Components

    private GameObject m_Player;

    private void GetPlayer()
    {
        m_Player = transform.parent.gameObject;
        if (m_Player == null)
        {
            DebugWarning("Player not found. Please add a GameObject with the tag 'Player'");
        }
    }

    #endregion

    #region Camera Movement

    [SerializeField] private float m_MouseSensitivity;
    [SerializeField] private float m_MaxCamTop;
    [SerializeField] private float m_MaxCamBottom;
    private float m_Angle;

    private void MoveCamera()
    {
        float _mouseX = Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

        m_Angle -= _mouseY;
        m_Angle = Mathf.Clamp(m_Angle, m_MaxCamBottom, m_MaxCamTop);

        transform.localRotation = Quaternion.Euler(m_Angle, 0, 0);
        m_Player.transform.Rotate(Vector3.up * _mouseX);
    }

    #endregion

    #region Debugging

    private static void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
