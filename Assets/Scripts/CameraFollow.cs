using UnityEngine;

/// <summary>
/// Camera đi theo một đối tượng (thường là Player).
/// Gắn script này lên Main Camera.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;        // Đối tượng camera đi theo (kéo Player vào đây)
    [SerializeField] private float smoothSpeed = 5f;  // Càng lớn thì camera bám càng sát

    // LateUpdate chạy SAU mọi Update, tức là sau khi Player đã di chuyển xong trong frame này
    private void LateUpdate()
    {
        if (target == null) return;   // Chưa gán target thì không làm gì

        // Vị trí camera muốn tới: cùng x, y với Player, giữ nguyên z của camera
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Dịch camera từ chỗ hiện tại về phía vị trí muốn tới
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}