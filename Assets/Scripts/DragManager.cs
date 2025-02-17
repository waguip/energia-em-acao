using UnityEngine;
using DG.Tweening;
using System;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance { get; private set; }
    public static event Action OnDragStart;
    public static event Action OnDragEnd;
    private Rect boundingBox;
    private DragObject currentDraggedObject = null;
    public DragObject CurrentDraggedObject => currentDraggedObject;
    [SerializeField] private Transform dragLayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterDraggedObject(DragObject drag)
    {
        currentDraggedObject = drag;
        drag.transformAfterDrag = drag.OriginalSlot;
        drag.transform.SetParent(dragLayer);
        if (drag.spriteOnDrag != null)
        {
            drag.SetSprite(drag.spriteOnDrag);
        }
        drag.image.raycastTarget = false;
        if (drag.currentDropSlot != null)
        {
            drag.currentDropSlot.dragObject = null;
            drag.currentDropSlot = null;
        }
        OnDragStart?.Invoke();
    }

    public void UnregisterDraggedObject(DragObject drag)
    {
        Drop(drag);

        OnDragEnd?.Invoke();
        currentDraggedObject = null;
    }

    public void Drop(DragObject dragObject)
    {
        dragObject.transform.SetParent(dragObject.transformAfterDrag);
        dragObject.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.OutBounce);
        dragObject.image.raycastTarget = true;
    }
}
