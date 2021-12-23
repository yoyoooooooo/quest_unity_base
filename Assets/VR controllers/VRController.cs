using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRController : MonoBehaviour
{
    public enum Chirality
	{
        Left,
        Right
	}

    [SerializeField]
    protected Chirality chirality;

	bool previousTriggerValue;
	float previousGripValue;

	[SerializeField]
	Events._chirality triggerPressedEvent = new Events._chirality();

	[SerializeField]
	Events._chirality triggerReleasedEvent = new Events._chirality();

	[SerializeField]
	Events._chirality_float gripValueEvent = new Events._chirality_float();

	[SerializeField]
	Events._chirality gripPressedEvent = new Events._chirality();
	
	[SerializeField]
	Events._chirality gripReleasedEvent = new Events._chirality();

	public Chirality ControllerChirality
	{
		get { return chirality; }
	}

	public Events._chirality TriggerPressedEvent
	{
		get	{ return triggerPressedEvent; }
	}

	public Events._chirality TriggerReleasedEvent
	{
		get { return triggerReleasedEvent; }
	}

	public Events._chirality_float GripValueEvent
	{
		get { return gripValueEvent; }
	}

	public Events._chirality GripPressedEvent
	{
		get { return gripPressedEvent; }
	}

	public Events._chirality GripReleasedEvent
	{
		get { return gripReleasedEvent; }
	}

	// Update is called once per frame
	void Update()
    {
        InputDeviceCharacteristics chirality_characteristics;

        chirality_characteristics = chirality == Chirality.Left ? InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right;

        var controllers = new List<InputDevice>();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | chirality_characteristics | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, controllers);
	
        if(controllers.Count > 0)
        {
            InputDevice controller = controllers[0];

            Vector3 position;
            Quaternion orientation;
            controller.TryGetFeatureValue(CommonUsages.devicePosition, out position);
            controller.TryGetFeatureValue(CommonUsages.deviceRotation, out orientation);

            transform.localPosition = position;
            transform.localRotation = orientation;

			bool triggerValue;

			if(controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) )
			{
				if(!previousTriggerValue && triggerValue)
				{
					triggerPressedEvent.Invoke(chirality);
				}

				if(previousTriggerValue && !triggerValue)
				{
					triggerReleasedEvent.Invoke(chirality);
				}

				previousTriggerValue = triggerValue;
			}

			float gripValue;

			if(controller.TryGetFeatureValue(CommonUsages.grip, out gripValue))
			{
				gripValueEvent.Invoke(chirality, gripValue);


				float graspThreshold = 0.9f;

				if(previousGripValue < graspThreshold && gripValue >= graspThreshold)
				{
					gripPressedEvent.Invoke(chirality);
				}

				if(previousGripValue >= graspThreshold && gripValue < graspThreshold)
				{
					gripReleasedEvent.Invoke(chirality);
				}

				previousGripValue = gripValue;
			}
		}
    }
}
