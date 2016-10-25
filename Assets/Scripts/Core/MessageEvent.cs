using UnityEngine;
using System;
using System.Collections;

namespace GameFoundations
{
	public class MessageEvent : GameEvent, IMessageEvent
	{
		public DateTime timeRaised { private set; get; }
		public MessagePriority priority { private set; get; }
		public float displayTime { private set; get; }
		public object message { private set; get; }

		public MessageEvent (object message, float displayTime, MessagePriority priority)
		{
			this.message = message;
			this.displayTime = displayTime;
			this.priority = priority;
			timeRaised = DateTime.Now;
		}
	}
}