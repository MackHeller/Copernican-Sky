using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A pretty basic script that handles the muting of audio, and the displaying of icons to the player
// Will definetly need to be expanded on to contain the ability to switch tracks, change volume, and to keep tracks going through scene changes

public class SoundController : MonoBehaviour {

	public Sprite OnSprite;
	public Sprite OffSprite;

	private SpriteRenderer spriteRenderer;
	private AudioSource audioSource;


	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = OnSprite;
		spriteRenderer.FadeSprite(this, 2);
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.M)) {
			audioSource.mute = !audioSource.mute;
			if (spriteRenderer.sprite == OnSprite)
				spriteRenderer.sprite = OffSprite;
			else
				spriteRenderer.sprite = OnSprite;

			spriteRenderer.FadeSprite(this, 2);
			
		}
	}
}

// An extension of the SpriteRenderer Class that allows for fading, see Start() for example of usage
public static class SpriteRendererExtension
{
	public static void FadeSprite 
	(	this SpriteRenderer renderer,
		MonoBehaviour mono,
		float duration
	)
	{
		mono.StartCoroutine(SpriteCoroutine(renderer, duration));

	}

	private static IEnumerator SpriteCoroutine
	(   SpriteRenderer renderer,
		float duration
	)
	{
		// Fading animation
		float start = Time.time;
		while (Time.time <= start + duration)
		{
			Color color = renderer.color;
			color.a = 1f - Mathf.Clamp01((Time.time - start) / duration);
			renderer.color = color;
			yield return new WaitForEndOfFrame();
		}
	}
}