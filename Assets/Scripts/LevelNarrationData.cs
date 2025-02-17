using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelNarrationData", menuName = "ScriptableObjects/LevelNarrationData", order = 1)]
public class LevelNarrationData : ScriptableObject
{
    public int sceneIndex;
    public AudioClip levelStartNarration;
    public AudioClip correctActionNarration;
    public AudioClip incorrectActionNarration;
    public AudioClip levelCompleteNarration;
}
