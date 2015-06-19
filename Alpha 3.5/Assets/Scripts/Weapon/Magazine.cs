using UnityEngine;
using System.Collections.Generic;

public class Magazine : MonoBehaviour 
{
	//private float _mass;
	//private float _price;
	//private float _reliability;
	//private Quality _quality;
	//private Type _type;

	public int _projectileCountMax;
	public int _projectileCount;
	public Cartridge _cartridge; //For single type of ammo.

	//private List<Projectile> _projectiles; //For loading different types of ammo.

	public void Reload ()
	{
		_projectileCount = _projectileCountMax;
	}

	public void Fire ()
	{
		_projectileCount--;
	}

	public bool CheckEmpty ()
	{
		return _projectileCount == 0;
	}

	public GameObject GetCurrentProjectilePrefab ()
	{
		return _cartridge._prefab;
	}

	public enum Type
	{
		Box,
		Belt,
		Drum
	}
}
