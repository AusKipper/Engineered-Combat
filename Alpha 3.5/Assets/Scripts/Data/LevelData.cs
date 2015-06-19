using UnityEngine;

public class LevelData : MonoBehaviour
{
	[SerializeField] private string _briefingText;
	public static LevelData _current;

	private void OnEnable ()
	{
		_current = this;
	}

	public string GetBriefingText ()
	{
		return _briefingText;
	}
}