using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image medalImage;
    [SerializeField] private Image circleImage;
    [SerializeField] private float animationDuration = 5f;

    private void Start()
    {
        // Background começa transparente, mantendo a cor original
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        // Medalha começa na parte de baixo da tela
        medalImage.transform.localPosition = new Vector3(0, -Screen.height*2, 0);
        // Círculo começa com escala 0
        circleImage.transform.localScale = Vector3.zero;

        // Anima o background
        background.DOFade(1f, animationDuration/2);

        // Anima o círculo
        circleImage.transform.DOScale(Vector3.one, animationDuration/2)
            .SetEase(Ease.OutBack);

        // Anima a medalha com bounce
        medalImage.transform.DOLocalMove(Vector3.zero, animationDuration)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() =>
            {
                // Aguarda alguns segundos e vai para próxima cena
                DOVirtual.DelayedCall(1f, () =>
                {
                    LevelManager.Instance.NextLevel();
                });
            });
    }
}