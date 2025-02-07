using System.Collections;
using UnityEngine;

public class ShiningItem : MonoBehaviour {

	public float ShiningTime = 1f;
    public float PlayTime = 4f;

	SpriteRenderer spriteRenderer;
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		PlayShine();
    }
	private void PlayShine()
	{
        StartCoroutine(Shine());
    }
	IEnumerator Shine () {
		float currentTime = 0;
		float speed = 1f / ShiningTime;

		while (currentTime <= ShiningTime) {
			currentTime += Time.deltaTime;
			float value = Mathf.Lerp (0, 1, speed * currentTime);
			spriteRenderer.material.SetFloat ("_TimeController", value);
			yield return null;
		}
		spriteRenderer.material.SetFloat ("_Width", 0);
	
    }

}
