using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    private static DialogueManager m_Instance;
    public static DialogueManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<DialogueManager>();
                if (m_Instance == null)
                {
                    GameObject _obj = new();
                    _obj.name = typeof(DialogueManager).Name;
                    m_Instance = _obj.AddComponent<DialogueManager>();
                }
            }
            return m_Instance;
        }
    }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GetDialogueComponents();
        GetAudioManager();
        DisableDialogueBox();
    }

    #endregion

    #region Components



    #endregion

    #region Dialogue Input

    private bool m_IsWaitingForInput;

    public void NextSentence(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            m_IsWaitingForInput = false;
        }
    }

    #endregion

    #region Dialogue

    [SerializeField] private List<Dialogue> m_Dialogues = new();
    private GameObject m_DialogueBox;
    private TextMeshProUGUI m_TextArea;
    private TextMeshProUGUI m_NameArea;
    private Image m_SpriteArea;

    private void GetDialogueComponents()
    {
        m_DialogueBox = GameObject.Find("DialogueBox");
        if (m_DialogueBox == null)
        {
            DebugWarning("No DialogueBox found. Please add a DialogueBox to the scene");
        }
        m_TextArea = GameObject.Find("TextArea").GetComponent<TextMeshProUGUI>();
        if (m_TextArea == null)
        {
            DebugWarning("No TextArea found. Please add a TextArea to the scene");
        }
        m_NameArea = GameObject.Find("NameArea").GetComponent<TextMeshProUGUI>();
        if (m_NameArea == null)
        {
            DebugWarning("No NameArea found. Please add a NameArea to the scene");
        }
        m_SpriteArea = GameObject.Find("SpriteArea").GetComponent<Image>();
        if (m_SpriteArea == null)
        {
            DebugWarning("No SpriteArea found. Please add a SpriteArea to the scene");
        }
    }

    private void DisableDialogueBox()
    {
        m_DialogueBox.SetActive(false);
    }

    public void StartDialogue(int _dialogueIndex)
    {
        // Stop the player from moving
        m_DialogueBox.SetActive(true);
        StartCoroutine(DialogueCoroutine(_dialogueIndex));
    }

    private IEnumerator DialogueCoroutine(int _dialogueIndex)
    {
        var dialogue = m_Dialogues[_dialogueIndex];
        for (int i = 0; i < dialogue.m_Sentence.Count; i++)
        {
            m_TextArea.text = "";
            m_NameArea.text = dialogue.m_Name[i];
            m_SpriteArea.sprite = dialogue.m_Sprite[i];
            m_AudioManager.PlayOneShot(dialogue.m_AudioClip[i]);

            foreach (char letter in dialogue.m_Sentence[i].ToCharArray())
            {
                m_TextArea.text += letter;
                yield return new WaitForSeconds(0.05f);
            }

            m_IsWaitingForInput = true;
            yield return new WaitUntil(() => !m_IsWaitingForInput);
            m_AudioManager.Stop();
        }
        EndDialogue();
    }

    private void EndDialogue()
    {
        // Reset the player speed
        DisableDialogueBox();
    }

    #endregion

    #region Audio

    private AudioSource m_AudioManager;

    private void GetAudioManager()
    {
        m_AudioManager = GetComponent<AudioSource>();
        if (m_AudioManager == null)
        {
            DebugWarning("No AudioSource component found. Please add an AudioSource component to the GameObject");
        }
    }

    #endregion

    #region Debugging

    private void DebugWarning(string _warning)
    {
        Debug.LogWarning("Warning: " + _warning);
    }

    #endregion
}

#region Dialogue Class

[System.Serializable]
public class Dialogue
{
    public List<string> m_Name;
    [TextArea(3, 10)]
    public List<string> m_Sentence;
    public List<Sprite> m_Sprite;
    public List<AudioClip> m_AudioClip;
}

#endregion
