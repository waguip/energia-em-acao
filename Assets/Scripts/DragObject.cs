using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 centerPoint;
    private Vector2 WorldCenterPoint => transform.TransformPoint(centerPoint);
    [HideInInspector] public Image image;
    private Transform originalSlot;
    public Transform OriginalSlot => originalSlot;
    public Transform transformAfterDrag;
    public bool shouldBeDropped = true;
    public DropSlot currentDropSlot;
    public Sprite spriteOnDrag;
    public Sprite spriteOnDrop;

    private void Awake()
    {
        centerPoint = (transform as RectTransform).rect.center;
        image = GetComponent<Image>();
        originalSlot = transform.parent;
        spriteOnDrag = image.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragManager.Instance.RegisterDraggedObject(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.Translate(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragManager.Instance.UnregisterDraggedObject(this);
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}