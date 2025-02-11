using UnityEngine;

public class WeaponData : MonoBehaviour, IInteractable
{
    WeaponController weaponController;
    GameObject player;
    
    void Start()
    {
        player = GameObject.Find("BareHandRiggedNM");
        weaponController = player.GetComponent<WeaponController>();
    }

    public void Interact()
    {
        Debug.Log(weaponController + "||" + player.name);
        weaponController.enabled = true;
        weaponController.weapon = this.gameObject;
    }
}
