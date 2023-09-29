using Map_Generator.Parsing.Json.Enums;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.Classes
{
    public class Enemy
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("difficulty")] public int Difficulty { get; set; }
        [JsonProperty("rougedifficulty")] public int RougeDifficulty { get; set; }
        [JsonProperty("canbesolo")] public bool CanBeSolo { get; set; }
        [JsonProperty("max")] public int Max { get; set; }
        [JsonProperty("type")] public int Type { get; set; }
        [JsonProperty("icon")] public EnemyIcon EnemyIcon { get; set; }
        [JsonProperty("health")] public HealthExt? Health { get; set; }

        public int GetDifficulty() => Save.storymode ? Difficulty : RougeDifficulty;
        public static Enemy GetEnemy(string name) => JsonDecoder.Enemies[name];

        public class HealthExt
        {
            public float CalculateHealth()
            {
                return Health;
                float health = Health;
                if (Save.notwhip)
                {
                    if (Save.Check(Requirement))
                    {
                        health *= Save.summon_count * HPScaleFactor;
                    }

                    if (Save.Check(HexUpgrade))
                    {
                        if (HexHPScaleFactor > 0)
                            health *= HexHPScaleFactor - 1;
                    }
                }

                return health;
            }

            public float CalculateDamage()
            {
                float dmg = 0f;
                if (Save.notwhip)
                {
                    if (Save.Check(Requirement))
                    {
                        dmg *= Save.summon_count * DamageScaleFactor;
                    }

                    if (Save.Check(HexUpgrade))
                    {
                        if (HexDamageScaleFactor > 0)
                            dmg *= HexDamageScaleFactor - 1;
                    }
                }

                return dmg;
            }


            [JsonProperty("hp")] public int Health { get; set; }
            [JsonProperty("hptier")] public int HPTier { get; set; }
            [JsonProperty("requirement")] public string Requirement { get; set; } = "summon_count_check";
            [JsonProperty("hpscalefactor")] public float HPScaleFactor { get; set; } = 0.15f;
            [JsonProperty("damagescalefactor")] public float DamageScaleFactor { get; set; } = 0.15f;

            [JsonProperty("roguemodehpscalefactor")]
            public int RogueModeHPScaleFactor { get; set; } = 1;

            [JsonProperty("roguemodedamagescalefactor")]
            public int RogueModeDamageScaleFactor { get; set; } = 1;

            [JsonProperty("hexupgrade")] public string HexUpgrade { get; set; } = "dreadful_fog";
            [JsonProperty("hexhpscalefactor")] public int HexHPScaleFactor { get; set; }
            [JsonProperty("hexdamagescalefactor")] public int HexDamageScaleFactor { get; set; }
        }
    }
}