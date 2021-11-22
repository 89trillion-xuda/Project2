﻿using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using TableConfig;
using UnityEngine;

public class FrostArcherManager : MonoBehaviour
{
    public static FrostArcherManager instance;

    private void Awake()
    {
        instance = this;
    }

    #region FrostArcher

    private ITable2Data<FrostArcherModel> frostArcherTable;
    private List<FrostArcherModel> frostArcherModel;

    private ITable2Data<FrostArcherModel> FrostArcherModelTable => frostArcherTable ?? (frostArcherTable = new TableManager<FrostArcherModel>());

    public List<FrostArcherModel> GetFrostArcherModel()
    {
        return frostArcherModel ?? (frostArcherModel = FrostArcherModelTable.GetAllModel());
    }

    public FrostArcherModel GetFrostArcherModel(int id)
    {
        var frostArcherModelDic = GetFrostArcherModelDic();

        if (frostArcherModelDic != null && frostArcherModelDic.ContainsKey(id))
        {
            return frostArcherModelDic[id];
        }

        return null;
    }

    private Dictionary<int, FrostArcherModel> FrostArcherModelDic;

    public Dictionary<int, FrostArcherModel> GetFrostArcherModelDic()
    {
        if (FrostArcherModelDic == null)
        {
            FrostArcherModelDic = new Dictionary<int, FrostArcherModel>();
            List<FrostArcherModel> list = GetFrostArcherModel();
            foreach (var item in list)
            {
                FrostArcherModelDic.Add(item.id, item);
            }
        }

        return FrostArcherModelDic;
    }
    #endregion
}
