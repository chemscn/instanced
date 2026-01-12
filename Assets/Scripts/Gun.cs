using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Objects/Gun")]
public class Gun : ScriptableObject
{
	public string name;
	public float fireRate;
	public float maxSpread;
	public int currentAmmoCount;
	public int maxAmmoCount;
	public int reserveAmmoCount;
	public int maxReserveAmmoCount;
	public SpriteRenderer model;

	public void UpdateCurrentAmmoCount(int count) => currentAmmoCount = count;
	public void Reload()
	{
		int ammoToLoad = reserveAmmoCount - (maxAmmoCount - currentAmmoCount);
		if (ammoToLoad > 0)
		{
			reserveAmmoCount = reserveAmmoCount - ammoToLoad;
			currentAmmoCount = maxAmmoCount;
		}
	}
	public void AddAmmo(int count)
	{
		reserveAmmoCount = reserveAmmoCount + count;
		if (reserveAmmoCount > maxReserveAmmoCount)
			reserveAmmoCount = maxReserveAmmoCount;
	}
}
