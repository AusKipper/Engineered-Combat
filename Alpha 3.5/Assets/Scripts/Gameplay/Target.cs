using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour 
{
	public int _hp;
	public int _score;
	public bool _friendly;

	public static MissionManager _game;

	public void Damage (int amount)
	{
		_hp -= amount;
		if (_hp <= 0)
		{
			DestroyImmediate(gameObject);
			if (!_friendly)
			{
				MissionManager._instance.OnTargetDestroyed(_score);
			}
		}
		if (_friendly)
		{
			MissionManager._instance.OnCollateralDamage(amount);//Not precise if damage is greater than hp amount.
		}
	}
}
