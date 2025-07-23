using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public bool canBePickedUp = true;

    public void PickUp()
    {
        // Disable the item game object when picked up
        gameObject.SetActive(false);
        Debug.Log(itemName + " has been picked up.");
    }
}
