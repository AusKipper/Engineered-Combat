using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	[SerializeField] private Transform _transform;
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private GameObject _impactPrefab;
	[SerializeField] private LayerMask _collisionMask;
	[SerializeField] private Caliber _caliber;

	public void Fire ()
	{
		_rigidbody.velocity = _transform.forward * (_caliber.GetForce() / _caliber.GetMass());//TODO: Adjust formula to get realistic force values?
		//_rigidbody.AddForce(_transform.forward * _caliber.GetForce());//Does not work in first frame, but gives realistic results.
		Destroy(gameObject, 2f);//Self-destruct.
		MissionManager._instance.OnBulletFired(_caliber.GetCost());
	}

	public void SetCaliber (Caliber caliber)
	{
		_rigidbody.mass = _caliber.GetMass();
		_caliber = caliber;
	}

	public void FixedUpdate ()
	{
		RaycastHit hit;
		if (Physics.Linecast(_rigidbody.position, _rigidbody.position + (_rigidbody.velocity * Time.fixedDeltaTime), out hit, _collisionMask))
		{
			if (DebugManager._instance._spawnProjectileImpactMarkers)
			{
				GameObject decal = Instantiate(_impactPrefab, hit.point, Quaternion.identity) as GameObject;
				decal.transform.parent = hit.transform;
				Destroy(decal, 2f);
				//Destroy(Instantiate(_impactPrefab, hit.point, Quaternion.identity) as GameObject, 2f);
			}
			Target impactObject = hit.collider.GetComponent<Target>();
			if (impactObject != null)
			{
				float velocity = _rigidbody.velocity.magnitude;
				print ("Damage: " + (_caliber.GetPower() * velocity));
				impactObject.Damage((int)(_caliber.GetPower() * velocity));
			}
			_rigidbody.MovePosition(hit.point);
			_rigidbody.isKinematic = true;
			_transform.position = hit.point;
			Destroy(gameObject, 1f);//TODO: Implement pooling instead. Delayed to see trail.
		}
	}
}
