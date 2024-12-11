using UnityEngine;

public class MapLikeCamera : MonoBehaviour
{
    #region Singleton

    private static MapLikeCamera m_Instance;
    public static MapLikeCamera Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<MapLikeCamera>();
                if (m_Instance == null)
                {
                    GameObject _obj = new();
                    _obj.name = typeof(MapLikeCamera).Name;
                    m_Instance = _obj.AddComponent<MapLikeCamera>();
                }
            }
            return m_Instance;
        }
    }

    #endregion

    #region Unity Methods

    private void Start()
    {
        GetCameraParent();
        StartZoom();
        StartRotation();
    }

    private void Update()
    {
        Zoom();
        MoveCamera();
        RotateCamera();
        LookAtMiddle();
    }

    #endregion

    #region Components

    private GameObject m_CameraParent;

    private void GetCameraParent()
    {
        m_CameraParent = transform.parent.gameObject;
        if (m_CameraParent == null)
        {
            DebugWarning("CameraParent not found");
        }
    }

    #endregion

    #region CameraZoom

    private float m_Zoom;
    [SerializeField] private float m_ZoomMulty = 0.2f;
    [SerializeField] private float m_ZoomMin = 0.8f;
    [SerializeField] private float m_ZoomMax = 3f;

    private void StartZoom()
    {
        m_Zoom = m_CameraParent.transform.localScale.x;
    }

    private void Zoom()
    {
        m_Zoom -= Input.mouseScrollDelta.y * m_ZoomMulty;
        m_Zoom = Mathf.Clamp(m_Zoom, m_ZoomMin, m_ZoomMax);
        m_CameraParent.transform.localScale = new Vector3(1, 1, 1) * m_Zoom;
    }

    #endregion

    #region CameraMovement

    private Vector3 m_PreviousPositionPos;

    private void MoveCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_PreviousPositionPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 _delta = m_PreviousPositionPos - Input.mousePosition;
            Vector3 forwardMovement = new Vector3(transform.forward.x, 0, transform.forward.z) * _delta.y * Time.deltaTime;
            Vector3 rightMovement = transform.right * _delta.x * Time.deltaTime;
            m_CameraParent.transform.position += forwardMovement + rightMovement;
            m_PreviousPositionPos = Input.mousePosition;
        }
    }

    #endregion

    #region CameraRotation

    private Vector3 m_PreviousRotationPos;
    [SerializeField] private float m_RotateSpeed = 3f;
    [SerializeField] private float m_XRotationMin = 10;
    [SerializeField] private float m_XRotationMax = 80;

    private void StartRotation()
    {
        m_CameraParent.transform.eulerAngles = new Vector3(20, 0, 0);
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            m_PreviousRotationPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 _delta = m_PreviousRotationPos - Input.mousePosition;
            float newRotationX = Mathf.Clamp(m_CameraParent.transform.eulerAngles.x + _delta.y * m_RotateSpeed * Time.deltaTime, m_XRotationMin, m_XRotationMax);
            float newRotationY = m_CameraParent.transform.eulerAngles.y + _delta.x * m_RotateSpeed * m_RotateSpeed * Time.deltaTime;
            m_CameraParent.transform.eulerAngles = new Vector3(newRotationX, newRotationY, 0);
            m_PreviousRotationPos = Input.mousePosition;
        }
    }

    private void LookAtMiddle()
    {
        transform.LookAt(m_CameraParent.transform.position);
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _message)
    {
        Debug.LogWarning("Warning: " + _message);
    }

    #endregion
}
