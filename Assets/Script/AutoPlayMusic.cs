using UnityEngine;

public class AutoPlayMusic : MonoBehaviour
{
    [Header("Kéo máy phát nhạc (có Audio Source) vào đây")]
    public AudioSource musicPlayer;

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem người bước vào có phải là Player không
        if (other.CompareTag("Player"))
        {
            // Chỉ phát nhạc nếu máy chưa bật để tránh bị phát lại từ đầu khi lỡ bước ra bước vào
            if (musicPlayer != null && !musicPlayer.isPlaying)
            {
                musicPlayer.Play();
            }
        }
    }

    // (Tùy chọn) Nếu bạn muốn ra khỏi nhà nhạc tự tắt thì dùng đoạn này
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (musicPlayer != null && musicPlayer.isPlaying)
            {
                musicPlayer.Pause(); // Dùng Pause để khi vào lại sẽ phát tiếp bài đó
            }
        }
    }
}
