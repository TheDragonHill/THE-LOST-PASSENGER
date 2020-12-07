using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoralCalculator : MonoBehaviour
{
	public StatsManager stats;

    /// <summary>
    /// Calculates the loss of people the player loses during a given event based on people's morale
    /// </summary>
    /// <param name="valuesName"></param>
    /// <param name="valuesNumber"></param>
    /// <returns></returns>
	public float CalculateMoral(string valuesName, float valuesNumber)
	{

        if (valuesName == "amountSecurity")
        {
            if (stats.moralSecurity != 0)
            {
                valuesNumber -= (stats.amountSecurity / 3) / stats.moralSecurity;
            }
            else
            {
                valuesNumber -= (stats.amountSecurity / 3);
            }
        }
        if (valuesName == "amountCivilian")
        {
            if (stats.moralCivilian != 0)
            {
                valuesNumber -= (stats.amountCivilian / 3) / stats.moralCivilian;
            }
            else
            {
                valuesNumber -= (stats.amountCivilian / 3);
            }
        }

        return Mathf.RoundToInt(valuesNumber);
	}
}
