using UnityEditor;
using UnityEngine;

namespace TT.Core.Editor
{
	public class TTPlayerPrefsEditor
	{
		[MenuItem("TTCore/PlayerPrefs/DeleteAllPlayerPrefs")]
		static void DeleteAllPlayerPrefs()
		{
			PlayerPrefs.DeleteAll();
			TTDebug.Log("Delete All Data Of PlayerPrefs!!");
		}
	}
}