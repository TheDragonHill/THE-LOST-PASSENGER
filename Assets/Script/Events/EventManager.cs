using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI eventText;

    public Animator animator;

    private Queue<string> sentences;
    Queue<string> names;

    public GameObject continueButton;
    public GameObject endButton;

    public GameObject decisions;
    public UnityEngine.UI.Button[] decisionsButtons = new UnityEngine.UI.Button[3];

    bool end;
    public bool endDecisions;

    public int selectedOpinion = 0;

    public EventTrigger currentEvent;

    public UnityEngine.UI.Button campButton;

    public Timer timer;

    public Movement movement;

    public DecisionManager decisionManager;

    public AudioSource eventSound;
    public AudioClip lettersSound;

    public MoralCalculator moralCalculator;

    void Awake()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        decisionManager = GetComponent<DecisionManager>();
    }

    /// <summary>
    /// Starts the first part of the event by triggering it via the DialogTrigger
    /// </summary>
    /// <param name="currentTrigger"></param>
    public void StartEvent(EventTrigger currentTrigger)
    {
        currentEvent = currentTrigger;

        timer.isStop = true;

        //Reset the Buttons
        continueButton.SetActive(true);
        decisions.SetActive(false);
        campButton.interactable = false;

        //Opens the first part of the event
        Event option = currentTrigger.events[selectedOpinion];

        //Checks the number of options and then activates the corresponding buttons
        if (option.decisions.Length != decisionsButtons.Length && option.decisions.Length != 0)
        {
            if (option.decisions.Length == 2)
            {
                decisionsButtons[2].gameObject.SetActive(false);
            }
            if (option.decisions.Length == 1)
            {
                decisionsButtons[1].gameObject.SetActive(false);
                decisionsButtons[2].gameObject.SetActive(false);
            }
        }

        end = option.endEvent;
        endDecisions = option.endDecisions;

        //Changes the corresponding values of the buttons
        for (int i = 0; i < option.decisions.Length; i++)
        {
            //Set text
            decisionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(option.decisions[i].decisionText.ToString());

            if (!end && currentTrigger.events[currentTrigger.currentEventTrigger].connectedWaypoint == 0)
            {
                //Set decision number
                decisionsButtons[i].GetComponent<DecisionButtons>().decisionNumber = option.decisions[i].nextDecision;
            }
        }

        //Opens the event box
        animator.SetBool("IsOpen", true);

        StartTalk(option);

        // SetupButtons to select new options
        for (int i = 0; i < decisionsButtons.Length; i++)
        {
            decisionsButtons[i].onClick.RemoveAllListeners();

            if (!endDecisions)
            {
                decisionsButtons[i].onClick.AddListener(() => StartEvent(currentTrigger));
            }
        }

        //Checks whether there are decisions and then enables functions in the decision
        if (option.decisions != null)
        {
            //Go through all the decisons
            for (int i = 0; i < option.decisions.Length; i++)
            {
                int yolo = i;

                if (option.decisions[i].eventDecisions != null)
                {
                    if (currentEvent.GetComponent<MoralCalculator>())
                    {
                        for (int j = 0; j < option.decisions[i].eventDecisions.Length; j++)
                        {
                            int yolo2 = j;
                            decisionsButtons[yolo].onClick.AddListener(() => decisionsButtons[yolo].GetComponent<DecisionButtons>().SetDecisionFunction(option.decisions[yolo].eventDecisions[yolo2].valueName, moralCalculator.CalculateMoral(option.decisions[yolo].eventDecisions[yolo2].valueName, option.decisions[yolo].eventDecisions[yolo2].valueNumber)));
                        }
                    }
                    else
                    {
                        for (int j = 0; j < option.decisions[i].eventDecisions.Length; j++)
                        {
                            int yolo2 = j;
                            decisionsButtons[yolo].onClick.AddListener(() => decisionsButtons[yolo].GetComponent<DecisionButtons>().SetDecisionFunction(option.decisions[yolo].eventDecisions[yolo2].valueName, option.decisions[yolo].eventDecisions[yolo2].valueNumber));
                        }
                    }
                }

                //Allows you to skip waypoints after an event to take a different path
                if (option.decisions[i].nextWaypoint != null)
                {
                    decisionsButtons[yolo].onClick.AddListener(() => decisionsButtons[yolo].GetComponent<DecisionButtons>().SkipWaypoints(option.decisions[yolo].nextWaypoint));
                }

                //Checks whether it has to save the player's decision in order to use it in next events
                if (option.saveDecision)
                {
                    decisionsButtons[yolo].onClick.AddListener(() => decisionManager.SaveDecision(movement.current, decisionsButtons[yolo].GetComponent<DecisionButtons>().decisionNumber));
                }
            }
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// Displays the next sentence of the Event
    /// </summary>
    public void DisplayNextSentence()
    {
        //Reset Buttons
        if (end == true && sentences.Count == 1 && !endDecisions)
        {
            continueButton.SetActive(false);
            endButton.SetActive(true);
        }
        else if (end == false && sentences.Count == 1)
        {
            continueButton.SetActive(false);
            decisions.SetActive(true);
        }

        string sentence = sentences.Dequeue();
        string charName = names.Dequeue();

        //Set Charactername
        if (!string.IsNullOrEmpty(charName) && !nameText.text.Equals(charName))
        {
            nameText.SetText(charName);
        }

        //Stop typing
        StopAllCoroutines();

        //Start typing
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Animates the words
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        eventText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            eventText.text += letter;
            eventSound.Play();
            yield return null;
        }
    }

    public void DisableButtons()
    {
        for (int i = 0; i < decisionsButtons.Length; i++)
        {
            decisionsButtons[i].gameObject.SetActive(false);
        }

        continueButton.gameObject.SetActive(false);
        endButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Ends the event
    /// </summary>
    public void EndEvent()
    {
        selectedOpinion = 0;

        if (movement.current == movement.waypoint.Count)
        {
            StartCoroutine(currentEvent.GetComponent<StartCredits>().LoadLevel());
        }

        StopCoroutine(nameof(TypeSentence));
        animator.SetBool("IsOpen", false);

        //Reset the Buttons
        continueButton.SetActive(false);
        decisions.SetActive(false);
        endButton.SetActive(false);

        currentEvent = null;

        for (int i = 0; i < decisionsButtons.Length; i++)
        {
            decisionsButtons[i].gameObject.SetActive(true);
        }

        if (end || endDecisions)
        {
            campButton.interactable = true;

            timer.isStop = false;

            movement.isStop = false;
        }
    }

    void StartTalk(Event option)
    {
        sentences.Clear();
        names.Clear();

        //Go through talks
        for (int i = 0; i < option.talks.Length; i++)
        {
            names.Enqueue(option.talks[i].name);
            sentences.Enqueue(option.talks[i].sentence);
        }
    }
}
