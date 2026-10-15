using UnityEngine;

// Yêu cầu phải có component Outline trên cùng vật thể
[RequireComponent(typeof(Outline))]
public class Highlight : MonoBehaviour
{
    private Outline outline;
    
    public Color hoverColor = Color.white;  // Màu viền khi đưa tay lại gần
    public Color selectColor = Color.red;   // Màu viền khi nắm chặt

    void Start()
    {
        // Lấy component Outline và tắt nó đi khi mới bắt đầu game
        outline = GetComponent<Outline>();
        outline.enabled = false; 
    }

    public void OnHoverEnter()
    {
        outline.OutlineColor = hoverColor;
        outline.enabled = true; // Bật viền
    }

    public void OnHoverExit()
    {
        outline.enabled = false; // Tắt viền
    }

    public void OnSelectEnter()
    {
        outline.OutlineColor = selectColor;
        outline.enabled = true; // Bật viền màu khác
    }
    
    public void OnSelectExit()
    {
        // Trở về màu hover khi thả tay ra (vì tay vẫn đang ở gần)
        outline.OutlineColor = hoverColor;
        outline.enabled = true; 
    }
}