using System;
using UnityEngine;

public class ProgressWithAction : IProgress<float>
{
    public float CurrentProgressFloatValue {get; private set;}
    public event Action FloatValueChanged;

    public void Report(float value)
    {
        CurrentProgressFloatValue = value;
        FloatValueChanged?.Invoke();
    }
}
