using Godot;
using System;
using System.Collections.Generic;

public class Level
{
    public event Action OnComplete;
    public event Action<string> OnMatched;

    public List<Card> Cards { get; } = new List<Card>();
    private List<string> targets { get; } = new List<string>();
    private Equation equation = new Equation();

    public Level(LevelData data)
    {
        Cards.AddRange(data.Cards.ConvertAll(CardDataHolder.GetCard));
        targets.AddRange(data.Targets);
        equation.OnCombined += CheckMatch;
    }

    private void CheckMatch(string result)
    {
        if (targets.Contains(result))
        {
            equation.RemoveAllCards().ForEach(a => Cards.Remove(a));
            OnMatched?.Invoke(result);
            targets.Remove(result);
            if (targets.Count <= 0)
            {
                OnComplete?.Invoke();
            }
        }
    }
}
