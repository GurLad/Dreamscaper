using Godot;
using System;
using System.Collections.Generic;

public class Level
{
    public event Action OnComplete;
    public event Action<string> OnMatched;

    private List<string> targets;
    private List<Card> cards;
    private Equation equation;
    private Deck deck;

    public Level()
    {
        // TODO: Data...
        equation.OnCombined += CheckMatch;
    }

    private void CheckMatch(string result)
    {
        if (targets.Contains(result))
        {
            equation.RemoveAllCards().ForEach(a => cards.Remove(a));
            OnMatched?.Invoke(result);
            targets.Remove(result);
            if (targets.Count <= 0)
            {
                OnComplete?.Invoke();
            }
        }
    }
}
