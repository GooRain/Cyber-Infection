using System.Collections;
using System.Collections.Generic;
using Data.Settings;
using Data.Settings.Base;
using Persistent;
using UnityEngine;

namespace Data.Loading
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Loading Data", order = -1)]
	public class LoadingData : ScriptableObject, IEnumerable
	{
		[System.Serializable]
		private class LoadingDataList : List<IInitializable>
		{
			
		}
		
		[SerializeField]
		private LoadingDataList _dataToLoadList;
		
		public IEnumerator<IInitializable> GetEnumerator()
		{
			return _dataToLoadList.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _dataToLoadList.GetEnumerator();
		}
	}
}