using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	public int current;
	public float[] position;

	public float counter;
	public int timeMinute;
	public int timeHour;
	public int dateDay;
	public int dateMonth;

	public float suppliesS;
	public float suppliesC;
	public float suppliesI;

	public float supplies;

	public float amountSecurity;
	public float amountCivilian;
	public float amountInjured;

	public float moralSecurity;
	public float moralCivilian;
	public float moralModifierS;
	public float moralModifierC;

	public int[] savedDecisions;

	public SaveData(Movement move, Timer timer, Group group, StatsManager stats, DecisionManager decision)
	{
		current = move.current;
		position = new float[3];
		position[0] = move.transform.position.x;
		position[1] = move.transform.position.y;
		position[2] = move.transform.position.z;

		counter = timer.counter;
		timeMinute = timer.timeMinute;
		timeHour = timer.timeHour;
		dateDay = timer.dateDay;
		dateMonth = timer.dateMonth;

		suppliesS = group.suppliesS;
		suppliesC = group.suppliesC;
		suppliesI = group.suppliesI;

		supplies = stats.supplies;

		amountSecurity = stats.amountSecurity;
		amountCivilian = stats.amountCivilian;
		amountInjured = stats.amountInjured;

		moralSecurity = stats.moralSecurity;
		moralCivilian = stats.moralCivilian;
		moralModifierS = stats.moralModifierS;
		moralModifierC = stats.moralModifierC;

		savedDecisions = decision.savedDecisions;
	}
}
