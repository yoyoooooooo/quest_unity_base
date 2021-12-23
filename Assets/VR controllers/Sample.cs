using UnityEngine;

public class Sample : MonoBehaviour
{
    public void OnTriggerPressed(VRController.Chirality chirality)
	{
		Debug.Log(chirality.ToString() + " trigger pressed");
	}

	public void OnTriggerReleased(VRController.Chirality chirality)
	{
		Debug.Log(chirality.ToString() + " trigger released");
	}

	public void OnGripPressed(VRController.Chirality chirality)
	{
		Debug.Log(chirality.ToString() + " grip pressed");
	}

	public void OnGripReleased(VRController.Chirality chirality)
	{
		Debug.Log(chirality.ToString() + " grip released");
	}
}
