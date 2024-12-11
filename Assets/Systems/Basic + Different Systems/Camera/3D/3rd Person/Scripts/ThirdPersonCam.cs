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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GetPlayer();
        GetCameraParent();
    }

    private void Update()
    {
        MoveCamera();
    }

    #endregion

    #region Components

    private GameObject m_Player;
    private GameObject m_CameraParent;

    private void GetPlayer()
    {
        m_Player = transform.parent.parent.gameObject;
        if (m_Player == null)
        {
            DebugWarning("Player not found. Please add a GameObject with the tag 'Player'");
        }
    }

    private void GetCameraParent()
    {
        m_CameraParent = transform.parent.gameObject;
        if (m_CameraParent == null)
        {
            DebugWarning("CameraParent not found");
        }
    }

    #endregion

    #region Camera Movement

    [SerializeField] private float m_MouseSensitivity;
    [SerializeField] private float m_MaxCamTop;
    [SerializeField] private float m_MaxCamBottom;
    private float m_AngleX;
    private float m_AngleY;

    private void MoveCamera()
    {
        float _mouseX = Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

        m_AngleX += _mouseX;
        m_AngleY -= _mouseY;
        m_AngleY = Mathf.Clamp(m_AngleY, m_MaxCamBottom, m_MaxCamTop);

        m_CameraParent.transform.localRotation = Quaternion.Euler(m_AngleY, m_AngleX, 0f);
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}
