using UnityEngine;
using System;
using System.Collections;

namespace GameFoundations
{
	public enum MessagePriority
	{
		Low,
		Medium,
		High
	}

	public interface IMessageEvent
	{
		DateTime timeRaised { get; }
		float displayTime { get; }
		MessagePriority priority { get; }
		object message { get; }
	}
}
