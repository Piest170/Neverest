using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Models
{
    [Serializable]
    public class CharacterModel
    {
        public int characterId;
        public string characterName;
        public string jobName;
        public string completedSkill;
        public List<Skill> Skills;
    }

    [Serializable]
    public class Skill: CharacterModel
    {
        public int SkillId;
        public string SkillName;
        public string SkillGroup;
        public int SkillLevel;
        public int MaxSkillLevel;
        public string SkillStatus;
    }

    [Serializable]
    public class CharacterSkill
    {
        public int characterId;
        public int skillId;
        public int learningLevel;
    }

    [Serializable]
    public class UpdateName
    {
        public int id;
        public string characterName;
    }
}
