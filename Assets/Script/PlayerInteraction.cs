// PlayerInteraction.cs
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah objek yang bertabrakan adalah item
        Item item = other.GetComponent<Item>();
        if (item != null && item.canBePickedUp)
        {
            // Panggil metode AddItem dari InventoryManager
            // Kita menggunakan Singleton instance untuk mengakses InventoryManager dengan mudah
            bool added = InventoryManager.instance.AddItem(item);
            
            if (added)
            {
                item.PickUp(); // Jika berhasil ditambahkan, panggil metode PickUp pada item
            }
        }
    }

    // Opsional: Untuk menguji buka/tutup inventaris
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Tekan tombol 'I' untuk membuka/menutup inventaris
        {
            InventoryManager.instance.ToggleInventoryPanel();
        }
    }
}