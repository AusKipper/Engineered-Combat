using UnityEngine;

public class DebugManager : MonoBehaviour 
{
	public bool _spawnProjectileImpactMarkers;
	public KeyCode _ImpactMarkersToggleKey;

	public static DebugManager _instance;

	public void Awake ()
	{	
		_instance = this;
	}

	public void Update ()
	{
		if (Input.GetKeyDown(_ImpactMarkersToggleKey))
		{
			_spawnProjectileImpactMarkers = !_spawnProjectileImpactMarkers;
		}
	}
}
