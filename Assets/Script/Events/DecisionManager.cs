using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionManager : MonoBehaviour
{
	public int[] savedDecisions;

	//Saves the important decision of the player
	public void SaveDecision(int waypointNumber, int decisionNumber)
	{
		for (int i = 0; i < savedDecisions.Length; i++)
		{
			if (i == waypointNumber)
			{
				savedDecisions[i] = decisionNumber;
			}
		}
	}

	//Loads a decision by the player to use for the next event
	public int LoadDecisions(int waypointNumber)
	{
		int correctDecision = 0;

		for (int i = 0; i < savedDecisions.Length; i++)
		{
			if (i == waypointNumber)
			{
				correctDecision = savedDecisions[i];
			}
		}

		return correctDecision;
	}
}
