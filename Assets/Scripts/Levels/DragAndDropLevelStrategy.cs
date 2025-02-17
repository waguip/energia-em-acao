using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragAndDropLevelStrategy : ILevelStrategy
{
    private readonly List<DropSlot> dropSlots;
    private readonly List<DragObject> dragObjects;
    private int correctQuantity;
    private int currentCorrectCount = 0;
    private int currentWrongCount = 0;

    public DragAndDropLevelStrategy(List<DropSlot> dropSlots, List<DragObject> dragObjects)
    {
        this.dropSlots = dropSlots;
        this.dragObjects = dragObjects;
    }

    public void InitializeLevel()
    {
        this.correctQuantity = dragObjects.Count(drag => drag.shouldBeDropped);
    }

    public bool CheckWinCondition()
    {
        int correctCount = dropSlots.Count(slot => slot.dragObject?.shouldBeDropped == true);
        int wrongCount = dropSlots.Count(slot => slot.dragObject?.shouldBeDropped == false);

        if (correctCount > currentCorrectCount)
        {
            GameEvents.CorrectAction();
        }

        if (wrongCount > currentWrongCount)
        {
            GameEvents.IncorrectAction();
        }

        currentCorrectCount = correctCount;
        currentWrongCount = wrongCount;

        return correctCount == correctQuantity && wrongCount == 0;
    }
}
