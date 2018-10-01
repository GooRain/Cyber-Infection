using Data.Settings;
using UnityEngine;

namespace Persistent.Settings
{
	public class MapSettings : SettingsBase<MapSettings>
	{
		public int MinAreaSize
		{
			get { return _data.minAreaSize; }
		}

		public int MaxAreaSize
		{
			get { return _data.maxAreaSize; }
		}

		public Rectangle mapSize
		{
			get { return _data.mapSize; }
		}

		private MapSettingsData _data;

		private void Awake()
		{
			_data = (MapSettingsData) MapSettingsData.instance;
		}
	}
}