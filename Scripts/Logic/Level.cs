using Godot;
using System;
using System.Collections.Generic;

public class Level
{
    public event Action OnComplete;
    public event Action<List<bool>> OnUpdated;

    private List<string> targets;
    private List<Equation> equations;
    private List<Card> cards;
    private Deck deck;

    public Level()
    {
        // TODO: Data...
        equations.ForEach(a =>
        {
            a.OnCombined += (s) => CheckComplete();
            a.OnUnattached += UpdateScore;
        });
    }

    private void CheckComplete()
    {
        UpdateScore();
        if (CalculateScore().Complete)
        {
            OnComplete?.Invoke();
        }
    }

    private void UpdateScore()
    {
        OnUpdated?.Invoke(CalculateScore().Correct);
    }

    private (bool Complete, List<bool> Correct) CalculateScore()
    {
        List<string> results = equations.ConvertAll(a => a.Result).FindAll(a => a != null);
        List<bool> correct = targets.ConvertAll(a => false);
        bool complete = true;
        targets.ForEach((a, i) =>
        {
            if (!results.Contains(a))
            {
                complete = false;
            }
            else
            {
                results.Remove(a);
                correct[i] = true;
            }
        });
        return (complete, correct);
    }
}
