using UnityEngine;
using Random = UnityEngine.Random;
using System;

public abstract class AudioEvent : ScriptableObject
{
	public abstract void Play(AudioSource source);
}
