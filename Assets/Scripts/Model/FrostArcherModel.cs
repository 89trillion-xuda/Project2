using System;
using System.Collections.Generic;
using TableConfig;

namespace Model
{
    [Serializable]
    public class FrostArcherModel : BaseModel,ITableModel
    {
        public int id;
        public string Name;
        public string note;
        public int MaxHp;
        public int Atk;
        public int Def;
        public int ShootSpeed;
        public Dictionary<string, string> ParsePerValue { get; set; }

        public override object Key()
        {
            return id;
        }
    }
}
