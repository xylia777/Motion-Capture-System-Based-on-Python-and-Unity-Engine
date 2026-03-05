using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera : MonoBehaviour
{
    //摄像机始终看向角色
    public GameObject body;
    public Camera cam;

    //视野缩放速率，摄像机移动速率
    public float view_value=200, move_speed=200;

    [Header("跟随目标")]
    public Transform target;  // 要跟随的目标物体

    [Header("跟随设置")]
    public float followSpeed = 5f;  // 跟随速度
    public Vector3 offset = new Vector3(0, 2, -5);  // 相对于目标的偏移量

    // Start is called before the first frame update
    void Start()
    {
        body = GameObject.Find("body");
        cam = GetComponent<Camera>();


    }

    // Update is called once per frame
    void Update()
    {
        /*//摄像机对准body
        cam.transform.LookAt(body.transform);  //有倾斜*/


        //放大、缩小
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * view_value));
        }
        //移动视角
        if (Input.GetMouseButton(2))
        {
            transform.Translate(Vector3.left * Input.GetAxis("Mouse X") * move_speed);
            transform.Translate(Vector3.up * Input.GetAxis("Mouse Y") * -move_speed);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 计算目标位置（目标前方 + 偏移）
        Vector3 targetPosition = target.position +
        target.forward * offset.z +
                                target.up * offset.y;

        // 平滑移动到目标位置
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // 始终看向目标
        transform.LookAt(target.position);
    }
}

