using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public Timer timer;
    public Movement movement;
    public Group group;
    public StatsManager stats;
    public DecisionManager decision;

    public GameObject groupWindow;

    private void Start()
    {
        MenuData data = SaveSystem.LoadMenu();

        if (data.isLoad == true)
        {
            Load();

            SaveSystem.SaveMenu(false);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void Pause()
    {
        timer.isStop = true;
        movement.isStop = true;
    }

    public void Resume()
    {
        timer.isStop = false;
        movement.isStop = false;
    }

    public void Save()
    {
        SaveSystem.Save(movement, timer, group, stats, decision);
    }

    public void Load()
    {
        SaveData data = SaveSystem.Load();

        movement.current = data.current;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        movement.transform.position = position;

        timer.counter = data.counter;
        timer.timeMinute = data.timeMinute;
        timer.timeHour = data.timeHour;
        timer.dateDay = data.dateDay;
        timer.dateMonth = data.dateMonth;

        group.suppliesS = data.suppliesS;
        group.suppliesC = data.suppliesC;
        group.suppliesI = data.suppliesI;

        stats.supplies = data.supplies;

        stats.amountSecurity = data.amountSecurity;
        stats.amountCivilian = data.amountCivilian;
        stats.amountInjured = data.amountInjured; 

        stats.moralSecurity = data.moralSecurity;
        stats.moralCivilian = data.moralCivilian;
        stats.moralModifierS = data.moralModifierS;
        stats.moralModifierC = data.moralModifierC;

        decision.savedDecisions = data.savedDecisions;
    }
}
