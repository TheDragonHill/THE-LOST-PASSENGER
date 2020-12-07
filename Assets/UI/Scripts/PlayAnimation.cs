using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
	public AudioSource highlight;

	private void Start()
	{
		this.GetComponent<PlayAnimation>().enabled = false;	
	}

	public void PlayAudio()
	{
		highlight.Play();
	}
}
