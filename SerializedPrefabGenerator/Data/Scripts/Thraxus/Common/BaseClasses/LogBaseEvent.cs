using Thraxus.Common.DataTypes;

namespace Thraxus.Common.BaseClasses
{
	public abstract class LogBaseEvent
	{
		public event TriggerLog OnWriteToLog;
		public delegate void TriggerLog(string caller, string message, LogType logType);

		public void WriteToLog(string caller, string message, LogType logType)
		{
			OnWriteToLog?.Invoke(caller, message, logType);
		}
	}
}
