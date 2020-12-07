using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public TextMeshProUGUI textAmountS;
    public TextMeshProUGUI textAmountC;
    public TextMeshProUGUI textAmountI;
    public TextMeshProUGUI textSupplies;

    public float supplies = 80;

    public float amountSecurity = 20;
    public float amountCivilian = 200;
    public float amountInjured = 30;

    public float moralSecurity = 2;
    public float moralCivilian = 2;
    public float moralModifierS;
    public float moralModifierC;

    public GameObject[] moralSecurityImages;
    public GameObject[] moralCivilianImages;

    public Group group;

    private void Update()
    {
        textAmountS.text = amountSecurity.ToString();
        textAmountC.text = amountCivilian.ToString();
        textAmountI.text = amountInjured.ToString();
        textSupplies.text = supplies.ToString();
    }

    public void HourlyConsumption()
    {
        //Hourly supplies consumption
        float consumption = ((amountCivilian * group.suppliesC) + (amountSecurity * group.suppliesS) + ((amountInjured * group.suppliesI) * 2)) / 1000;
        consumption = Mathf.Round(consumption * 100) / 100;
        supplies = Mathf.Round(supplies * 100) / 100 - consumption;

        //Hourly moral check
        ChangeMoral();

        //Hourly dying or healing injured 
        float maxAffected = (amountInjured / 4);
        float randomAffectedNumber = Mathf.Round(Random.Range(0, maxAffected - group.suppliesI));
        amountInjured -= randomAffectedNumber;
        amountCivilian += Mathf.Round(Random.Range(0, randomAffectedNumber)); 
    }

    //Calculates the morale based on the supply of the people and the modifiers and then changes the morale display
    public void ChangeMoral()
    {
        moralCivilian = group.suppliesC;
        moralSecurity = group.suppliesS;

        moralCivilian += moralModifierC;
        moralSecurity += moralModifierS;

        if (moralCivilian > 4)
        {
            moralCivilian = 4;
        }
        if (moralCivilian < 0)
        {
            moralCivilian = 0;
        }
        if (moralSecurity > 4)
        {
            moralCivilian = 4;
        }
        if (moralSecurity < 0)
        {
            moralCivilian = 0;
        }

        for (int i = 0; i < moralSecurityImages.Length; i++)
        {
            moralSecurityImages[i].SetActive(false);

            if (moralSecurity == i)
            {
                moralSecurityImages[i].SetActive(true);
            }

            moralCivilianImages[i].SetActive(false);

            if (moralCivilian == i)
            {
                moralCivilianImages[i].SetActive(true);
            }
        }
    }
}
