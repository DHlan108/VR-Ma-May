using UnityEngine;

public class ToggleInfoPanel : MonoBehaviour
{
    [Header("Kéo bảng thông tin (Canvas) vào đây")]
    public GameObject infoPanel;

    // Hàm này sẽ đảo ngược trạng thái: đang tắt thành bật, đang bật thành tắt
    public void TogglePanel()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
    }
}