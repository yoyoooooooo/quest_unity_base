using System;
using UnityEngine;
using UnityEngine.Events;


public class Events : MonoBehaviour
{
	[Serializable]
	public class _chirality : UnityEvent<VRController.Chirality> { }

	[Serializable]
	public class _chirality_float : UnityEvent<VRController.Chirality, float> { }
}
