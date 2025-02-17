using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChangingSpriteObject : MonoBehaviour, ISpriteMatching
{
    [SerializeField] private Sprite[] sprites;
    public int correctSpriteIndex;
    private Image image;
    [SerializeField] private int currentSpriteIndex;
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;
    public static event Action OnCorrectSprite;
    public static event Action<ChangingSpriteObject> OnRotate;
    public static event Action OnLeaveCorrectSprite;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (sprites.Length < 2)
        {
            Debug.LogError("O objeto " + gameObject.name + " não possui sprites suficientes para funcionar corretamente.");
            return;
        }

        if (animator != null)
        {
            animator.enabled = false;
        }

        do
        {
            currentSpriteIndex = Random.Range(0, sprites.Length);
        } while (currentSpriteIndex == correctSpriteIndex);

        image.sprite = sprites[currentSpriteIndex];
    }

    private void OnEnable()
    {
        buttonLeft?.onClick.AddListener(() => Rotate(-1));
        buttonRight?.onClick.AddListener(() => Rotate(1));
    }

    private void OnDisable()
    {
        buttonLeft?.onClick.RemoveListener(() => Rotate(-1));
        buttonRight?.onClick.RemoveListener(() => Rotate(1));
    }

    private void Rotate(int quantity)
    {
        OnRotate?.Invoke(this);

        if (IsOnCorrectSprite())
        {
            OnLeaveCorrectSprite?.Invoke();
            if (animator != null)
            {
                animator.enabled = false;
            }
        }

        currentSpriteIndex += quantity;

        if (currentSpriteIndex < 0)
        {
            currentSpriteIndex = sprites.Length - 1;
        }
        else if (currentSpriteIndex >= sprites.Length)
        {
            currentSpriteIndex = 0;
        }

        image.sprite = sprites[currentSpriteIndex];

        if (IsOnCorrectSprite())
        {
            OnCorrectSprite?.Invoke();
            if (animator != null)
            {
                animator.enabled = true;
            }
        }

    }

    public bool IsOnCorrectSprite()
    {
        return currentSpriteIndex == correctSpriteIndex;
    }
}
