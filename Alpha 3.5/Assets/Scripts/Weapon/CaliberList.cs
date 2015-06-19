using UnityEngine;

public class CaliberList : MonoBehaviour
{
	[SerializeField] private Caliber[] _calibers;

	public Caliber[] GetCalibers ()
	{
		return _calibers;
	}
}

[System.Serializable]
public class Caliber
{
	[SerializeField] private string _name;
	[SerializeField] private float _cost;
	[SerializeField] private float _power;
	[SerializeField] private float _force;
	[SerializeField] private float _mass;
	
	public string GetName ()
	{
		return _name + " (" + _cost + "$)";
	}

	public float GetCost ()
	{
		return _cost;
	}

	public float GetPower ()
	{
		return _power;
	}

	public float GetForce ()
	{
		return _force;
	}

	public float GetMass ()
	{
		return _mass;
	}

	public override string ToString ()
	{
		return _name;
	}
}