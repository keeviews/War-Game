using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class PromptSystem : MonoBehaviour
{

    public TextMeshProUGUI Objectname;
    public TextMeshProUGUI description;
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI maxAmmo;

    public Guns weapon;
    public GunSO gunSO;

    private void Update()
    {
        currentAmmo.text = gunSO.currentAmmo.ToString();
        maxAmmo.text = weapon.MaxAmmo.ToString();
    }
    
    public void SetUpPrompt()
    {
        Objectname.text = weapon.name;
        description.text = weapon.description;
    }
}
