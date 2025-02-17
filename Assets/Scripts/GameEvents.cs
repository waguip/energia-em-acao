using System;

public static class GameEvents
{
    public static event Action OnLevelEnd;
    public static event Action OnCorrectAction;
    public static event Action OnIncorrectAction;

    public static void LevelEnd() => OnLevelEnd?.Invoke();
    public static void CorrectAction() => OnCorrectAction?.Invoke();
    public static void IncorrectAction() => OnIncorrectAction?.Invoke();
}