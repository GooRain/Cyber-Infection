using System.Collections.Generic;
using Data.Settings;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Loading Data", order = -1)]
	public class LoadingData : SettingsDataBase<LoadingData>
	{
		public List<SettingsDataBase<LoadingData>> objects;
	}
}