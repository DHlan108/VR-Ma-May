using UnityEngine;
using TMPro; // Thư viện dùng cho TextMeshPro (Nếu dùng Text thường thì đổi thành UnityEngine.UI)

public class AutoPlayMusic : MonoBehaviour
{
    [Header("Cài đặt Nguồn Nhạc")]
    public AudioSource musicPlayer;
    public AudioClip[] playlist; // Danh sách các bài hát

    [Header("Cài đặt Giao diện (World Space UI)")]
    [Tooltip("Kéo bảng Canvas hoặc Panel chứa danh sách bài hát vào đây")]
    public GameObject musicMenuUI; 
    [Tooltip("Kéo Text hiển thị tên bài hát đang phát vào đây")]
    public TextMeshProUGUI statusText; 

    private int currentTrackIndex = 0;
    private bool isPlayerInZone = false;
    private bool hasStartedPlaying = false;

    private void Start()
    {
        // Đảm bảo tắt Loop của AudioSource để nhạc có thể kết thúc và tự chuyển bài mới
        if (musicPlayer != null)
        {
            musicPlayer.loop = false;
        }

        // Ẩn bảng chọn nhạc lúc mới vào game
        if (musicMenuUI != null)
        {
            musicMenuUI.SetActive(false);
        }
    }

    private void Update()
    {
        // Nếu người chơi đang ở trong phòng, nhạc đã từng bật, nhưng hiện tại AudioSource đã dừng (tức là vừa hết bài)
        if (isPlayerInZone && hasStartedPlaying && musicPlayer != null)
        {
            if (!musicPlayer.isPlaying && musicPlayer.time == 0f)
            {
                PlayNextTrack();
            }
        }
    }

    // --- XỬ LÝ RA VÀO VÙNG TRIGGER ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;

            if (musicPlayer != null && playlist.Length > 0)
            {
                if (!hasStartedPlaying)
                {
                    // Lần đầu bước vào: Tự động phát bài đầu tiên (Index 0)
                    PlayTrack(0);
                }
                else if (!musicPlayer.isPlaying)
                {
                    // Bước vào lại sau khi ra ngoài: Phát tiếp đoạn đang dừng
                    musicPlayer.UnPause();
                    UpdateStatusText();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;

            if (musicPlayer != null && musicPlayer.isPlaying)
            {
                musicPlayer.Pause();
            }

            // Tự động đóng bảng chọn nhạc khi người chơi đi ra xa
            if (musicMenuUI != null)
            {
                musicMenuUI.SetActive(false);
            }
        }
    }

    // --- CÁC HÀM ĐIỀU KHIỂN NHẠC & GIAO DIỆN ---

    // 1. Gắn hàm này vào sự kiện khi bấm vào máy phát nhạc trong VR
    public void ToggleMusicMenu()
    {
        if (musicMenuUI != null)
        {
            bool isActive = !musicMenuUI.activeSelf;
            musicMenuUI.SetActive(isActive);
            
            if (isActive)
            {
                UpdateStatusText();
            }
        }
    }

    // 2. Gắn hàm này vào các Nút bấm (Button) chọn bài trên UI
    public void SelectTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < playlist.Length)
        {
            PlayTrack(trackIndex);
        }
    }

    private void PlayTrack(int index)
    {
        if (musicPlayer == null || playlist.Length == 0) return;

        currentTrackIndex = index;
        musicPlayer.clip = playlist[currentTrackIndex];
        musicPlayer.Play();
        hasStartedPlaying = true;

        UpdateStatusText();
    }

    public void PlayNextTrack()
    {
        if (playlist.Length == 0) return;

        // Tăng thứ tự bài hát, nếu hết danh sách thì quay lại bài đầu tiên (0)
        int nextIndex = (currentTrackIndex + 1) % playlist.Length;
        PlayTrack(nextIndex);
    }

    private void UpdateStatusText()
    {
        if (statusText != null && playlist.Length > 0)
        {
            statusText.text = "Đang phát: " + playlist[currentTrackIndex].name + "\nHãy chọn bài hát bạn thích:";
        }
    }
}