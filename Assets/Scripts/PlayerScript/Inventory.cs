using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEditor;

public class Inventory : MonoBehaviour
{

    public Guns currentWeapon;
    public Guns holsteredWeapon;
    public Guns SecondaryWeapon;

    [SerializeField]
    GameObject obj;

    int secondaryAmmo;
    int primaryAmmo;

    public int smallAmmo;
    public int mediumAmmo;
    public int largeAmmo;

    private bool isInPickUpZone;

    GunRotation gunRotation;

    [SerializeField] TextMeshProUGUI smallAmmoText;
    [SerializeField] TextMeshProUGUI mediumAmmoText;
    [SerializeField] TextMeshProUGUI largeAmmoText;

    [SerializeField] GameObject PromptSystemObject;

    private void Start()
    {
        gunRotation = GetComponentInChildren<GunRotation>();
        PromptSystemObject.SetActive(false);
        secondaryAmmo = SecondaryWeapon.currentAmmo;
        gunRotation.currentAmmo = currentWeapon.ammoType switch
        {
            Guns.AmmoType.smallAmmo => smallAmmo,
            Guns.AmmoType.mediumAmmo => mediumAmmo,
            Guns.AmmoType.largeAmmo => largeAmmo,
            _ => gunRotation.currentAmmo
        };
    }

    private void Update()
    {
        smallAmmoText.text = smallAmmo.ToString();
        mediumAmmoText.text = mediumAmmo.ToString();
        largeAmmoText.text = largeAmmo.ToString();

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.zero, 0f, LayerMask.GetMask("PickUp"), Mathf.Infinity);

        if(hit .collider != null)
        {
            isInPickUpZone = true;
            obj = hit.collider.gameObject;

            if (obj.CompareTag("Gun"))
            {
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<GunSO>().Guns.outlineImage;
                PromptSystemObject.SetActive(true);
                PromptSystemObject.GetComponent<PromptSystem>().weapon = obj.GetComponent<GunSO>().Guns;
                PromptSystemObject.GetComponent<PromptSystem>().gunSO = obj.GetComponent<GunSO>();
                PromptSystemObject.GetComponent<PromptSystem>().SetUpPrompt();
            }
            else if (obj.CompareTag("SmallAmmo"))
            {
                PromptSystemObject.SetActive(true);
                obj = hit.collider.gameObject;
                PromptSystemObject.GetComponent<PromptSystem>().Objectname.text = "Small Ammo";
            }
            else if (obj.CompareTag("MediumAmmo"))
            {
                PromptSystemObject.SetActive(true);
                obj = hit.collider.gameObject;
                PromptSystemObject.GetComponent<PromptSystem>().Objectname.text = "Medium Ammo";
            }
            else if (obj.CompareTag("LargeAmmo"))
            {
                PromptSystemObject.SetActive(true);
                obj = hit.collider.gameObject;
                PromptSystemObject.GetComponent<PromptSystem>().Objectname.text = "Large Ammo";
            }
        }
        else
        {
            if(obj != null && obj.CompareTag("Gun"))
            {
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<GunSO>().Guns.art;
            }
            isInPickUpZone = false;
            obj = null;
            PromptSystemObject.SetActive(false);
        }
        
        if (isInPickUpZone && obj != null && obj.gameObject.CompareTag("Gun") && Input.GetKeyDown(KeyCode.F)  && obj.GetComponent<GunSO>().Guns.Secondary == true)
        {
            Debug.Log("Pressed");
            if (currentWeapon == null)
            {
                obj.GetComponent<GunSO>().PickUp();
                if(currentWeapon == SecondaryWeapon)
                {
                    int CA = obj.GetComponent<GunSO>().currentAmmo;
                    secondaryAmmo = obj.GetComponent<GunSO>().currentAmmo;
                    gunRotation.ResetWeapon(CA);
                }

            }
            if (currentWeapon != null)
            {
                Drop(true);
                int currentAmmo = obj.GetComponent<GunSO>().currentAmmo;
                obj.GetComponent<GunSO>().PickUp();
                if (currentWeapon == SecondaryWeapon)
                {
                    secondaryAmmo = currentAmmo;
                    gunRotation.ResetWeapon(currentAmmo);
                }
            }
        }

        if (isInPickUpZone && obj != null && obj.gameObject.CompareTag("Gun") && Input.GetKeyDown(KeyCode.F) && obj.GetComponent<GunSO>().Guns.Secondary == false)
        {
            Debug.Log("Pressed");
            if (currentWeapon == null)
            {
                int CA = obj.GetComponent<GunSO>().currentAmmo;
                obj.GetComponent<GunSO>().PickUp();
                gunRotation.ResetWeapon(CA);
            }
            if (currentWeapon != null)
            {
                Drop(false);
                int CA = obj.GetComponent<GunSO>().currentAmmo;
                obj.GetComponent<GunSO>().PickUp();
                gunRotation.ResetWeapon(CA);
            }
        }

        if(isInPickUpZone && Input.GetKeyDown(KeyCode.F) && obj.CompareTag("SmallAmmo"))
        {
            smallAmmo += 10;
            Destroy(obj);
            isInPickUpZone = false;
            obj = null;
            PromptSystemObject.SetActive(false);
        }
        if(isInPickUpZone && Input.GetKeyDown(KeyCode.F) && obj.CompareTag("MediumAmmo"))
        {
            mediumAmmo += 10;
            Destroy(obj);
            isInPickUpZone = false;
            obj = null;
            PromptSystemObject.SetActive(false);
        }
        if(isInPickUpZone && Input.GetKeyDown(KeyCode.F) && obj.CompareTag("LargeAmmo"))
        {
            largeAmmo += 10;
            Destroy(obj);
            isInPickUpZone = false;
            obj = null;
            PromptSystemObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.G) && currentWeapon != SecondaryWeapon)
        {
            primaryAmmo = gunRotation.ammo;
            holsteredWeapon = currentWeapon;
            currentWeapon = SecondaryWeapon;
            gunRotation.ResetWeapon(secondaryAmmo);
        }
        else if (Input.GetKeyDown(KeyCode.G) && currentWeapon == SecondaryWeapon)
        {
            secondaryAmmo = gunRotation.ammo;
            currentWeapon = holsteredWeapon;
            holsteredWeapon = SecondaryWeapon;
            gunRotation.ResetWeapon(primaryAmmo);
        }
    }

    [SerializeField] CircleCollider2D CircleCollider2D;


    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CircleCollider2D.IsTouching(collision) && collision.gameObject.CompareTag("Gun"))
        {
            isInPickUpZone = true;
            obj = collision.gameObject;
            obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<GunSO>().Guns.outlineImage;
            PromptSystemObject.SetActive(true);
            PromptSystemObject.GetComponent<PromptSystem>().weapon = obj.GetComponent<GunSO>().Guns;
            PromptSystemObject.GetComponent<PromptSystem>().gunSO = obj.GetComponent<GunSO>();
            PromptSystemObject.GetComponent<PromptSystem>().SetUpPrompt();
        }
        if(CircleCollider2D.IsTouching(collision) && collision.gameObject.CompareTag("SmallAmmo"))
        {
            isInPickUpZone = true;
            PromptSystemObject.SetActive(true);
            obj = collision.gameObject;
            PromptSystemObject.GetComponent<PromptSystem>().Objectname.text = "Small Ammo";
        }
        if (CircleCollider2D.IsTouching(collision) && collision.gameObject.CompareTag("MediumAmmo"))
        {
            isInPickUpZone = true;
            PromptSystemObject.SetActive(true);
            obj = collision.gameObject;
            PromptSystemObject.GetComponent<PromptSystem>().Objectname.text = "Medium Ammo";
        }
        if (CircleCollider2D.IsTouching(collision) && collision.gameObject.CompareTag("LargeAmmo"))
        {
            isInPickUpZone = true;
            PromptSystemObject.SetActive(true);
            obj = collision.gameObject;
            PromptSystemObject.GetComponent<PromptSystem>().Objectname.text = "Large Ammo";
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!CircleCollider2D.IsTouching(collision) && (collision.gameObject.CompareTag("Gun") || collision.gameObject.CompareTag("SmallAmmo") || collision.gameObject.CompareTag("MediumAmmo") || collision.gameObject.CompareTag("LargeAmmo")))
        {
            if(collision.gameObject.CompareTag("Gun"))
            {
                collision.gameObject.GetComponent<SpriteRenderer>().sprite = collision.gameObject.GetComponent<GunSO>().Guns.art;
            }
            isInPickUpZone = false;
            obj = null;
            PromptSystemObject.SetActive(false);
        }
    }
    */

    void Drop(bool secondary)
    {
        if (secondary)
        {
            if (currentWeapon.Secondary == true)
            {
                GameObject clone = Instantiate(currentWeapon.prefab);
                GunSO gunSO = clone.GetComponent<GunSO>();
                gunSO.currentAmmo = gunRotation.ammo;
            }
            if (currentWeapon.Secondary == false)
            {
                GameObject clone = Instantiate(SecondaryWeapon.prefab);
                GunSO gunSO = clone.GetComponent<GunSO>();
                gunSO.currentAmmo = secondaryAmmo;
            }

        }
        else
        {
            if (currentWeapon.Secondary == true)
            {
                GameObject clone = Instantiate(holsteredWeapon.prefab);
                GunSO gunSO = clone.GetComponent<GunSO>();
                gunSO.currentAmmo = primaryAmmo;
            }
            if (currentWeapon.Secondary == false)
            {
                GameObject clone = Instantiate(currentWeapon.prefab);
                GunSO gunSO = clone.GetComponent<GunSO>();
                gunSO.currentAmmo = primaryAmmo;
            }
        }

    }
}
