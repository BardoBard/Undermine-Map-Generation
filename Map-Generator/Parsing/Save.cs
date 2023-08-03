using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Map_Generator.Json;
using Map_Generator.Parsing.Json.Classes;
using Newtonsoft.Json.Linq;

namespace Map_Generator.Parsing;

public class Discoverable
{
    public bool hasBeenDiscovered { get; set; }
    public Guid guid { get; set; }

    public Discoverable(bool hasBeenDiscovered, Guid guid)
    {
        this.hasBeenDiscovered = hasBeenDiscovered;
        this.guid = guid;
    }
}

public static class Save
{
    //TODO: make a lot of stuff private instead of public

    //guid
    private static readonly Guid TitleScreenGuid = new("219d813ae07049b39d1bf35f1863c2b1");

    public static Discoverable discovered_wayland_boots { get; set; } =
        new(false, new("1981b4af04434077afafc78691056387"));

    public static Discoverable discoveredRatBond { get; set; } = new(false, new("3776afb876a74e50911b6d3080f0388d"));

    public static Discoverable discoveredHungrySpirit { get; set; } =
        new(false, new("91f466ecbb87497b943eabd77f6e4681"));

    public static Discoverable foundPartyPopcornPotion { get; set; } =
        new(false, new("0f00680feaf340518a0185d4cf9038e4"));

    //status effects
    public static Discoverable hexDesolation { get; set; } = new(false, new("aaf06aa35fa4482cabfba81e687bfef4"));
    public static Discoverable relicCircinus { get; set; } = new(false, new("633c1aeec47b4378a56f48ea7d965ea0"));

    public static Discoverable relicAdventurersWhip { get; set; } =
        new(false, new("0e7e31f44491456bb5f6268f2c2686c2"));

    public static Discoverable relicGuacamole { get; set; } = new(false, new("80242154d4284cc6aab221292cb0ae93"));


    //game data
    public static bool storymode { get; set; } = true;

    public static int Seed { get; set; }
    public static Guid Zone { get; set; }
    public static bool roguemode { get; set; } //TODO: Check if this is the correct name
    public static bool bard_met { get; set; }
    public static bool altar_encountered { get; set; }
    public static bool tribute_fountain_encountered { get; set; } //TODO: Check if this is the correct name
    public static int floor_number { get; private set; }
    public static int zone_index { get; set; }

    public static int FloorNumber
    {
        get { return zone_index + 1; }
    }

    public static int FloorIndex
    {
        get { return zone_index; }
    }

    public static int ZoneIndex
    {
        get { return floor_number / 5; }
    }


    public static bool whip_enabled { get; set; }

    //upgrade string
    public static bool secret_treasure_note { get; set; }
    public static int apprentice_met { get; set; }
    public static int arkanos_defeated { get; set; }
    public static int arkanos_talk_count { get; set; }
    public static bool black_rabbit_met { get; set; }
    public static bool blacksmith_rescued { get; set; }
    public static bool bog_unlocked { get; set; }
    public static int cavern_entered { get; set; }
    public static int cavern_key { get; set; }
    public static int collector_book { get; set; }
    public static int core_key { get; set; }
    public static int core_opened { get; set; }
    public static int crone_unlocked { get; set; }
    public static bool crystallord_defeated { get; set; }
    public static int crystallord_revived { get; set; }
    public static int debt { get; set; }
    public static int delve_count { get; set; }
    public static int dibble_discount { get; set; }
    public static int dibble_extra_item { get; set; }
    public static int dibble_relic { get; set; }
    public static int dibble_upgrade_count { get; set; }
    public static int dog_count { get; set; }
    public static bool dog_dillon_found { get; set; }
    public static bool dog_engine_found { get; set; }
    public static bool dog_shadow_found { get; set; }
    public static int dungeon_key { get; set; }
    public static int dungeon_opened { get; set; }
    public static int final_gate_opened { get; set; }
    public static bool firelord_defeated { get; set; }
    public static int firelord_revived { get; set; }
    public static int game_over { get; set; }
    public static int geckos_foot { get; set; }
    public static bool gold_keep_percent { get; set; }
    public static int guards_defeated { get; set; }
    public static int halls_key { get; set; }
    public static bool halls_opened { get; set; }
    public static bool hoodie_met { get; set; }
    public static bool hoodie_met_cavern { get; set; }
    public static bool hoodie_met_dungeon { get; set; } = false;
    public static bool hoodie_met_hall { get; set; }
    public static bool hoodie_met_mine { get; set; }
    public static int library_key { get; set; }
    public static int lillyth_final_speech { get; set; }
    public static int map_collected { get; set; }
    public static bool masters_key { get; set; }
    public static int meal_ticket_new { get; set; }
    public static bool mushroom_blue { get; set; }
    public static bool mushroom_green { get; set; }
    public static bool mushroom_purple { get; set; }
    public static int nether_collected { get; set; }
    public static int othermine_unlocked { get; set; }
    public static bool peasant1_unlocked { get; set; }
    public static bool peasant2_unlocked { get; set; }
    public static bool peasant4_unlocked { get; set; }
    public static int peon_count { get; set; }
    public static int play_count { get; set; }
    public static int priestess_met { get; set; }
    public static bool prisoner_key { get; set; }
    public static int retaliation { get; set; }
    public static bool rockmimic_defeated { get; set; }
    public static bool sandworm_defeated { get; set; }
    public static int sandworm_revived { get; set; }
    public static bool shadowlord_defeated { get; set; }
    public static int shadowlord_revived { get; set; }
    public static int shaker { get; set; }
    public static int shop_basic_item { get; set; }
    public static int shop_food { get; set; }
    public static int shop_loyalty_program { get; set; }
    public static int shop_potion_relic { get; set; }
    public static int shop_transmute_machine { get; set; }
    public static int simple_chest_new { get; set; }
    public static int start_blessing { get; set; }
    public static int statue_defeated { get; set; }
    public static bool stonelord_defeated { get; set; }
    public static int summon_count { get; set; }
    public static int talking_gem_count { get; set; }
    public static int tavern_key { get; set; }
    public static int tavern_opened { get; set; }
    public static int theft_blessing { get; set; }
    public static bool tutorial_complete { get; set; }
    public static int woodpigeon_met { get; set; }

    //rooms
    public static bool adventurers_hat { get; set; }
    public static bool relicadventurerswhip => relicAdventurersWhip.hasBeenDiscovered;

    public static bool reliccircinus => relicCircinus.hasBeenDiscovered;

    //rooms and encounters
    public static bool relicguacamole => relicGuacamole.hasBeenDiscovered;

    //encounters
    public static bool waylandshop =>
        ((!discovered_wayland_boots.hasBeenDiscovered || !blacksmith_rescued) && !whip_enabled);

    public static bool mushroomblue => (!mushroom_blue && apprentice_met > 0 && !whip_enabled && storymode);
    public static bool mushroompurple => (!mushroom_purple && apprentice_met > 0 && !whip_enabled && storymode);
    public static bool mushroomgreen => (!mushroom_green && apprentice_met > 0 && !whip_enabled && storymode);
    public static bool blackrabbitfirst => (!black_rabbit_met && !whip_enabled && storymode);
    public static bool hoodieminel => (rockmimic_defeated && !hoodie_met_mine && (floor_number == 1) && storymode);
    public static bool hoodiemineu => (rockmimic_defeated && hoodie_met_mine && (floor_number == 1) && storymode);
    public static bool hoodiedungeonl => (!hoodie_met_dungeon && (floor_number == 6) && storymode);
    public static bool hoodiedungeonu => (hoodie_met_dungeon && (floor_number == 6) && storymode);
    public static bool hoodiehalll => (!hoodie_met_hall && (floor_number == 11) && storymode);
    public static bool hoodiehallu => (!hoodie_met_hall && (floor_number == 11) && storymode);
    public static bool hoodiecavernl => (!hoodie_met_cavern && (floor_number == 16) && storymode);
    public static bool hoodiecavernu => (!hoodie_met_cavern && (floor_number == 16) && storymode);
    public static bool nofountain => (storymode && !tribute_fountain_encountered && bog_unlocked);
    public static bool nohexdesolation => (!hexDesolation.hasBeenDiscovered);
    public static bool dogshadow => !dog_shadow_found && (delve_count > 5) && !whip_enabled;
    public static bool dogengine => !dog_engine_found && (delve_count > 6) && !whip_enabled;
    public static bool dogdillion => !dog_dillon_found && (delve_count > 7) && !whip_enabled;
    public static bool notbardmet => !bard_met;
    public static bool secretfountain => storymode && bog_unlocked;
    public static bool treasurehunt => (!secret_treasure_note && !whip_enabled);
    public static bool ratfriendship => (!discoveredRatBond.hasBeenDiscovered && !whip_enabled);
    public static bool priestessrescued => priestess_met > 2;
    public static bool relicguacamolebug => relicGuacamole.hasBeenDiscovered; //TODO: check this
    public static bool rockmimic => !prisoner_key && !whip_enabled && storymode;

    public static bool alchemistapprentice0 =>
        !(apprentice_met > 0) && blacksmith_rescued && !whip_enabled && storymode;

    public static bool alchemistapprentice3 =>
        ((apprentice_met == 4) && blacksmith_rescued && !whip_enabled && storymode);

    public static bool relicaltar => !altar_encountered && !whip_enabled;
    public static bool blackrabbitmet => black_rabbit_met;
    public static bool dangeroustogo => delve_count > 8;
    public static bool secretshop => peasant2_unlocked && (dibble_upgrade_count < 4) && storymode;
    public static bool dibblesstoreroom => (!peasant2_unlocked && !whip_enabled && storymode);
    public static bool dungeonlibrary => (!(collector_book > 0) && !whip_enabled && storymode);
    public static bool priestessentrance => (!(priestess_met > 0) && !whip_enabled && storymode);
    public static bool kurtz => (storymode && (!discoveredHungrySpirit.hasBeenDiscovered || !peasant4_unlocked));
    public static bool storynotwhip => (!whip_enabled && storymode);
    public static bool masterskey => priestess_met > 0 && !masters_key && !whip_enabled && storymode;
    public static bool notwhip => !whip_enabled; //TODO: check this
    public static bool rougemode => !roguemode; //TODO: check this

    public static bool partypopcornroom =>
        (foundPartyPopcornPotion.hasBeenDiscovered && !whip_enabled); //TODO: check this

    public static bool halllibrarycombat => (!(collector_book > 0) && !whip_enabled && storymode);
    public static bool dodsonnotrescued => (!peasant1_unlocked && !whip_enabled && storymode);

    //zondata:
    public static bool treasurehuntx => secret_treasure_note;
    public static bool tutorialincomplete => !tutorial_complete;
    public static bool firstdelve => delve_count <= 1;

    public static bool tutorialcomplete => !rockmimic_defeated &&
                                           !(sandworm_defeated) &&
                                           !(stonelord_defeated) &&
                                           !(shadowlord_defeated);

    public static bool mimickilled => (rockmimic_defeated &&
                                       !(sandworm_defeated) &&
                                       !(stonelord_defeated) &&
                                       !(shadowlord_defeated));

    public static bool minesandwormkilled => (sandworm_defeated) &&
                                             !(stonelord_defeated) &&
                                             !(shadowlord_defeated);

    public static bool minestonelordkilled => stonelord_defeated &&
                                              !(shadowlord_defeated);

    public static bool mineshadowlordkilled => shadowlord_defeated;

    public static bool allbossesalive => !(sandworm_defeated) &&
                                         !(stonelord_defeated) &&
                                         !(shadowlord_defeated);
    // && !(crystallord_defeated);

    public static bool sandwormkilled => (sandworm_defeated) &&
                                         !(stonelord_defeated) &&
                                         !(shadowlord_defeated);
    // && !(crystallord_defeated);

    public static bool stonelordkilled => stonelord_defeated && !(shadowlord_defeated);
    // && !(crystallord_defeated);

    public static bool crystallordkilled => (crystallord_defeated);

    public static bool stonelordnotkilled => !(stonelord_defeated) &&
                                             !(shadowlord_defeated);
    // && !(crystallord_defeated);

    public static bool shadowlordkilled => (shadowlord_defeated);
    // && !(crystallord_defeated));

    public static bool shadowlordnotkilled => !(shadowlord_defeated);
    // && !(crystallord_defeated);

    public static bool crystallordnotkilled => (!(crystallord_defeated) &&
                                                !(firelord_defeated));

    public static bool firelordkilled => firelord_defeated;
    // && !bog_unlocked; //TODO: check this bog (enterBog)

    public static bool enterbog => bog_unlocked; //TODO: check this bog (enterBog)

    public static bool crystallordkillednotfire => crystallord_defeated &&
                                                   !(firelord_defeated);

    public static string GetZoneName(RoomType room) =>
        room.IsHidden && Save.FloorNumber == 4 ? MapType.GetNextMapName() : MapType.GetMapName();
    // && !bog_unlocked; //TODO: check this bog (enterBog)

    public static void Initialize(string saveJsonFile)
    {
        var path = saveJsonFile;
        var json = File.ReadAllText(path);
        var jsonObject = JObject.Parse(json);
        ParseUpgradeString(jsonObject["upgradeString"]);
        ParseAutoSaveData(jsonObject["autoSaveData"]);
        ParseDiscovered(jsonObject["discovered"]);
    }

    public static bool Check(string requirement)
    {
        bool result = (bool)(typeof(Save).GetProperty(requirement)?.GetValue(typeof(Save), null) ??
                             throw new InvalidOperationException($"couldn't get value of {requirement}"));
        return result;
    }

    public static void IncrementFloorNumber() => floor_number += FloorNumber == 4 ? 2 : 1;

    private static bool ParseUpgradeString(JToken? upgradeToken)
    {
        string upgradeString = (string)upgradeToken! ?? throw new Exception("upgradeString not found");

        string[] keyValuePairs = upgradeString.Split(',');

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
                if (property.Name != key) continue;

                typeof(Save).GetProperty(key)?.SetValue(typeof(Save),
                    Convert.ChangeType(int.Parse(value), property.PropertyType));

                break;
            }
        }

        return true;
    }

    private static void ParseAutoSaveData(JToken? input)
    {
        Seed = (int)(input?["seed"] ?? throw new Exception("Seed not found"));
        Zone = Guid.TryParse((string)input["zone"], out var zoneGuid) ? zoneGuid : TitleScreenGuid;

        List<string?> guidStrings = (input["statusEffects"] ?? throw new ArgumentNullException(nameof(input)))
            .Select(token => token["id"].Value<string>()).ToList();
        foreach (string? guidString in guidStrings)
        {
            foreach (var property in typeof(Save).GetProperties())
            {
                if (property.PropertyType != typeof(Discoverable)) continue;

                if (typeof(Save).GetProperty(property.Name)?.GetValue(typeof(Discoverable)) is not Discoverable
                        discoverable || discoverable.guid != Guid.Parse(guidString ??
                                                                        throw new InvalidOperationException(
                                                                            "something went wrong while parsing discovered")))
                    continue;
                discoverable.hasBeenDiscovered = true;
            }
        }
    }

    private static void ParseDiscovered(JToken? input)
    {
        List<string?> guidStrings = (input ?? throw new ArgumentNullException(nameof(input)))
            .Select(token => token.Value<string>()).ToList();
        foreach (string? guidString in guidStrings)
        {
            foreach (var property in typeof(Save).GetProperties())
            {
                if (property.PropertyType != typeof(Discoverable)) continue;

                if (typeof(Save).GetProperty(property.Name)?.GetValue(typeof(Discoverable)) is not Discoverable
                        discoverable || discoverable.guid != Guid.Parse(guidString ??
                                                                        throw new InvalidOperationException(
                                                                            "something went wrong while parsing discovered")))
                    continue;
                discoverable.hasBeenDiscovered = true;
            }
        }
    }
}