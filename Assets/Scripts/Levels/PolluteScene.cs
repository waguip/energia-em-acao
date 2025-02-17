using UnityEngine;
using UnityEngine.UI;

public class PolluteScene : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite cleanBackground;
    [SerializeField] private Sprite pollutedBackground;
    [SerializeField] private DropSlot[] dropSlots;

    private void OnEnable()
    {
        DragManager.OnDragEnd += CheckLevelConditions;
    }

    private void OnDisable()
    {
        DragManager.OnDragEnd -= CheckLevelConditions;
    }

    private void CheckLevelConditions()
    {
        bool hasPollutant = false;

        // Verifica todos os DropSlots na cena
        foreach (DropSlot slot in dropSlots)
        {
            if (slot.dragObject != null && !slot.dragObject.shouldBeDropped)
            {
                hasPollutant = true;
                break;
            }
        }

        // Atualiza o background baseado na presença de poluentes
        if (hasPollutant)
        {
            backgroundImage.sprite = pollutedBackground;
        }
        else
        {
            backgroundImage.sprite = cleanBackground;
        }
    }
}
