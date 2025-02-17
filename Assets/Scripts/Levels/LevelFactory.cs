using System;
using System.Collections.Generic;

public static class LevelFactory
{
    public static ILevelStrategy CreateLevelStrategy(LevelType levelType, List<DropSlot> dropSlots = null, List<DragObject> dragObjects = null, List<ISpriteMatching> spriteObjects = null)
    {
        switch (levelType)
        {
            case LevelType.DragAndDrop:
                return new DragAndDropLevelStrategy(dropSlots, dragObjects);
            case LevelType.SpriteMatching:
                return new SpriteMatchingLevelStrategy(spriteObjects);
            default:
                throw new ArgumentException("Tipo de nível inválido");
        }
    }
}

public enum LevelType
{
    DragAndDrop,
    SpriteMatching,
    None,
}
