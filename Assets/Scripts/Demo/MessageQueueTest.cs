using UnityEngine;
using System.Collections;
using GameFoundations;

public class MessageQueueTest : MonoBehaviour
{
	private int count = 1;

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			MessagePriority priorty = MessagePriority.High;

			float random = Random.value;
			if (random < 0.333f) {
				priorty = MessagePriority.Low;
			} else if (random < 0.666f) {
				priorty = MessagePriority.Medium;
			}

			Events.instance.Raise (new MessageEvent ("Test Message " + count++, Time.time + 3f, priorty));
		}
	}
}
