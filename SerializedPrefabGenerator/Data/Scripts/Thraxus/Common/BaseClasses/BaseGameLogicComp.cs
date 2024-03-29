﻿using Thraxus.Common.DataTypes;
using Thraxus.Common.Utilities.Tools.Logging;
using VRage.Game.Components;

namespace Thraxus.Common.BaseClasses
{
	internal abstract class BaseGameLogicComp : MyGameLogicComponent
	{
		protected string EntityName = "PlaceholderName";
		protected long EntityId = 0L;

		public void WriteToLog(string caller, string message, LogType logType)
		{
			switch (logType)
			{
				case LogType.General:
					GeneralLog(caller, message);
					return;
				case LogType.Debug:
					DebugLog(caller, message);
					return;
				case LogType.Exception:
					ExceptionLog(caller, message);
					return;
				default:
					return;
			}
		}

		private void GeneralLog(string caller, string message)
		{
			StaticLog.WriteToLog($"{EntityName} ({EntityId}): {caller}", message, LogType.General);
		}

		private void DebugLog(string caller, string message)
		{
			StaticLog.WriteToLog($"{EntityName} ({EntityId}): {caller}", message, LogType.Debug);
		}

		private void ExceptionLog(string caller, string message)
		{
			StaticLog.WriteToLog($"{EntityName} ({EntityId}): {caller}", $"Exception! {message}", LogType.Exception);
		}
	}
}
