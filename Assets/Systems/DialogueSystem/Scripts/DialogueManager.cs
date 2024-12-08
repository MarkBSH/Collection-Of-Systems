using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
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

    [SerializeField] private List<Dialogue> m_Dialogues = new();
    private GameObject m_DialogueBox;
    private TextMeshProUGUI m_TextArea;
    private TextMeshProUGUI m_NameArea;
    private Image m_SpriteArea;
    private AudioSource m_AudioManager;
    private bool m_IsWaitingForInput;

    private float m_normalPlayerSpeed = 5f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        m_DialogueBox = GameObject.Find("DialogueBox");
        m_TextArea = GameObject.Find("TextArea").GetComponent<TextMeshProUGUI>();
        m_NameArea = GameObject.Find("NameArea").GetComponent<TextMeshProUGUI>();
        m_SpriteArea = GameObject.Find("SpriteArea").GetComponent<Image>();
        m_AudioManager = GetComponent<AudioSource>();
        m_DialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (m_IsWaitingForInput && Input.GetKeyDown(KeyCode.Space))
        {
            m_IsWaitingForInput = false;
        }
    }

    public void StartDialogue(int _dialogueIndex)
    {
        m_normalPlayerSpeed = BasicPlayerMovement.m_Speed;
        BasicPlayerMovement.m_Speed = 0;

        m_DialogueBox.SetActive(true);
        Debug.Log("Starting dialogue");
        StartCoroutine(DialogueCoroutine(_dialogueIndex));
    }

    private IEnumerator DialogueCoroutine(int _dialogueIndex)
    {
        for (int i = 0; i < m_Dialogues[_dialogueIndex].m_Sentence.Count; i++)
        {
            m_TextArea.text = "";
            m_NameArea.text = m_Dialogues[_dialogueIndex].m_Name[i];
            m_SpriteArea.sprite = m_Dialogues[_dialogueIndex].m_Sprite[i];
            m_AudioManager.PlayOneShot(m_Dialogues[_dialogueIndex].m_AudioClip[i]);

            foreach (char letter in m_Dialogues[_dialogueIndex].m_Sentence[i].ToCharArray())
            {
                m_TextArea.text += letter;
                yield return new WaitForSeconds(0.05f); // Adjust the speed of the text display here
            }

            m_IsWaitingForInput = true;
            yield return new WaitUntil(() => !m_IsWaitingForInput);
            m_AudioManager.Stop(); // Stop the audio after waiting for input
        }
        Debug.Log("Ending dialogue");

        EndDialogue();
    }

    private void EndDialogue()
    {
        BasicPlayerMovement.m_Speed = m_normalPlayerSpeed;

        m_DialogueBox.SetActive(false);
    }
}

[System.Serializable]
public class Dialogue
{
    public List<string> m_Name;
    [TextArea(3, 10)]
    public List<string> m_Sentence;
    public List<Sprite> m_Sprite;
    public List<AudioClip> m_AudioClip;
}
