// InventoryManager.cs
using System.Collections.Generic; // Untuk menggunakan List
using UnityEngine;
using UnityEngine.UI;             // Untuk mengelola UI

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton pattern agar mudah diakses

    public List<Item> inventoryItems = new List<Item>(); // Daftar item yang dimiliki pemain
    public int maxInventorySlots = 4; // Jumlah slot inventaris maksimal

    [Header("UI References")]
    public GameObject inventoryPanel;     // Panel UI tempat slot inventaris berada
    public Image[] inventorySlotIcons;    // Array gambar untuk menampilkan ikon item di setiap slot

    void Awake()
    {
        // Mengatur Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Pastikan panel inventaris tersembunyi di awal
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false); 
        }
        UpdateInventoryUI(); // Perbarui UI di awal
    }

    // Metode untuk menambahkan item ke inventaris
    public bool AddItem(Item item)
    {
        if (inventoryItems.Count >= maxInventorySlots)
        {
            Debug.Log("Inventaris penuh! Tidak bisa menambahkan " + item.itemName);
            return false; // Gagal menambahkan item
        }

        inventoryItems.Add(item);
        Debug.Log(item.itemName + " ditambahkan ke inventaris.");
        UpdateInventoryUI(); // Perbarui UI setiap kali item ditambahkan
        return true; // Berhasil menambahkan item
    }

    // Metode untuk memperbarui tampilan UI inventaris
    void UpdateInventoryUI()
    {
        int slotCount = Mathf.Min(maxInventorySlots, inventorySlotIcons.Length);
        for (int i = 0; i < slotCount; i++)
        {
            if (i < inventoryItems.Count)
            {
                // Jika ada item di slot ini, tampilkan ikonnya
                inventorySlotIcons[i].sprite = inventoryItems[i].itemIcon;
                inventorySlotIcons[i].enabled = true; // Pastikan gambar aktif
            }
            else
            {
                // Jika tidak ada item, sembunyikan ikon
                inventorySlotIcons[i].sprite = null;
                inventorySlotIcons[i].enabled = false; // Sembunyikan gambar
            }
        }
    }

    // Metode untuk menyembunyikan/menampilkan panel inventaris
    public void ToggleInventoryPanel()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf); // Mengubah status aktif/nonaktif
            Debug.Log("Panel Inventaris: " + inventoryPanel.activeSelf);
        }
    }
}
