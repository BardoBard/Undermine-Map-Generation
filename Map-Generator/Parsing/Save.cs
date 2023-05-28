using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Map_Generator.Json;
using Newtonsoft.Json.Linq;

namespace Map_Generator.Parsing;

public static class Save
{
    //????
    private static bool secret_treasure_note { get; set; }

    //guid
    private static Guid discovered_wayland_boots_guid = new("1981b4af04434077afafc78691056387");
    private static bool discovered_wayland_boots { get; set; }
    private static Guid discoveredRatBond_guid = new("3776afb876a74e50911b6d3080f0388d");
    private static bool discoveredRatBond { get; set; }
    private static Guid discoveredGuacamole_guid = new("80242154d4284cc6aab221292cb0ae93");
    private static bool discoveredGuacamole { get; set; }
    private static Guid discoveredHungrySpirit_guid = new("91f466ecbb87497b943eabd77f6e4681");
    private static bool discoveredHungrySpirit { get; set; }
    private static bool foundPartyPopcornPotion { get; set; } //TODO: guid check

    //status effects
    private static bool hexDesolation { get; set; }
    private static bool relicCircinus { get; set; }
    private static bool relicAdventurersWhip { get; set; }

    //game data
    public static bool storyMode { get; set; } //TODO: Check if this is the correct name
    public static bool rougeMode { get; set; } //TODO: Check if this is the correct name
    public static bool bard_met { get; set; }
    public static bool altar_encountered { get; set; }
    public static bool tribute_fountain_encountered { get; set; } //TODO: Check if this is the correct name
    public static int floor_number { get; set; }

    public static bool whip_enabled { get; set; }
    public static int zone_index { get; set; }

    //upgrade string
    private static bool adventurersHat { get; set; }
    private static int apprentice_met { get; set; }
    private static int arkanos_defeated { get; set; }
    private static int arkanos_talk_count { get; set; }
    private static bool black_rabbit_met { get; set; }
    private static int blacksmith_rescued { get; set; }
    private static bool bog_unlocked { get; set; }
    private static int cavern_entered { get; set; }
    private static int cavern_key { get; set; }
    private static int collector_book { get; set; }
    private static int core_key { get; set; }
    private static int core_opened { get; set; }
    private static int crone_unlocked { get; set; }
    private static int crystallord_defeated { get; set; }
    private static int crystallord_revived { get; set; }
    private static int debt { get; set; }
    private static int delve_count { get; set; }
    private static int dibble_discount { get; set; }
    private static int dibble_extra_item { get; set; }
    private static int dibble_relic { get; set; }
    private static int dibble_upgrade_count { get; set; }
    private static int dog_count { get; set; }
    private static bool dog_dillon_found { get; set; }
    private static bool dog_engine_found { get; set; }
    private static bool dog_shadow_found { get; set; }
    private static int dungeon_key { get; set; }
    private static int dungeon_opened { get; set; }
    private static int final_gate_opened { get; set; }
    private static int firelord_defeated { get; set; }
    private static int firelord_revived { get; set; }
    private static int game_over { get; set; }
    private static int geckos_foot { get; set; }
    private static bool gold_keep_percent { get; set; }
    private static int guards_defeated { get; set; }
    private static int halls_key { get; set; }
    private static bool halls_opened { get; set; }
    private static bool hoodie_met { get; set; }
    private static bool hoodie_met_cavern { get; set; }
    private static bool hoodie_met_dungeon { get; set; }
    private static bool hoodie_met_hall { get; set; }
    private static bool hoodie_met_mine { get; set; }
    private static int library_key { get; set; }
    private static int lillyth_final_speech { get; set; }
    private static int map_collected { get; set; }
    private static bool masters_key { get; set; }
    private static int meal_ticket_new { get; set; }
    private static bool mushroom_blue { get; set; }
    private static bool mushroom_green { get; set; }
    private static bool mushroom_purple { get; set; }
    private static int nether_collected { get; set; }
    private static int othermine_unlocked { get; set; }
    private static bool peasant1_unlocked { get; set; }
    private static bool peasant2_unlocked { get; set; }
    private static bool peasant4_unlocked { get; set; }
    private static int peon_count { get; set; }
    private static int play_count { get; set; }
    private static int priestess_met { get; set; }
    private static bool prisoner_key { get; set; }
    private static int retaliation { get; set; }
    private static bool rockmimic_defeated { get; set; }
    private static int sandworm_defeated { get; set; }
    private static int sandworm_revived { get; set; }
    private static int shadowlord_defeated { get; set; }
    private static int shadowlord_revived { get; set; }
    private static int shaker { get; set; }
    private static int shop_basic_item { get; set; }
    private static int shop_food { get; set; }
    private static int shop_loyalty_program { get; set; }
    private static int shop_potion_relic { get; set; }
    private static int shop_transmute_machine { get; set; }
    private static int simple_chest_new { get; set; }
    private static int start_blessing { get; set; }
    private static int statue_defeated { get; set; }
    private static int stonelord_defeated { get; set; }
    private static int summon_count { get; set; }
    private static int talking_gem_count { get; set; }
    private static int tavern_key { get; set; }
    private static int tavern_opened { get; set; }
    private static int theft_blessing { get; set; }
    private static bool tutorial_complete { get; set; }
    private static int woodpigeon_met { get; set; }

    //rooms
    public static bool adventurers_hat = adventurersHat;
    public static bool relicadventurerswhip = relicAdventurersWhip;

    public static bool reliccircinus = relicCircinus;

    //rooms and encounters
    public static bool relicguacamole = discoveredGuacamole;

    //encounters
    public static bool waylandshop = ((!discovered_wayland_boots || !(blacksmith_rescued > 0)) && !whip_enabled);
    public static bool mushroomblue = (!mushroom_blue && apprentice_met > 0 && !whip_enabled && storyMode);
    public static bool mushroompurple = (!mushroom_purple && apprentice_met > 0 && !whip_enabled && storyMode);
    public static bool mushroomgreen = (!mushroom_green && apprentice_met > 0 && !whip_enabled && storyMode);
    public static bool blackrabbitfirst = (!black_rabbit_met && !whip_enabled && storyMode);
    public static bool hoodieminel = (rockmimic_defeated && !hoodie_met_mine && (floor_number == 1) && storyMode);
    public static bool hoodiemineu = (rockmimic_defeated && hoodie_met_mine && (floor_number == 1) && storyMode);
    public static bool hoodiedungeonl = (!hoodie_met_dungeon && (floor_number == 5) && storyMode);
    public static bool hoodiedungeonu = (!hoodie_met_dungeon && (floor_number == 5) && storyMode);
    public static bool hoodiehalll = (!hoodie_met_hall && (floor_number == 11) && storyMode);
    public static bool hoodiehallu = (!hoodie_met_hall && (floor_number == 11) && storyMode);
    public static bool hoodiecavernl = (!hoodie_met_cavern && (floor_number == 16) && storyMode);
    public static bool hoodiecanveru = (!hoodie_met_cavern && (floor_number == 16) && storyMode);
    public static bool nofountain = (storyMode && !tribute_fountain_encountered && bog_unlocked);
    public static bool nohexdesolation = (!hexDesolation);
    public static bool dogshadow = !dog_shadow_found && (delve_count > 5) && !whip_enabled;
    public static bool dogengine = !dog_engine_found && (delve_count > 6) && !whip_enabled;
    public static bool dogdillion = !dog_dillon_found && (delve_count > 7) && !whip_enabled;
    public static bool notmetbard = !bard_met;
    public static bool secretfountain = storyMode && bog_unlocked;
    public static bool treasurehunt = (!secret_treasure_note && !whip_enabled);
    public static bool ratfriendship = (!discoveredRatBond && !whip_enabled);
    public static bool priestessrescued = priestess_met > 2;
    public static bool relicguacamolebug = discoveredGuacamole; //TODO: check this
    public static bool rockmimic = !prisoner_key && !whip_enabled && storyMode;
    public static bool alchemistapprentice0 = !(apprentice_met > 0) && (blacksmith_rescued > 0) && !whip_enabled && storyMode;
    public static bool alchemistapprentice3 = ((apprentice_met == 4) && blacksmith_rescued > 0 && !whip_enabled && storyMode);
    public static bool relicaltar = !altar_encountered && !whip_enabled;
    public static bool blackrabbitmet = black_rabbit_met;
    public static bool dangeroustogo = delve_count > 8;
    public static bool secretshop = peasant2_unlocked && (dibble_upgrade_count < 4) && storyMode;
    public static bool dibblesstoreroom = (!peasant2_unlocked && !whip_enabled && storyMode);
    public static bool dungeonlibrary = (!(collector_book > 0) && !whip_enabled && storyMode);
    public static bool priestessentrance = (!(priestess_met > 0) && !whip_enabled && storyMode);
    public static bool kurtz = (storyMode && (!discoveredHungrySpirit || !peasant4_unlocked));
    public static bool storynotwhip = (!whip_enabled && storyMode);
    public static bool masterskey = priestess_met > 0 && !masters_key && !whip_enabled && storyMode;
    public static bool notwhip = !whip_enabled; //TODO: check this
    public static bool rougemode = !rougeMode; //TODO: check this
    public static bool partypopcornroom = (foundPartyPopcornPotion && !whip_enabled); //TODO: check this
    public static bool halllibrarycombat = (!(collector_book > 0) && !whip_enabled && storyMode);
    public static bool dodsonnotrescued = (!peasant1_unlocked && !whip_enabled && storyMode);

    //zondata:
    public static bool tutorialincomplete = !tutorial_complete;

    public static bool tutorialcomplete = (!rockmimic_defeated &&
                                    !(sandworm_defeated > 0) &&
                                    !(stonelord_defeated > 0) &&
                                    !(shadowlord_defeated > 0));

    public static bool mimickilled = (rockmimic_defeated &&
                               !(sandworm_defeated > 0) &&
                               !(stonelord_defeated > 0) &&
                               !(shadowlord_defeated > 0));

    public static bool minesandwormkilled = (sandworm_defeated > 0) &&
                                     !(stonelord_defeated > 0) &&
                                     !(shadowlord_defeated > 0);

    public static bool minestonelordkilled = stonelord_defeated > 0 &&
                                      !(shadowlord_defeated > 0);

    public static bool mineshadowlordkilled = shadowlord_defeated > 0;

    public static bool allbossesalive = !(sandworm_defeated > 0) &&
                                 !(stonelord_defeated > 0) &&
                                 !(shadowlord_defeated > 0) &&
                                 !(crystallord_defeated > 0);

    public static bool sandwormkilled = (sandworm_defeated > 0) &&
                                 !(stonelord_defeated > 0) &&
                                 !(shadowlord_defeated > 0) &&
                                 !(crystallord_defeated > 0);

    public static bool stonelordkilled = (stonelord_defeated > 0 &&
                                   !(shadowlord_defeated > 0) &&
                                   !(crystallord_defeated > 0));

    public static bool crystallordkilled = (crystallord_defeated > 0);

    public static bool stonelordnotkilled = (!(stonelord_defeated > 0) &&
                                      !(shadowlord_defeated > 0) &&
                                      !(crystallord_defeated > 0));

    public static bool shadowlordkilled = (shadowlord_defeated > 0 &&
                                    !(crystallord_defeated > 0));

    public static bool shadowlordnotkilled = (!(shadowlord_defeated > 0) &&
                                       !(crystallord_defeated > 0));

    public static bool crystallordnotkilled = (!(crystallord_defeated > 0) &&
                                        !(firelord_defeated > 0));

    public static bool firelordkilled = (firelord_defeated > 0 &&
                                  !bog_unlocked); //TODO: check this bog (enterBog)

    public static bool enterbog = bog_unlocked; //TODO: check this bog (enterBog)

    public static bool crystallordKilledNotFire = (crystallord_defeated > 0 &&
                                            !(firelord_defeated > 0) &&
                                            !bog_unlocked); //TODO: check this bog (enterBog)

    public static void Initialize(String saveString)
    {
        var path = JsonDecoder.UnderminePath + @"\Saves\" + saveString;
        var json = File.ReadAllText(path);
        var jsonObject = JObject.Parse(json);
        ParseUpgradeString((string)jsonObject["upgradeString"]);
    }

    public static bool Check(string requirement)
    {
        return (bool)typeof(Save).GetProperty(requirement).GetValue(typeof(Save), null);
    }

    public static int IncrementFloorNumber(int n)
    {
        return n + (n - 1) / 5;
    }

    private static bool ParseUpgradeString(string input)
    {
        string[] keyValuePairs = input.Split(',');

        foreach (string pair in keyValuePairs)
        {
            if (pair == "")
                continue;
            string[] parts = pair.Split(':');
            string key = parts[0];
            string value = parts[1];

            //if key is in class set its value
            foreach (var property in typeof(Save).GetProperties())
            {
                if (property.Name == key)
                {
                    typeof(Save).GetProperty(key)?.SetValue(typeof(Save), Convert.ChangeType(int.Parse(value), property.PropertyType));
                    break;
                }
            }
        }

        return true;
    }
}