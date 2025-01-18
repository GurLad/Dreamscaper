using Godot;
using System;
using System.Collections.Generic;

public class Level
{
    public event Action OnComplete;
    public event Action<string> OnMatched;

    public List<Card> Cards { get; } = new List<Card>();
    public Equation Equation { get; }= new Equation();
    public List<string> Targets { get; } = new List<string>();

    public Level(LevelData data)
    {
        Cards.AddRange(data.Cards.ConvertAll(CardDataHolder.GetCard));
        Targets.AddRange(data.Targets);
        Equation.OnCombined += CheckMatch;
    }

    private void CheckMatch(string result)
    {
        if (Targets.Contains(result))
        {
            OnMatched?.Invoke(result);
            Targets.Remove(result);
            if (Targets.Count <= 0)
            {
                OnComplete?.Invoke();
            }
        }
    }
}
