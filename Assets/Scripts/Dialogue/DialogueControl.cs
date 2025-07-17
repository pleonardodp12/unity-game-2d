using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj;
    public Image profileSprite;
    public Text speechText;
    public Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;

    private bool isShowing;
    private int index;
    private string[] sentences;

    public static DialogueControl instance;

    public bool IsShowing { get => isShowing; set => isShowing = value; }
    public string[] Sentences { get => sentences; set => sentences = value; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in Sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if(speechText.text == Sentences[index])
        {
            if(index < Sentences.Length -1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                Sentences = null;
                IsShowing = false;
            }
        }
    }

    public void Speech(string[] txt)
    {
        if(!IsShowing)
        {
            dialogueObj.SetActive(true);
            Sentences = txt;
            StartCoroutine(TypeSentence());
            IsShowing = true;
        }
    }
}
