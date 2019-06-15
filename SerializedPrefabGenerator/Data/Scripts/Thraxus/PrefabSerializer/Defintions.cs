using System.Collections.Generic;
using Sandbox.Definitions;

namespace Thraxus.PrefabSerializer
{
	public static class Defintions
	{
		public static Dictionary<string, MyPrefabDefinition> PrefabDefinitions;
		private static bool _initialized;
		
		static Defintions()
		{
			PrefabDefinitions = new Dictionary<string, MyPrefabDefinition>();
			_initialized = false;
		}

		public static void InitDefnitions()
		{
			if (_initialized) return;
			_initialized = true;

			foreach (KeyValuePair<string, MyPrefabDefinition> prefabDef in MyDefinitionManager.Static.GetPrefabDefinitions())
			{
				if (!prefabDef.Value.Public || prefabDef.Value.Context.IsBaseGame) continue;
				PrefabDefinitions.Add(prefabDef.Key, prefabDef.Value);
			}
		}
	}
}
