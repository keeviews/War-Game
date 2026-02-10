using UnityEngine;

[CreateAssetMenu(fileName = "Guns", menuName = "Scriptable Objects/Guns")]
public class Guns : ScriptableObject
{
    public new string Name;
    public string description;

    public enum AmmoType{
        smallAmmo,
        mediumAmmo,
        largeAmmo
    }

    public AmmoType ammoType;

    public Sprite art;
    public Sprite ingameArt;
    public Sprite outlineImage;
    public GameObject prefab;
    public Vector2 shootOffset;

    public float GunFocusSpeed;
    public float MaxAccuracyVariance;
    public float GunUnFocusSpeed;
    public float GunRecoil;

    public float Damage;

    public bool Secondary;

    public bool Automatic;
    public float fireRate;
    public float reloadSpeed;
    public int currentAmmo;
    public int MaxAmmo;

    public float SoundAreaMultiplier;
}
