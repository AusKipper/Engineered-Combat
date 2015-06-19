using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ProfileManager : MonoBehaviour 
{
	private ProfileData _saveData;
	private string _fileName = "Profile";
	private string _extentionName = "bin";

	public void CreateProfile (int index)
	{
		_saveData = new ProfileData(index);
		SaveProfile();
	}

	public void SaveProfile ()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/" + _fileName + _saveData._profileIndex + "." + _extentionName, FileMode.OpenOrCreate);
		bf.Serialize(file, _saveData);
		file.Close();
	}

	public void LoadProfile (int index)
	{
		_saveData = LoadSaveData(index);
	}

	public void DeleteProfile (int index)
	{
		File.Delete(Application.persistentDataPath + "/" + _fileName + index + "." + _extentionName);
	}
	
	private ProfileData LoadSaveData (int index)
	{
		ProfileData saveData = null;
		if (File.Exists(Application.persistentDataPath + "/" + _fileName + index + "." + _extentionName))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + _fileName + index + "." + _extentionName, FileMode.OpenOrCreate);
			saveData = (ProfileData)bf.Deserialize(file);
			file.Close();
		}
		return saveData;
	}

	public int GetHighScore (int index)
	{
		return _saveData._highScores[index];
	}

	public bool ProfileExists (int profileIndex)
	{
		ProfileData saveData = LoadSaveData(profileIndex);
		
		if (saveData != null)
		{
			return true;
		}
		else 
		{
			return false;
		}
	}

	public int GetProfileNetWorth (int profileIndex)
	{
		ProfileData saveData = LoadSaveData(profileIndex);
		if (saveData != null)
		{
			return saveData._netWorth;
		}
		else
		{
			return 0;
		}
	}

	public int GetProfileNetWorth ()
	{
		return _saveData._netWorth;
	}

	public WeaponData[] GetWeaponData ()
	{
		return _saveData._weaponData;
	}

	public int GetProfileIndex ()
	{
		return _saveData._profileIndex;
	}

	public void SaveWeaponData (WeaponData[] weaponData)
	{
		_saveData._weaponData = weaponData;
		SaveProfile();
	}

	public void SubmitHighScore (int highScore)
	{
		int currentHighScore = GetHighScore(MissionManager.GetCurrentMissionIndex());
		if (highScore > currentHighScore)
		{
			_saveData._highScores[MissionManager.GetCurrentMissionIndex()] = highScore;
			_saveData._netWorth += highScore - currentHighScore;
			SaveProfile();
		}
	}
}

[System.Serializable]
public class ProfileData
{
	//public string _name;
	public int _profileIndex;
	public int _netWorth;
	public int[] _highScores;
	public WeaponData[] _weaponData;

	public ProfileData (int index)
	{
		_profileIndex = index;
		_highScores = new int[Application.levelCount - 2];
	}
}