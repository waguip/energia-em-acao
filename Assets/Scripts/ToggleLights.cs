using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLights : MonoBehaviour, ISpriteMatching
{
    [SerializeField] private Button switchButton;
    [SerializeField] private Sprite switchOn;
    [SerializeField] private Sprite switchOff;
    [SerializeField] private Image chandelier;
    [SerializeField] private Image cover;
    [SerializeField] private bool isOn = true;
    [SerializeField] private bool shouldBeOn = false;

    private void OnEnable()
    {
        switchButton.onClick.AddListener(ToggleLight);
    }

    private void OnDisable()
    {
        switchButton.onClick.RemoveListener(ToggleLight);
    }

    private void ToggleLight()
    {
        isOn = !isOn;
        cover.color = isOn ? new Color(0, 0, 0, 0) : new Color(0, 0, 0, 0.9f);
        // chandelier.color = isOn ? new Color(1, 1, 1, 1) : new Color(0, 0, 0.41f, 1f);
        switchButton.image.sprite = isOn ? switchOn : switchOff;
    }

    public bool IsOnCorrectSprite()
    {
        return isOn == shouldBeOn;
    }
}
