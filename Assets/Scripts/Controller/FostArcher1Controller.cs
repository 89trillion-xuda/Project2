using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TextCore;
using DG.Tweening;

public class FostArcher1Controller : MonoBehaviour
{

    //获得动画控制器组件
    [SerializeField] private Animator animator;
    //获得攻击动作的运行时动画控制器
    [SerializeField] private RuntimeAnimatorController attack;
    //获得待机动作的运行时动画控制器
    [SerializeField] private RuntimeAnimatorController idle;
    //获得奔跑动作的运行时动画控制器
    [SerializeField] private RuntimeAnimatorController run;
    
    //获得冰弓箭预制体的克隆
    [SerializeField] private GameObject IceArrowClone;
    //获得弓箭手1
    [SerializeField] private GameObject FrostArcher1Clone;
    //获得弓箭手2
    [SerializeField] private GameObject FrostArcher2Clone;

    // Update is called once per frame
    void Update()
    {
        //键盘按下A，展示攻击动画
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.runtimeAnimatorController = null;
            animator.runtimeAnimatorController = attack;
            Invoke("IceArrow", 0.4f);
        }

        //键盘按下R，展示奔跑动画
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.runtimeAnimatorController = run;
        }

        //键盘按下I，展示待机动画
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.runtimeAnimatorController = idle;
        }
        
        //垂直轴 1 0 -1
        float vertical = Input.GetAxis("Vertical");
        //水平轴 1 0 -1
        float horizontal = Input.GetAxis("Horizontal");
        //方向向量 x y z，获取摇杆的方向
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        
        if (dir != Vector3.zero)//按下了按键
        {
            //只能用上下左右控制走动，防止按下A键时也发生旋转和移动
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.RightArrow))
            {
                //旋转
                transform.rotation = Quaternion.LookRotation(dir);
                //移动
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            }
        }
    }

    //发出弓箭函数
    public void IceArrow()
    {
        //实例化一支箭
        GameObject iceArrow = GameObject.Instantiate(IceArrowClone,transform) as GameObject;
        
        //设置弓箭起始坐标
        iceArrow.transform.position = FrostArcher1Clone.transform.position + new Vector3(0,0.3f,0);
        //设置弓箭的朝向，为弓箭手1的朝向
        Vector3 dir = new Vector3(FrostArcher1Clone.transform.position.x,
            FrostArcher1Clone.transform.position.y, 
            FrostArcher1Clone.transform.position.z);
        iceArrow.transform.rotation = Quaternion.LookRotation(-dir);

        //移动弓箭，完成后销毁
        iceArrow.transform
            .DOMove(FrostArcher2Clone.transform.position + new Vector3(-0.2f,0.3f,0), 0.7f)
            .OnComplete(() => Destroy(iceArrow));
    }

    private void Start()
    {
        
    }
}
