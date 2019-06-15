using System;
using System.Collections.Generic;
using Sandbox.Definitions;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using Thraxus.Common.BaseClasses;
using Thraxus.Common.DataTypes;
using Thraxus.Common.Utilities.Tools.Networking;
using VRage.Game;
using VRage.ModAPI;

namespace Thraxus.PrefabSerializer
{
	public class Spawner : LogBaseEvent
	{
		private bool _spawnEnabled;

		public void StartSpawn(string unused)
		{
			_spawnEnabled = true;
			try
			{
				Spawn();
			}
			catch (Exception e)
			{ 
				WriteToLog("StartSpawn", $"Exception! {e}", LogType.Exception);
			}
		}

		public void StopSpawn(string unused)
		{
			_spawnEnabled = false;
		}

		private void Spawn()
		{
			MyAPIGateway.Entities.OnEntityAdd += ManageEntities;
			int counter = 0;
			MyAPIGateway.Parallel.Start(delegate
			{
				foreach (KeyValuePair<string, MyPrefabDefinition> prefab in Defintions.PrefabDefinitions)
				{
					if (!_spawnEnabled) break;
					SpawnPrefab(prefab.Value);
					WriteToLog("Spawn",$"Spawning [{counter++} of {Defintions.PrefabDefinitions.Count}]: {prefab.Key}", LogType.General);
					Messaging.ShowLocalNotification($"Spawning [{counter} of {Defintions.PrefabDefinitions.Count}]: {prefab.Key}");
					MyAPIGateway.Parallel.Sleep(1000);
				}
				MyAPIGateway.Entities.OnEntityAdd -= ManageEntities;
			});
		}

		private void ManageEntities(IMyEntity entity)
		{
			try
			{
				if (entity.GetType() != typeof(MyCubeGrid)) return;
				WriteToLog("ManageEntities",$"Despawning: {entity.DisplayName}", LogType.General);
				//Messaging.ShowLocalNotification($"Despawning: {entity.DisplayName}");
				entity.Close();
			}
			catch (Exception e)
			{
				WriteToLog("ManageEntities", $"Exception! {e}", LogType.Exception);
			}
		}

		private void SpawnPrefab(MyPrefabDefinition prefab)
		{
			try
			{
				MyAPIGateway.Entities.RemapObjectBuilderCollection(prefab.CubeGrids);
				foreach (MyObjectBuilder_CubeGrid grid in prefab.CubeGrids)
					MyAPIGateway.Entities.CreateFromObjectBuilderParallel(grid, true);
			}
			catch (Exception e)
			{
				WriteToLog("SpawnPrefab", $"Exception! {e}", LogType.Exception);
			}
		}
	}
}
