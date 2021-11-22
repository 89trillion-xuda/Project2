using System;
using System.Collections.Generic;
using Model;
using TableConfig;
using UnityEngine;

public class GameModelManager: MonoBehaviour
{
    public static GameModelManager instance;

    private void Awake()
    {
        instance = this;
    }

    #region BuffModel

    private ITable2Data<BuffModel> buffModelTable;
    private List<BuffModel> buffModel;

    private ITable2Data<BuffModel> BuffModelTable => buffModelTable ?? (buffModelTable = new TableManager<BuffModel>());

    public List<BuffModel> GetBuffModel()
    {
        return buffModel ?? (buffModel = BuffModelTable.GetAllModel());
    }

    public BuffModel GetBuffModel(int id)
    {
        var buffModelDic = GetBuffModelDic();

        if (buffModelDic != null && buffModelDic.ContainsKey(id))
        {
            return buffModelDic[id];
        }

        return null;
    }

    private Dictionary<int, BuffModel> BuffModelDic;

    public Dictionary<int, BuffModel> GetBuffModelDic()
    {
        if (BuffModelDic == null)
        {
            BuffModelDic = new Dictionary<int, BuffModel>();
            List<BuffModel> list = GetBuffModel();
            foreach (var item in list)
            {
                BuffModelDic.Add(item.id, item);
            }
        }

        return BuffModelDic;
    }

    #endregion
}