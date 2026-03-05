using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive udpReceive;
    public GameObject[] bodyPoints;
    
    // 包含所有Sphere的父对象 校正
    public Transform spheresParent; 
    private Vector3 parentInitialPosition;
    private Quaternion parentInitialRotation;

  

    void Start()
    {   //校正
        parentInitialPosition = spheresParent.position;
        parentInitialRotation = spheresParent.rotation;
        ResetSpheres();
    }

    public void ResetSpheres()
    {   //校正
        spheresParent.position = parentInitialPosition;
        spheresParent.rotation = parentInitialRotation;
    }
    // Update is called once per frame
    void Update()
    {

        string data = udpReceive.data;
        
        print(data); //data的数据  33个3维坐标
        string[] points = data.Split(',');
        print("points[0]:"+points[0]);
        print("points[1]:"+points[1]);
        print("points[2]:"+points[2]);

       

        for (int i = 0; i < 33; i++)
        {

            float x = float.Parse(points[0 + (i * 3)]) / 100;
            float y = float.Parse(points[1 + (i * 3)]) / 100;
            float z = float.Parse(points[2 + (i * 3)]) / 300;
            /* float z = 0f;*/

            bodyPoints[i].transform.localPosition = new Vector3(x, y, z);

        }
       


    }
}

