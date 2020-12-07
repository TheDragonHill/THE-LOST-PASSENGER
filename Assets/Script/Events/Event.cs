using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
	public int connectedWaypoint;

	[System.Serializable]
	public class Talk
	{
		public string name;

		[TextArea(3, 10)]
		public string sentence;
	}

	public bool endEvent;
	public bool endDecisions;

	public Talk[] talks;

	[System.Serializable]
	public class Decisions
	{
		public string decisionText;
		public int nextDecision;

		[System.Serializable]
		public class EventDecisions
		{
			public string valueName;
			public float valueNumber;
		}

		public EventDecisions[] eventDecisions;

		public GameObject nextWaypoint;
	}

	public bool saveDecision;
	public Decisions[] decisions;
}
