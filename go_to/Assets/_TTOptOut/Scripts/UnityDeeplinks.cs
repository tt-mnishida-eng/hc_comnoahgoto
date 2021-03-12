using UnityEngine;
using System;
using TT;
using TT.OptOut;

public class UnityDeeplinks : SingletonMonoBehaviour<UnityDeeplinks> {
	public void onDeeplink(string deeplink)
	{
		ParseUrl(deeplink);
	}
	void ParseUrl(string url)
	{
		Debug.Log("### UnityDeeplinks " + url);
		var uri = new Uri(url);
		
		string query = uri.Query;
		if (query.Length == 0)
		{
			return;
		}
		query = query.Substring(1);
		foreach (var param in query.Split('&'))
		{
			var tmp = param.Split('=');
			if (tmp[0] == "optout")
			{
				if (tmp[1] == "1")
				{
					Debug.Log("### UnityDeeplinks Opt Out Processing : true");
					TTOptOut.SetNeedOptOut(true);
					break;
				}
				else
				{
					Debug.Log("### UnityDeeplinks Opt Out Processing : false");
				}
			}
		}
	}
	
}
