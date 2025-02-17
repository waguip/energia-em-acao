using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    public RectTransform RectTransform => rectTransform;
    public DragObject dragObject;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragObject drag = DragManager.Instance.CurrentDraggedObject;
        if (drag == null)
        {
            return;
        }

        if (dragObject != null)
        {
            dragObject.transformAfterDrag = dragObject.OriginalSlot;
            dragObject.currentDropSlot = null;
            DragManager.Instance.Drop(dragObject);
            dragObject = null;
        }

        AssignDragObjectToSlot(drag);
    }

    private void AssignDragObjectToSlot(DragObject drag)
    {
        drag.transformAfterDrag = rectTransform;
        drag.currentDropSlot = this;
        if (drag.spriteOnDrop != null)
        {
            drag.SetSprite(drag.spriteOnDrop);
        }
        dragObject = drag;
    }
}
