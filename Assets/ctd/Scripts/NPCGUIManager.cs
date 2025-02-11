using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCGUIManager : MonoBehaviour
{
    public MonoBehaviour[] scripts;
    public GameObject gui;
    public NPCAGAT npcagata;
    
    // Butonun tıkladığı objeyi dışarıdan alalım
    public void UseButton(GameObject clickedObject)
    {
        if (clickedObject == null)
        {
            Debug.LogError("Clicked Object is NULL! Butonlar doğru atanmış mı?");
            return;
        }

        Debug.Log("Clicked Object: " + clickedObject.name);

        switch (clickedObject.name)
        {
            case "ragdoll1":
                Debug.Log("flip");
                foreach (var script in scripts)
                {
                    script.enabled = true;
                }
                npcagata.animator.SetTrigger("backflip");
                npcagata.walk = false;
                break;

            case "ragdoll2":
                Debug.Log("walk");
                foreach (var script in scripts)
                {
                    script.enabled = true;
                }
                npcagata.follow();
                break;

            case "ragdoll3":
                Debug.Log("cry");
                foreach (var script in scripts)
                {
                    script.enabled = true;
                }
                npcagata.animator.SetTrigger("cry");
                npcagata.walk = false;
                break;

            case "ragdoll4":
                Debug.Log("ragdoll");
                npcagata.ragdoll();
                foreach (var script in scripts)
                {
                    script.enabled = true;
                }
                npcagata.walk = false;
                break;
            
            case "ragdoll5":
                Debug.Log("face change");    
                npcagata.faceLevel++;
                npcagata.walk = false;
                foreach (var script in scripts)
                {
                    script.enabled = true;
                }
                break;
                

            default:
                Debug.Log("Tıklanan obje tanımlı değil: " + clickedObject.name);
                break;
        }

        foreach (var script in scripts)
        {
            script.enabled = true;
        }
        gui.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}