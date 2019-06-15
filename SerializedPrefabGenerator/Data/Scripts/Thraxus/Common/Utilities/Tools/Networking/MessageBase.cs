using ProtoBuf;
using Sandbox.ModAPI;
using Thraxus.Common.Utilities.Tools.Networking.Messages;

namespace Thraxus.Common.Utilities.Tools.Networking
{
	[ProtoInclude(10, typeof(ExampleMessage))]
	[ProtoContract]
	public abstract class MessageBase
	{
		[ProtoMember(1)] protected readonly ulong SenderId;

		protected MessageBase()
		{
			SenderId = MyAPIGateway.Multiplayer.MyId;
		}

		public abstract void HandleServer();

		public abstract void HandleClient();
	}
}
