using UnityEngine;
public class Timer
{
    public float StartValue;
    public float CurrentValue;

    public Timer(float UstartValue)
    {
        StartValue = UstartValue;
        CurrentValue = StartValue;
    }

    public void Refresh()
    {
        CurrentValue -= Time.deltaTime;
    }

    public void Add(float value)
    {
        CurrentValue += value;
    }
    public void Remove(float value)
    {
        CurrentValue -= value;
    }
    public void Reset()
    {
        CurrentValue = StartValue;
    }
    public bool Done()
    {
        if(CurrentValue <= 0)
        {
            CurrentValue = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}
