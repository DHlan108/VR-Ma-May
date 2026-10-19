using UnityEngine;

public class AutoShowInfo : MonoBehaviour
{
    [Header("Kéo bảng thông tin (Canvas) vào đây")]
    public GameObject infoPanel;

    // Hàm này tự động chạy khi có vật thể chạm vào vùng tàng hình
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem vật chạm vào có phải là người chơi không
        if (other.CompareTag("Player"))
        {
            if (infoPanel != null) infoPanel.SetActive(true); // Bật bảng
        }
    }

    // Hàm này tự động chạy khi vật thể đi ra khỏi vùng tàng hình
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (infoPanel != null) infoPanel.SetActive(false); // Tắt bảng
        }
    }
}