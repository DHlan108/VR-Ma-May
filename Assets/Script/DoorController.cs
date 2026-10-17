using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Gắn 2 cánh cửa vào đây")]
    public Transform leftDoor;  // Kéo Door
    public Transform rightDoor; // Kéo Door.001

    [Header("Góc mở cửa")]
    public float leftDoorOpenAngle = 90f;   // Cánh trái xoay 90 độ
    public float rightDoorOpenAngle = -90f; // Cánh phải xoay ngược lại -90 độ
    public float openSpeed = 2f;            // Tốc độ mở

    private bool isOpen = false;
    private bool isMoving = false;

    private Quaternion leftClosedRot;
    private Quaternion rightClosedRot;
    private Quaternion leftOpenRot;
    private Quaternion rightOpenRot;

    void Start()
    {
        // Lưu góc đóng ban đầu của 2 cửa
        leftClosedRot = leftDoor.localRotation;
        rightClosedRot = rightDoor.localRotation;

        // Tính sẵn góc khi cửa mở hết (xoay quanh trục Y)
        leftOpenRot = leftClosedRot * Quaternion.Euler(0, leftDoorOpenAngle, 0);
        rightOpenRot = rightClosedRot * Quaternion.Euler(0, rightDoorOpenAngle, 0);
    }

    // Hàm này sẽ được gọi khi bạn tương tác bằng tay VR
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
        isOpen = !isOpen; // Đổi trạng thái đóng/mở

        Quaternion targetLeft = isOpen ? leftOpenRot : leftClosedRot;
        Quaternion targetRight = isOpen ? rightOpenRot : rightClosedRot;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * openSpeed;
            // Xoay cả 2 cửa cùng lúc một cách mượt mà
            leftDoor.localRotation = Quaternion.Slerp(leftDoor.localRotation, targetLeft, time);
            rightDoor.localRotation = Quaternion.Slerp(rightDoor.localRotation, targetRight, time);
            yield return null;
        }

        isMoving = false;
    }
}