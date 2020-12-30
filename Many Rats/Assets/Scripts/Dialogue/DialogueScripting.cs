using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScripting : MonoBehaviour
{
    [SerializeField] float textDelay;

    private int currentDialogueLine;
    private int currentDialoguePlaying;
    private bool activeDialogue;

    [SerializeField] private GameObject dialogue1Group;
    [SerializeField] private Text dialogue1SpeakerName;
    [SerializeField] private Text dialogue1Text;
    [SerializeField] private Image dialogue1SpeakerImage;
    [SerializeField] private GameObject dialogue2Group;
    [SerializeField] private Text dialogue2SpeakerName;
    [SerializeField] private Text dialogue2Text;
    [SerializeField] private Image dialogue2SpeakerImage;
    [SerializeField] private GameObject tutorialPromptGroup;
    [SerializeField] private Text tutorialPromptText;

    [SerializeField] private Sprite ratgusherChatSprite;
    [SerializeField] private Sprite witchChatSprite;
    [SerializeField] private Sprite mayorChatSprite;

    void Start()
    {
        activeDialogue = false;
        PlayDialogue1();
    }

    void Update()
    {
        if (activeDialogue)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (currentDialoguePlaying == 2 && currentDialogueLine == 6 || currentDialoguePlaying == 3 && currentDialogueLine == 6)
                {
                    // do not progress with space if they do not cast spell for dialogue 2
                }
                else
                {
                    currentDialogueLine++;
                    Debug.Log(currentDialogueLine);
                    switch (currentDialoguePlaying)
                    {
                        case 1:
                            PlayDialogue1();
                            break;
                        case 2:
                            PlayDialogue2();
                            break;
                        case 3:
                            PlayDialogue3();
                            break;
                        case 4:
                            PlayDialogue4();
                            break;
                        default:
                            break;
                    }
                }
            }

            // for dialogue 2 (tutorial), progress past the click prompt only if the player left clicks
            if(Input.GetButtonDown("Fire1"))
            {
                if (currentDialoguePlaying == 2 && currentDialogueLine == 6)
                {
                    currentDialogueLine++;
                    PlayDialogue2();
                }
            }

            // for dialogue 3 (cheese), progress past the click prompt only if the player right clicks
            if (Input.GetButtonDown("Fire2"))
            {
                if (currentDialoguePlaying == 3 && currentDialogueLine == 6)
                {
                    currentDialogueLine++;
                    PlayDialogue3();
                }
            }
        }

        if(!activeDialogue)
        {
            currentDialogueLine = 1;
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            PlayDialogue2();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayDialogue3();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayDialogue4();
        }
    }

    IEnumerator AnimateText1(string currentDisplayText)
    {
        for(int i=0; i< currentDisplayText.Length + 1; i++)
        {
            dialogue1Text.text = currentDisplayText.Substring(0,i);
            yield return new WaitForSecondsRealtime(textDelay);
        }
    }

    IEnumerator AnimateText2(string currentDisplayText)
    {
        for (int i = 0; i < currentDisplayText.Length + 1; i++)
        {
            dialogue2Text.text = currentDisplayText.Substring(0, i);
            yield return new WaitForSecondsRealtime(textDelay);
        }
    }

    IEnumerator AnimateText3(string currentDisplayText)
    {
        for (int i = 0; i < currentDisplayText.Length + 1; i++)
        {
            tutorialPromptText.text = currentDisplayText.Substring(0, i);
            yield return new WaitForSecondsRealtime(textDelay);
        }
    }

    private void SkipTextAnimation()
    {
        StopAllCoroutines();
    }

    public void PlayDialogue1()
    {
        Time.timeScale = 0;
        activeDialogue = true;
        currentDialoguePlaying = 1;

        dialogue1SpeakerName.text = "Ratgusher";
        dialogue1SpeakerImage.sprite = ratgusherChatSprite;
        dialogue2SpeakerName.text = "Mayor";
        dialogue2SpeakerImage.sprite = mayorChatSprite;

        switch (currentDialogueLine)
        {
            case 1:
                dialogue1Group.SetActive(true);
                StartCoroutine(AnimateText1(DialogueText.DialogueIntro_1));
                break;
            case 2:
                SkipTextAnimation();
                dialogue1Text.text = DialogueText.DialogueIntro_1;
                break;
            case 3:
                dialogue1Group.SetActive(false);
                dialogue2Group.SetActive(true);
                StartCoroutine(AnimateText2(DialogueText.DialogueIntro_2));
                break;
            case 4:
                SkipTextAnimation();
                dialogue2Text.text = DialogueText.DialogueIntro_2;
                break;
            case 5:
                dialogue2Group.SetActive(false);
                dialogue1Group.SetActive(true);
                StartCoroutine(AnimateText1(DialogueText.DialogueIntro_3));
                break;
            case 6:
                SkipTextAnimation();
                dialogue1Text.text = DialogueText.DialogueIntro_3;
                break;
            case 7:
                dialogue1Group.SetActive(false);
                dialogue2Group.SetActive(true);
                StartCoroutine(AnimateText2(DialogueText.DialogueIntro_4));
                break;
            case 8:
                SkipTextAnimation();
                dialogue2Text.text = DialogueText.DialogueIntro_4;
                break;
            case 9:
                StartCoroutine(AnimateText2(DialogueText.DialogueIntro_5));
                break;
            case 10:
                SkipTextAnimation();
                dialogue2Text.text = DialogueText.DialogueIntro_5;
                break;
            case 11:
                dialogue1Group.SetActive(false);
                dialogue2Group.SetActive(false);
                activeDialogue = false;
                Time.timeScale = 1;
                break;
            default:
                Debug.Log(currentDialogueLine);
                break;
        }
    }

    public void PlayDialogue2()
    {
        Time.timeScale = 0;
        activeDialogue = true;
        currentDialoguePlaying = 2;

        dialogue1SpeakerName.text = "Ratgusher";
        dialogue1SpeakerImage.sprite = ratgusherChatSprite;

        switch (currentDialogueLine)
        {
            case 1:
                dialogue1Group.SetActive(true);
                StartCoroutine(AnimateText1(DialogueText.DialogueTutorial_1));
                break;
            case 2:
                SkipTextAnimation();
                dialogue1Text.text = DialogueText.DialogueTutorial_1;
                break;
            case 3:
                StartCoroutine(AnimateText1(DialogueText.DialogueTutorial_1));
                break;
            case 4:
                SkipTextAnimation();
                dialogue1Text.text = DialogueText.DialogueTutorial_2;
                break;
            case 5:
                tutorialPromptGroup.SetActive(true);
                dialogue1Group.SetActive(false);
                StartCoroutine(AnimateText3(DialogueText.DialogueTutorial_3));
                break;
            case 6:
                SkipTextAnimation();
                tutorialPromptText.text = DialogueText.DialogueTutorial_3;
                break;
            case 7:
                dialogue1Group.SetActive(true);
                tutorialPromptGroup.SetActive(false);
                StartCoroutine(AnimateText1(DialogueText.DialogueTutorial_4));
                break;
            case 8:
                SkipTextAnimation();
                dialogue1Text.text = DialogueText.DialogueTutorial_4;
                break;
            case 9:
                dialogue1Group.SetActive(false);
                tutorialPromptGroup.SetActive(false);
                activeDialogue = false;
                Time.timeScale = 1;
                break;
            default:
                Debug.Log(currentDialogueLine);
                break;
        }
    }

    public void PlayDialogue3()
    {
        Time.timeScale = 0;
        activeDialogue = true;
        currentDialoguePlaying = 3;

        dialogue1SpeakerName.text = "Ratgusher";
        dialogue1SpeakerImage.sprite = ratgusherChatSprite;

        switch (currentDialogueLine)
        {
            case 1:
                dialogue1Group.SetActive(true);
                StartCoroutine(AnimateText1(DialogueText.DialogueCheese_1));
                break;
            case 2:
                SkipTextAnimation();
                dialogue1Text.text = DialogueText.DialogueCheese_1;
                break;
            case 3:
                StartCoroutine(AnimateText1(DialogueText.DialogueCheese_1));
                break;
            case 4:
                SkipTextAnimation();
                dialogue1Text.text = DialogueText.DialogueCheese_2;
                break;
            case 5:
                tutorialPromptGroup.SetActive(true);
                dialogue1Group.SetActive(false);
                StartCoroutine(AnimateText3(DialogueText.DialogueCheese_3));
                break;
            case 6:
                SkipTextAnimation();
                tutorialPromptText.text = DialogueText.DialogueCheese_3;
                break;
            case 7:
                dialogue1Group.SetActive(true);
                tutorialPromptGroup.SetActive(false);
                StartCoroutine(AnimateText1(DialogueText.DialogueCheese_4));
                break;
            case 8:
                SkipTextAnimation();
                dialogue1Text.text = DialogueText.DialogueCheese_4;
                break;
            case 9:
                dialogue1Group.SetActive(false);
                tutorialPromptGroup.SetActive(false);
                activeDialogue = false;
                Time.timeScale = 1;
                break;
            default:
                Debug.Log(currentDialogueLine);
                break;
        }
    }

    public void PlayDialogue4()
    {
        Time.timeScale = 0;
        activeDialogue = true;
        currentDialoguePlaying = 4;

        dialogue1SpeakerName.text = "Ratgusher";
        dialogue1SpeakerImage.sprite = ratgusherChatSprite;
        dialogue2SpeakerName.text = "Witch";
        dialogue2SpeakerImage.sprite = witchChatSprite;

        switch (currentDialogueLine)
        {
            case 1:
                dialogue2Group.SetActive(true);
                StartCoroutine(AnimateText2(DialogueText.DialogueWitch_1));
                break;
            case 2:
                SkipTextAnimation();
                dialogue2Text.text = DialogueText.DialogueWitch_1;
                break;
            case 3:
                dialogue1Group.SetActive(true);
                dialogue2Group.SetActive(false);
                StartCoroutine(AnimateText1(DialogueText.DialogueWitch_2));
                break;
            case 4:
                SkipTextAnimation();
                dialogue1Text.text = DialogueText.DialogueWitch_2;
                break;
            case 5:
                dialogue2Group.SetActive(true);
                dialogue1Group.SetActive(false);
                StartCoroutine(AnimateText2(DialogueText.DialogueWitch_3));
                break;
            case 6:
                SkipTextAnimation();
                dialogue2Text.text = DialogueText.DialogueWitch_3;
                break;
            case 7:
                dialogue1Group.SetActive(false);
                dialogue2Group.SetActive(false);
                activeDialogue = false;
                Time.timeScale = 1;
                break;
            default:
                Debug.Log(currentDialogueLine);
                break;
        }
    }
}
