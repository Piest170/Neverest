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
        public int CharacterId;
        public int UserId;
        public int JobId;
        public int CharacterSkillsId;
        public int CharacterQuests;
    }

    [Serializable]
    public class CharacterSkill
    {
        public int characterId;
        public int skillId;
        public int learningLevel;
    }
}
