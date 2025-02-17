using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteMatchingLevelStrategy : ILevelStrategy
{
    private List<ISpriteMatching> spriteObjects;
    private int correctQuantity;
    private int currentCorrectCount = 0;

    public SpriteMatchingLevelStrategy(List<ISpriteMatching> objects)
    {
        spriteObjects = objects;
    }

    public void InitializeLevel()
    {
        correctQuantity = spriteObjects.Count;
    }

    public bool CheckWinCondition()
    {
        int correctCount = spriteObjects.Count(obj => obj.IsOnCorrectSprite());

        if (correctCount > currentCorrectCount)
        {
            GameEvents.CorrectAction();
        }

        currentCorrectCount = correctCount;

        return currentCorrectCount == correctQuantity;
    }
}
