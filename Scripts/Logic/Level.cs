using Godot;
using System;
using System.Collections.Generic;

public class Level
{
    public event Action OnComplete;
    public event Action<string> OnMatched;

    public List<Card> Cards { get; } = new List<Card>();
    public Equation Equation { get; }= new Equation();
    private List<string> targets { get; } = new List<string>();

    public Level(LevelData data)
    {
        Cards.AddRange(data.Cards.ConvertAll(CardDataHolder.GetCard));
        targets.AddRange(data.Targets);
        Equation.OnCombined += CheckMatch;
    }

    private void CheckMatch(string result)
    {
        if (targets.Contains(result))
        {
            OnMatched?.Invoke(result);
            targets.Remove(result);
            if (targets.Count <= 0)
            {
                OnComplete?.Invoke();
            }
        }
    }
}
