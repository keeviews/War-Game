using UnityEngine;

public class GunSO : MonoBehaviour
{
    public Guns Guns;

    Inventory inventory;

    public int currentAmmo;
    public int maxAmmo;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Guns.art;
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        maxAmmo = Guns.MaxAmmo;
    }

    public void PickUp()
    {
        if(Guns.Secondary == false) {
            if (inventory.currentWeapon.Secondary == false)
            {
                inventory.currentWeapon = Guns;
            }
            if(inventory.currentWeapon.Secondary == true)
            {
                inventory.holsteredWeapon = Guns;
            }
        }
        if (Guns.Secondary == true)
        {
            if (inventory.currentWeapon.Secondary == true)
            {
                inventory.currentWeapon = Guns;
            }
            if (inventory.currentWeapon.Secondary == false)
            {
                inventory.SecondaryWeapon = Guns;
                inventory.holsteredWeapon = Guns;
            }
        }
        Destroy(gameObject);
    }
}
