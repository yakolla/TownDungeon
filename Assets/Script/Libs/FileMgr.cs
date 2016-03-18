using UnityEngine;
using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class FileMgr {

	public static void Deserialize<T>(ref T records, string fileNameWithoutExtention)
	{ 
		TextAsset textDocument =  Resources.Load(fileNameWithoutExtention) as TextAsset;

		records = JsonConvert.DeserializeObject<T>(textDocument.text);			


	}
}
