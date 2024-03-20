using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followObj;
    public float followSpeed = 10f;
    public float sensitivity = 200f;
    public float clampAngle = 70f;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;

    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;

    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;


        //처음 카메라의 방향을 가져옴
        //밑에 이거 오프셋으로 바꿔야됨
        dirNormalized = realCamera.localPosition.normalized;

        //처음 카메라의 길이를 가져옴 오프셋 느낌이지
        finalDistance = realCamera.localPosition.magnitude;
    }

    void Update()
    {
        rotX -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0f);
        transform.rotation = rot;
    }
    void LateUpdate()
    {
        //팔로우 오브젝트 따라가게 거의 붙는 수준으로
        //transform.position = Vector3.MoveTowards(transform.position, followObj.position, followSpeed * Time.deltaTime);
        transform.position = followObj.position;
        // TransformPoint() <- 로컬 포지션을 월드 포지션의 트랜스폼으로 변경해줌
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;
        if(Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);


        //Debug.Log("디렉션 * 최종 거리");
        //Debug.Log(dirNormalized * finalDistance);

        //Debug.Log("리얼카메라 로컬포지션");
        //Debug.Log(realCamera.localPosition);
    }
}
