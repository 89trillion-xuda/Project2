using System;
using System.Collections.Generic;
using TableConfig;

namespace Model
{
    [Serializable]
    public class BuffModel : BaseModel,ITableModel
    {
        public int id;
        public int skillId;
        public int BuffTarget;
        public int Race;
        public int[] AttackType;
        public int[] BuffTargetArmy;
        public int BuffEffectType;
        public float BuffEffectValueUp;
        public float BuffEffectValueDe;

        public override object Key()
        {
            return id;
        }

        public Dictionary<string, string> ParsePerValue { get; set; }
    }
}