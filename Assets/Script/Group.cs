using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group : MonoBehaviour
{
    public float suppliesS;
    public float suppliesC;
    public float suppliesI;

    public GameObject manager;
    public StatsManager statsManager;

    public GameObject clock;

    public Slider securitySlider;
    public Slider civilianSlider;
    public Slider injuredSlider;
    public Slider trainingSlider;

    private void Start()
    {
        //Sets the maximum of the slider
        securitySlider.maxValue = 4;//SecuritySlider
        civilianSlider.maxValue = 4;//CivilianSlider
        injuredSlider.maxValue = 4;//InjuredSlider
    }

    //Change the group datas
    public void Security(float vol)
    {
        suppliesS = Mathf.RoundToInt(vol);
    }

    public void Civilian(float vol)
    {
        suppliesC = Mathf.RoundToInt(vol);
    }

    public void Injured(float vol)
    {
        suppliesI = Mathf.RoundToInt(vol);
    }
}
