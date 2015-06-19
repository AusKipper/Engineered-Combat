using UnityEngine;

public class Cartridge : MonoBehaviour 
{
	public int _cost;
	public float _mass;
	public float _projectileMass;
	public float _accuracy;
	public float _aerodynamics;
	public float _force;
	public float _penetration;
	public float _damage;//Per m/s.
	public GameObject _prefab;
	public GameObject _impactPrefabTest;//TEST
	public LayerMask _tracingMask;//TODO: Make single global variable.
}