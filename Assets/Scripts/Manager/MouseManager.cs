using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;



public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;

    
    public event Action<Vector3> OnMouseClicked;
    //用于注册要移动的角色(C#自带委托Action)
    private RaycastHit hitInfo;
    //接收RayCast返回的数据
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
        SetCursorTexture();//生成ray，刷新hitInfo
        MouseControll();//传点击位置到OnMouseClicked（Vector3）
    }


    private void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//ScreenPointToRay:返回从camera到传入位置的ray

        if (Physics.Raycast(ray,out hitInfo))//Physics.Raycast:判断射线是否接触collider返回bool值
        {
            //切换指针贴图
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
                OnMouseClicked?.Invoke(hitInfo.point);//?.(来自C#6.0):判断？前对象是否为NULL，如果为NULL则返回NULL，否则执行后续语句
        }
    }
}




/*//非继承MonoBehavior，使用Serializeable序列化使可见
//EventVector3是鼠标点击的位置，需要获取该位置Vector3
[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }//需要传入Vector3的Event事件*/