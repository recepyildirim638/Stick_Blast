using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShiningItem : MonoBehaviour {

	public float ShiningTime = 1f;
    public float PlayTime = 4f;

	SpriteRenderer spriteRenderer;
	bool isLock;
    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayShine();
    }
  
	private void PlayShine()
	{
		if (!isLock)
		{
			isLock = true;
			StartCoroutine(Shine());

		}
    }
	IEnumerator Shine () {
		float currentTime = 0;
        spriteRenderer.material.SetFloat("_Width", 0.45f);
        float speed = 1f / ShiningTime;
        while (currentTime <= ShiningTime) {
			currentTime += Time.deltaTime;
			float value = Mathf.Lerp (0, 1, speed * currentTime);
			spriteRenderer.material.SetFloat ("_TimeController", value);
			yield return null;
		}
		spriteRenderer.material.SetFloat ("_Width", 0);
		isLock = false;
    }

}
