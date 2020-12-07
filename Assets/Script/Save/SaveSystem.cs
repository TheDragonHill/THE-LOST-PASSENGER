using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	public static void SaveMenu(bool or)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/menu.tlp";
		FileStream stream = new FileStream(path, FileMode.Create);

		MenuData data = new MenuData(or);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static MenuData LoadMenu()
	{
		string path = Application.persistentDataPath + "/menu.tlp";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			MenuData data = formatter.Deserialize(stream) as MenuData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}

	public static void Save(Movement move, Timer timer, Group group, StatsManager stats, DecisionManager decision)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/saveData.tlp";
		FileStream stream = new FileStream(path, FileMode.Create);

		SaveData data = new SaveData(move, timer, group, stats, decision);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static SaveData Load()
	{
		string path = Application.persistentDataPath + "/saveData.tlp";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			SaveData data = formatter.Deserialize(stream) as SaveData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}
}
