using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace GameFoundations
{
	public class MessageQueue : MonoBehaviour
	{
		public bool logToConsole = false;
		public bool prependDateTime = false;

		[Header ("Message Colours")]
		public Color
			highPriorityColour = Color.red;
		public Color normalPriorityColour = Color.black;
		public Color lowPriorityColour = Color.white;

		[Header ("Message Font Style")]
		public FontStyle
			highPriorityStyle = FontStyle.Bold;
		public FontStyle normalPriorityStyle = FontStyle.Normal;
		public FontStyle lowPriorityStyle = FontStyle.Normal;
		
		[Header ("Message Location")]
		public Vector2
			queueLocation = new Vector2 (25, 25); 
		public Vector2 messageSize = new Vector2 (200, 15); 
	
		private static readonly GUIStyle LOW_PRIORTY = new GUIStyle (), NORMAL_PRIORITY = new GUIStyle (), HIGH_PRIORITY = new GUIStyle ();

		private List<IMessageEvent> _pending = new List<IMessageEvent> ();
	
		void OnEnable ()
		{
			if (_pending.Count > 0) {
				_pending.Clear ();
			}

			LOW_PRIORTY.normal.textColor = lowPriorityColour;
			LOW_PRIORTY.fontStyle = lowPriorityStyle;

			NORMAL_PRIORITY.normal.textColor = normalPriorityColour;
			NORMAL_PRIORITY.fontStyle = normalPriorityStyle;

			HIGH_PRIORITY.normal.textColor = highPriorityColour;
			HIGH_PRIORITY.fontStyle = highPriorityStyle;

			Events.instance.AddListener<MessageEvent> (OnMessage);
		}
	
		void OnDisable ()
		{
			Events.instance.RemoveListener<MessageEvent> (OnMessage);
		}
	
		void Update ()
		{
			for (int i = _pending.Count - 1; i>=0; i--) {
				if (Time.time > _pending [i].displayTime)
					_pending.RemoveAt (i);
			}
		}
	
		void OnMessage (IMessageEvent e)
		{
			_pending.Add (e);

			if (logToConsole) {
				Debug.Log ("Message Recieved [" + System.DateTime.Now + "]: " + e.message.ToString ());
			}
		}

		void OnGUI ()
		{
			float yPos = queueLocation.y;

			foreach (var m in _pending) {

				GUIStyle style = GetMessageStyle (m);

				string message = (prependDateTime) ? "[" + m.timeRaised + "]: " + m.message.ToString () : m.message.ToString ();

				GUI.Label (new Rect (queueLocation.x, yPos, messageSize.x, messageSize.y), message, style);

				yPos += messageSize.y;
			}
		}

		GUIStyle GetMessageStyle (IMessageEvent e)
		{
			switch (e.priority) {
			case MessagePriority.Low:
				return LOW_PRIORTY;
			case MessagePriority.Medium:
				return NORMAL_PRIORITY;
			default:
				return HIGH_PRIORITY;
			}

		}
	}
}
