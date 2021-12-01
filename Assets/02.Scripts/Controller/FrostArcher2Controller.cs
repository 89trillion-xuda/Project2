using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using DataAnalysis;

namespace FrostArcherControl
{
    /// <summary>
    /// 敌方士兵2的控制类，读取数据来设置敌方士兵的血量初始值
    /// </summary>
    public class FrostArcher2Controller : MonoBehaviour
    {
        //接收传过来的FrostArcherManager脚本组件，以便后面进行实例化
        [SerializeField] private FrostArcherManager frostArcherManagerClone;
        //接收敌方弓箭手的血量显示文本组件
        [SerializeField] private TextMesh textMeshClone;
    
        // Start is called before the first frame update
        void Start()
        {
            //实例化一个FrostArcherManager.cs脚本组件，以便使用里面的方法
            FrostArcherManager frostArcherManager = FrostArcherManager.Instantiate(frostArcherManagerClone, transform);
            //获得cav里的弓箭手数据
            List<FrostArcherModel> list = frostArcherManager.GetFrostArcherModel();

            //设置初始血量值
            textMeshClone.text = list[0].MaxHp.ToString();
        }
    
    }

}