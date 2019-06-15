using Sandbox.ModAPI;
using Thraxus.Common.BaseClasses;
using Thraxus.Common.DataTypes;
using Thraxus.Common.Utilities.Tools.Networking;
using Thraxus.PrefabSerializer;
using VRage.Game;
using VRage.Game.Components;

namespace Thraxus
{
	[MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation, priority: int.MinValue + 1)]
	class SessionCore : BaseServerSessionComp
	{
		private const string GeneralLogName = "CoreGeneral";
		private const string DebugLogName = "CoreDebug";
		private const string SessionCompName = "Core";

		public SessionCore() : base(GeneralLogName, DebugLogName, SessionCompName) { } // Do nothing else

		/// <inheritdoc />
		protected override void EarlySetup()
		{
			base.EarlySetup();
		}

		public static readonly Spawner Spawner = new Spawner();

		/// <inheritdoc />
		protected override void SuperEarlySetup()
		{
			base.SuperEarlySetup();
			Messaging.Register();
			Spawner.OnWriteToLog += WriteToLog;
		}

		/// <inheritdoc />
		protected override void LateSetup()
		{
			base.LateSetup();
			WriteToLog("LateSetup", $"Cargo: {MyAPIGateway.Session.SessionSettings.CargoShipsEnabled}", LogType.General);
			WriteToLog("LateSetup", $"Encounters: {MyAPIGateway.Session.SessionSettings.EnableEncounters}", LogType.General);
			WriteToLog("LateSetup", $"Drones: {MyAPIGateway.Session.SessionSettings.EnableDrones}", LogType.General);
			WriteToLog("LateSetup", $"Scripts: {MyAPIGateway.Session.SessionSettings.EnableIngameScripts}", LogType.General);
			WriteToLog("LateSetup", $"Sync: {MyAPIGateway.Session.SessionSettings.SyncDistance}", LogType.General);
			WriteToLog("LateSetup", $"View: {MyAPIGateway.Session.SessionSettings.ViewDistance}", LogType.General);
			WriteToLog("LateSetup", $"PiratePCU: {MyAPIGateway.Session.SessionSettings.PiratePCU}", LogType.General);
			WriteToLog("LateSetup", $"TotalPCU: {MyAPIGateway.Session.SessionSettings.TotalPCU}", LogType.General);
			foreach (MyObjectBuilder_Checkpoint.ModItem mod in MyAPIGateway.Session.Mods)
				WriteToLog("LateSetup", $"Mod: {mod}", LogType.General);
			Defintions.InitDefnitions();
		}

		/// <inheritdoc />
		protected override void Unload()
		{
			Spawner.OnWriteToLog -= WriteToLog;
			Messaging.Unregister();
			base.Unload();
		}
	}
}
