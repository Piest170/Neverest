using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Models
{
    public class SkillModel
    {
        public int SkillId;
        public string SkillName;
        public string SkillGroup;
        public string MaxSkillLevel;
    }
    [Serializable]
    public class SkillCreds
    {
        public int characterId;
        public int skillId;
        public int learningLevel;
        public string learningStatus;
    }
}
