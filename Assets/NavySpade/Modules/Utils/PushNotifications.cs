using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class PushNotifications : MonoBehaviour
{
	public static PushNotifications Instance;

	protected static AndroidNotificationChannel notifyDefaultChannel;
	protected static AndroidNotificationChannel notifyImportantChannel;

	private void Awake() {
		Instance = this;
	}

	void Start()
	{
		notifyDefaultChannel = new AndroidNotificationChannel() {
			Id = "game_default_channel_id",
			Name = "Default Channel",
			Importance = Importance.Default,
			Description = "Generic notifications",
		};
		AndroidNotificationCenter.RegisterNotificationChannel(notifyDefaultChannel);

		notifyImportantChannel = new AndroidNotificationChannel() {
			Id = "game_important_channel_id",
			Name = "Important Channel",
			Importance = Importance.High,
			Description = "Important messages",
		};

		AndroidNotificationCenter.RegisterNotificationChannel(notifyImportantChannel);
	}

	protected static void SendNotify(AndroidNotificationChannel _channel, string _title, string _text, System.DateTime _time) {
		string curChannelId = _channel.Id;
		string notifyTitle = _title;
		string notifyText = _text;

		System.DateTime dateTime = _time; // example: System.DateTime.Now.AddMinutes(5);

		AndroidNotification newNotify = new AndroidNotification();
		newNotify.Title = notifyTitle;
		newNotify.Text = notifyText;
		newNotify.FireTime = dateTime;

		AndroidNotificationCenter.SendNotification(newNotify, curChannelId);
	}

	public static void SendDefaultNotify(string _title, string _text, System.DateTime _time) {
		SendNotify(notifyDefaultChannel, _title, _text, _time);
	}

	public static void SendImportantNotify(string _title, string _text, System.DateTime _time) {
		SendNotify(notifyImportantChannel, _title, _text, _time);
	}
}
