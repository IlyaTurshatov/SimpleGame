using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour {

    public int totalSheeps;

    public int maleSheeps;
    public int femaleSheeps;

    public int youngSheeps;

    public int matureSheeps;
    public int pregnantSheeps;

	void OnGUI()
    {
        GUI.Label(new Rect(4, 4, 130, 30), "Total: " + totalSheeps);
        GUI.Label(new Rect(4, 34, 130, 30), "Male: " + maleSheeps);
        GUI.Label(new Rect(4, 64, 130, 30), "Female: " + femaleSheeps);
        GUI.Label(new Rect(4, 94, 130, 30), "Young: " + youngSheeps);
        GUI.Label(new Rect(4, 124, 130, 30), "Mature: " + matureSheeps);
        GUI.Label(new Rect(4, 154, 130, 30), "Pregnant: " + pregnantSheeps);

    }

    public void NewSheep(LifeCycle sheep)
    {
        totalSheeps++;
        if (sheep.isFemale)
        {
            femaleSheeps++;
            if (sheep.state == LifeCycle.State.pregnant)
                pregnantSheeps--;
        }
        else
            maleSheeps++;
        youngSheeps++;
    }

    public void Died(LifeCycle sheep)
    {
        totalSheeps--;
        if (sheep.isFemale)
        {
            femaleSheeps--;
            if (sheep.state == LifeCycle.State.pregnant)
                pregnantSheeps--;
        }
        else
            maleSheeps--;
        matureSheeps--;
    }

    public void Pregnant()
    {
        pregnantSheeps++;
    }

    public void NotPregnant()
    {
        pregnantSheeps--;
    }

    public void Mature()
    {
        youngSheeps--;
        matureSheeps++;
    }

}
