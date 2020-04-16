namespace ProtoBuf.BaseInclude.Samples
{
	[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
	[ProtoBase(10)]
	public class EmailEnvironment : INotificationEnvironment
	{
		public string WebsiteUrl { get; set; }
		public string ResourceWebsiteUrl { get; set; }

		public EmailEnvironment(bool isAdmin)
		{
			// Setup
		}
	}

	[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
	public class InvalidNotification : INotification
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public INotificationEnvironment Environment { get; set; } 
	}

	[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
	[ProtoBase(10)]
	public abstract class AdminNotificationBase : INotification
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public INotificationEnvironment Environment { get; set; } = new EmailEnvironment(true);
	}

	[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
	[ProtoBase(20, InterfaceType = typeof(INotification))]
	public abstract class NotificationBase : INotification
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public INotificationEnvironment Environment { get; set; } = new EmailEnvironment(false);
	}

	[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
	[ProtoBase(11)]
	public class UserAccount : AdminNotificationBase
	{
	}

	[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
	[ProtoBase(21)]
	public class UserNotification : NotificationBase
	{
	}

	[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
	[ProtoBase(22, typeof(GenericUserNotification<string>))]
	[ProtoBase(23, typeof(GenericUserNotification<int>))]
	public class GenericUserNotification<TValue> : NotificationBase
	{
		public TValue Value { get; set; }
	}

	[ProtoContract]
	[ProtoBase(10)]
	public class UserMessage : MessageBase
	{
		[ProtoMember(1)]
		public string UserName { get; set; }

		[ProtoMember(2)]
		public string Password { get; set; }
	}
}
