using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TextCore;
using DG.Tweening;
using Model;

/// <summary>
/// 己方士兵1的控制类，控制动画的播放、扣血等
/// </summary>
public class FrostArcher1Controller : MonoBehaviour
{
    //获得动画控制器组件
    [SerializeField] private Animator animator;

    //获得冰弓箭预制体的克隆
    [SerializeField] private GameObject IceArrowClone;
    //获得弓箭手1，己方弓箭手
    [SerializeField] private GameObject FrostArcher1Clone;
    //获得弓箭手2，敌方弓箭手
    [SerializeField] private GameObject FrostArcher2Clone;
    
    //接收敌方弓箭手2的血量显示文本组件
    [SerializeField] private TextMesh textMeshClone;
    
    //接收传过来的FrostArcherManager脚本组件，以便后面进行实例化
    [SerializeField] private FrostArcherManager frostArcherManagerClone;

    //控制攻速的bool类型标志锁，判断下一只箭能否射出？控制攻击速度为 每秒射出1只箭
    private bool nextArrow = true;
    
    // Update is called once per frame
    void Update()
    {
        //键盘按下A，展示攻击动画
        if (Input.GetKeyDown(KeyCode.A))
        {
            //如果下一只箭可以射出，再执行射箭动画的播放
            if (nextArrow == true)
            {
                //下一只箭还不能射出
                nextArrow = false;
                //设置进攻动画布尔参数为true，别的为false
                animator.SetBool("victory",false);
                animator.SetBool("idel",false);
                animator.SetBool("run",false);
                animator.SetBool("attack",true);
                //延迟执行弓箭飞出的动画，来衔接匹配人物的射箭动画
                Invoke("IceArrow", 0.5f);
            }
        }

        //键盘按下R，展示奔跑动画
        if (Input.GetKeyDown(KeyCode.R))
        {
            //设置跑步动画布尔参数为true，别的为false
            animator.SetBool("victory",false);
            animator.SetBool("idel",false);
            animator.SetBool("attack",false);
            animator.SetBool("run",true);
            //移动
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }

        //键盘按下I，展示待机动画
        if (Input.GetKeyDown(KeyCode.I))
        {
            //设置待机动画布尔参数为true，别的为false
            animator.SetBool("victory",false);
            animator.SetBool("run",false);
            animator.SetBool("attack",false);
            animator.SetBool("idel",true);
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
        //设置弓箭的朝向，为己方弓箭手的朝向
        Vector3 dir = new Vector3(FrostArcher1Clone.transform.position.x,
            FrostArcher1Clone.transform.position.y, 
            FrostArcher1Clone.transform.position.z);
        iceArrow.transform.rotation = Quaternion.LookRotation(-dir);

        //移动弓箭，完成后销毁
        iceArrow.transform
            .DOMove(FrostArcher2Clone.transform.position + new Vector3(-0.2f,0.3f,0), 0.5f)
            .OnComplete(() =>
            {
                //销毁弓箭
                Destroy(iceArrow);
                
                //实例化一个FrostArcherManager.cs脚本组件，以便使用里面的方法
                FrostArcherManager frostArcherManager = FrostArcherManager.Instantiate(frostArcherManagerClone, transform);
                //获得cav里的弓箭手数据，以便获得攻击力数据进行后续扣血
                List<FrostArcherModel> list = frostArcherManager.GetFrostArcherModel();
                
                //血量 >0 时
                if (int.Parse(textMeshClone.text) > 0)
                {
                    //扣血，刷新血量
                    textMeshClone.text = (int.Parse(textMeshClone.text) - list[0].Atk).ToString();
                }
                //血量 <= 0 时
                if (int.Parse(textMeshClone.text) <= 0)
                {
                    //设置敌方士兵禁用，敌方士兵消失
                    FrostArcher2Clone.SetActive(false);
                    //己方士兵播放获胜动画
                    animator.SetBool("victory",true);
                }

                //设置攻击动画布尔参数为false，攻击动画结束。
                animator.SetBool("attack",false);
                //设置下一只箭可以射出
                nextArrow = true;
            });
    }
    
}
