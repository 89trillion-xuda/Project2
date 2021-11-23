using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //单例
    public static ObjectPool instance;
    private Stack<GameObject> pool = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    

    //将对象伪删除，将对象放回对象池
    public void Delete(GameObject g)
    {
        //设置弃用，隐藏对象
        g.SetActive(false);
        //将对象放回对象池
        pool.Push(g);
    }
    
    //从对象池取出对象
    public GameObject Create(GameObject prefab)
    {
        GameObject g;
        
        //池中有对象时
        if (pool.Count > 0)
        {
            //取出对象
            g = pool.Pop();
            //设置启用，显示对象
            g.SetActive(true);
        }
        else //池中没有对象时
        {
            //实例化一个对象
            g = Instantiate(prefab);
        }

        return g;
    }
}
