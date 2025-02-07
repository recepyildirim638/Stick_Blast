using DG.Tweening;
using UnityEngine;

public class FillSquare : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(7f, .2f).From(2.5f));
        sequence.Append(transform.DOScale(5f, .4f));
        spriteRenderer.DOColor(ThemaManager.Instance.complateFillsquereColor, .4f).From(Color.white);
        AudioManager.Instance.PlaySound(AUDIO_TYPE.FILL);

    }

    public void ClearGrid()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(7f, 0.2f));
        sequence.Join(spriteRenderer.DOColor(Color.white, 0.2f));
        sequence.AppendCallback(() => ParticleManager.ins.SetParticle(PoolItems.FILL_SQUARE_CLEAR, transform.position));
        sequence.Append(transform.DOScale(Vector3.zero, 0.2f));
        
        sequence.AppendCallback(() => 
        gameObject.SetActive(false));
    }
}