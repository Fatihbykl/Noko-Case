using System;
using System.Collections.Generic;

[Serializable]
public class CharacterStat
{
    public float BaseValue;
    
    private List<float> _modifiers = new List<float>();

    public float GetValue()
    {
        float finalValue = BaseValue;
        foreach (float modifier in _modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    public void AddModifier(float modifier)
    {
        if (modifier != 0) _modifiers.Add(modifier);
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != 0) _modifiers.Remove(modifier);
    }
}