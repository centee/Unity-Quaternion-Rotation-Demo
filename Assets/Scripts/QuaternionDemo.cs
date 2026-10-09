using UnityEngine;
using TMPro;

public class QuaternionDemo : MonoBehaviour
{
    public GameObject cubeEuler;
    public GameObject cubeQuat;
    public TextMeshProUGUI infoText;

    private float angleX = 0;
    private float angleY = 0;
    private float angleZ = 0;

    void Update()
    {
        // 键盘控制旋转增量
        if (Input.GetKey(KeyCode.A)) angleY -= Time.deltaTime * 60f;
        if (Input.GetKey(KeyCode.D)) angleY += Time.deltaTime * 60f;
        if (Input.GetKey(KeyCode.W)) angleX += Time.deltaTime * 60f;
        if (Input.GetKey(KeyCode.S)) angleX -= Time.deltaTime * 60f;
        if (Input.GetKey(KeyCode.Q)) angleZ -= Time.deltaTime * 60f;
        if (Input.GetKey(KeyCode.E)) angleZ += Time.deltaTime * 60f;

        // ========== 欧拉角立方体 ==========
        cubeEuler.transform.eulerAngles = new Vector3(angleX, angleY, angleZ);

        // ========== 四元数立方体：轴角构造 + 四元数乘法叠加 ==========
        Quaternion qX = Quaternion.AngleAxis(angleX, Vector3.right);
        Quaternion qY = Quaternion.AngleAxis(angleY, Vector3.up);
        Quaternion qZ = Quaternion.AngleAxis(angleZ, Vector3.forward);
        Quaternion rotQuat = qY * qX * qZ;
        cubeQuat.transform.rotation = rotQuat;

        // UI文字输出
        infoText.text =
$@"Euler Angle
X:{angleX:F1} Y:{angleY:F1} Z:{angleZ:F1}

Quaternion
w:{rotQuat.w:F2}, x:{rotQuat.x:F2}, y:{rotQuat.y:F2}, z:{rotQuat.z:F2}

Controls:
W/S -> X axis
A/D -> Y axis
Q/E -> Z axis
Tip: Observe Gimbal Lock near X=90";
    }

    // Gizmos绘制物体局部坐标轴，Scene窗口可见
    void OnDrawGizmos()
    {
        DrawAxis(cubeEuler.transform, Color.red, Color.green, Color.blue);
        DrawAxis(cubeQuat.transform, Color.magenta, Color.yellow, Color.cyan);
    }

    void DrawAxis(Transform t, Color rx, Color ry, Color rz)
    {
        if (t == null) return;
        Gizmos.color = rx;
        Gizmos.DrawRay(t.position, t.right * 1.5f);
        Gizmos.color = ry;
        Gizmos.DrawRay(t.position, t.up * 1.5f);
        Gizmos.color = rz;
        Gizmos.DrawRay(t.position, t.forward * 1.5f);
    }
}
