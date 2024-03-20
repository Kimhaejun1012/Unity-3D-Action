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


        //ó�� ī�޶��� ������ ������
        //�ؿ� �̰� ���������� �ٲ�ߵ�
        dirNormalized = realCamera.localPosition.normalized;

        //ó�� ī�޶��� ���̸� ������ ������ ��������
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
        //�ȷο� ������Ʈ ���󰡰� ���� �ٴ� ��������
        //transform.position = Vector3.MoveTowards(transform.position, followObj.position, followSpeed * Time.deltaTime);
        transform.position = followObj.position;
        // TransformPoint() <- ���� �������� ���� �������� Ʈ���������� ��������
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


        //Debug.Log("�𷺼� * ���� �Ÿ�");
        //Debug.Log(dirNormalized * finalDistance);

        //Debug.Log("����ī�޶� ����������");
        //Debug.Log(realCamera.localPosition);
    }
}
