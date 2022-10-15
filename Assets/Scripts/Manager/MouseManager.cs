using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;



public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;

    
    public event Action<Vector3> OnMouseClicked;
    //����ע��Ҫ�ƶ��Ľ�ɫ(C#�Դ�ί��Action)
    private RaycastHit hitInfo;
    //����RayCast���ص�����
    public Texture2D point, doorway, attack, target, arrow;


    private void Awake()
    {
        if (null != Instance)
        {
            Destroy(gameObject);
        }
        else
            Instance = this;
    }


    private void Update()
    {
        SetCursorTexture();//����ray��ˢ��hitInfo
        MouseControll();//�����λ�õ�OnMouseClicked��Vector3��
    }


    private void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//ScreenPointToRay:���ش�camera������λ�õ�ray

        if (Physics.Raycast(ray,out hitInfo))//Physics.Raycast:�ж������Ƿ�Ӵ�collider����boolֵ
        {
            //�л�ָ����ͼ
            switch(hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(point,new Vector2(16,16),CursorMode.Auto);
                    break;

                case "doorway":
                    Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                    break;

                case "Enemy":
                    Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                    break;

                case "target":
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;

                case "arrow":
                    Cursor.SetCursor(arrow, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
        }
    }


    private void MouseControll()
    {
        if(Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
                OnMouseClicked?.Invoke(hitInfo.point);//?.(����C#6.0):�жϣ�ǰ�����Ƿ�ΪNULL�����ΪNULL�򷵻�NULL������ִ�к������
        }
    }
}




/*//�Ǽ̳�MonoBehavior��ʹ��Serializeable���л�ʹ�ɼ�
//EventVector3���������λ�ã���Ҫ��ȡ��λ��Vector3
[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }//��Ҫ����Vector3��Event�¼�*/