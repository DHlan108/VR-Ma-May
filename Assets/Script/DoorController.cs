using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Gắn 2 cánh cửa vào đây")]
    public Transform leftDoor;  
    public Transform rightDoor; 

    [Header("Góc mở cửa")]
    public float leftDoorOpenAngle = -90f;   
    public float rightDoorOpenAngle = 90f; 
    public float openSpeed = 2f;            

    [Header("Trạng thái ban đầu")]
    public bool startOpen = true; // Đánh dấu tích để cửa mở sẵn lúc mới vào game

    private bool isOpen = false;
    private bool isMoving = false;

    private Quaternion leftClosedRot;
    private Quaternion rightClosedRot;
    private Quaternion leftOpenRot;
    private Quaternion rightOpenRot;

    void Start()
    {
        // 1. Code vẫn ngầm hiểu vị trí hiện tại trong Scene là góc ĐÓNG chuẩn xác
        leftClosedRot = leftDoor.localRotation;
        rightClosedRot = rightDoor.localRotation;

        // 2. Tính toán sẵn góc MỞ
        leftOpenRot = leftClosedRot * Quaternion.Euler(0, leftDoorOpenAngle, 0);
        rightOpenRot = rightClosedRot * Quaternion.Euler(0, rightDoorOpenAngle, 0);

        // 3. Nếu bạn muốn cửa mở sẵn, ép cửa nhảy sang góc mở ngay lập tức
        if (startOpen)
        {
            leftDoor.localRotation = leftOpenRot;
            rightDoor.localRotation = rightOpenRot;
            isOpen = true; // Cập nhật hệ thống: cửa đang mở
        }
        else
        {
            isOpen = false;
        }
    }

    // Hàm này được gọi khi tay cầm XR bấm Select
    public void ToggleDoubleDoors()
    {
        if (!isMoving)
        {
            StartCoroutine(AnimateDoors());
        }
    }

    IEnumerator AnimateDoors()
    {
        isMoving = true;
        isOpen = !isOpen; // Đảo trạng thái

        Quaternion targetLeft = isOpen ? leftOpenRot : leftClosedRot;
        Quaternion targetRight = isOpen ? rightOpenRot : rightClosedRot;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * openSpeed;
            leftDoor.localRotation = Quaternion.Slerp(leftDoor.localRotation, targetLeft, time);
            rightDoor.localRotation = Quaternion.Slerp(rightDoor.localRotation, targetRight, time);
            yield return null;
        }
        
        isMoving = false;
    }
}