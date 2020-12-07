using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
	public int currentDialogue;

	public Event[] events;

	public int currentEventTrigger;

	public EventManager eventManager;

	//Starts the dialog
	public void StartEvent()
	{
		if (events[currentEventTrigger].connectedWaypoint != 0)
		{
			eventManager.selectedOpinion = eventManager.decisionManager.LoadDecisions(events[currentEventTrigger].connectedWaypoint);
			eventManager.StartEvent(this);
		}
		else
		{
			if (!eventManager.currentEvent)
			{
				eventManager.StartEvent(this);
			}
		}
	}
}
