using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DecisionButtons : MonoBehaviour
{
    public StatsManager statsManager;
    public Timer timer;
    public Movement movement;
    public DecisionManager decisionManager;

    public int decisionNumber;

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(() => SetDecisionNumber());
    }

	public void SetDecisionNumber()
	{
        if (GetComponentInParent<EventManager>().endDecisions)
        {
            GetComponentInParent<EventManager>().EndEvent();
        }
        else
        {
            GetComponentInParent<EventManager>().selectedOpinion = decisionNumber;
        }
	}

    /// <summary>
    /// check Input and change the values, Time, Supplies, Moral, Number of People
    /// </summary>
    /// <param name="valuesName"></param>
    /// <param name="valuesNumber"></param>
	public void SetDecisionFunction(string valuesName, float valuesNumber)
	{
        if (valuesName == "supplies")
        {
            statsManager.supplies += valuesNumber;
        }
        if (valuesName == "amountSecurity")
        {
            statsManager.amountSecurity += valuesNumber;
        }
        if (valuesName == "amountCivilian")
        {
            statsManager.amountCivilian += valuesNumber;
        }
        if (valuesName == "amountInjured")
        {
            statsManager.amountInjured += valuesNumber;
        }
        if (valuesName == "moralCivilian")
        {
            statsManager.moralModifierC += valuesNumber;
            statsManager.ChangeMoral();
        }
        if (valuesName == "moralSecurity")
        {
            statsManager.moralModifierS += valuesNumber;
            statsManager.ChangeMoral();
        }
        if (valuesName == "timeMinute")
        {
            timer.timeMinute += Mathf.RoundToInt(valuesNumber);
        }
        if (valuesName == "timeHour")
        {
            timer.timeHour += Mathf.RoundToInt(valuesNumber);
            statsManager.HourlyConsumption();
        }
        if (valuesName == "speed")
        {
            movement.speed += valuesNumber;
        }
    }

    /// <summary>
    /// Skips waypoints in the list to make the player take a different path
    /// </summary>
    /// <param name="nextWaypoint"></param>
    public void SkipWaypoints(GameObject nextWaypoint)
    {
        for (int i = 0; i < movement.waypoint.Count; i++)
        {
            if (nextWaypoint.name == movement.waypoint[i].name)
            {
                movement.current = i;
            }
        }
    }

    public void SaveDecisionButton()
    {
        decisionManager.SaveDecision(movement.current, decisionNumber);
    }
}
