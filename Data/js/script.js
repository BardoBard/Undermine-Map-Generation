"use strict";
function e$(name, parent = document) {
    switch (name.charAt(0)) {
        case ".":
            return parent.getElementsByClassName(name.slice(1));
        default:
            return parent.getElementById(name);
    }
}
function e$c(type, options) {
    if (options) {
        return Object.assign(document.createElement(type), options);
    }
    else {
        return document.createElement(type);
    }
}
function loop(func, loops) {
    for (let i = 0; i < loops; ++i) {
        func();
    }
}
function rangeInclusive2(min, max) {
    var x = range(min, max + 1);
    console.log(x);
return x;
}
function nextUInt() {
    const firstSeed = toUInt32(1), secondSeed = nextSeed(firstSeed), thirdSeed = nextSeed(secondSeed), fourthSeed = nextSeed(thirdSeed);
    const seed = [
        firstSeed,
        secondSeed,
        thirdSeed,
        fourthSeed
    ];
    let x = seed.shift();
    let y = seed[2];
    x ^= x << 11;
    x ^= x >>> 8;
    y ^= y >>> 19;
    y = toUInt32(y ^ x);
    seed.push(y);
    console.log(y);
    console.log("output");
    return y;
}
function range2(min = 0, max = 99999999) {
    if (max < min) {
        [min, max] = [max, min];
    }
    return 3337875633 % (max - min) + min;
}
const nextSeed = (seed) => toUInt32(Math.imul(1812433253, seed) + 1);
function toUInt32(floatVar) { return floatVar >>> 0; }
class Random {
    constructor(initSeed = 51113926) {
        const nextSeed = (seed) => toUInt32(Math.imul(1812433253, seed) + 1);
        const firstSeed = toUInt32(initSeed), secondSeed = nextSeed(firstSeed), thirdSeed = nextSeed(secondSeed), fourthSeed = nextSeed(thirdSeed);
        this.seed = [
            firstSeed,
            secondSeed,
            thirdSeed,
            fourthSeed
        ];
    }
    get state() {
        return [...this.seed];
    }
    set state(oldState) {
        this.seed = [...oldState];
    }
    get nextInt() {
        return this.nextUInt % 0x7FFFFFFF;
    }
    get next() {
        return this.nextInt;
    }
    get value() {
        return 1 - this.rangeFloat();
    }
    get nextUInt() {
        let x = this.seed.shift();
        console.log("x")
        console.log(x)
        let y = this.seed[2];
        x ^= x << 11;
        x ^= x >>> 8;
        y ^= y >>> 19;
        y = toUInt32(y ^ x);
        this.seed.push(y);
        return y;
    }
    getWeight(table) {
        return table.reduce((totalWeight, currentItem) => totalWeight + currentItem.weight, 0);
    }
    getItem(table, masterTable, randomNum) {
        return masterTable.relic[(table.find((currentItem) => ((randomNum -= currentItem.weight) <= 0))).masterIndex].display;
    }
    range(min = 0, max = 99999999) {
        if (max < min) {
            [min, max] = [max, min];
        }
        var z = this.nextUInt;
        console.log("max: ")
        console.log(max)
        console.log("nextuint: ")
        console.log(z)
        return z % (max - min) + min;
    }
    rangeFloat(min = 0, max = 1) {
        if (max < min) {
            [min, max] = [max, min];
        }
        return (max - min) * (1 - (toUInt32(this.nextUInt << 9) / 0xFFFFFFFF)) + min;
    }
    rangeInclusive(min, max) {
        var x = this.range(min, max + 1);   
        console.log("range: ")
        console.log(x)
    return x;
    }
    getWeightedElement(table) {
        let output = 0;
        let o = 0;
        const totalWeight = table.reduce((totalWeight, currentItem) => totalWeight + currentItem.weight, 0);
        table.reduce((currentItem, index) => console.log(currentItem), 0);
        console.log("totalWeight: ");
        console.log(totalWeight);
        let randWeight = this.rangeInclusive(1, totalWeight);
        let randWeightt = randWeight
        console.log(randWeight)
        try {
            i = 0;
            table.forEach((currentItem, index) => {
                i++
                randWeightt -= currentItem.weight
                if (randWeight <= 0){
                console.log(i + " i ")
                }
                
                
            });
            output = table.find((currentItem) => ((randWeight -= currentItem.weight) <= 0)).masterIndex;
            o = table.find((currentItem) => ((randWeight -= currentItem.weight) <= 0));
        }
        catch {
            console.log("yes")
            output = 156;
        }

        console.log(o)      
        console.log("out")
        console.log(totalWeight)
        console.log(randWeight)

        return output;
    }
    loot(table) {
        return this.getWeightedElement(table);
    }
    getWeightedTable(table) {
        const totalWeight = table.reduce((totalWeight, currentItem) => totalWeight + currentItem.weight, 0);
        let randWeight = this.rangeInclusive(1, totalWeight);
        return table.find((currentItem) => ((randWeight -= currentItem.weight) <= 0));
    }
    arrayPick(array) {
        const totalWeight = array.reduce((total, current) => total + current, 0);
        let randWeight = this.rangeInclusive(1, totalWeight);
        return array.findIndex(current => ((randWeight -= current) <= 0));
    }
    shuffle(list) {
        let workingList = [...list];
        let i = workingList.length;
        while (i > 1) {
            let index = this.range(0, i--);
            let value = workingList[index];
            workingList[index] = workingList[i];
            workingList[i] = value;
        }
        return workingList;
    }
    chance(chance) {
        return chance == 1 || (chance && chance > this.value);
    }
}
const lootTables = {
    relic: [],
    relicAll: [],
    relicShop: [],
    relicFinal: [],
    relicStarter: [],
    relicLegendary: [],
    potion: [],
    potionShop: [],
    potionDibble: [],
    potionFinal: [],
    food: [],
    item: [],
    itemShop: [],
    itemDibble: [],
    dibble: [],
    dibbleRelic: [],
    potionOrRelic: []
};
const rand = {
    default: null,
    layout: null,
    relic: null,
    blessing: null,
    blueprint: null,
    basicItem: null,
    potion: null,
    health: null,
    resource: null,
    shopRelic: null,
    shopPotion: null,
    shopHealth: null,
    shopBasicItem: null,
    Legendary: null,
    dibble: null,
    misc: null,
    shop: null,
    majorCurse: null,
    minorCurse: null,
    familiar: null,
    upgrade: null,
    chaos: null,
    table: null
};
let settings = {
    checkboxes: {
        relic: null,
        potion: null
    },
    altarRelic: -1,
    flags: {
        altar_encountered: 0,
        apprentice_met: 0,
        bard_met: 0,
        black_rabbit_met: 0,
        blacksmith_rescued: 0,
        bog_unlocked: 0,
        bog_visited: 0,
        collector_book: 0,
        crystallord_defeated: 0,
        debt: 0,
        delve_count: 0,
        dibble_discount: 0,
        dibble_extra_item: 0,
        dibble_relic: 0,
        dibble_upgrade_count: 0,
        dog_dillon_found: 0,
        dog_engine_found: 0,
        dog_shadow_found: 0,
        firelord_defeated: 0,
        floor_number: 0,
        guacamole: 0,
        hoodie_met: 0,
        hoodie_met_cavern: 0,
        hoodie_met_dungeon: 0,
        hoodie_met_hall: 0,
        hoodie_met_mine: 0,
        masters_key: 0,
        mushroom_blue: 0,
        mushroom_green: 0,
        mushroom_purple: 0,
        peasant1_unlocked: 0,
        peasant2_unlocked: 0,
        peasant4_unlocked: 0,
        priestess_met: 0,
        prisoner_key: 0,
        rockmimic_defeated: 0,
        sandworm_defeated: 0,
        secret_treasure_note: 0,
        shadowlord_defeated: 0,
        shop_basic_item: 1,
        shop_food: 1,
        shop_item_count: 0,
        shop_potion_relic: 1,
        stonelord_defeated: 0,
        tribute_fountain_encountered: 0,
        tutorial_complete: 0,
        whip_enabled: 0,
        storyMode: 0,
        rougeMode: 0,
        hexDesolation: 0,
        discoveredRatBond: 0,
        discoveredWaylandBoots: 0,
        discoveredHungrySpirit: 0,
        hasMaxGoldIntegrity: 0,
        relicWhip: 0,
        relicHat: 0,
        relicCircinus: 0,
    }
};
const seedRand = new Random(Date.now());
function loadSave(radio, file) {
    const settingsOut = {
        checkboxes: {
            relic: null,
            potion: null
        },
        altarRelic: -1,
        flags: {
            altar_encountered: 0,
            apprentice_met: 0,
            bard_met: 0,
            black_rabbit_met: 0,
            blacksmith_rescued: 0,
            bog_unlocked: 0,
            bog_visited: 0,
            collector_book: 0,
            crystallord_defeated: 0,
            debt: 0,
            delve_count: 0,
            dibble_discount: 0,
            dibble_extra_item: 0,
            dibble_relic: 0,
            dibble_upgrade_count: 0,
            dog_dillon_found: 0,
            dog_engine_found: 0,
            dog_shadow_found: 0,
            firelord_defeated: 0,
            floor_number: 0,
            guacamole: 0,
            hoodie_met: 0,
            hoodie_met_cavern: 0,
            hoodie_met_dungeon: 0,
            hoodie_met_hall: 0,
            hoodie_met_mine: 0,
            masters_key: 0,
            mushroom_blue: 0,
            mushroom_green: 0,
            mushroom_purple: 0,
            peasant1_unlocked: 0,
            peasant2_unlocked: 0,
            peasant4_unlocked: 0,
            priestess_met: 0,
            prisoner_key: 0,
            rockmimic_defeated: 0,
            sandworm_defeated: 0,
            secret_treasure_note: 0,
            shadowlord_defeated: 0,
            shop_basic_item: 1,
            shop_food: 1,
            shop_item_count: 0,
            shop_potion_relic: 1,
            stonelord_defeated: 0,
            tribute_fountain_encountered: 0,
            tutorial_complete: 0,
            whip_enabled: 0,
            storyMode: 0,
            rougeMode: 0,
            hexDesolation: 0,
            discoveredRatBond: 0,
            discoveredWaylandBoots: 0,
            discoveredHungrySpirit: 0,
            hasMaxGoldIntegrity: 0,
            unlockedKurtzStache: 0,
            relicWhip: 0,
            relicHat: 0,
            relicCircinus: 0,
        }
    };
    const CBRelic = Object.values(e$("relic-selection").getElementsByTagName("input"));
    const CBPotion = Object.values(e$("potion-selection").getElementsByTagName("input"));
    switch (radio.value) {
        case "0":
            break;
        case "100":
            settingsOut.checkboxes.relic = CBRelic;
            settingsOut.checkboxes.potion = CBPotion;
            settingsOut.altarRelic = 149;
            settingsOut.flags.apprentice_met = 7;
            settingsOut.flags.black_rabbit_met = 1;
            settingsOut.flags.blacksmith_rescued = 3;
            settingsOut.flags.bog_unlocked = 1;
            settingsOut.flags.bog_visited = 1;
            settingsOut.flags.collector_book = 3;
            settingsOut.flags.crystallord_defeated = 1;
            settingsOut.flags.debt = 0;
            settingsOut.flags.delve_count = 99;
            settingsOut.flags.dibble_discount = 2;
            settingsOut.flags.dibble_extra_item = 1;
            settingsOut.flags.dibble_relic = 1;
            settingsOut.flags.dibble_upgrade_count = 4;
            settingsOut.flags.dog_dillon_found = 1;
            settingsOut.flags.dog_engine_found = 1;
            settingsOut.flags.dog_shadow_found = 1;
            settingsOut.flags.firelord_defeated = 1;
            settingsOut.flags.floor_number = 0;
            settingsOut.flags.hoodie_met = 1;
            settingsOut.flags.hoodie_met_cavern = 1;
            settingsOut.flags.hoodie_met_dungeon = 1;
            settingsOut.flags.hoodie_met_hall = 1;
            settingsOut.flags.hoodie_met_mine = 1;
            settingsOut.flags.masters_key = 1;
            settingsOut.flags.mushroom_blue = 1;
            settingsOut.flags.mushroom_green = 1;
            settingsOut.flags.mushroom_purple = 1;
            settingsOut.flags.peasant1_unlocked = 1;
            settingsOut.flags.peasant2_unlocked = 1;
            settingsOut.flags.peasant4_unlocked = 1;
            settingsOut.flags.priestess_met = 3;
            settingsOut.flags.prisoner_key = 1;
            settingsOut.flags.rockmimic_defeated = 1;
            settingsOut.flags.sandworm_defeated = 1;
            settingsOut.flags.secret_treasure_note = 0;
            settingsOut.flags.shadowlord_defeated = 1;
            settingsOut.flags.shop_basic_item = 2;
            settingsOut.flags.shop_food = 3;
            settingsOut.flags.shop_item_count = 8;
            settingsOut.flags.stonelord_defeated = 1;
            settingsOut.flags.tutorial_complete = 1;
            settingsOut.flags.storyMode = 1;
            settingsOut.flags.rougeMode = 0;
            settingsOut.flags.hexDesolation = 0;
            settingsOut.flags.discoveredRatBond = 1;
            settingsOut.flags.discoveredWaylandBoots = 1;
            settingsOut.flags.discoveredHungrySpirit = 1;
            settingsOut.flags.hasMaxGoldIntegrity = 1;
            settingsOut.flags.unlockedKurtzStache = 1;
            settingsOut.flags.relicWhip = 0;
            settingsOut.flags.relicHat = 0;
            settingsOut.flags.relicCircinus = 0;
            break;
        case "own":
            const saveData = JSON.parse(file);
            settingsOut.checkboxes.relic = saveData.unlocked.flatMap(craftedGUID => CBRelic.find(box => parseInt(box.value) === masterTable.relic.findIndex(relic => relic.guid === craftedGUID)) ?? []);
            settingsOut.checkboxes.potion = saveData.unlocked.flatMap(craftedGUID => CBPotion.find(box => parseInt(box.value) === masterTable.potion.findIndex(potion => potion.guid === craftedGUID)) ?? []);
            settingsOut.altarRelic = masterTable.relic.findIndex(relic => relic.guid === saveData.altarItemID);
            saveData.upgradeString.split(",").forEach(item => {
                const [key, value] = item.split(":");
                settingsOut.flags[key] = parseInt(value);
            });
            settingsOut.flags.hasMaxGoldIntegrity = settingsOut.flags["gold_keep_percent"] === 95;
            settingsOut.flags.unlockedKurtzStache = saveData.runHistory.length >= 4;
            break;
    }
    return settingsOut;
}
function applySettings(settings) {
    checkAll(e$("relic-selection").getElementsByTagName("input"), false);
    if (settings.checkboxes.relic) {
        checkAll(settings.checkboxes.relic, true);
    }
    checkAll(e$("potion-selection").getElementsByTagName("input"), false);
    if (settings.checkboxes.potion) {
        checkAll(settings.checkboxes.potion, true);
    }
    e$("altar").value = settings.altarRelic;
}
;
const masterTable = {
    relic: [
        {
            guid: "1981b4af04434077afafc78691056387",
            name: "WaylandsBoots",
            display: "Wayland's Boots",
            details: "Break spikes",
            rarity: "Common",
            crafting: 8,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 5,
                    weight: 9
                },
                relicAll: {
                    index: 3,
                    weight: 9
                }
            }
        },
        {
            guid: "25a2fe2d57a34b6598d34cbbb58fdeb0",
            name: "Galoshes",
            display: "Galoshes",
            details: "Walk and jump on oil and poison",
            rarity: "Common",
            crafting: 0,
            cost: 275,
            extra: "",
            tables: {
                relic: {
                    index: 6,
                    weight: 9
                },
                relicAll: {
                    index: 56,
                    weight: 9
                },
                relicShop: {
                    index: 31,
                    weight: 9
                }
            }
        },
        {
            guid: "d2b0fec2b4cf44e8aa63b0aa94441fd5",
            name: "FloatBoots",
            display: "Float Boots",
            details: "Walk on air",
            rarity: "Common",
            crafting: 0,
            cost: 450,
            extra: "",
            tables: {
                relic: {
                    index: 41,
                    weight: 9
                },
                relicAll: {
                    index: 54,
                    weight: 9
                },
                relicShop: {
                    index: 15,
                    weight: 9
                }
            }
        },
        {
            guid: "7ee59f418896447ca03ce09665771ae6",
            name: "LavaWalkers",
            display: "Lava Walkers",
            details: "Walk on fire",
            rarity: "Common",
            crafting: 26,
            cost: 275,
            extra: "",
            tables: {
                relic: {
                    index: 50,
                    weight: 9
                },
                relicAll: {
                    index: 103,
                    weight: 9
                },
                relicShop: {
                    index: 18,
                    weight: 3
                }
            }
        },
        {
            guid: "a7ae4ce21346402ca6fe2ad0c09b1c14",
            name: "HeliosBoots",
            display: "Helios Boots",
            details: "Oil + Fire",
            rarity: "Legendary",
            crafting: 0,
            cost: 550,
            extra: "Combined from Galoshes & Lava Walkers",
            tables: {}
        },
        {
            guid: "23f1ec241ee449419dfcf3db6ede4f8a",
            name: "HeavyBoots",
            display: "Heavy Boots",
            details: "Jump again in midair",
            rarity: "Common",
            crafting: 22,
            cost: 275,
            extra: "",
            tables: {
                relic: {
                    index: 111,
                    weight: 9
                },
                relicAll: {
                    index: 121,
                    weight: 9
                },
                relicShop: {
                    index: 51,
                    weight: 9
                }
            }
        },
        {
            guid: "fe3880cccffb46a6ba510453822df02b",
            name: "ButchersCleaver",
            display: "Butcher's Cleaver",
            details: "Sometimes drop meat from your enemies",
            rarity: "Common",
            crafting: 0,
            cost: 525,
            extra: "",
            tables: {
                relic: {
                    index: 48,
                    weight: 9
                },
                relicAll: {
                    index: 28,
                    weight: 9
                },
                relicStarter: {
                    index: 1,
                    weight: 9
                }
            }
        },
        {
            guid: "637c8bf20a8d4e6da5bcd6bbbac68f12",
            name: "KeyBlade",
            display: "Key Blade",
            details: "Increases swing damage for each key you have",
            rarity: "Common",
            crafting: 0,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 42,
                    weight: 9
                },
                relicAll: {
                    index: 57,
                    weight: 9
                }
            }
        },
        {
            guid: "9919d2d2d31941f2a96f898d56e8d297",
            name: "LegendaryBlade",
            display: "Master Pickaxe",
            details: "Fire projectiles at full health",
            rarity: "Rare",
            crafting: 50,
            cost: 600,
            extra: "",
            tables: {
                relic: {
                    index: 52,
                    weight: 3
                },
                relicAll: {
                    index: 104,
                    weight: 3
                },
                relicFinal: {
                    index: 14,
                    weight: 3
                }
            }
        },
        {
            guid: "49e257bc3ed04cde9e5b0a7089d7af10",
            name: "Mjolnir",
            display: "Mjölnir",
            details: "Thrown pickaxe summons lightning on hit",
            rarity: "Legendary",
            crafting: 0,
            cost: 850,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 5,
                    weight: 1
                }
            }
        },
        {
            guid: "212720105e1a4605b4d5bc4295a684cd",
            name: "DoomBlade",
            display: "Doom Blade",
            details: "Gain damage for each carried curse",
            rarity: "Legendary",
            crafting: 0,
            cost: 1000,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 4,
                    weight: 1
                }
            }
        },
        {
            guid: "2d5d75f5824242b781e8295d5f25b996",
            name: "BattleAxe",
            display: "Battle Axe",
            details: "Increases swing size,but slightly decreases swing damage",
            rarity: "Common",
            crafting: 0,
            cost: 450,
            extra: "",
            tables: {
                relic: {
                    index: 74,
                    weight: 9
                },
                relicAll: {
                    index: 40,
                    weight: 9
                },
                relicFinal: {
                    index: 23,
                    weight: 9
                }
            }
        },
        {
            guid: "2e1f6f7792eb4409a359778402737ed1",
            name: "Glaive",
            display: "Glaive",
            details: "Increases throw size.",
            rarity: "Common",
            crafting: 26,
            cost: 500,
            extra: "",
            tables: {
                relic: {
                    index: 75,
                    weight: 9
                },
                relicAll: {
                    index: 91,
                    weight: 9
                },
                relicFinal: {
                    index: 24,
                    weight: 9
                }
            }
        },
        {
            guid: "05ad55b489ff4af28e34ab471ecd0640",
            name: "Masa",
            display: "Masa",
            details: "Reduces throw damage and increases swing damage",
            rarity: "Rare",
            crafting: 0,
            cost: 410,
            extra: "",
            tables: {
                relic: {
                    index: 79,
                    weight: 3
                },
                relicAll: {
                    index: 43,
                    weight: 3
                }
            }
        },
        {
            guid: "78d8214111974925b1e2039f20b728cf",
            name: "Mune",
            display: "Mune",
            details: "Reduces swing damage and increases throw damage",
            rarity: "Rare",
            crafting: 1,
            cost: 490,
            extra: "",
            tables: {
                relic: {
                    index: 81,
                    weight: 3
                },
                relicAll: {
                    index: 45,
                    weight: 3
                }
            }
        },
        {
            guid: "c5791c753f7e42de8a2814bd4b17504a",
            name: "Masamune",
            display: "Masamune",
            details: "Instantly kills enemies sometimes",
            rarity: "Legendary",
            crafting: 0,
            cost: 900,
            extra: "Combined from Masa & Mune",
            tables: {}
        },
        {
            guid: "f568bbf4ecb54daf9d7d472f06d97521",
            name: "Suneater",
            display: "Suneater",
            details: "Consumes all current and future blessings and converts them to swing damage",
            rarity: "Legendary",
            crafting: 0,
            cost: 900,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 12,
                    weight: 1
                }
            }
        },
        {
            guid: "e161e4e3083948eaa22f9830394b1235",
            name: "VorpalBlade",
            display: "Vorpal Blade",
            details: "Attack quickly without stopping",
            rarity: "Common",
            crafting: 50,
            cost: 400,
            extra: "Unique Swing",
            tables: {
                relic: {
                    index: 88,
                    weight: 9
                },
                relicAll: {
                    index: 50,
                    weight: 9
                },
                relicFinal: {
                    index: 26,
                    weight: 9
                }
            }
        },
        {
            guid: "66b7f22d45354e549d2a4a18ce9792f7",
            name: "TwistedBlade",
            display: "Twisted Blade",
            details: "Increases critical strike chance for each carried curse",
            rarity: "Common",
            crafting: 30,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 76,
                    weight: 9
                },
                relicAll: {
                    index: 41,
                    weight: 9
                }
            }
        },
        {
            guid: "2945df6b2ea043dd94c67ba09aefbe2b",
            name: "ObsidianKnife",
            display: "Obsidian Knife",
            details: "Dramatically increases damage,but breaks when hit",
            rarity: "Rare",
            crafting: 0,
            cost: 650,
            extra: "",
            tables: {
                relic: {
                    index: 102,
                    weight: 3
                },
                relicAll: {
                    index: 113,
                    weight: 3
                },
                relicFinal: {
                    index: 30,
                    weight: 3
                }
            }
        },
        {
            guid: "54d1936d55494194a42d56c6f314c763",
            name: "DirksHammer",
            display: "Dirk's Hammer",
            details: "Transmute an item by hitting it",
            rarity: "Rare",
            crafting: 50,
            cost: 500,
            extra: "",
            tables: {
                relic: {
                    index: 90,
                    weight: 3
                },
                relicAll: {
                    index: 86,
                    weight: 3
                },
                relicShop: {
                    index: 37,
                    weight: 3
                }
            }
        },
        {
            guid: "a8d3215241a94555866450ab791bc7e3",
            name: "Resurrection",
            display: "Resurrection",
            details: "Resurrect on death",
            rarity: "Rare",
            crafting: 48,
            cost: 1200,
            extra: "",
            tables: {
                relic: {
                    index: 47,
                    weight: 3
                },
                relicAll: {
                    index: 27,
                    weight: 3
                },
                relicFinal: {
                    index: 12,
                    weight: 6
                }
            }
        },
        {
            guid: "623c929400f44c05b07827fef3426e60",
            name: "GordonsTunic",
            display: "Gordon's Tunic",
            details: "Reduces elemental damage",
            rarity: "Common",
            crafting: 14,
            cost: 280,
            extra: "",
            tables: {
                relic: {
                    index: 61,
                    weight: 9
                },
                relicAll: {
                    index: 95,
                    weight: 9
                },
                relicShop: {
                    index: 21,
                    weight: 9
                }
            }
        },
        {
            guid: "b367a2746211405a8dda8d560f88b6d3",
            name: "Breastplate",
            display: "Breastplate",
            details: "Adds a point of armor to the health bar",
            rarity: "Common",
            crafting: 55,
            cost: 450,
            extra: "",
            tables: {
                relic: {
                    index: 89,
                    weight: 5
                },
                relicAll: {
                    index: 79,
                    weight: 9
                },
                relicFinal: {
                    index: 27,
                    weight: 5
                }
            }
        },
        {
            guid: "53f62e71c29143f79a27abca6069f52b",
            name: "Pauldron",
            display: "Pauldron",
            details: "",
            rarity: "Common",
            crafting: 0,
            cost: 450,
            extra: "",
            tables: {
                relic: {
                    index: 101,
                    weight: 5
                },
                relicAll: {
                    index: 112,
                    weight: 5
                },
                relicShop: {
                    index: 45,
                    weight: 5
                }
            }
        },
        {
            guid: "2f342d2da24340b383d49f1fb9374e5a",
            name: "Gauntlets",
            display: "Gauntlets",
            details: "Adds a point of armor to the health bar",
            rarity: "Common",
            crafting: 35,
            cost: 450,
            extra: "",
            tables: {
                relic: {
                    index: 99,
                    weight: 5
                },
                relicAll: {
                    index: 110,
                    weight: 5
                }
            }
        },
        {
            guid: "e930152d55884ffbb8a0ce9edd6a46f1",
            name: "Greaves",
            display: "Greaves",
            details: "Adds a point of armor to the health bar",
            rarity: "Common",
            crafting: 0,
            cost: 450,
            extra: "",
            tables: {
                relic: {
                    index: 100,
                    weight: 5
                },
                relicAll: {
                    index: 111,
                    weight: 5
                },
                relicShop: {
                    index: 44,
                    weight: 5
                }
            }
        },
        {
            guid: "19dc5130b5404db7a09262d609d9b8ba",
            name: "ShieldofQuills",
            display: "Shield of Quills",
            details: "Gain two points of armor,armor increases damage",
            rarity: "Legendary",
            crafting: 0,
            cost: 770,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 15,
                    weight: 1
                }
            }
        },
        {
            guid: "630bd51306d34a9c9de7b041f1a3caee",
            name: "BlastSuit",
            display: "Blast Suit",
            details: "Invulnerability to friendly bomb blasts and increases bomb damage",
            rarity: "Common",
            crafting: 14,
            cost: 275,
            extra: "",
            tables: {
                relic: {
                    index: 91,
                    weight: 9
                },
                relicAll: {
                    index: 76,
                    weight: 9
                },
                relicShop: {
                    index: 38,
                    weight: 9
                }
            }
        },
        {
            guid: "ca4e94bdc2cc43329474ef40230aca52",
            name: "Pillow",
            display: "Hoodie's Pillow",
            details: "Reduces physical damage",
            rarity: "Rare",
            crafting: 22,
            cost: 350,
            extra: "",
            tables: {
                relic: {
                    index: 21,
                    weight: 3
                },
                relicAll: {
                    index: 11,
                    weight: 3
                },
                relicFinal: {
                    index: 7,
                    weight: 3
                }
            }
        },
        {
            guid: "a1ca0abb4f8c47bbb719be48fb7fd470",
            name: "SoulGuard",
            display: "Soul Guard",
            details: "Reduces and redirects damage to your max HP",
            rarity: "Legendary",
            crafting: 0,
            cost: 850,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 16,
                    weight: 1
                }
            }
        },
        {
            guid: "2a64aa4851c94841ae297322646e6bdc",
            name: "WetBlanket",
            display: "Wet Blanket",
            details: "Puts out fires immediately,but requires water charges",
            rarity: "Common",
            crafting: 0,
            cost: 275,
            extra: "",
            tables: {
                relicAll: {
                    index: 69,
                    weight: 9
                },
                relicShop: {
                    index: 34,
                    weight: 9
                }
            }
        },
        {
            guid: "95c9369deaba40caa44b1b48989a6a17",
            name: "WarPaint",
            display: "War Paint",
            details: "Increase attack damage and speed when killing enemies",
            rarity: "Common",
            crafting: 0,
            cost: 480,
            extra: "",
            tables: {
                relic: {
                    index: 43,
                    weight: 9
                },
                relicAll: {
                    index: 68,
                    weight: 9
                }
            }
        },
        {
            guid: "fd702e9d2662490f886876ee598b15d7",
            name: "Battlestandard",
            display: "Battle Standard",
            details: "Increases move,attack,and throw speed at the beginning of battle",
            rarity: "Common",
            crafting: 0,
            cost: 450,
            extra: "",
            tables: {
                relic: {
                    index: 72,
                    weight: 9
                },
                relicAll: {
                    index: 38,
                    weight: 9
                }
            }
        },
        {
            guid: "6bbe9142f1144e7cac6b0f2edf13cac9",
            name: "Catalyst",
            display: "Catalyst",
            details: "When healed,heal again",
            rarity: "Common",
            crafting: 16,
            cost: 280,
            extra: "",
            tables: {
                relic: {
                    index: 62,
                    weight: 9
                },
                relicAll: {
                    index: 80,
                    weight: 9
                },
                relicShop: {
                    index: 22,
                    weight: 9
                }
            }
        },
        {
            guid: "ecb92c9f921647a69fa3bcd95bb535e7",
            name: "SeltsEgg",
            display: "Selt's Egg",
            details: "Spawn larvae on entrance",
            rarity: "Rare",
            crafting: 15,
            cost: 500,
            extra: "",
            tables: {
                relic: {
                    index: 46,
                    weight: 3
                },
                relicAll: {
                    index: 26,
                    weight: 3
                }
            }
        },
        {
            guid: "5482a1b1b71b433d886865f2394f2fcc",
            name: "ElectrifiedOrb",
            display: "Electrified Orb",
            details: "Orbits the peasant and shocks enemies on contact",
            rarity: "Rare",
            crafting: 30,
            cost: 425,
            extra: "",
            tables: {
                relic: {
                    index: 85,
                    weight: 3
                },
                relicAll: {
                    index: 89,
                    weight: 3
                },
                relicFinal: {
                    index: 25,
                    weight: 3
                }
            }
        },
        {
            guid: "cd5580348c4d4a3cbcba94eafc169f4a",
            name: "OthermineConduit",
            display: "Othermine Conduit",
            details: "Ghastly hands will attack the enemy",
            rarity: "Rare",
            crafting: 55,
            cost: 480,
            extra: "",
            tables: {
                relic: {
                    index: 73,
                    weight: 3
                },
                relicAll: {
                    index: 39,
                    weight: 3
                },
                relicFinal: {
                    index: 22,
                    weight: 3
                }
            }
        },
        {
            guid: "ca58b2a9d6cc4f56ae0699e33578a2e2",
            name: "Doll",
            display: "Doll",
            details: "Blocks the next few curses.",
            rarity: "Common",
            crafting: 22,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 78,
                    weight: 9
                },
                relicAll: {
                    index: 87,
                    weight: 9
                },
                relicShop: {
                    index: 30,
                    weight: 9
                }
            }
        },
        {
            guid: "d21a2fe3ce9d4fddb34a59dd4df3773f",
            name: "Devotion",
            display: "Devotion",
            details: "Pray twice at altars",
            rarity: "Common",
            crafting: 30,
            cost: 450,
            extra: "",
            tables: {
                relicAll: {
                    index: 85,
                    weight: 9
                },
                relicShop: {
                    index: 16,
                    weight: 9
                }
            }
        },
        {
            guid: "4b36bbc087004750a1526304aef7b088",
            name: "108Beads",
            display: "108 Beads",
            details: "Heal when praying at an altar",
            rarity: "Common",
            crafting: 12,
            cost: 360,
            extra: "",
            tables: {
                relic: {
                    index: 87,
                    weight: 9
                },
                relicAll: {
                    index: 70,
                    weight: 9
                },
                relicShop: {
                    index: 36,
                    weight: 9
                }
            }
        },
        {
            guid: "80242154d4284cc6aab221292cb0ae93",
            name: "HolyGuacamole",
            display: "Holy Guacamole",
            details: "Find more altar rooms",
            rarity: "Rare",
            crafting: 20,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 94,
                    weight: 2
                },
                relicAll: {
                    index: 97,
                    weight: 3
                }
            }
        },
        {
            guid: "25d89c4cdce0444bb617016d3fa547d0",
            name: "SonicBoom",
            display: "Sonic Boom",
            details: "Throw really fast",
            rarity: "Common",
            crafting: 0,
            cost: 280,
            extra: "",
            tables: {
                relic: {
                    index: 44,
                    weight: 9
                },
                relicAll: {
                    index: 24,
                    weight: 9
                }
            }
        },
        {
            guid: "d8f7d033bec0417ab0b553f3a51a9a7f",
            name: "SewingKit",
            display: "Sewing Kit",
            details: "Keep all your gold when you die",
            rarity: "Rare",
            crafting: 0,
            cost: 750,
            extra: "",
            tables: {
                relic: {
                    index: 39,
                    weight: 3
                },
                relicAll: {
                    index: 22,
                    weight: 3
                },
                relicShop: {
                    index: 14,
                    weight: 3
                }
            }
        },
        {
            guid: "b29ea427f7f64aa3a06c8e19a0495648",
            name: "SimpleChest",
            display: "Simple Chest",
            details: "Overstocks the shop",
            rarity: "Common",
            crafting: 0,
            cost: 250,
            extra: "",
            tables: {
                relic: {
                    index: 32,
                    weight: 9
                },
                relicAll: {
                    index: 18,
                    weight: 9
                },
                relicShop: {
                    index: 9,
                    weight: 9
                }
            }
        },
        {
            guid: "074da99ced7f4993b38cb90df3a27d84",
            name: "MealTicket",
            display: "Meal Ticket",
            details: "Free food at the shop,right now!",
            rarity: "Common",
            crafting: 0,
            cost: 300,
            extra: "",
            tables: {
                relic: {
                    index: 23,
                    weight: 9
                },
                relicAll: {
                    index: 13,
                    weight: 9
                },
                relicShop: {
                    index: 6,
                    weight: 9
                }
            }
        },
        {
            guid: "6774d4be9573481f8e6fa97add614285",
            name: "AdventurersHat",
            display: "Adventurer's Hat",
            details: "",
            rarity: "Common",
            crafting: 20,
            cost: 250,
            extra: "",
            tables: {
                relic: {
                    index: 58,
                    weight: 9
                },
                relicAll: {
                    index: 71,
                    weight: 6
                },
                relicShop: {
                    index: 20,
                    weight: 6
                }
            }
        },
        {
            guid: "376feb7893994baf826df320010a2eae",
            name: "AdventurersWhip",
            display: "Adventurer's Whip",
            details: "",
            rarity: "Rare",
            crafting: 0,
            cost: 475,
            extra: "",
            tables: {
                relic: {
                    index: 57,
                    weight: 3
                },
                relicAll: {
                    index: 33,
                    weight: 3
                }
            }
        },
        {
            guid: "bc3b600bea3d416391e2549c137fff63",
            name: "GoldenIdol",
            display: "Golden Idol",
            details: "Discover more rooms and get rich",
            rarity: "Legendary",
            crafting: 0,
            cost: 725,
            extra: "Combined from Adventurer's Hat & Adventurer's Whip",
            tables: {}
        },
        {
            guid: "d6045725bae4407ea8492ff1e4fa7c3f",
            name: "TotemOfLife",
            display: "Totem of Life",
            details: "Sustain yourself through adventure",
            rarity: "Common",
            crafting: 0,
            cost: 550,
            extra: "",
            tables: {
                relic: {
                    index: 34,
                    weight: 9
                },
                relicAll: {
                    index: 19,
                    weight: 9
                }
            }
        },
        {
            guid: "fe3df61649f84dd2b08492c1788d92af",
            name: "AphoticCharm",
            display: "Aphotic Charm",
            details: "Heal in each new room for each curse",
            rarity: "Common",
            crafting: 18,
            cost: 0,
            extra: "",
            tables: {
                relic: {
                    index: 71,
                    weight: 9
                },
                relicAll: {
                    index: 72,
                    weight: 9
                },
                relicShop: {
                    index: 28,
                    weight: 9
                }
            }
        },
        {
            guid: "2380817a345d4458b5630768a38f35a1",
            name: "DillonsClaw",
            display: "Dillon's Claw",
            details: "Deal even more damage when you critical strike",
            rarity: "Common",
            crafting: 0,
            cost: 350,
            extra: "",
            tables: {
                relic: {
                    index: 25,
                    weight: 9
                },
                relicAll: {
                    index: 15,
                    weight: 9
                },
                relicFinal: {
                    index: 9,
                    weight: 18
                }
            }
        },
        {
            guid: "6910c28b900043d48911f9ae27e343cf",
            name: "ShadowsFang",
            display: "Shadow's Fang",
            details: "Higher chance to critical strike",
            rarity: "Common",
            crafting: 10,
            cost: 350,
            extra: "",
            tables: {
                relic: {
                    index: 24,
                    weight: 9
                },
                relicAll: {
                    index: 14,
                    weight: 9
                },
                relicFinal: {
                    index: 8,
                    weight: 18
                }
            }
        },
        {
            guid: "815f242a079240b8833a32c5f408e03a",
            name: "BrambleVest",
            display: "Bramble Vest",
            details: "Return damage but amplified",
            rarity: "Common",
            crafting: 0,
            cost: 200,
            extra: "",
            tables: {
                relic: {
                    index: 26,
                    weight: 9
                },
                relicAll: {
                    index: 16,
                    weight: 9
                },
                relicShop: {
                    index: 29,
                    weight: 9
                }
            }
        },
        {
            guid: "be05dceb707e415ca9224898a15bd1d3",
            name: "Duplicator",
            display: "Duplicator",
            details: "Relic rooms present a tantalizing choice",
            rarity: "Rare",
            crafting: 26,
            cost: 600,
            extra: "",
            tables: {
                relic: {
                    index: 31,
                    weight: 3
                },
                relicAll: {
                    index: 88,
                    weight: 3
                },
                relicShop: {
                    index: 8,
                    weight: 3
                }
            }
        },
        {
            guid: "87447b9566774ef486aa065a51a2bf4e",
            name: "Hyperstone",
            display: "Hyperstone",
            details: "Increase attack speed",
            rarity: "Rare",
            crafting: 45,
            cost: 510,
            extra: "",
            tables: {
                relic: {
                    index: 37,
                    weight: 3
                },
                relicAll: {
                    index: 99,
                    weight: 3
                },
                relicFinal: {
                    index: 10,
                    weight: 18
                }
            }
        },
        {
            guid: "262277e1e3914d1aae80a2d9f5acb3bd",
            name: "MinersFlask",
            display: "Miner's Flask",
            details: "Feel the effects of a potion for longer",
            rarity: "Common",
            crafting: 0,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 36,
                    weight: 9
                },
                relicAll: {
                    index: 21,
                    weight: 9
                },
                relicShop: {
                    index: 13,
                    weight: 9
                }
            }
        },
        {
            guid: "1e47701bb3324ddfb2c097dda2506453",
            name: "Lunchbox",
            display: "Lunchbox",
            details: "Store a piece of food for later",
            rarity: "Common",
            crafting: 0,
            cost: 380,
            extra: "",
            tables: {
                relic: {
                    index: 80,
                    weight: 9
                },
                relicAll: {
                    index: 44,
                    weight: 9
                },
                relicShop: {
                    index: 32,
                    weight: 9
                }
            }
        },
        {
            guid: "2725a7b0032447a98b7604d29395317a",
            name: "Immolate",
            display: "Large Ember",
            details: "",
            rarity: "Rare",
            crafting: 20,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 29,
                    weight: 3
                },
                relicAll: {
                    index: 100,
                    weight: 3
                }
            }
        },
        {
            guid: "70965440d15240d1a7ac7c8ddff4c9b2",
            name: "ItemPopcorn",
            display: "Popcorn",
            details: "Items will sometimes duplicate themselves",
            rarity: "Common",
            crafting: 16,
            cost: 650,
            extra: "",
            tables: {
                relicAll: {
                    index: 102,
                    weight: 9
                },
                relicShop: {
                    index: 10,
                    weight: 9
                }
            }
        },
        {
            guid: "6b18a3a60fe342f1833aff1526898e11",
            name: "GoldPopcorn",
            display: "Golden Popcorn",
            details: "Gold will sometimes duplicate itself",
            rarity: "Common",
            crafting: 0,
            cost: 550,
            extra: "",
            tables: {
                relic: {
                    index: 35,
                    weight: 9
                },
                relicAll: {
                    index: 20,
                    weight: 9
                },
                relicShop: {
                    index: 11,
                    weight: 9
                },
                relicStarter: {
                    index: 3,
                    weight: 9
                }
            }
        },
        {
            guid: "81b81569168949aeaea9412aea00601d",
            name: "FoodPopcorn",
            display: "Seasoned Popcorn",
            details: "Food will sometimes duplicate itself",
            rarity: "Common",
            crafting: 0,
            cost: 500,
            extra: "",
            tables: {
                relicAll: {
                    index: 55,
                    weight: 9
                },
                relicShop: {
                    index: 12,
                    weight: 9
                }
            }
        },
        {
            guid: "586b91a55c884c48832f9503dd8947c2",
            name: "CaramelPopcorn",
            display: "Caramel Popcorn",
            details: "Duplicated food packs a surprise",
            rarity: "Legendary",
            crafting: 0,
            cost: 1100,
            extra: "Combined from Golden Popcorn & Seasoned Popcorn",
            tables: {}
        },
        {
            guid: "ebb2f31ddd77460196d44fabcd18f57a",
            name: "PocketGrill",
            display: "Pocket Grill",
            details: "Cooks all your food",
            rarity: "Common",
            crafting: 0,
            cost: 500,
            extra: "",
            tables: {
                relicAll: {
                    index: 65,
                    weight: 9
                },
                relicShop: {
                    index: 17,
                    weight: 9
                }
            }
        },
        {
            guid: "6851f6a8812f4bcbabaa9a2414ec70d5",
            name: "Leftovers",
            display: "Leftovers",
            details: "Find old, gross food in chests",
            rarity: "Common",
            crafting: 0,
            cost: 475,
            extra: "",
            tables: {
                relic: {
                    index: 27,
                    weight: 9
                },
                relicAll: {
                    index: 61,
                    weight: 9
                },
                relicShop: {
                    index: 7,
                    weight: 9
                }
            }
        },
        {
            guid: "ed388fc9c09943bc987cf1c3388e8755",
            name: "SpareOrdinance",
            display: "Spare Ordnance",
            details: "Discover a bomb in every chest",
            rarity: "Common",
            crafting: 0,
            cost: 450,
            extra: "",
            tables: {
                relic: {
                    index: 28,
                    weight: 9
                },
                relicAll: {
                    index: 108,
                    weight: 9
                },
                relicShop: {
                    index: 43,
                    weight: 9
                }
            }
        },
        {
            guid: "f7eca5455bdf49fe9d2dafd4ec32d1e7",
            name: "Miniaturizer",
            display: "Miniaturizer",
            details: "Find small boxes in big boxes",
            rarity: "Legendary",
            crafting: 0,
            cost: 850,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 7,
                    weight: 1
                }
            }
        },
        {
            guid: "5abbafb002b04ee495f179afdb550ffa",
            name: "KeyDoubler",
            display: "Key Doubler",
            details: "Sometimes drop a new key when using an old key",
            rarity: "Common",
            crafting: 0,
            cost: 480,
            extra: "",
            tables: {
                relicAll: {
                    index: 58,
                    weight: 9
                },
                relicShop: {
                    index: 4,
                    weight: 9
                }
            }
        },
        {
            guid: "e56c7298c19e47d990140eceb14b95cc",
            name: "BombDoubler",
            display: "Bomb Doubler",
            details: "Sometimes drop a new bomb when using an old bomb",
            rarity: "Common",
            crafting: 18,
            cost: 500,
            extra: "",
            tables: {
                relicAll: {
                    index: 78,
                    weight: 9
                },
                relicShop: {
                    index: 5,
                    weight: 9
                }
            }
        },
        {
            guid: "fb374cc41c744cdbbbe227889cf001c4",
            name: "DoubleDoubler",
            display: "Double Doubler",
            details: "Chance to drop a key and bomb when using either",
            rarity: "Legendary",
            crafting: 0,
            cost: 980,
            extra: "Combined from Key Doubler & Bomb Doubler",
            tables: {}
        },
        {
            guid: "ec20d69533674c59a1af67db438bf172",
            name: "PilfersGift",
            display: "Pilfer's Gift",
            details: "Be blessed with two gifts from the pilfers",
            rarity: "Rare",
            crafting: 99,
            cost: 900,
            extra: "",
            tables: {
                relic: {
                    index: 30,
                    weight: 3
                },
                relicAll: {
                    index: 17,
                    weight: 3
                }
            }
        },
        {
            guid: "cad1d7d1cd454671ac62d6496c5aa49e",
            name: "GoldenBombs",
            display: "Golden Powder",
            details: "Turns rock into gold",
            rarity: "Common",
            crafting: 15,
            cost: 750,
            extra: "Unique Bomb",
            tables: {
                relic: {
                    index: 0,
                    weight: 9
                },
                relicAll: {
                    index: 92,
                    weight: 9
                }
            }
        },
        {
            guid: "82d1d9ec3cb74d019e07ac5750e5433d",
            name: "ClusterBombs",
            display: "Bombushka",
            details: "Bombs, in bombs, in bombs",
            rarity: "Common",
            crafting: 0,
            cost: 400,
            extra: "Unique Bomb",
            tables: {
                relic: {
                    index: 1,
                    weight: 9
                },
                relicAll: {
                    index: 0,
                    weight: 9
                },
                relicStarter: {
                    index: 2,
                    weight: 9
                }
            }
        },
        {
            guid: "04579544c0354d24a712f465b0a4a504",
            name: "IncendiaryBombs",
            display: "Seer's Blood",
            details: "Bombs explode in a shower of fire",
            rarity: "Common",
            crafting: 0,
            cost: 350,
            extra: "Unique Bomb",
            tables: {
                relic: {
                    index: 2,
                    weight: 9
                },
                relicAll: {
                    index: 1,
                    weight: 9
                }
            }
        },
        {
            guid: "96b62be9e4f04a329eed69ab143957aa",
            name: "RookBombs",
            display: "Rook's Bomb",
            details: "Death at ninety degrees",
            rarity: "Common",
            crafting: 0,
            cost: 480,
            extra: "Unique Bomb",
            tables: {
                relic: {
                    index: 3,
                    weight: 9
                },
                relicAll: {
                    index: 2,
                    weight: 9
                }
            }
        },
        {
            guid: "80c079e5b87345b28557d545c6f0131e",
            name: "BishopBombs",
            display: "Bishop's Bomb",
            details: "Death at forty five degrees",
            rarity: "Common",
            crafting: 8,
            cost: 470,
            extra: "Unique Bomb",
            tables: {
                relic: {
                    index: 38,
                    weight: 9
                },
                relicAll: {
                    index: 75,
                    weight: 9
                }
            }
        },
        {
            guid: "78622bf8ccfb4b6b8f4ad11165615e88",
            name: "QueenBombs",
            display: "Queen's Bomb",
            details: "Death in all directions",
            rarity: "Legendary",
            crafting: 0,
            cost: 950,
            extra: "Unique Bomb\nCombined from Rook's Bomb & Bishop's Bomb",
            tables: {}
        },
        {
            guid: "079a976e3dd24ea8819941b9f81259c2",
            name: "TransmutagenBombs",
            display: "Transmutagen Blast",
            details: "Transforms items",
            rarity: "Legendary",
            crafting: 0,
            cost: 1111,
            extra: "Unique Bomb",
            tables: {
                relicLegendary: {
                    index: 13,
                    weight: 1
                }
            }
        },
        {
            guid: "01dd414291604477aa1405b5683a29a3",
            name: "BrandingBombs",
            display: "Branding Bomb",
            details: "Bombs brand enemies for sacrifice, +10 bombs",
            rarity: "Legendary",
            crafting: 0,
            cost: 666,
            extra: "Unique Bomb",
            tables: {
                relicLegendary: {
                    index: 11,
                    weight: 1
                }
            }
        },
        {
            guid: "0948c27e6bf3483292c1e2e065499018",
            name: "LightningBombs",
            display: "Lightning Bomb",
            details: "Bomb explosions chain lightning",
            rarity: "Common",
            crafting: 0,
            cost: 350,
            extra: "Unique Bomb",
            tables: {
                relic: {
                    index: 4,
                    weight: 9
                },
                relicAll: {
                    index: 62,
                    weight: 9
                }
            }
        },
        {
            guid: "f3fc59fe276e4b5eb28425f43940c0f8",
            name: "MidasBombs",
            display: "M.E.G.A. Bomb",
            details: "A mess of electrified gold",
            rarity: "Legendary",
            crafting: 0,
            cost: 0,
            extra: "Combined from Lightning Bomb & Golden Powder",
            tables: {}
        },
        {
            guid: "5e270234064d40d08b17881eee751dfd",
            name: "BloodBomb",
            display: "Blood Bomb",
            details: "Leeches life from enemies slain by bombs",
            rarity: "Common",
            crafting: 10,
            cost: 430,
            extra: "Unique Bomb",
            tables: {
                relic: {
                    index: 93,
                    weight: 9
                },
                relicAll: {
                    index: 77,
                    weight: 9
                }
            }
        },
        {
            guid: "7a06da12309d4136917c71be7772e055",
            name: "TsarBomba",
            display: "Tsar Bomba",
            details: "Killing enemies with a bomb spawns a new bomb. Carried bombs decrease swing and throw damage.",
            rarity: "Legendary",
            crafting: 0,
            cost: 875,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 14,
                    weight: 1
                }
            }
        },
        {
            guid: "c3cf34b72ca743f7986c53215ec3a358",
            name: "U235",
            display: "U-235",
            details: "Bomb damage is proportional to the number of carried bombs, +5 bombs",
            rarity: "Rare",
            crafting: 60,
            cost: 560,
            extra: "",
            tables: {
                relic: {
                    index: 84,
                    weight: 3
                },
                relicAll: {
                    index: 48,
                    weight: 3
                }
            }
        },
        {
            guid: "8c781614f3e545cd9dc53a1862f87dc7",
            name: "Shrapnel",
            display: "Shrapnel",
            details: "Friendly bomb blasts fire projectiles",
            rarity: "Common",
            crafting: 0,
            cost: 310,
            extra: "",
            tables: {
                relic: {
                    index: 112,
                    weight: 9
                },
                relicAll: {
                    index: 122,
                    weight: 9
                },
                relicShop: {
                    index: 52,
                    weight: 9
                }
            }
        },
        {
            guid: "3640d085d4624902a191cefbacabd840",
            name: "GeckoBlast",
            display: "Gecko Blast",
            details: "Bomb blasts attract items",
            rarity: "Common",
            crafting: 0,
            cost: 500,
            extra: "",
            tables: {
                relic: {
                    index: 97,
                    weight: 1
                },
                relicAll: {
                    index: 107,
                    weight: 1
                },
                relicShop: {
                    index: 42,
                    weight: 9
                }
            }
        },
        {
            guid: "fd98fd90983c49f49e88267708a16807",
            name: "CaptureSphere",
            display: "Capture Sphere",
            details: "Bomb kills permanently increase bomb damage",
            rarity: "Common",
            crafting: 0,
            cost: 285,
            extra: "",
            tables: {
                relicShop: {
                    index: 47,
                    weight: 9
                }
            }
        },
        {
            guid: "a1231c3c35814d68aaae8d98cc882fd9",
            name: "RemoteDetonator",
            display: "Remote Detonator",
            details: "Detonate bombs when you feel like it",
            rarity: "Rare",
            crafting: 30,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 82,
                    weight: 3
                },
                relicAll: {
                    index: 46,
                    weight: 3
                },
                relicShop: {
                    index: 33,
                    weight: 3
                }
            }
        },
        {
            guid: "403c20cf86844b8ba5a495752f890817",
            name: "ShortWicks",
            display: "Short Wicks",
            details: "Reduces the bomb cooldown",
            rarity: "Common",
            crafting: 20,
            cost: 225,
            extra: "",
            tables: {
                relicShop: {
                    index: 46,
                    weight: 9
                }
            }
        },
        {
            guid: "724a50c9ac524f9fb3f527a5052e02a6",
            name: "Guantes",
            display: "Guantes",
            details: "Throw that pickaxe a bit harder",
            rarity: "Common",
            crafting: 10,
            cost: 500,
            extra: "",
            tables: {
                relic: {
                    index: 11,
                    weight: 9
                },
                relicAll: {
                    index: 96,
                    weight: 9
                },
                relicFinal: {
                    index: 3,
                    weight: 9
                }
            }
        },
        {
            guid: "1808d1da72b540c1a611a560f5f89fb3",
            name: "SequenceBreaker",
            display: "Sequence Breaker",
            details: "Teleport to your pickaxe",
            rarity: "Common",
            crafting: 26,
            cost: 500,
            extra: "Unique Throw",
            tables: {
                relic: {
                    index: 68,
                    weight: 9
                },
                relicAll: {
                    index: 37,
                    weight: 9
                },
                relicShop: {
                    index: 27,
                    weight: 9
                }
            }
        },
        {
            guid: "67aea971b4214697bb0cf4bdc4e904aa",
            name: "ThrowingStar",
            display: "Throwing Star",
            details: "Thrown pickaxe penetrates enemies and objects",
            rarity: "Common",
            crafting: 16,
            cost: 350,
            extra: "",
            tables: {
                relic: {
                    index: 86,
                    weight: 9
                },
                relicAll: {
                    index: 49,
                    weight: 9
                }
            }
        },
        {
            guid: "b4e5f598694c439d8260e8539e0320b2",
            name: "Guidance",
            display: "Guidance",
            details: "400",
            rarity: "Common",
            crafting: 0,
            cost: 400,
            extra: "Unique Throw",
            tables: {
                relic: {
                    index: 9,
                    weight: 9
                },
                relicAll: {
                    index: 6,
                    weight: 9
                },
                relicFinal: {
                    index: 1,
                    weight: 9
                },
                relicStarter: {
                    index: 4,
                    weight: 9
                }
            }
        },
        {
            guid: "01e0ef72372d4531a4a1cdb3cc265f97",
            name: "PhantasmalAxe",
            display: "Phantasmal Axe",
            details: "Thrown pickaxes duplicate themselves",
            rarity: "Common",
            crafting: 0,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 95,
                    weight: 9
                },
                relicAll: {
                    index: 63,
                    weight: 9
                },
                relicStarter: {
                    index: 5,
                    weight: 9
                }
            }
        },
        {
            guid: "830216e9492c484489ef90c303f07b68",
            name: "Chakram",
            display: "Chakram",
            details: "Throw a whirling blade of death",
            rarity: "Legendary",
            crafting: 0,
            cost: 800,
            extra: "Unique Throw",
            tables: {
                relicLegendary: {
                    index: 1,
                    weight: 1
                }
            }
        },
        {
            guid: "48f2bb8ba1f742ca830edfaa91772625",
            name: "Fork",
            display: "Fork",
            details: "Splits your ranged attacks",
            rarity: "Rare",
            crafting: 0,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 104,
                    weight: 3
                },
                relicAll: {
                    index: 115,
                    weight: 3
                },
                relicStarter: {
                    index: 8,
                    weight: 1
                }
            }
        },
        {
            guid: "b6f58486915b43b8bcd0004d74f9182b",
            name: "BottledLightning",
            display: "Bottled Lightning",
            details: "Chance on hit to chain lightning",
            rarity: "Common",
            crafting: 0,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 7,
                    weight: 9
                },
                relicAll: {
                    index: 4,
                    weight: 9
                },
                relicFinal: {
                    index: 0,
                    weight: 9
                },
                relicStarter: {
                    index: 0,
                    weight: 9
                }
            }
        },
        {
            guid: "efa267f58ead4081a7c8d7769a5e6bf8",
            name: "SalamanderTail",
            display: "Salamander Tail",
            details: "Chance on hit to ignite your enemies",
            rarity: "Common",
            crafting: 0,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 8,
                    weight: 9
                },
                relicAll: {
                    index: 5,
                    weight: 9
                },
                relicStarter: {
                    index: 7,
                    weight: 9
                }
            }
        },
        {
            guid: "aede3a51037549b9b0eeeec881d5168e",
            name: "CripplingPoison",
            display: "Crippling Poison",
            details: "Chance on hit to poison your enemies",
            rarity: "Common",
            crafting: 14,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 64,
                    weight: 9
                },
                relicAll: {
                    index: 83,
                    weight: 9
                }
            }
        },
        {
            guid: "e25c4adfa3e04022ae18673db8f83276",
            name: "CausticVial",
            display: "Caustic Vial",
            details: "Enemies hit with the thrown pickaxe explode on death",
            rarity: "Common",
            crafting: 24,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 92,
                    weight: 9
                },
                relicAll: {
                    index: 81,
                    weight: 9
                }
            }
        },
        {
            guid: "0c33fc50f8d94d518803789c0b7f2185",
            name: "CrackedOrb",
            display: "Cracked Orb",
            details: "Curse enemies with increased damage",
            rarity: "Rare",
            crafting: 0,
            cost: 460,
            extra: "",
            tables: {
                relic: {
                    index: 15,
                    weight: 3
                },
                relicAll: {
                    index: 9,
                    weight: 9
                },
                relicFinal: {
                    index: 5,
                    weight: 3
                }
            }
        },
        {
            guid: "9fcfbe4652be48ec82ea6a8431a18d37",
            name: "UrsineRing",
            display: "Ursine Ring",
            details: "Increase health",
            rarity: "Common",
            crafting: 0,
            cost: 500,
            extra: "",
            tables: {
                relic: {
                    index: 10,
                    weight: 9
                },
                relicAll: {
                    index: 67,
                    weight: 9
                },
                relicFinal: {
                    index: 2,
                    weight: 9
                }
            }
        },
        {
            guid: "29799d6270d24f018cef3d360eebc5e7",
            name: "DemonRing",
            display: "Demon Ring",
            details: "Increase swing damage",
            rarity: "Common",
            crafting: 0,
            cost: 500,
            extra: "",
            tables: {
                relic: {
                    index: 12,
                    weight: 9
                },
                relicAll: {
                    index: 7,
                    weight: 9
                },
                relicFinal: {
                    index: 4,
                    weight: 18
                }
            }
        },
        {
            guid: "651c313904884b19afcd5a1da136c01c",
            name: "MediocreRing",
            display: "Mediocre Ring",
            details: "Increases health and damage",
            rarity: "Common",
            crafting: 8,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 63,
                    weight: 9
                },
                relicAll: {
                    index: 35,
                    weight: 9
                },
                relicFinal: {
                    index: 20,
                    weight: 9
                }
            }
        },
        {
            guid: "3693999de5e94fcd81b5fcd53c13e8b4",
            name: "BerserkerPendant",
            display: "Berserker's Pendant",
            details: "Deal more swing damage at low health",
            rarity: "Common",
            crafting: 20,
            cost: 420,
            extra: "",
            tables: {
                relic: {
                    index: 16,
                    weight: 9
                },
                relicAll: {
                    index: 74,
                    weight: 9
                },
                relicFinal: {
                    index: 6,
                    weight: 9
                }
            }
        },
        {
            guid: "2fe62c4bfc084a7a8516c18f23575fb3",
            name: "AxeThrowersPendant",
            display: "Axe Thrower's Pendant",
            details: "Deal more throw damage at low health",
            rarity: "Common",
            crafting: 0,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 59,
                    weight: 9
                },
                relicAll: {
                    index: 34,
                    weight: 9
                },
                relicFinal: {
                    index: 18,
                    weight: 9
                }
            }
        },
        {
            guid: "10444b01cc944bdc92bbc183ecfd4dfe",
            name: "KnightPendant",
            display: "Knight's Pendant",
            details: "Deal more swing damage at high health",
            rarity: "Common",
            crafting: 0,
            cost: 420,
            extra: "",
            tables: {
                relic: {
                    index: 53,
                    weight: 9
                },
                relicAll: {
                    index: 59,
                    weight: 9
                },
                relicFinal: {
                    index: 15,
                    weight: 9
                }
            }
        },
        {
            guid: "77af6531c8294f249cf55fd69c74bd8a",
            name: "ArchersPendant",
            display: "Archer's Pendant",
            details: "Deal more throw damage at high health",
            rarity: "Common",
            crafting: 20,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 60,
                    weight: 9
                },
                relicAll: {
                    index: 73,
                    weight: 9
                },
                relicFinal: {
                    index: 19,
                    weight: 9
                }
            }
        },
        {
            guid: "e790a903a95240ffa6ff75f0c17475cd",
            name: "IronBranch",
            display: "Iron Branch",
            details: "Increase health, swing damage, and attack speed",
            rarity: "Common",
            crafting: 0,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 49,
                    weight: 9
                },
                relicAll: {
                    index: 29,
                    weight: 9
                },
                relicFinal: {
                    index: 13,
                    weight: 9
                }
            }
        },
        {
            guid: "e21cf8a29a6b4277b40ba59cb04c299b",
            name: "CrownQueens",
            display: "Queen's Crown",
            details: "Increase throw damage, swing size, and swing speed",
            rarity: "Rare",
            crafting: 0,
            cost: 550,
            extra: "",
            tables: {
                relic: {
                    index: 54,
                    weight: 3
                },
                relicAll: {
                    index: 31,
                    weight: 3
                },
                relicFinal: {
                    index: 16,
                    weight: 6
                }
            }
        },
        {
            guid: "0248a19ceeb042c096a827dbcb5b10de",
            name: "CrownKings",
            display: "King's Crown",
            details: "Increase swing damage, throw size, and throw speed",
            rarity: "Rare",
            crafting: 26,
            cost: 550,
            extra: "",
            tables: {
                relic: {
                    index: 55,
                    weight: 3
                },
                relicAll: {
                    index: 84,
                    weight: 3
                }
            }
        },
        {
            guid: "5efb8642758041cfb29b8555ce405f41",
            name: "CrownEmperors",
            display: "Emperor's Crown",
            details: "Increases stats and receive a blessing",
            rarity: "Legendary",
            crafting: 0,
            cost: 1100,
            extra: "Combined from Queen's Crown & King's Crown",
            tables: {}
        },
        {
            guid: "6e31da144f9b4820a3442e42883a52e5",
            name: "PilferRing",
            display: "Pilfer Ring",
            details: "Soak up gold and get a discount at the shop",
            rarity: "Common",
            crafting: 0,
            cost: 800,
            extra: "",
            tables: {
                relicAll: {
                    index: 64,
                    weight: 9
                },
                relicShop: {
                    index: 0,
                    weight: 9
                }
            }
        },
        {
            guid: "7c1a74b4bc5e4398b2a10459f9b50b21",
            name: "UnstableConcoction",
            display: "Unstable Concoction",
            details: "Gold hits the floor with explosive force",
            rarity: "Common",
            crafting: 0,
            cost: 420,
            extra: "",
            tables: {
                relic: {
                    index: 33,
                    weight: 9
                },
                relicAll: {
                    index: 66,
                    weight: 9
                }
            }
        },
        {
            guid: "9c707dd6871a44b7b7326ad378f41a38",
            name: "GoldTooth",
            display: "Gold Tooth",
            details: "Sustain yourself on gold",
            rarity: "Common",
            crafting: 15,
            cost: 600,
            extra: "",
            tables: {
                relic: {
                    index: 20,
                    weight: 9
                },
                relicAll: {
                    index: 94,
                    weight: 9
                }
            }
        },
        {
            guid: "ebe6a743ac76455ea4771e300ebc0b7b",
            name: "GoldenDelicious",
            display: "Golden Delicious",
            details: "All food is golden and extra delicious",
            rarity: "Common",
            crafting: 8,
            cost: 150,
            extra: "",
            tables: {
                relicAll: {
                    index: 93,
                    weight: 9
                },
                relicShop: {
                    index: 3,
                    weight: 9
                }
            }
        },
        {
            guid: "277d2316237d49109d6d1439f7df6e14",
            name: "Conductor",
            display: "Conductor",
            details: "Electrify your enemies when picking up gold",
            rarity: "Common",
            crafting: 0,
            cost: 550,
            extra: "",
            tables: {
                relic: {
                    index: 19,
                    weight: 9
                },
                relicAll: {
                    index: 10,
                    weight: 9
                }
            }
        },
        {
            guid: "0533edf2633049baa08a73d7ba5ef518",
            name: "GoldFrenzy",
            display: "Gold Frenzy",
            details: "Gain temporary damage when picking up gold",
            rarity: "Common",
            crafting: 0,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 45,
                    weight: 9
                },
                relicAll: {
                    index: 25,
                    weight: 9
                }
            }
        },
        {
            guid: "7c3dcc99d36d4f6f8bcc2524ecdb93e8",
            name: "Intensifier",
            display: "Intensifier",
            details: "Increases damage when killing enemies",
            rarity: "Common",
            crafting: 0,
            cost: 500,
            extra: "",
            tables: {
                relic: {
                    index: 14,
                    weight: 9
                },
                relicAll: {
                    index: 8,
                    weight: 9
                }
            }
        },
        {
            guid: "a596ffca42664024be1aeba28ad46f24",
            name: "SkullShield",
            display: "Floating Skull",
            details: "A shield that blocks projectiles, most of the time",
            rarity: "Rare",
            crafting: 0,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 40,
                    weight: 3
                },
                relicAll: {
                    index: 23,
                    weight: 3
                },
                relicStarter: {
                    index: 6,
                    weight: 9
                }
            }
        },
        {
            guid: "23ed5e24dd3c420eacbf5c006e61be87",
            name: "ReflectProjectiles",
            display: "Grimhilde's Mirror",
            details: "Return projectiles with an attack",
            rarity: "Common",
            crafting: 0,
            cost: 350,
            extra: "",
            tables: {
                relic: {
                    index: 22,
                    weight: 9
                },
                relicAll: {
                    index: 12,
                    weight: 9
                }
            }
        },
        {
            guid: "db4baf760be740b99ff679a838ee3ee4",
            name: "MirrorShield",
            display: "Mirror Shield",
            details: "Automatically reflect projectiles",
            rarity: "Legendary",
            crafting: 0,
            cost: 750,
            extra: "Combined from Floating Skull & Grimhilde's Mirror",
            tables: {}
        },
        {
            guid: "91f466ecbb87497b943eabd77f6e4681",
            name: "HungrySpirit",
            display: "Hungry Ghost",
            details: "Leach the life of your enemies",
            rarity: "Rare",
            crafting: 25,
            cost: 480,
            extra: "",
            tables: {
                relic: {
                    index: 13,
                    weight: 3
                },
                relicAll: {
                    index: 98,
                    weight: 3
                }
            }
        },
        {
            guid: "dd44a1f545ad48cda6d653cb6b5eaafb",
            name: "NullStone",
            display: "Nullstone",
            details: "Block a hit once in a while",
            rarity: "Legendary",
            crafting: 0,
            cost: 650,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 8,
                    weight: 1
                }
            }
        },
        {
            guid: "35ccd3bee9604d10988601bfecb53a3a",
            name: "Mushroom",
            display: "Mushroom",
            details: "Gain maximum health when killing enemies",
            rarity: "Legendary",
            crafting: 0,
            cost: 540,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 9,
                    weight: 1
                }
            }
        },
        {
            guid: "eaf60a76dda742928cdd8f75d3e22eca",
            name: "Clover",
            display: "Four Leaf Clover",
            details: "Hit the gold right out of your enemies' pockets",
            rarity: "Rare",
            crafting: 22,
            cost: 525,
            extra: "",
            tables: {
                relic: {
                    index: 18,
                    weight: 3
                },
                relicAll: {
                    index: 82,
                    weight: 3
                },
                relicShop: {
                    index: 2,
                    weight: 3
                }
            }
        },
        {
            guid: "69994ce940d94e439ab01d04a9c176e7",
            name: "Tent",
            display: "Tent",
            details: "Adds a tent to the starting room of a floor, one use only",
            rarity: "Common",
            crafting: 0,
            cost: 300,
            extra: "",
            tables: {
                relic: {
                    index: 77,
                    weight: 9
                },
                relicAll: {
                    index: 42,
                    weight: 9
                },
                relicShop: {
                    index: 39,
                    weight: 9
                }
            }
        },
        {
            guid: "56fb37c63e434aa08fa3c868c3c4d9c3",
            name: "Aegis",
            display: "Aegis",
            details: "Increase defense at critical health",
            rarity: "Common",
            crafting: 0,
            cost: 350,
            extra: "",
            tables: {
                relic: {
                    index: 56,
                    weight: 9
                },
                relicAll: {
                    index: 32,
                    weight: 9
                },
                relicShop: {
                    index: 19,
                    weight: 9
                },
                relicFinal: {
                    index: 17,
                    weight: 9
                }
            }
        },
        {
            guid: "fdf3edb47a594d0f870acabb1d82d433",
            name: "Map",
            display: "Map",
            details: "Get a peep at your surroundings",
            rarity: "Rare",
            crafting: 20,
            cost: 250,
            extra: "",
            tables: {
                relic: {
                    index: 17,
                    weight: 3
                },
                relicAll: {
                    index: 106,
                    weight: 3
                },
                relicShop: {
                    index: 1,
                    weight: 3
                }
            }
        },
        {
            guid: "d537cbb8584b4a3fbe45f0ceef4f2ef1",
            name: "PetrifiedRock",
            display: "Petrified Rock",
            details: "Increase drop rate of items from rocks",
            rarity: "Rare",
            crafting: 0,
            cost: 999,
            extra: "",
            tables: {}
        },
        {
            guid: "3776afb876a74e50911b6d3080f0388d",
            name: "RatBond",
            display: "Rat Bond",
            details: "Become charming to rats",
            rarity: "Rare",
            crafting: 15,
            cost: 250,
            extra: "",
            tables: {
                relic: {
                    index: 51,
                    weight: 3
                },
                relicAll: {
                    index: 30,
                    weight: 3
                }
            }
        },
        {
            guid: "97fbd863c09b44fa9605e675e7275f85",
            name: "CosmicEgg",
            display: "Cosmic Egg",
            details: "Increases experience gain for a familiar",
            rarity: "Rare",
            crafting: 0,
            cost: 750,
            extra: "",
            tables: {
                relic: {
                    index: 65,
                    weight: 3
                },
                relicAll: {
                    index: 36,
                    weight: 3
                },
                relicShop: {
                    index: 23,
                    weight: 3
                }
            }
        },
        {
            guid: "3d321813e7904a1486d6243defa59cc3",
            name: "BirthingPod",
            display: "Birthing Pod",
            details: "Consumes all healing until birth",
            rarity: "Rare",
            crafting: 50,
            cost: 1000,
            extra: "",
            tables: {
                relic: {
                    index: 103,
                    weight: 3
                },
                relicAll: {
                    index: 114,
                    weight: 3
                },
                relicShop: {
                    index: 50,
                    weight: 3
                }
            }
        },
        {
            guid: "898d87f98b184697ac89672f9bf955e4",
            name: "CreditCardSilver",
            display: "Pilfer Credit Card Silver",
            details: "A Gold10.png 2,500 limit at 0% interest",
            rarity: "Common",
            crafting: 0,
            cost: 1,
            extra: "",
            tables: {
                relicAll: {
                    index: 53,
                    weight: 3
                },
                relicShop: {
                    index: 24,
                    weight: 9
                }
            }
        },
        {
            guid: "f83c843bf28e48afbb6f3aec7d5d0eed",
            name: "CreditCardGold",
            display: "Pilfer Credit Card Gold",
            details: "A Gold10.png 5,000 limit at 0% interest",
            rarity: "Common",
            crafting: 0,
            cost: 1,
            extra: "",
            tables: {
                relicAll: {
                    index: 52,
                    weight: 1
                },
                relicShop: {
                    index: 41,
                    weight: 1
                }
            }
        },
        {
            guid: "87dd4661d57b48c3812b4c962c00cc2e",
            name: "CreditCardBlack",
            display: "Pilfer Credit Card Black Edition",
            details: "7,500 limit and 5% cash back",
            rarity: "Legendary",
            crafting: 0,
            cost: 0,
            extra: "Combined from Pilfer Credit Card Silver & Pilfer Credit Card Gold",
            tables: {}
        },
        {
            guid: "189b6fc98b1849ea848523610481af7d",
            name: "ButchersClover",
            display: "Four Leaf Cleaver",
            details: "Sometimes drop golden meat from your enemies",
            rarity: "Legendary",
            crafting: 0,
            cost: 1050,
            extra: "Combined from Butcher's Cleaver & Four Leaf Clover",
            tables: {}
        },
        {
            guid: "f3973f00fda94bfbb4eed373ed28b775",
            name: "RabbitGloves",
            display: "Rabbit Gloves",
            details: "Get a free item in every shop",
            rarity: "Legendary",
            crafting: 0,
            cost: 1200,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 3,
                    weight: 1
                }
            }
        },
        {
            guid: "4202645d8fa34a97a70ded11ee0f8f31",
            name: "KarmicScale",
            display: "Karmic Scale",
            details: "Health, damage, and healing become small and even",
            rarity: "Legendary",
            crafting: 0,
            cost: 1100,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 2,
                    weight: 1
                }
            }
        },
        {
            guid: "bab552535a104b059e636abfd7d05685",
            name: "PocketOfHolding",
            display: "Pocket of Holding",
            details: "Get some temporary bombs each room",
            rarity: "Legendary",
            crafting: 0,
            cost: 550,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 6,
                    weight: 1
                }
            }
        },
        {
            guid: "4cf72d4585f64a95b4d614bd559e4d58",
            name: "LockPick",
            display: "Lockpick",
            details: "Open locks for free, but for how long?",
            rarity: "Legendary",
            crafting: 0,
            cost: 900,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 0,
                    weight: 1
                }
            }
        },
        {
            guid: "5a5b61097a034cfeb6858d2d6c809af0",
            name: "LuckyCharm",
            display: "Lucky Charm",
            details: "A chance at avoiding death",
            rarity: "Common",
            crafting: 38,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 67,
                    weight: 9
                },
                relicAll: {
                    index: 105,
                    weight: 9
                },
                relicShop: {
                    index: 26,
                    weight: 9
                },
                relicFinal: {
                    index: 21,
                    weight: 9
                }
            }
        },
        {
            guid: "52d4e1bd77c84ec790991cf8e87d2ebc",
            name: "LuckyLockpick",
            display: "Lucky Lockpick",
            details: "A chance to not die, and no chance to break",
            rarity: "Legendary",
            crafting: 0,
            cost: 1300,
            extra: "Combined from Lockpick & Lucky Charm",
            tables: {}
        },
        {
            guid: "7b5f646318f3454dacb28fc75eb8c0f2",
            name: "Inverter",
            display: "Inverter",
            details: "Lost curses become blessings",
            rarity: "Rare",
            crafting: 25,
            cost: 475,
            extra: "",
            tables: {
                relic: {
                    index: 69,
                    weight: 2
                },
                relicAll: {
                    index: 101,
                    weight: 2
                }
            }
        },
        {
            guid: "82b826e6d3044504bfa46c4caa6f1b0b",
            name: "Recycler",
            display: "Recycler",
            details: "Destroy empty chests for items",
            rarity: "20",
            crafting: 20,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 83,
                    weight: 9
                },
                relicAll: {
                    index: 47,
                    weight: 9
                },
                relicShop: {
                    index: 35,
                    weight: 9
                }
            }
        },
        {
            guid: "0682d92aff14474d895c2e91f8826f25",
            name: "FanOfKnives",
            display: "Fan of Knives",
            details: "Release a spray of daggers when you throw",
            rarity: "Common",
            crafting: 16,
            cost: 500,
            extra: "",
            tables: {
                relic: {
                    index: 70,
                    weight: 9
                },
                relicAll: {
                    index: 90,
                    weight: 9
                }
            }
        },
        {
            guid: "f5e0082e84c74286829fa52bf633cbeb",
            name: "KurtzStache",
            display: "Kurtz' Stache",
            details: "A mysterious box that invites calamity",
            rarity: "Common",
            crafting: 0,
            cost: 200,
            extra: "",
            tables: {
                relic: {
                    index: 66,
                    weight: 9
                },
                relicAll: {
                    index: 60,
                    weight: 9
                },
                relicShop: {
                    index: 25,
                    weight: 9
                }
            }
        },
        {
            guid: "b7a8798a25ca4137a95538358085f2f4",
            name: "GlassCannon",
            display: "Glass Cannon",
            details: "Increases damage, but decreases maximum health",
            rarity: "Legendary",
            crafting: 0,
            cost: 775,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 10,
                    weight: 1
                }
            }
        },
        {
            guid: "2e8dd836a09f4e47b57e23b4d00f16ec",
            name: "SoulCannon",
            display: "Soul Cannon",
            details: "Fire a projectile while swinging",
            rarity: "Rare",
            crafting: 0,
            cost: 550,
            extra: "",
            tables: {
                relic: {
                    index: 98,
                    weight: 3
                },
                relicAll: {
                    index: 109,
                    weight: 3
                },
                relicFinal: {
                    index: 29,
                    weight: 3
                }
            }
        },
        {
            guid: "ecc0480e43924477ad68b3e9beb631b3",
            name: "Ara",
            display: "Ara",
            details: "Consumes a future curse after praying",
            rarity: "Common",
            crafting: 26,
            cost: 425,
            extra: "",
            tables: {
                relic: {
                    index: 105,
                    weight: 9
                },
                relicAll: {
                    index: 116,
                    weight: 9
                },
                relicShop: {
                    index: 48,
                    weight: 9
                }
            }
        },
        {
            guid: "6ca902940c004d7c957fe367f77e7337",
            name: "UrsaMajor",
            display: "Ursa Major",
            details: "Increases max health after eating food",
            rarity: "Common",
            crafting: 0,
            cost: 400,
            extra: "",
            tables: {
                relic: {
                    index: 106,
                    weight: 9
                },
                relicAll: {
                    index: 117,
                    weight: 9
                }
            }
        },
        {
            guid: "4ca66dcd2df34cbcbef82e2c5cafd5fc",
            name: "CanisMajor",
            display: "Canis Major",
            details: "Crits temporarily increase crit chance",
            rarity: "Common",
            crafting: 0,
            cost: 300,
            extra: "",
            tables: {
                relic: {
                    index: 107,
                    weight: 9
                },
                relicAll: {
                    index: 118,
                    weight: 9
                },
                relicFinal: {
                    index: 31,
                    weight: 9
                }
            }
        },
        {
            guid: "993daf5814304f9da36d0080fe4b9cbc",
            name: "Sagitta",
            display: "Sagitta",
            details: "Enemies explode in arrows after a critical strike",
            rarity: "Common",
            crafting: 0,
            cost: 325,
            extra: "",
            tables: {
                relic: {
                    index: 108,
                    weight: 9
                },
                relicAll: {
                    index: 119,
                    weight: 9
                }
            }
        },
        {
            guid: "7aedab48d5ca4e5b8d9f33a292ef38ef",
            name: "Circinus",
            display: "Circinus",
            details: "Temporarily reveals secret rooms, secret rooms can have secret rooms",
            rarity: "Common",
            crafting: 0,
            cost: 380,
            extra: "",
            tables: {
                relic: {
                    index: 109,
                    weight: 9
                },
                relicAll: {
                    index: 120,
                    weight: 9
                },
                relicShop: {
                    index: 49,
                    weight: 9
                }
            }
        },
        {
            guid: "1981b4af04434077afafc78691056387",
            name: "CursedAegis",
            display: "Siegfried's Aegis",
            details: "Inflicts pain on the bearer",
            rarity: "Common",
            crafting: 0,
            cost: 1,
            extra: "",
            tables: {}
        },
        {
            guid: "a6587dfd53e54a20ab1a167bc2b40e4a",
            name: "PaladinShield",
            display: "Paladin Shield",
            details: "Siegfried's shield, rid of its agonizing curse. Purified by a nameless wanderer. His legacy redeemed.",
            rarity: "Rare",
            crafting: 1,
            cost: 1000,
            extra: "",
            tables: {
                relic: {
                    index: 96,
                    weight: 2
                },
                relicAll: {
                    index: 51,
                    weight: 2
                },
                relicFinal: {
                    index: 28,
                    weight: 6
                }
            }
        },
        {
            guid: "1981b4af04434077afafc78691056387",
            name: "HotCrossBun",
            display: "Hot Cross Bun",
            details: "Increases maximum health, very slightly",
            rarity: "Common",
            crafting: 0,
            cost: 5,
            extra: "",
            tables: {}
        },
        {
            guid: "898d87f98b184697ac89672f9bf955e4",
            name: "CreditCardSilver",
            display: "Pilfer Credit Card Silver",
            details: "A Gold10.png 2,500 limit at 0% interest",
            rarity: "Common",
            crafting: 0,
            cost: 1,
            extra: "",
            tables: {
                relicShop: {
                    index: 40,
                    weight: 2
                }
            }
        },
        {
            guid: "62b6f012430249ce8befe02ab52482db",
            name: "MagnetizedOre",
            display: "Magnetized Ore",
            details: "Teleports enemies to bombs",
            rarity: "Common",
            crafting: 25,
            cost: 350,
            extra: "",
            tables: {
                relic: {
                    index: 113,
                    weight: 9
                },
                relicAll: {
                    index: 123,
                    weight: 9
                },
                relicShop: {
                    index: 55,
                    weight: 9
                }
            }
        },
        {
            guid: "e7cb0d0449d640baa7d05b92994d34ce",
            name: "OrionsSword",
            display: "Orion's Sword",
            details: "Gives bonus damage when hit",
            rarity: "Common",
            crafting: 0,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 110,
                    weight: 9
                },
                relicAll: {
                    index: 124,
                    weight: 9
                }
            }
        },
        {
            guid: "872540dbf7be4f2c9ce3a79f346c41a7",
            name: "LuckyNugget",
            display: "Lucky Nugget",
            details: "Spawns gold on crits",
            rarity: "Rare",
            crafting: 45,
            cost: 600,
            extra: "",
            tables: {
                relic: {
                    index: 114,
                    weight: 3
                },
                relicAll: {
                    index: 123,
                    weight: 3
                },
                relicShop: {
                    index: 54,
                    weight: 3
                }
            }
        },
        {
            guid: "51be11a14af842569bf13dd47d53fd84",
            name: "TwinAxe",
            display: "Twin Axe",
            details: "Throw a second pickaxe",
            rarity: "Rare",
            crafting: 65,
            cost: 725,
            extra: "",
            tables: {
                relic: {
                    index: 115,
                    weight: 3
                },
                relicAll: {
                    index: 126,
                    weight: 3
                },
                relicShop: {
                    index: 57,
                    weight: 1
                }
            }
        },
        {
            guid: "a4186f3d71cb4706b4884f6efcf186dc",
            name: "TangleGuard",
            display: "Tortoise Shield",
            details: "Slows enemy projectiles",
            rarity: "Common",
            crafting: 0,
            cost: 375,
            extra: "",
            tables: {
                relic: {
                    index: 116,
                    weight: 9
                },
                relicAll: {
                    index: 127,
                    weight: 9
                },
                relicShop: {
                    index: 53,
                    weight: 9
                }
            }
        },
        {
            guid: "2f9b6e005bf64057936c3101264f812e",
            name: "Javelin",
            display: "Javelin",
            details: "Increases throw damage with distance",
            rarity: "Common",
            crafting: 35,
            cost: 475,
            extra: "",
            tables: {
                relic: {
                    index: 117,
                    weight: 9
                },
                relicAll: {
                    index: 128,
                    weight: 9
                },
                relicFinal: {
                    index: 32,
                    weight: 9
                }
            }
        },
        {
            guid: "53326db2732f4b32a8da1e63b6939fbe",
            name: "GoldenAxe",
            display: "Golden Axe",
            details: "Increases damage with gold spent",
            rarity: "Rare",
            crafting: 0,
            cost: 700,
            extra: "",
            tables: {
                relic: {
                    index: 118,
                    weight: 3
                },
                relicAll: {
                    index: 129,
                    weight: 3
                }
            }
        },
        {
            guid: "43637fecaef7443482c195c01ede0a32",
            name: "Bounty",
            display: "Bounty Contract",
            details: "Adds a bounty to a random enemy",
            rarity: "Common",
            crafting: 30,
            cost: 425,
            extra: "",
            tables: {
                relic: {
                    index: 119,
                    weight: 9
                },
                relicAll: {
                    index: 130,
                    weight: 9
                },
                relicShop: {
                    index: 56,
                    weight: 9
                }
            }
        },
        {
            guid: "93272a3527ad43f7a68272a0fdb560a3",
            name: "SupplyCrate",
            display: "Box of Holding",
            details: "Pilfer shop is always restocked",
            rarity: "Legendary",
            crafting: 0,
            cost: 1500,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 17,
                    weight: 1
                },
            }
        },
        {
            guid: "e622f355e6a944ea9d65eb1603125429",
            name: "StoneforgeBroth",
            display: "Stoneforge Broth",
            details: "Grants a random legendary relic",
            rarity: "Legendary",
            crafting: 0,
            cost: 500,
            extra: "",
            tables: {
                relicLegendary: {
                    index: 18,
                    weight: 1
                }
            }
        }
    ],
    potion: [
        {
            guid: "f3c6fcf6f86a44178c4649f71fb5d18f",
            name: "TincturePotion",
            display: "Tincture",
            details: "Recovers health",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 1,
                    weight: 9
                },
                potionShop: {
                    index: 1,
                    weight: 10
                },
                potionDibble: {
                    index: 1,
                    weight: 9
                },
                potionFinal: {
                    index: 0,
                    weight: 9
                }
            }
        },
        {
            guid: "27b4611c53e04c0a9d771b76d47d7deb",
            name: "ApprenticesTincturePotion",
            display: "Apprentice's Tincture",
            details: "Recovers a random amount of health",
            rarity: "Common",
            crafting: 12,
            cost: 130,
            extra: "",
            tables: {
                potion: {
                    index: 24,
                    weight: 9
                },
                potionShop: {
                    index: 18,
                    weight: 9
                },
                potionDibble: {
                    index: 18,
                    weight: 9
                },
                potionFinal: {
                    index: 5,
                    weight: 9
                }
            }
        },
        {
            guid: "cea67360abf84347a264ef64ed806e58",
            name: "SalvagingSludgePotion",
            display: "Salvaging Sludge",
            details: "Recover health for each carried curse",
            rarity: "Common",
            crafting: 18,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 54,
                    weight: 9
                },
                potionShop: {
                    index: 38,
                    weight: 9
                }
            }
        },
        {
            guid: "bfe7c47be6fc4bdbb86e364a6b1cf076",
            name: "RegenPotion",
            display: "Troll Sweat",
            details: "Regenerate health over time",
            rarity: "Common",
            crafting: 0,
            cost: 200,
            extra: "",
            tables: {
                potion: {
                    index: 2,
                    weight: 7
                },
                potionShop: {
                    index: 2,
                    weight: 7
                },
                potionDibble: {
                    index: 2,
                    weight: 7
                },
                potionFinal: {
                    index: 1,
                    weight: 7
                }
            }
        },
        {
            guid: "d2c408e370584302b135fc159d6b0b25",
            name: "GhostlyIchorPotion",
            display: "Ghostly Ichor",
            details: "Heal over time for each curse",
            rarity: "Common",
            crafting: 18,
            cost: 200,
            extra: "",
            tables: {
                potion: {
                    index: 41,
                    weight: 9
                },
                potionShop: {
                    index: 28,
                    weight: 9
                },
                potionDibble: {
                    index: 31,
                    weight: 9
                }
            }
        },
        {
            guid: "db3a699d040b4fd6a4a64399f1275d61",
            name: "CureAllPotion",
            display: "Cure All",
            details: "Create a circle of healing",
            rarity: "Rare",
            crafting: 30,
            cost: 100,
            extra: "",
            tables: {
                potion: {
                    index: 40,
                    weight: 2
                },
                potionShop: {
                    index: 27,
                    weight: 2
                },
                potionDibble: {
                    index: 30,
                    weight: 2
                }
            }
        },
        {
            guid: "a3f7691e471243ed899c52b5ae1bc2de",
            name: "CoffeePotion",
            display: "Coffee",
            details: "Recover health proportional to missing health",
            rarity: "Common",
            crafting: 10,
            cost: 160,
            extra: "",
            tables: {
                potion: {
                    index: 45,
                    weight: 9
                },
                potionShop: {
                    index: 30,
                    weight: 9
                },
                potionDibble: {
                    index: 33,
                    weight: 9
                },
                potionFinal: {
                    index: 12,
                    weight: 9
                }
            }
        },
        {
            guid: "f84e9f7aa7b04c648a08c6b75eebaf2e",
            name: "ElixirPotion",
            display: "Elixir",
            details: "Restores all missing health",
            rarity: "Rare",
            crafting: 38,
            cost: 300,
            extra: "",
            tables: {
                potion: {
                    index: 22,
                    weight: 3
                },
                potionShop: {
                    index: 16,
                    weight: 3
                },
                potionDibble: {
                    index: 17,
                    weight: 3
                },
                potionFinal: {
                    index: 4,
                    weight: 3
                }
            }
        },
        {
            guid: "27497c3cdd91494ba066eb45ee59d528",
            name: "PartyPopcornPotion",
            display: "Rainbow Kernels",
            details: "Duplicate almost everything in the room",
            rarity: "Rare",
            crafting: 44,
            cost: 500,
            extra: "",
            tables: {
                potion: {
                    index: 18,
                    weight: 1
                }
            }
        },
        {
            guid: "0455a3d8711146e3a479e71d4d37954b",
            name: "GoldPopcornPotion",
            display: "Golden Kernels",
            details: "Duplicate all gold in the room",
            rarity: "Common",
            crafting: 16,
            cost: 100,
            extra: "",
            tables: {
                potion: {
                    index: 16,
                    weight: 2
                },
                potionShop: {
                    index: 12,
                    weight: 2
                },
                potionDibble: {
                    index: 14,
                    weight: 3
                }
            }
        },
        {
            guid: "dc421ae2fb974a01b8cc4b2a9093244e",
            name: "ItemPopcornPotion",
            display: "Popcorn Kernels",
            details: "Duplicate all basic items in the room",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 17,
                    weight: 7
                },
                potionShop: {
                    index: 13,
                    weight: 5
                }
            }
        },
        {
            guid: "908bacbc988a46559da7ef9f4fe8e042",
            name: "FoodPopcornPotion",
            display: "Seasoned Kernels",
            details: "Duplicate all food in the room",
            rarity: "Common",
            crafting: 15,
            cost: 110,
            extra: "",
            tables: {
                potion: {
                    index: 15,
                    weight: 7
                },
                potionShop: {
                    index: 11,
                    weight: 5
                },
                potionDibble: {
                    index: 13,
                    weight: 9
                }
            }
        },
        {
            guid: "5638e38a67e04f4eaec6473d5042890f",
            name: "WhiplashPotion",
            display: "Whiplash Serum",
            details: "Temporarily increases throw damage",
            rarity: "Common",
            crafting: 0,
            cost: 175,
            extra: "",
            tables: {
                potion: {
                    index: 26,
                    weight: 9
                },
                potionShop: {
                    index: 50,
                    weight: 6
                },
                potionDibble: {
                    index: 20,
                    weight: 9
                },
                potionFinal: {
                    index: 6,
                    weight: 9
                }
            }
        },
        {
            guid: "fb789e8f36a349f1a71ceb2e9c1e3f46",
            name: "StrengthPotion",
            display: "Strength Serum",
            details: "Temporarily increases swing damage",
            rarity: "Common",
            crafting: 0,
            cost: 175,
            extra: "",
            tables: {
                potion: {
                    index: 27,
                    weight: 9
                },
                potionShop: {
                    index: 47,
                    weight: 6
                },
                potionDibble: {
                    index: 21,
                    weight: 9
                },
                potionFinal: {
                    index: 7,
                    weight: 9
                }
            }
        },
        {
            guid: "5178abb3de614cb59411b8144fd2dc32",
            name: "SavagryPotion",
            display: "Savagery Serum",
            details: "Temporarily increases critical chance",
            rarity: "Common",
            crafting: 0,
            cost: 175,
            extra: "",
            tables: {
                potion: {
                    index: 28,
                    weight: 9
                },
                potionShop: {
                    index: 46,
                    weight: 6
                },
                potionDibble: {
                    index: 22,
                    weight: 9
                },
                potionFinal: {
                    index: 8,
                    weight: 9
                }
            }
        },
        {
            guid: "00772a2e1a7d4755a76e8183b1d6b0ce",
            name: "AlacrityPotion",
            display: "Alacrity Serum",
            details: "Temporarily increases attack speed",
            rarity: "Common",
            crafting: 0,
            cost: 175,
            extra: "",
            tables: {
                potion: {
                    index: 29,
                    weight: 9
                },
                potionShop: {
                    index: 42,
                    weight: 6
                },
                potionDibble: {
                    index: 23,
                    weight: 9
                },
                potionFinal: {
                    index: 9,
                    weight: 9
                }
            }
        },
        {
            guid: "44cd9c54d22c4ab289b5edf7b4fe40f4",
            name: "CleaveSerumPotion",
            display: "Sundering Serum",
            details: "Temporarily increases swing size",
            rarity: "Common",
            crafting: 0,
            cost: 200,
            extra: "",
            tables: {
                potion: {
                    index: 50,
                    weight: 9
                },
                potionShop: {
                    index: 34,
                    weight: 6
                },
                potionDibble: {
                    index: 34,
                    weight: 9
                },
                potionFinal: {
                    index: 13,
                    weight: 9
                }
            }
        },
        {
            guid: "fe0fbfc6bceb4a2f91ea280ba82f7d38",
            name: "GustSerumPotion",
            display: "Cyclonic Serum",
            details: "Temporarily increases throw size",
            rarity: "Common",
            crafting: 0,
            cost: 200,
            extra: "",
            tables: {
                potion: {
                    index: 51,
                    weight: 9
                },
                potionShop: {
                    index: 35,
                    weight: 6
                },
                potionDibble: {
                    index: 35,
                    weight: 9
                },
                potionFinal: {
                    index: 14,
                    weight: 9
                }
            }
        },
        {
            guid: "617b7758377d4230a24823645d3898f4",
            name: "HealthSerumPotion",
            display: "Durability Serum",
            details: "",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 52,
                    weight: 9
                },
                potionShop: {
                    index: 36,
                    weight: 6
                },
                potionDibble: {
                    index: 36,
                    weight: 9
                },
                potionFinal: {
                    index: 15,
                    weight: 9
                }
            }
        },
        {
            guid: "df251e38396f44119823faa3840db809",
            name: "DecursePotion",
            display: "Holy Water",
            details: "Remove a curse",
            rarity: "Common",
            crafting: 0,
            cost: 325,
            extra: "",
            tables: {
                potion: {
                    index: 14,
                    weight: 9
                },
                potionShop: {
                    index: 10,
                    weight: 9
                },
                potionDibble: {
                    index: 12,
                    weight: 9
                }
            }
        },
        {
            guid: "e99410fecfac4015b5504ec3b87b5cd0",
            name: "PurgePotion",
            display: "Purge Potion",
            details: "Removes a curse and deals 75 damage.",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 35,
                    weight: 9
                },
                potionShop: {
                    index: 21,
                    weight: 9
                },
                potionDibble: {
                    index: 27,
                    weight: 9
                }
            }
        },
        {
            guid: "2d9d51a29ebe40a99d37445de1954951",
            name: "PurificationPotion",
            display: "Purification Potion",
            details: "Removes all curses, bombs, keys, and sets health to 1",
            rarity: "Rare",
            crafting: 0,
            cost: 1000,
            extra: "",
            tables: {
                potion: {
                    index: 39,
                    weight: 1
                },
                potionShop: {
                    index: 26,
                    weight: 1
                }
            }
        },
        {
            guid: "7ca4f0f6567c4b348fd6ccae072dbded",
            name: "Aether",
            display: "Aether",
            details: "Remove a specific curse",
            rarity: "Common",
            crafting: 80,
            cost: 600,
            extra: "",
            tables: {
                potion: {
                    index: 49,
                    weight: 2
                },
                potionShop: {
                    index: 33,
                    weight: 2
                }
            }
        },
        {
            guid: "28737e052b414273a67aca33267fc2e7",
            name: "Absolution",
            display: "Absolution",
            details: "If you have exactly 5 curses, removes 5 curses",
            rarity: "Common",
            crafting: 0,
            cost: 800,
            extra: "",
            tables: {
                potion: {
                    index: 38,
                    weight: 2
                },
                potionShop: {
                    index: 25,
                    weight: 2
                },
                potionDibble: {
                    index: 29,
                    weight: 2
                }
            }
        },
        {
            guid: "b0dd99d6f5184fb49df441af4bf298a5",
            name: "BombDoublingPotion",
            display: "Doubling Saison",
            details: "Double your bombs",
            rarity: "Common",
            crafting: 0,
            cost: 220,
            extra: "",
            tables: {
                potion: {
                    index: 13,
                    weight: 3
                },
                potionShop: {
                    index: 9,
                    weight: 3
                },
                potionDibble: {
                    index: 11,
                    weight: 3
                }
            }
        },
        {
            guid: "f34771cdd3514f379f42355fb1b4a41d",
            name: "SwapPotion",
            display: "Impish Key Bomb",
            details: "Swap your items around",
            rarity: "Common",
            crafting: 0,
            cost: 240,
            extra: "",
            tables: {
                potion: {
                    index: 9,
                    weight: 9
                },
                potionShop: {
                    index: 6,
                    weight: 9
                },
                potionDibble: {
                    index: 7,
                    weight: 9
                }
            }
        },
        {
            guid: "20d260544d224607b1f2363d06def2a0",
            name: "IronGlazePotion",
            display: "Iron Glaze",
            details: "Average the number of held keys and bombs",
            rarity: "Rare",
            crafting: 10,
            cost: 175,
            extra: "",
            tables: {
                potion: {
                    index: 47,
                    weight: 9
                },
                potionShop: {
                    index: 32,
                    weight: 9
                }
            }
        },
        {
            guid: "2caf27e525fa4787bd629282ef202094",
            name: "HolyGlaze",
            display: "Holy Glaze",
            details: "Average the levels of all blessings",
            rarity: "Common",
            crafting: 20,
            cost: 275,
            extra: "",
            tables: {
                potion: {
                    index: 58,
                    weight: 9
                },
                potionShop: {
                    index: 41,
                    weight: 9
                },
                potionDibble: {
                    index: 37,
                    weight: 9
                }
            }
        },
        {
            guid: "15ca0afa442849328a5a8ff6411b301a",
            name: "AmbrosiaPotion",
            display: "Ambrosia",
            details: "Double the level of a random blessing",
            rarity: "Rare",
            crafting: 38,
            cost: 375,
            extra: "",
            tables: {
                potion: {
                    index: 57,
                    weight: 3
                },
                potionShop: {
                    index: 40,
                    weight: 3
                }
            }
        },
        {
            guid: "87304ff317824d638d8c2a591b5ce37b",
            name: "NitroglycerinPotion",
            display: "Nitroglycerin",
            details: "Drop bombs continuously",
            rarity: "Common",
            crafting: 0,
            cost: 220,
            extra: "",
            tables: {
                potion: {
                    index: 12,
                    weight: 9
                },
                potionShop: {
                    index: 8,
                    weight: 9
                },
                potionDibble: {
                    index: 10,
                    weight: 9
                }
            }
        },
        {
            guid: "1c0df4b6c9f2494c8eb2376ec97ca4fb",
            name: "AuglycerinPotion",
            display: "Auglycerin",
            details: "Drop gold continuously",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 19,
                    weight: 9
                },
                potionShop: {
                    index: 43,
                    weight: 9
                },
                potionDibble: {
                    index: 15,
                    weight: 9
                }
            }
        },
        {
            guid: "4673c7f91a7b4d35b8ebe30a16ce650a",
            name: "ImmolatePotion",
            display: "Immolation Potion",
            details: "Burns nearby enemies",
            rarity: "Common",
            crafting: 10,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 11,
                    weight: 9
                },
                potionShop: {
                    index: 52,
                    weight: 9
                },
                potionDibble: {
                    index: 9,
                    weight: 9
                }
            }
        },
        {
            guid: "d42df1015fa04546bfd1cc0958cfc34b",
            name: "FloatPotion",
            display: "Float Potion",
            details: "Avoid falling into holes",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 10,
                    weight: 9
                },
                potionShop: {
                    index: 7,
                    weight: 9
                },
                potionDibble: {
                    index: 8,
                    weight: 9
                }
            }
        },
        {
            guid: "b994f67a8e3f44578e1899cd043e3997",
            name: "SeltsBlood",
            display: "Selt's Blood",
            details: "Spawn larvae",
            rarity: "Rare",
            crafting: 10,
            cost: 220,
            extra: "",
            tables: {
                potion: {
                    index: 33,
                    weight: 3
                },
                potionShop: {
                    index: 53,
                    weight: 3
                },
                potionDibble: {
                    index: 26,
                    weight: 3
                }
            }
        },
        {
            guid: "0b736c7fe4974e65a08cd5c0ab189929",
            name: "BloodChalice",
            display: "Blood Chalice",
            details: "Consumes 25% health, drops items and sometimes another blood chalice.",
            rarity: "Rare",
            crafting: 50,
            cost: 290,
            extra: "",
            tables: {
                potion: {
                    index: 37,
                    weight: 3
                },
                potionShop: {
                    index: 24,
                    weight: 3
                }
            }
        },
        {
            guid: "12784fa0c4a949fdadf21288bc5ec170",
            name: "FireballPotion",
            display: "Fury Potion",
            details: "Fire some fireballs",
            rarity: "Common",
            crafting: 0,
            cost: 100,
            extra: "",
            tables: {
                potion: {
                    index: 8,
                    weight: 9
                },
                potionShop: {
                    index: 44,
                    weight: 9
                },
                potionDibble: {
                    index: 6,
                    weight: 9
                }
            }
        },
        {
            guid: "5f8cb66127e74d0baff7a1ba5b4cf972",
            name: "ReverseDamagePotion",
            display: "Antimatter",
            details: "The next time you would take damage, gain that much health instead",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 7,
                    weight: 9
                },
                potionShop: {
                    index: 5,
                    weight: 9
                },
                potionDibble: {
                    index: 5,
                    weight: 9
                },
                potionFinal: {
                    index: 3,
                    weight: 9
                }
            }
        },
        {
            guid: "05572761ed6d4d4f91203cd46f890adf",
            name: "PickledPilferPotion",
            display: "Bottled Pilfer",
            details: "Releases a Hoarding Pilfer",
            rarity: "Rare",
            crafting: 22,
            cost: 500,
            extra: "",
            tables: {
                potion: {
                    index: 6,
                    weight: 3
                },
                potionShop: {
                    index: 4,
                    weight: 3
                }
            }
        },
        {
            guid: "bcfda0149d944afcb246305be2695172",
            name: "ShopPotion",
            display: "Shop in a Bottle",
            details: "Discover the secret shop",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 4,
                    weight: 3
                },
                potionShop: {
                    index: 22,
                    weight: 3
                }
            }
        },
        {
            guid: "f83096ef1b674f6c832dec65a4a3d758",
            name: "PotionPotion",
            display: "Bottles in a Bottle",
            details: "Drop two potions",
            rarity: "Rare",
            crafting: 0,
            cost: 300,
            extra: "",
            tables: {
                potion: {
                    index: 48,
                    weight: 3
                },
                potionShop: {
                    index: 45,
                    weight: 3
                }
            }
        },
        {
            guid: "15c7f082e7454fc38695e6152782bbf9",
            name: "ChestInABottle",
            display: "Chest in a Bottle",
            details: "Drop a random chest",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 20,
                    weight: 9
                },
                potionShop: {
                    index: 14,
                    weight: 9
                }
            }
        },
        {
            guid: "cda68d3762ef4aa6bc5b00e2f8eeec6d",
            name: "TransmuterInABottle",
            display: "TRANSMUT3 in a Bottle",
            details: "Add a TRANSMUT3 to the room",
            rarity: "Common",
            crafting: 26,
            cost: 300,
            extra: "",
            tables: {
                potion: {
                    index: 34,
                    weight: 9
                }
            }
        },
        {
            guid: "f05bebbd21e04c468299c7763704f332",
            name: "AltarInABottle",
            display: "Altar in a Bottle",
            details: "Spawn an altar",
            rarity: "Common",
            crafting: 35,
            cost: 200,
            extra: "",
            tables: {
                potion: {
                    index: 46,
                    weight: 9
                },
                potionShop: {
                    index: 31,
                    weight: 9
                }
            }
        },
        {
            guid: "6da20a3b58c3488c8b764ae90689cb04",
            name: "DopplePotion",
            display: "Doppelbock",
            details: "Summon a doppelganger to work alongside you",
            rarity: "Common",
            crafting: 0,
            cost: 300,
            extra: "",
            tables: {
                potion: {
                    index: 67,
                    weight: 9
                },
                potionShop: {
                    index: 58,
                    weight: 9
                },
                potionDibble: {
                    index: 41,
                    weight: 1
                },
                potionFinal: {
                    index: 19,
                    weight: 9
                }
            }
        },
        {
            guid: "b12beb42d1b549e392b00d00a01520c9",
            name: "Transmutagen",
            display: "Transmutagen",
            details: "Transforms all relics in the room",
            rarity: "Rare",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 5,
                    weight: 3
                },
                potionShop: {
                    index: 3,
                    weight: 9
                },
                potionDibble: {
                    index: 4,
                    weight: 3
                }
            }
        },
        {
            guid: "a72e7b460e8649b08298d8c4bf15f349",
            name: "SwapRelicPotion",
            display: "Metamorphim",
            details: "Transmute a carried relic",
            rarity: "Rare",
            crafting: 0,
            cost: 500,
            extra: "",
            tables: {
                potion: {
                    index: 23,
                    weight: 2
                }
            }
        },
        {
            guid: "06eea0e248e74a249d1fe9b3c92d0b70",
            name: "SwapAllRelicPotion",
            display: "Mighty Metamorphim",
            details: "Transmute all carried relics",
            rarity: "Rare",
            crafting: 50,
            cost: 1000,
            extra: "",
            tables: {
                potion: {
                    index: 30,
                    weight: 3
                },
                potionShop: {
                    index: 17,
                    weight: 3
                },
                potionDibble: {
                    index: 24,
                    weight: 3
                }
            }
        },
        {
            guid: "8a91e7875f294cb280f34898b5594f2d",
            name: "CircleTransmutePotion",
            display: "Circle of Transmutation",
            details: "Transmutes items in a small circle, but makes them fragile",
            rarity: "Rare",
            crafting: 85,
            cost: 360,
            extra: "",
            tables: {
                potion: {
                    index: 55,
                    weight: 3
                },
                potionShop: {
                    index: 51,
                    weight: 3
                }
            }
        },
        {
            guid: "200012b4fdb74289b228f292193cf146",
            name: "UnholyStrengthPotion",
            display: "Berserker's Brew",
            details: "Deal and take more damage",
            rarity: "Common",
            crafting: 0,
            cost: 175,
            extra: "",
            tables: {
                potion: {
                    index: 3,
                    weight: 9
                },
                potionShop: {
                    index: 49,
                    weight: 9
                },
                potionDibble: {
                    index: 3,
                    weight: 9
                },
                potionFinal: {
                    index: 2,
                    weight: 9
                }
            }
        },
        {
            guid: "b9c7849437254fcbbe60ab61143f1de5",
            name: "GhostPepperSauce",
            display: "Ghost Pepper Sauce",
            details: "Become immune to fire damage and ignite yourself",
            rarity: "Common",
            crafting: 15,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 43,
                    weight: 9
                },
                potionShop: {
                    index: 29,
                    weight: 9
                },
                potionDibble: {
                    index: 32,
                    weight: 9
                }
            }
        },
        {
            guid: "2849e0d8002e4800936de05abbde0ed7",
            name: "ToadvinesTonic",
            display: "Toadvine's Tonic",
            details: "Become immune to poison, if you can survive it",
            rarity: "Common",
            crafting: 15,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 66,
                    weight: 9
                },
                potionShop: {
                    index: 57,
                    weight: 9
                },
                potionDibble: {
                    index: 40,
                    weight: 1
                }
            }
        },
        {
            guid: "9c0401cc3c9a4188a6b3c800215b557d",
            name: "NumbingCream",
            display: "Numbing Cream",
            details: "Reduce incoming damage",
            rarity: "Rare",
            crafting: 26,
            cost: 375,
            extra: "",
            tables: {
                potion: {
                    index: 53,
                    weight: 3
                },
                potionShop: {
                    index: 37,
                    weight: 3
                },
                potionFinal: {
                    index: 16,
                    weight: 3
                }
            }
        },
        {
            guid: "e50bae2f580c4159a881f6c7852ca586",
            name: "EagleEyePotion",
            display: "Potion of True Sight",
            details: "Discover nearby secrets",
            rarity: "Common",
            crafting: 8,
            cost: 140,
            extra: "",
            tables: {
                potion: {
                    index: 0,
                    weight: 9
                },
                potionShop: {
                    index: 0,
                    weight: 9
                },
                potionDibble: {
                    index: 0,
                    weight: 9
                }
            }
        },
        {
            guid: "8372b8d3152445afa410b7bb9dbd45a8",
            name: "PotionofPlenty",
            display: "Potion of Plenty",
            details: "Drop some useful things",
            rarity: "Common",
            crafting: 0,
            cost: 210,
            extra: "",
            tables: {
                potion: {
                    index: 21,
                    weight: 9
                },
                potionShop: {
                    index: 15,
                    weight: 9
                },
                potionDibble: {
                    index: 16,
                    weight: 9
                }
            }
        },
        {
            guid: "84e344cdca10420a9db29efbd3d1dc72",
            name: "ProteinPotion",
            display: "Protein Shake",
            details: "Drop some protein",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                potion: {
                    index: 32,
                    weight: 9
                },
                potionShop: {
                    index: 20,
                    weight: 9
                },
                potionDibble: {
                    index: 25,
                    weight: 9
                },
                potionFinal: {
                    index: 10,
                    weight: 9
                }
            }
        },
        {
            guid: "a716c2ee1f634d74af7cc84ff341d374",
            name: "BlessedBlend",
            display: "Blessed Blend",
            details: "Drops a blessing",
            rarity: "Common",
            crafting: 16,
            cost: 275,
            extra: "",
            tables: {
                potion: {
                    index: 42,
                    weight: 7
                },
                potionFinal: {
                    index: 11,
                    weight: 7
                }
            }
        },
        {
            guid: "4bbe9f7680b34a17a1ca6dfc90c725e7",
            name: "ToxinPotion",
            display: "Toxin",
            details: "Coat your weapon with poison",
            rarity: "Common",
            crafting: 0,
            cost: 175,
            extra: "",
            tables: {
                potion: {
                    index: 25,
                    weight: 9
                },
                potionShop: {
                    index: 48,
                    weight: 9
                },
                potionDibble: {
                    index: 19,
                    weight: 9
                }
            }
        },
        {
            guid: "c0a32814b2e24d15a63664bcdc949d26",
            name: "WitchsBrew",
            display: "Witch's Brew",
            details: "Become cursed",
            rarity: "Rare",
            crafting: 0,
            cost: 500,
            extra: "",
            tables: {
                potion: {
                    index: 64,
                    weight: 0
                }
            }
        },
        {
            guid: "145bde979116479fabc648bebf584e32",
            name: "FreeloaderPotion",
            display: "Freeloader Draught",
            details: "Get something for nothing",
            rarity: "Rare",
            crafting: 0,
            cost: 400,
            extra: "",
            tables: {
                potion: {
                    index: 31,
                    weight: 2
                },
                potionShop: {
                    index: 19,
                    weight: 2
                }
            }
        },
        {
            guid: "6a41bdb034dd45549825a7b4f9198362",
            name: "BiscuitsPotion",
            display: "Biscuits",
            details: "Increse experience gain for a familiar.",
            rarity: "Common",
            crafting: 0,
            cost: 300,
            extra: "",
            tables: {
                potion: {
                    index: 36,
                    weight: 9
                },
                potionShop: {
                    index: 23,
                    weight: 9
                },
                potionDibble: {
                    index: 28,
                    weight: 9
                }
            }
        },
        {
            guid: "17392f3e7d05485a800ddcd201bfb98c",
            name: "MidasTouchPotion",
            display: "Midas Touch",
            details: "All enemies are turned to gold",
            rarity: "Rare",
            crafting: 20,
            cost: 375,
            extra: "",
            tables: {
                potion: {
                    index: 44,
                    weight: 3
                }
            }
        },
        {
            guid: "4f9154518bfe4a0ebd09e57aef8ebdf9",
            name: "KissSuccubusPotion",
            display: "Kiss of the Succubus",
            details: "Kill all enemies and steal their health",
            rarity: "Rare",
            crafting: 20,
            cost: 450,
            extra: "",
            tables: {
                potion: {
                    index: 56,
                    weight: 3
                },
                potionShop: {
                    index: 39,
                    weight: 3
                }
            }
        },
        {
            guid: "5829b0cc3f6149da9649aa302cf7ca57",
            name: "AllPotion",
            display: "All-Potion",
            details: "Is whatever you need it to be",
            rarity: "Rare",
            crafting: 99,
            cost: 500,
            extra: "",
            tables: {
                potion: {
                    index: 59,
                    weight: 2
                }
            }
        },
        {
            guid: "ee6c6499fedf45d891ce5c3ee09b2da3",
            name: "SomePotion",
            display: "Some-Potion",
            details: "Is whatever you need it to be, sometimes",
            rarity: "Rare",
            crafting: 98,
            cost: 400,
            extra: "",
            tables: {
                potion: {
                    index: 60,
                    weight: 3
                }
            }
        },
        {
            guid: "1cfe53e981a24b7cbbad91669f5ac52e",
            name: "StarlightSip",
            display: "Starlight Sip",
            details: "Pull the heavens down onto your foes",
            rarity: "Common",
            crafting: 28,
            cost: 500,
            extra: "",
            tables: {
                potion: {
                    index: 61,
                    weight: 6
                },
                potionShop: {
                    index: 54,
                    weight: 3
                },
                potionDibble: {
                    index: 38,
                    weight: 3
                },
                potionFinal: {
                    index: 17,
                    weight: 6
                }
            }
        },
        {
            guid: "43f16c0b6aa94743a87c235025a7f98d",
            name: "ChurchbellNectarPotion",
            display: "Churchbell Nectar",
            details: "Creates a temporary Churchbell shield",
            rarity: "Common",
            crafting: 16,
            cost: 300,
            extra: "",
            tables: {
                potion: {
                    index: 62,
                    weight: 9
                },
                potionShop: {
                    index: 55,
                    weight: 6
                },
                potionDibble: {
                    index: 39,
                    weight: 6
                },
                potionFinal: {
                    index: 18,
                    weight: 9
                }
            }
        },
        {
            guid: "4c8d7e3454124c4aab32c45bf05edc8b",
            name: "PangolinPotion",
            display: "Pangolin Potion",
            details: "Refills four armor points",
            rarity: "Common",
            crafting: 0,
            cost: 450,
            extra: "",
            tables: {
                potion: {
                    index: 63,
                    weight: 6
                },
                potionShop: {
                    index: 56,
                    weight: 6
                }
            }
        }
    ],
    item: [
        {
            guid: "8d0acbcdb33a46a9bfa9c623e8be72e6",
            name: "Bomb",
            display: "Bomb",
            details: "Deals damage and destroys rocks, as well as other objects",
            rarity: "Common",
            crafting: 0,
            cost: 50,
            extra: "",
            tables: {
                item: {
                    index: 0,
                    weight: 20
                },
                itemShop: {
                    index: 0,
                    weight: 20
                },
                itemDibble: {
                    index: 0,
                    weight: 9
                }
            }
        },
        {
            guid: "0f53c763ed3b4c77bceaa4853f95ead0",
            name: "BombBag",
            display: "Bag O' Bombs",
            details: "Full of bombs",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                item: {
                    index: 3,
                    weight: 2
                },
                itemShop: {
                    index: 3,
                    weight: 2
                },
                itemDibble: {
                    index: 2,
                    weight: 1
                }
            }
        },
        {
            guid: "c82f660a0fbd4eb983e98d5896253e09",
            name: "Key",
            display: "Key",
            details: "Unlocks chests, doors and other locked objects",
            rarity: "Common",
            crafting: 0,
            cost: 120,
            extra: "",
            tables: {
                item: {
                    index: 1,
                    weight: 15
                },
                itemShop: {
                    index: 1,
                    weight: 15
                },
                itemDibble: {
                    index: 1,
                    weight: 9
                }
            }
        },
        {
            guid: "32d2db84cca547699565e949c6815f06",
            name: "KeyRing",
            display: "Key Ring",
            details: "Full of keys",
            rarity: "Common",
            crafting: 0,
            cost: 240,
            extra: "",
            tables: {
                item: {
                    index: 4,
                    weight: 1
                },
                itemShop: {
                    index: 4,
                    weight: 1
                },
                itemDibble: {
                    index: 3,
                    weight: 1
                }
            }
        },
        {
            guid: "dd61c0367bbf41f9bb86a85c1e7b0aae",
            name: "Talisman",
            display: "Talisman",
            details: "Removes a curse",
            rarity: "Common",
            crafting: 0,
            cost: 300,
            extra: "",
            tables: {
                item: {
                    index: 2,
                    weight: 4
                },
                itemShop: {
                    index: 2,
                    weight: 4
                }
            }
        }
    ],
    food: [
        {
            guid: "3771da94280f49959634ebc9bc7098a4",
            name: "HealthS",
            display: "Medium Rare Steak",
            details: "Restores a small amount of health",
            rarity: "Common",
            crafting: 0,
            cost: 50,
            extra: "",
            tables: {
                food: {
                    index: 0,
                    weight: 15
                }
            }
        },
        {
            guid: "5d1d5865fd7b4353b3b43fe0d81b7960",
            name: "HealthM",
            display: "Fish Kebab",
            details: "Restores a moderate amount of health",
            rarity: "Common",
            crafting: 0,
            cost: 200,
            extra: "",
            tables: {
                food: {
                    index: 1,
                    weight: 4
                }
            }
        },
        {
            guid: "e218308da36941de8705ba950e0455f3",
            name: "HealthL",
            display: "Ham Shank",
            details: "Restores a large amount of health",
            rarity: "Common",
            crafting: 0,
            cost: 300,
            extra: "",
            tables: {
                food: {
                    index: 2,
                    weight: 1
                }
            }
        },
        {
            guid: "c0ee06997e624cd88c68171930159fa1",
            name: "ArmorShard",
            display: "Armor Shard",
            details: "Refills a point of armor",
            rarity: "Common",
            crafting: 0,
            cost: 150,
            extra: "",
            tables: {
                food: {
                    index: 3,
                    weight: 5
                }
            }
        }
    ],
    weightedTables: [
        {
            key: "itemDibble",
            type: "item",
            randState: "dibble",
            tables: {
                dibble: {
                    index: 0,
                    weight: 5,
                },
                dibbleRelic: {
                    index: 0,
                    weight: 5
                }
            }
        },
        {
            key: "potionDibble",
            type: "potion",
            randState: "dibble",
            tables: {
                dibble: {
                    index: 1,
                    weight: 2
                },
                dibbleRelic: {
                    index: 1,
                    weight: 2
                }
            }
        },
        {
            key: "relic",
            type: "relic",
            randState: "relic",
            tables: {
                dibbleRelic: {
                    index: 2,
                    weight: 1
                }
            }
        },
        {
            key: "potionShop",
            type: "potion",
            randState: "shopPotion",
            tables: {
                potionOrRelic: {
                    index: 0,
                    weight: 3
                }
            }
        },
        {
            key: "relicShop",
            type: "relic",
            randState: "shopRelic",
            tables: {
                potionOrRelic: {
                    index: 1,
                    weight: 1
                }
            }
        }
    ]
};
var direction;
(function (direction) {
    direction[direction["none"] = 0] = "none";
    direction[direction["north"] = 1] = "north";
    direction[direction["east"] = 2] = "east";
    direction[direction["south"] = 4] = "south";
    direction[direction["west"] = 8] = "west";
    direction[direction["ne"] = 3] = "ne";
    direction[direction["ns"] = 5] = "ns";
    direction[direction["nw"] = 9] = "nw";
    direction[direction["nes"] = 7] = "nes";
    direction[direction["new"] = 11] = "new";
    direction[direction["nsw"] = 13] = "nsw";
    direction[direction["es"] = 6] = "es";
    direction[direction["ew"] = 10] = "ew";
    direction[direction["esw"] = 14] = "esw";
    direction[direction["sw"] = 12] = "sw";
    direction[direction["block"] = 15] = "block";
})(direction || (direction = {}));
const cardinalDirections = [
    1,
    4,
    2,
    8,
];
function allowNeighbor(enc1, enc2) {
    if (enc1.noExit && enc2.noExit) {
        return (enc1.noExit | enc2.noExit) != 15;
    }
    return true;
}
function opposite(dir) {
    return dir < 4 ? dir << 2 : dir >> 2;
}
var icon;
(function (icon) {
    icon[icon["none"] = 0] = "none";
    icon[icon["start"] = 1] = "start";
    icon[icon["boss"] = 2] = "boss";
    icon[icon["exit"] = 3] = "exit";
    icon[icon["relicOn"] = 4] = "relicOn";
    icon[icon["altarOn"] = 5] = "altarOn";
    icon[icon["relicAltar"] = 6] = "relicAltar";
    icon[icon["secret"] = 7] = "secret";
    icon[icon["exclamation"] = 8] = "exclamation";
    icon[icon["shopBR"] = 9] = "shopBR";
    icon[icon["shop"] = 10] = "shop";
    icon[icon["combat"] = 11] = "combat";
    icon[icon["new"] = 12] = "new";
    icon[icon["hoody"] = 13] = "hoody";
    icon[icon["fountain"] = 14] = "fountain";
    icon[icon["exlamation"] = 15] = "exlamation";
})(icon || (icon = {}));
var door;
(function (door) {
    door[door["none"] = 0] = "none";
    door[door["normal"] = 1] = "normal";
    door[door["iron"] = 2] = "iron";
    door[door["rock"] = 3] = "rock";
    door[door["crystal"] = 4] = "crystal";
    door[door["locked"] = 5] = "locked";
    door[door["secret"] = 6] = "secret";
    door[door["hidden"] = 7] = "hidden";
})(door || (door = {}));
const conditions = {
    relicWhip: (flag) => flag.relicWhip ?? false,
    relicHat: (flag) => flag.relicHat ?? false,
    relicCircinus: (flag) => flag.relicCircinus ?? false,
    relicGuacamole: (flag) => flag.guacamole ?? false,
    relicGuacamoleBug: (flag) => (flag.guacamole > 2) ?? false,
    noHexDesolation: (flag) => !flag.hexDesolation ?? false,
    storyMode: (flag) => flag.storyMode,
    notWhip: (flag) => !flag.whip_enabled,
    storyNotWhip: (flag) => (!flag.whip_enabled && flag.storyMode) ?? false,
    rougeMode: (flag) => flag.rougeMode,
    priestessRescued: (flag) => (flag.priestess_met > 2) ?? false,
    noFountain: (flag) => (flag.storyMode && !flag.tribute_fountain_encountered && flag.bog_unlocked) ?? false,
    secretFountain: (flag) => (flag.storyMode && flag.bog_unlocked) ?? false,
    notBardMet: (flag) => !flag.bard_met ?? false,
    relicAltar: (flag) => (!flag.altar_encountered && !flag.whip_enabled) ?? false,
    blackRabbitMet: (flag) => (flag.black_rabbit_met > 0) ?? false,
    secretShop: (flag) => (flag.peasant2_unlocked && (flag.dibble_upgrade_count < 4) && flag.storyMode) ?? false,
    talismanSpawn: (flag) => (flag.priestess_met > 2),
    bossRoom: (flag) => (flag.storyMode),
    nextAreaEntrance: (flag) => (flag.storyMode),
    dogShadow: (flag) => (!flag.dog_shadow_found && (flag.delve_count > 5) && !flag.whip_enabled) ?? false,
    dogEngine: (flag) => (!flag.dog_engine_found && (flag.delve_count > 6) && !flag.whip_enabled) ?? false,
    dogDillion: (flag) => (!flag.dog_dillon_found && (flag.delve_count > 7) && !flag.whip_enabled) ?? false,
    alchemistApprentice0: (flag) => (!flag.apprentice_met && flag.blacksmith_rescued && !flag.whip_enabled && flag.storyMode) ?? false,
    alchemistApprentice3: (flag) => ((flag.apprentice_met == 4) && flag.blacksmith_rescued && !flag.whip_enabled && flag.storyMode) ?? false,
    mushroomGreen: (flag) => (!flag.mushroom_green && flag.apprentice_met && !flag.whip_enabled && flag.storyMode) ?? false,
    mushroomBlue: (flag) => (!flag.mushroom_blue && flag.apprentice_met && !flag.whip_enabled && flag.storyMode) ?? false,
    mushroomPurple: (flag) => (!flag.mushroom_purple && flag.apprentice_met && !flag.whip_enabled && flag.storyMode) ?? false,
    hoodieMineL: (flag) => (flag.rockmimic_defeated && !flag.hoodie_met_mine && (flag.floor_number == 1) && flag.storyMode) ?? false,
    hoodieMineU: (flag) => (flag.rockmimic_defeated && flag.hoodie_met_mine && (flag.floor_number == 1) && flag.storyMode) ?? false,
    hoodieDungeonL: (flag) => (!flag.hoodie_met_dungeon && (flag.floor_number == 5) && flag.storyMode) ?? false,
    hoodieDungeonU: (flag) => (flag.hoodie_met_dungeon && (flag.floor_number == 5) && flag.storyMode) ?? false,
    hoodieHallL: (flag) => (!flag.hoodie_met_hall && (flag.floor_number == 11) && flag.storyMode) ?? false,
    hoodieHallU: (flag) => (flag.hoodie_met_hall && (flag.floor_number == 11) && flag.storyMode) ?? false,
    hoodieCavernL: (flag) => (!flag.hoodie_met_cavern && (flag.floor_number == 16) && flag.storyMode) ?? false,
    hoodieCavernU: (flag) => (flag.hoodie_met_cavern && (flag.floor_number == 16) && flag.storyMode) ?? false,
    priestessEntrance: (flag) => (!flag.priestess_met && !flag.whip_enabled && flag.storyMode) ?? false,
    mastersKey: (flag) => (flag.priestess_met && !flag.masters_key && !flag.whip_enabled && flag.storyMode) ?? false,
    royalRoad: (flag) => (!flag.whip_enabled) ?? false,
    royalRoadStart: (flag) => (!flag.whip_enabled) ?? false,
    queensRoom: (flag) => (!flag.whip_enabled) ?? false,
    dodsonNotRescued: (flag) => (!flag.peasant1_unlocked && !flag.whip_enabled && flag.storyMode) ?? false,
    waylandShop: (flag) => ((!flag.discoveredWaylandBoots || !flag.blacksmith_rescued) && !flag.whip_enabled) ?? false,
    blackRabbitFirst: (flag) => (!flag.black_rabbit_met && !flag.whip_enabled && flag.storyMode) ?? false,
    treasureHunt: (flag) => (!flag.secret_treasure_note && !flag.whip_enabled) ?? false,
    ratFriendship: (flag) => (!flag.discoveredRatBond && !flag.whip_enabled) ?? false,
    rockMimic: (flag) => (!flag.prisoner_key && !flag.whip_enabled && flag.storyMode) ?? false,
    dangerousToGo: (flag) => (flag.delve_count > 8) ?? false,
    dungeonEntrance: (flag) => (flag.storyMode) ?? false,
    dungeonLibrary: (flag) => (!flag.collector_book && !flag.whip_enabled && flag.storyMode) ?? false,
    dibble: (flag) => (!flag.peasant2_unlocked && !flag.whip_enabled && flag.storyMode) && false,
    dibblesStoreRoom: (flag) => (!flag.peasant2_unlocked && !flag.whip_enabled && flag.storyMode) ?? false,
    kurtz: (flag) => (flag.storyMode && (!flag.discoveredHungrySpirit || !flag.peasant4_unlocked)) ?? false,
    threeChests: (flag) => (flag.storyMode) ?? false,
    hallLibrary: (flag) => (!flag.whip_enabled && flag.storyMode) ?? false,
    partyPopcornRoom: (flag) => (flag.foundPartyPopcornPotion && !flag.whip_enabled) ?? false,
    hallLibraryCombat: (flag) => (!flag.collector_book && !flag.whip_enabled && flag.storyMode) ?? false,
    storeRoom: (flag) => (flag.rougeMode) ?? false,
    tutorialIncomplete: (flag) => (!flag.tutorial_complete) ?? false,
    tutorialComplete: (flag) => (!flag.rockmimic_defeated &&
        !flag.sandworm_defeated &&
        !flag.stonelord_defeated &&
        !flag.shadowlord_defeated) ?? false,
    mimicKilled: (flag) => (flag.rockmimic_defeated &&
        !flag.sandworm_defeated &&
        !flag.stonelord_defeated &&
        !flag.shadowlord_defeated) ?? false,
    mineSandwormKilled: (flag) => (flag.sandworm_defeated &&
        !flag.stonelord_defeated &&
        !flag.shadowlord_defeated) ?? false,
    mineStonelordKilled: (flag) => (flag.stonelord_defeated &&
        !flag.shadowlord_defeated) ?? false,
    mineShadowlordKilled: (flag) => (flag.shadowlord_defeated) ?? false,
    allBossesAlive: (flag) => (!flag.sandworm_defeated &&
        !flag.stonelord_defeated &&
        !flag.shadowlord_defeated &&
        !flag.crystallord_defeated) ?? false,
    sandwormKilled: (flag) => (flag.sandworm_defeated &&
        !flag.stonelord_defeated &&
        !flag.shadowlord_defeated &&
        !flag.crystallord_defeated) ?? false,
    stonelordNotKilled: (flag) => (!flag.stonelord_defeated &&
        !flag.shadowlord_defeated &&
        !flag.crystallord_defeated) ?? false,
    stonelordKilled: (flag) => (flag.stonelord_defeated &&
        !flag.shadowlord_defeated &&
        !flag.crystallord_defeated) ?? false,
    shadowlordNotKilled: (flag) => (!flag.shadowlord_defeated &&
        !flag.crystallord_defeated) ?? false,
    shadowlordKilled: (flag) => (flag.shadowlord_defeated &&
        !flag.crystallord_defeated) ?? false,
    crystallordKilled: (flag) => (flag.crystallord_defeated) ?? false,
    crystallordNotKilled: (flag) => (!flag.crystallord_defeated &&
        !flag.firelord_defeated &&
        !flag.enterBog) ?? false,
    crystallordKilledNotFire: (flag) => (flag.crystallord_defeated &&
        !flag.firelord_defeated &&
        !flag.enterBog) ?? false,
    firelordKilled: (flag) => (flag.firelord_defeated &&
        !flag.enterBog) ?? false,
    enterBog: (flag) => (flag.enterBog) ?? false,
    treasureHuntX: (flag) => (flag.secret_treasure_note > 0) ?? false,
    chestBig: (flag) => (flag.floor_number > 5) ?? false,
    bogVisited: (flag) => (flag.bog_visited) ?? false,
};
function rooms(...roomNames) {
    return roomNames.map(room => rooms.list[room]);
}
rooms.multi = function (...roomNames) {
    return roomNames.map(room => [rooms.list[room]]);
};
rooms.list = {
    begin: { stage: ["small"], tags: "begin", branchWeight: 1 },
    end: { stage: ["small"], tags: "end", branchWeight: 1 },
    normalSmall: { stage: ["small"], tags: "normal", branchWeight: 4, },
    normalLarge: { stage: ["large"], tags: "normal", branchWeight: 4, },
    normal: { stage: ["small", "large"], tags: "normal", branchWeight: 4, },
    easy: { stage: ["small"], tags: "easy", branchWeight: 4 },
    relic: { stage: ["small"], tags: "relic" },
    relicUnlocked: { stage: ["small"], tags: "relic_unlocked", branchWeight: 1 },
    relicExtraCost: { stage: ["small"], tags: "relic", doorCost: "2 keys" },
    altar: { stage: ["small"], tags: "altar", branchWeight: 1 },
    altarNoBranch: { stage: ["small"], tags: "altar", },
    guacamole: { stage: ["small"], tags: "altar_guacamole" },
    guacamoleAlt: { stage: ["small"], tags: "altar_guacamole", requirements: conditions.relicGuacamole, },
    blackRabbit: { stage: ["extra"], tags: "black_rabbit", chance: 0.5 },
    hoodieTop: { stage: ["small"], tags: "hoodie_entrance", direction: 1 },
    hoody: { stage: ["extra"], tags: "hoody" },
    secret: { stage: ["small", "large"], tags: "secret" },
    secretChance: { stage: ["small", "large"], tags: "secret", chance: 0.5, requirements: conditions.relicHat },
    circinus: { stage: ["small", "large"], tags: "secret", chance: 0.25, requirements: conditions.relicCircinus },
    treasure: { stage: ["small", "large"], tags: "treasure", branchWeight: 1 },
    treasureChance: { stage: ["small", "large"], tags: "treasure", chance: 0.5, requirements: conditions.relicWhip },
    treasureBasic: { stage: ["small"], tags: "treasure_basic_encounters", branchWeight: 1 },
    treasureBasicChance: { stage: ["small", "large"], tags: "treasure_basic_encounters", chance: 0.5, requirements: conditions.relicWhip },
    hidden: { stage: ["small", "large"], tags: "hidden" },
    royalRoad0: { stage: ["large"], tags: "royal_road_0",
        floorChance: (floor) => (25 * (floor - 20)) / 100,
    },
    royalRoad1: { stage: ["large"], tags: "royal_road_1", direction: 2 },
    royalRoad2: { stage: ["large"], tags: "royal_road_2", direction: 1 },
    royalRoad3: { stage: ["large"], tags: "royal_road_3", direction: 8 },
    royalRoad4: { stage: ["large"], tags: "royal_road_4", direction: 1 },
    queenRoom: { stage: ["large"], tags: "queen_room", direction: 1 },
    endBoss: { stage: ["large"], tags: "down_boss", branchWeight: 1 },
    endBossSmall: { stage: ["small"], tags: "down_boss", branchWeight: 1 },
    bossRoom: { stage: ["extra"], tags: "bossRoom" },
    prechamber: { stage: ["large"], tags: "pre_room", direction: 1 },
    nextDown: { stage: ["small"], tags: "next_entrance", direction: 1 },
    bogPrechamber: { stage: ["large"], tags: "pre_bog", direction: 1 },
    bogDown: { stage: ["small"], tags: "next_entrance_bog", direction: 1 },
    bogPrechamberBug: { stage: ["large"], tags: "pre_bog" },
    bogDownBug: { stage: ["small"], tags: "next_entrance_bog" },
    shop: { stage: ["extra"], tags: "shop", branchWeight: 1 },
    shopExtraCost: { stage: ["extra"], tags: "shop", branchWeight: 1, doorCost: "2 keys" },
    marketBaby: { stage: ["extra"], tags: "market_baby" },
    marketBabyEast: { stage: ["extra"], tags: "market_baby", direction: 2 },
    relicAltar: { stage: ["large"], tags: "relic_altar",
        floorChance: (floor) => (625 * (floor - Math.trunc(floor / 5))) / 10000,
    },
    relicAltarLastFloors: { stage: ["large"], tags: "relic_altar",
        floorChance: (floor) => (625 * (floor - Math.trunc(floor / 5) - 4)) / 10000,
    },
    fountain: { stage: ["large"], tags: "tribute_fountain",
        floorChance: (floor) => (625 * (floor - Math.trunc(floor / 5))) / 10000,
    },
    tuBegin: { stage: ["small"], tags: "begin_tutorial", branchWeight: 1 },
    tuJump: { stage: ["small"], tags: "tutorial_jump", direction: 2, branchWeight: 1 },
    tuAttack: { stage: ["small"], tags: "tutorial_attack", direction: 2, branchWeight: 1 },
    tuBomb: { stage: ["small"], tags: "tutorial_bomb", direction: 4, branchWeight: 1 },
    tuThrow: { stage: ["large"], tags: "tutorial_throw", direction: 1, branchWeight: 1 },
    tuPilfer: { stage: ["large"], tags: "tutorial_pilfer", direction: 4, branchWeight: 1 },
    tuRelic: { stage: ["small"], tags: "tutorial_relic", direction: 1, branchWeight: 1 },
    tuEnd: { stage: ["small"], tags: "end_tutorial", direction: 1, branchWeight: 1 },
    tuSecret: { stage: ["small"], tags: "tutorial_secret", direction: 1, branchWeight: 1 },
    dodsonCage: { stage: ["small"], tags: "DodsonCageEncounter,normal", branchWeight: 4 },
    mimicFight: { stage: ["large"], tags: "RockMimicEncounter,normal" },
    waylandHallway: { stage: ["small"], tags: "waylandshophallway" },
    waylandShop: { stage: ["small"], tags: "waylandshop", direction: 1 },
    apprentice: { stage: ["large"], tags: "mushroom_apprentice", branchWeight: 1,
        floorChance: (floor) => (3 + (2 * floor)) / 10,
    },
    mushroom: { stage: ["small", "large"], tags: "mushroom",
        floorChance: (floor) => (5 + floor) / 10,
    },
    blackRabbitFirst: { stage: ["small"], tags: "black_rabbit_first", branchWeight: 1 },
    dibble: { stage: ["large"], tags: "dibble", direction: 1 },
    storeRoom: { stage: ["small"], tags: "store_room", direction: 1 },
    mastersKey: { stage: ["large"], tags: "masters_key" },
    priestess: { stage: ["small"], tags: "priestess" },
    priestesshall1: { stage: ["large"], tags: "priestesshall1", direction: 1 },
    priestesshall2: { stage: ["large"], tags: "priestesshall2", direction: 1 },
    priestesshall3: { stage: ["large"], tags: "priestesshall3", direction: 1 },
    priestessMain: { stage: ["large"], tags: "priestess_main", direction: 1 },
    libraryCombat: { stage: ["large"], tags: "hall_library_combat_arena", branchWeight: 1 },
    library: { stage: ["small"], tags: "hall_library", direction: 1 },
    threeChests: { stage: ["small"], tags: "hall_hidden_three_chests", direction: 2 },
    secretEast: { stage: ["small", "large"], tags: "secret", direction: 2 },
    hiddenHallway: { stage: ["small"], tags: "hidden_hallway", direction: 1 },
    hiddenCampsite: { stage: ["small"], tags: "hidden_campsite", direction: 1 },
    secretWest: { stage: ["small", "large"], tags: "secret", direction: 8 },
    maybeSecretEast: { stage: ["small", "large"], tags: "secret", chance: 0.5, direction: 2 },
};
const enemies = { name: "",
    assassin: { name: "assassin", difficulty: 7, rougeDifficulty: 5, max: 3, type: 1 },
    bat: { name: "bat", difficulty: 4, rougeDifficulty: 4, canBeSolo: true, type: 1 },
    bingBong: { name: "bingBong", difficulty: 15, rougeDifficulty: 6, max: 3, type: 3 },
    bishFish: { name: "bishFish", difficulty: 3, rougeDifficulty: 3, max: 3, type: 1 },
    bobo: { name: "bobo", difficulty: 5, rougeDifficulty: 5, max: 1, type: 3 },
    bugLightning: { name: "bugLightning", difficulty: 3, rougeDifficulty: 3, type: 1 },
    cauldron: { name: "cauldron", difficulty: 2, rougeDifficulty: 3, canBeSolo: true, type: 3 },
    churchbell: { name: "churchbell", difficulty: 6, rougeDifficulty: 3, max: 2, type: 4 },
    corruptedPilfer: { name: "corruptedPilfer", difficulty: 9, rougeDifficulty: 4, canBeSolo: true, type: 3 },
    corruptedPilferBoss: { name: "corruptedPilferBoss", difficulty: 9, rougeDifficulty: 4, canBeSolo: true, type: 3 },
    crossbowman: { name: "crossbowman", difficulty: 6, rougeDifficulty: 5, max: 2, type: 2 },
    crystalAnt: { name: "crystalAnt", difficulty: 8, rougeDifficulty: 5, canBeSolo: true, type: 1 },
    crystalFly: { name: "crystalFly", difficulty: 7, rougeDifficulty: 4, canBeSolo: true, type: 2 },
    crystalSpider: { name: "crystalSpider", difficulty: 6, rougeDifficulty: 3, canBeSolo: true, type: 1 },
    crystalWitch: { name: "crystalWitch", difficulty: 10, rougeDifficulty: 5, max: 2, type: 2 },
    cursedJar: { name: "cursedJar", difficulty: 3, rougeDifficulty: 3, canBeSolo: true, type: 4 },
    cursedJarWisp: { name: "cursedJarWisp", difficulty: 1, rougeDifficulty: 1, canBeSolo: true, type: 4 },
    firebat: { name: "firebat", difficulty: 9, rougeDifficulty: 4, max: 2, type: 1 },
    fireWitch: { name: "fireWitch", difficulty: 5, rougeDifficulty: 5, max: 1, type: 2 },
    flyBloat: { name: "flyBloat", difficulty: 9, rougeDifficulty: 4, canBeSolo: true, type: 2 },
    flyRanged: { name: "flyRanged", difficulty: 2, rougeDifficulty: 3, canBeSolo: true, type: 2, max: 7 },
    flyRanged02: { name: "flyRanged02", difficulty: 4, rougeDifficulty: 4, canBeSolo: true, type: 2 },
    flyRanged03: { name: "flyRanged03", difficulty: 7, rougeDifficulty: 5, canBeSolo: true, type: 2 },
    footman: { name: "footman", difficulty: 7, rougeDifficulty: 6, max: 2, type: 1 },
    gargoyle: { name: "gargoyle", difficulty: 6, rougeDifficulty: 6, max: 2, type: 2 },
    gargoyleStatue: { name: "gargoyleStatue", difficulty: 6, rougeDifficulty: 6, max: 2, type: 2 },
    glimmerweed: { name: "glimmerweed", difficulty: 8, rougeDifficulty: 4, max: 3, canBeSolo: true, type: 6 },
    goblinEngineer: { name: "goblinEngineer", difficulty: 5, rougeDifficulty: 4, max: 2, type: 4 },
    goblinPoisonEngineer: { name: "goblinPoisonEngineer", difficulty: 14, rougeDifficulty: 5, max: 2, type: 4 },
    goblinTickerEngineer: { name: "goblinTickerEngineer", difficulty: 11, rougeDifficulty: 6, max: 2, type: 4 },
    golem: { name: "golem", difficulty: 9, rougeDifficulty: 6, max: 1, canBeSolo: true, type: 1 },
    gremlin: { name: "gremlin", difficulty: 8, rougeDifficulty: 4, canBeSolo: true, type: 1 },
    gremlinFiery: { name: "gremlinFiery", difficulty: 8, rougeDifficulty: 4, canBeSolo: true, type: 2 },
    highPriest: { name: "highPriest", difficulty: 16, rougeDifficulty: 6, max: 2, type: 6 },
    hobbleDoll: { name: "hobbleDoll", difficulty: 0, rougeDifficulty: 2, type: 0 },
    jumperFire: { name: "jumperFire", difficulty: 3, rougeDifficulty: 3, canBeSolo: true, type: 3 },
    jumperFireLarge: { name: "jumperFireLarge", difficulty: 6, rougeDifficulty: 4, canBeSolo: true, type: 3 },
    jumperOli: { name: "jumperOil", difficulty: 3, rougeDifficulty: 3, canBeSolo: true, type: 3 },
    jumperOliLarge: { name: "jumperOilLarge", difficulty: 6, rougeDifficulty: 4, canBeSolo: true, type: 3 },
    jumperWater: { name: "jumperWater", difficulty: 3, rougeDifficulty: 3, canBeSolo: true, type: 3 },
    jumperWaterLarge: { name: "jumperWaterLarge", difficulty: 6, rougeDifficulty: 4, canBeSolo: true, type: 3 },
    lurkerBasic: { name: "lurkerBasic", difficulty: 4, rougeDifficulty: 4, max: 3, type: 2 },
    lurkerCrypt: { name: "lurkerCrypt", difficulty: 7, rougeDifficulty: 4, max: 1, type: 2 },
    lurkerCrystal: { name: "lurkerCrystal", difficulty: 10, rougeDifficulty: 5, max: 2, type: 2 },
    magmaWormHead: { name: "magmaWormHead", difficulty: 13, rougeDifficulty: 5, canBeSolo: true, type: 2 },
    mimicChest: { name: "mimicChest", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 3 },
    mimicRareChest: { name: "mimicRareChest", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 3 },
    mimicResourceChest: { name: "mimicResourceChest", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 3 },
    minicChest: { name: "minicChest", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 3 },
    necromancer: { name: "necromancer", difficulty: 12, rougeDifficulty: 7, max: 1, type: 6 },
    nemesis: { name: "nemesis", difficulty: 10, rougeDifficulty: 10, type: 1 },
    ogreAnnihilator: { name: "ogreAnnihilator", difficulty: 16, rougeDifficulty: 9, max: 2, type: 2 },
    ogreBombardier: { name: "ogreBombardier", difficulty: 6, rougeDifficulty: 6, max: 2, type: 2 },
    ogreFirebomber: { name: "ogreFirebomber", difficulty: 18, rougeDifficulty: 6, max: 2, type: 2 },
    pilferBasic: { name: "pilferBasic", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 0 },
    pilferBomb: { name: "pilferBomb", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 0 },
    pilferCrafting: { name: "pilferCrafting", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 0 },
    pilferFat: { name: "pilferFat", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 0 },
    pilferGolden: { name: "pilferGolden", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 0 },
    pilferHunter: { name: "pilferHunter", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 0 },
    poisonWormHead: { name: "poisonWormHead", difficulty: 15, rougeDifficulty: 7, max: 2, canBeSolo: true, type: 2 },
    practiseDummy: { name: "practiseDummy", difficulty: 0, rougeDifficulty: 0, canBeSolo: true, type: 0 },
    priest: { name: "priest", difficulty: 4, rougeDifficulty: 4, max: 2, type: 6 },
    priestInnerFire: { name: "priestInnerFire", difficulty: 4, rougeDifficulty: 4, max: 1, type: 6 },
    queenFish: { name: "queenFish", difficulty: 11, rougeDifficulty: 6, canBeSolo: true, type: 2 },
    quilledGolem: { name: "quilledGolem", difficulty: 20, rougeDifficulty: 8, canBeSolo: true, type: 1 },
    ratBasic: { name: "ratBasic", difficulty: 1, rougeDifficulty: 2, canBeSolo: true, type: 1 },
    ratExploding: { name: "ratExploding", difficulty: 1, rougeDifficulty: 2, canBeSolo: true, type: 3 },
    ratLarge: { name: "ratLarge", difficulty: 3, rougeDifficulty: 3, canBeSolo: true, type: 1 },
    ratNest: { name: "ratNest", difficulty: 3, rougeDifficulty: 3, type: 4 },
    ratTough: { name: "ratTough", difficulty: 4, rougeDifficulty: 4, canBeSolo: true, type: 1 },
    rookFish: { name: "rookFish", difficulty: 3, rougeDifficulty: 3, max: 3, type: 2 },
    sandwordLarvae: { name: "sandwordLarvae", difficulty: 1, rougeDifficulty: 1, canBeSolo: true, type: 1 },
    scaleTunnel: { name: "scaleTunnel", difficulty: 18, rougeDifficulty: 8, type: 4 },
    shaman: { name: "shaman", difficulty: 6, rougeDifficulty: 5, max: 2, type: 5 },
    shaman02: { name: "shaman02", difficulty: 9, rougeDifficulty: 5, max: 2, type: 6 },
    shaman03: { name: "shaman03", difficulty: 18, rougeDifficulty: 5, max: 2, type: 5 },
    skeletonEnemyCrimson: { name: "skeletonEnemyCrimson", difficulty: 8, rougeDifficulty: 6, canBeSolo: true, type: 3 },
    skeletonEnemyNorm: { name: "skeletonEnemyNorm", difficulty: 6, rougeDifficulty: 4, canBeSolo: true, type: 3 },
    spiderBasic: { name: "spiderBasic", difficulty: 2, rougeDifficulty: 1, canBeSolo: true, type: 1 },
    spiderWolf: { name: "spiderWolf", difficulty: 3, rougeDifficulty: 2, canBeSolo: true, type: 1 },
    throwbo: { name: "throwbo", difficulty: 12, rougeDifficulty: 7, max: 1, type: 3 },
    ticker: { name: "ticker", difficulty: 5, rougeDifficulty: 3, canBeSolo: true, type: 2 },
    trollBombardier: { name: "trollBombardier", difficulty: 14, rougeDifficulty: 6, max: 2, type: 2 },
    voodooDoll: { name: "voodooDoll", difficulty: 0, rougeDifficulty: 2, type: 0 },
};
const zoneData = [
    [
        {
            name: "mineTutorial",
            map: "mineEarly",
            connectivity: 0.15,
            zoneNumber: 1,
            startFloor: 1,
            baseDifficulty: 2,
            difficultyStep: 3,
            enemyTypeWeight: [0, 1, 2],
            requirements: conditions.tutorialIncomplete,
            extras: [
                {
                    min: 10,
                    max: 12,
                    percent: false,
                    items: [
                        { weight: 1, name: "SkeletonSpawner" },
                        { weight: 3, name: "Crate01" },
                        { weight: 2, name: "Crate02" },
                        { weight: 3, name: "Barrel01" },
                        { weight: 6, name: "Crate01" },
                        { weight: 5, name: "Crate02" },
                        { weight: 5, name: "Barrel01" },
                        { weight: 4, name: "Plant01" },
                        { weight: 4, name: "Plant02" },
                        { weight: 1, name: "BarrelExploding01" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 5, name: "PilferGolden" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 10, name: "CrystalFire" },
                        { weight: 10, name: "CrystalLightning" },
                        { weight: 10, name: "CrystalPoison" },
                    ],
                },
                {
                    min: 0,
                    max: 1,
                    percent: false,
                    items: [
                        { weight: 1, name: "TreasureHuntX" },
                    ],
                },
            ],
            resources: [
                {
                    min: 8,
                    max: 8,
                    percent: false,
                    items: [
                        { weight: 3, name: "RockGold01" },
                        { weight: 3, name: "RockGold02" },
                        { weight: 3, name: "RockGold03" },
                        { weight: 4, name: "OreWall" },
                    ],
                },
            ],
            setPieces: [
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 30, name: "ItemCageMineSetPiece" },
                    ],
                },
            ],
            floors: [
                {
                    override: {
                        map: "mineTutorial",
                        connectivity: 0,
                    },
                    enemies: [],
                },
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.jumperWater,
                        enemies.flyRanged,
                    ],
                },
                {
                    override: {
                        connectivity: 0,
                    },
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.jumperWater,
                        enemies.ratNest,
                    ],
                },
                {
                    override: {
                        difficulty: 10,
                        enemyTypeWeight: [0, 1, 2, 1],
                    },
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.flyRanged02,
                        enemies.jumperWater,
                        enemies.jumperOli,
                        enemies.ratNest,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                    ],
                },
            ],
        },
        {
            name: "mine1",
            map: "mineEarly",
            connectivity: 0.15,
            zoneNumber: 1,
            startFloor: 1,
            baseDifficulty: 3,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2],
            requirements: conditions.tutorialComplete,
            extras: [
                {
                    min: 10,
                    max: 12,
                    percent: false,
                    items: [
                        { weight: 1, name: "SkeletonSpawner" },
                        { weight: 3, name: "Crate01" },
                        { weight: 2, name: "Crate02" },
                        { weight: 3, name: "Barrel01" },
                        { weight: 6, name: "Crate01" },
                        { weight: 5, name: "Crate02" },
                        { weight: 5, name: "Barrel01" },
                        { weight: 4, name: "Plant01" },
                        { weight: 4, name: "Plant02" },
                        { weight: 1, name: "BarrelExploding01" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 5, name: "PilferGolden" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 10, name: "CrystalFire" },
                        { weight: 10, name: "CrystalLightning" },
                        { weight: 10, name: "CrystalPoison" },
                    ],
                },
                {
                    min: 0,
                    max: 1,
                    percent: false,
                    items: [
                        { weight: 1, name: "TreasureHuntX" },
                    ],
                },
            ],
            resources: [
                {
                    min: 8,
                    max: 8,
                    percent: false,
                    items: [
                        { weight: 3, name: "RockGold01" },
                        { weight: 3, name: "RockGold02" },
                        { weight: 3, name: "RockGold03" },
                        { weight: 4, name: "OreWall" },
                    ],
                },
            ],
            setPieces: [
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 30, name: "ItemCageMineSetPiece" },
                    ],
                },
            ],
            floors: [
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.jumperWater,
                        enemies.flyRanged,
                    ],
                },
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.jumperWater,
                        enemies.flyRanged,
                    ],
                },
                {
                    override: {
                        difficulty: 8,
                        connectivity: 0,
                    },
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.jumperWater,
                        enemies.ratNest,
                    ],
                },
                {
                    override: {
                        difficulty: 10,
                        enemyTypeWeight: [0, 1, 2, 1],
                    },
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.flyRanged02,
                        enemies.jumperWater,
                        enemies.jumperOli,
                        enemies.ratNest,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                    ],
                },
            ],
        },
        {
            name: "mine2",
            map: "mine",
            connectivity: 0.15,
            zoneNumber: 1,
            startFloor: 1,
            baseDifficulty: 5,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.mimicKilled,
            extras: [
                {
                    min: 10,
                    max: 12,
                    percent: false,
                    items: [
                        { weight: 1, name: "SkeletonSpawner" },
                        { weight: 3, name: "Crate01" },
                        { weight: 2, name: "Crate02" },
                        { weight: 3, name: "Barrel01" },
                        { weight: 6, name: "Crate01" },
                        { weight: 5, name: "Crate02" },
                        { weight: 5, name: "Barrel01" },
                        { weight: 4, name: "Plant01" },
                        { weight: 4, name: "Plant02" },
                        { weight: 1, name: "BarrelExploding01" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 5, name: "PilferGolden" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 10, name: "CrystalFire" },
                        { weight: 10, name: "CrystalLightning" },
                        { weight: 10, name: "CrystalPoison" },
                    ],
                },
                {
                    min: 0,
                    max: 1,
                    percent: false,
                    items: [
                        { weight: 1, name: "TreasureHuntX" },
                    ],
                },
            ],
            resources: [
                {
                    min: 8,
                    max: 8,
                    percent: false,
                    items: [
                        { weight: 3, name: "RockGold01" },
                        { weight: 3, name: "RockGold02" },
                        { weight: 3, name: "RockGold03" },
                        { weight: 4, name: "OreWall" },
                    ],
                },
            ],
            setPieces: [
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 30, name: "ItemCageMineSetPiece" },
                    ],
                },
            ],
            floors: [
                {
                    override: {
                        enemyTypeWeight: [0, 1, 2],
                    },
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.jumperWater,
                        enemies.ratNest,
                    ],
                },
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.ratTough,
                    ],
                },
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.flyRanged02,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                    ],
                },
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.flyRanged02,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                    ],
                },
            ],
        },
        {
            name: "mine3",
            map: "mine",
            connectivity: 0.25,
            zoneNumber: 1,
            startFloor: 1,
            baseDifficulty: 8,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.mineSandwormKilled,
            extras: [
                {
                    min: 10,
                    max: 12,
                    percent: false,
                    items: [
                        { weight: 1, name: "SkeletonSpawner" },
                        { weight: 3, name: "Crate01" },
                        { weight: 2, name: "Crate02" },
                        { weight: 3, name: "Barrel01" },
                        { weight: 6, name: "Crate01" },
                        { weight: 5, name: "Crate02" },
                        { weight: 5, name: "Barrel01" },
                        { weight: 4, name: "Plant01" },
                        { weight: 4, name: "Plant02" },
                        { weight: 1, name: "BarrelExploding01" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 5, name: "PilferGolden" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 10, name: "CrystalFire" },
                        { weight: 10, name: "CrystalLightning" },
                        { weight: 10, name: "CrystalPoison" },
                    ],
                },
                {
                    min: 0,
                    max: 1,
                    percent: false,
                    items: [
                        { weight: 1, name: "TreasureHuntX" },
                    ],
                },
            ],
            resources: [
                {
                    min: 8,
                    max: 8,
                    percent: false,
                    items: [
                        { weight: 3, name: "RockGold01" },
                        { weight: 3, name: "RockGold02" },
                        { weight: 3, name: "RockGold03" },
                        { weight: 4, name: "OreWall" },
                    ],
                },
            ],
            setPieces: [
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 30, name: "ItemCageMineSetPiece" },
                    ],
                },
            ],
            floors: [
                {
                    override: {
                        enemyTypeWeight: [0, 1, 2],
                    },
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.ratTough,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    enemies: [
                        enemies.ratLarge,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    enemies: [
                        enemies.ratLarge,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                        enemies.bat,
                        enemies.goblinEngineer,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
            ],
        },
        {
            name: "mine4",
            map: "mine",
            connectivity: 0.25,
            zoneNumber: 1,
            startFloor: 1,
            baseDifficulty: 12,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.mineStonelordKilled,
            extras: [
                {
                    min: 10,
                    max: 12,
                    percent: false,
                    items: [
                        { weight: 1, name: "SkeletonSpawner" },
                        { weight: 3, name: "Crate01" },
                        { weight: 2, name: "Crate02" },
                        { weight: 3, name: "Barrel01" },
                        { weight: 6, name: "Crate01" },
                        { weight: 5, name: "Crate02" },
                        { weight: 5, name: "Barrel01" },
                        { weight: 4, name: "Plant01" },
                        { weight: 4, name: "Plant02" },
                        { weight: 1, name: "BarrelExploding01" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 5, name: "PilferGolden" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 10, name: "CrystalFire" },
                        { weight: 10, name: "CrystalLightning" },
                        { weight: 10, name: "CrystalPoison" },
                    ],
                },
                {
                    min: 0,
                    max: 1,
                    percent: false,
                    items: [
                        { weight: 1, name: "TreasureHuntX" },
                    ],
                },
            ],
            resources: [
                {
                    min: 8,
                    max: 8,
                    percent: false,
                    items: [
                        { weight: 3, name: "RockGold01" },
                        { weight: 3, name: "RockGold02" },
                        { weight: 3, name: "RockGold03" },
                        { weight: 4, name: "OreWall" },
                    ],
                },
            ],
            setPieces: [
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 30, name: "ItemCageMineSetPiece" },
                    ],
                },
            ],
            floors: [
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.ratTough,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                        enemies.ogreBombardier,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    enemies: [
                        enemies.ratLarge,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                        enemies.ogreBombardier,
                        enemies.goblinEngineer,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    enemies: [
                        enemies.ratLarge,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                        enemies.bat,
                        enemies.ogreBombardier,
                        enemies.goblinEngineer,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
            ],
        },
        {
            name: "mine5",
            map: "mine",
            connectivity: 0.25,
            zoneNumber: 1,
            startFloor: 1,
            baseDifficulty: 16,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.mineShadowlordKilled,
            extras: [
                {
                    min: 10,
                    max: 12,
                    percent: false,
                    items: [
                        { weight: 1, name: "SkeletonSpawner" },
                        { weight: 3, name: "Crate01Loot" },
                        { weight: 2, name: "Crate02Loot" },
                        { weight: 3, name: "Barrel01Loot" },
                        { weight: 6, name: "Crate01NoLoot" },
                        { weight: 5, name: "Crate02NoLoot" },
                        { weight: 5, name: "Barrel01NoLoot" },
                        { weight: 4, name: "Plant01" },
                        { weight: 4, name: "Plant02" },
                        { weight: 1, name: "BarrelExploding01" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 5, name: "PilferGolden" },
                    ],
                },
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        { weight: 10, name: "CrystalFire" },
                        { weight: 10, name: "CrystalLightning" },
                        { weight: 10, name: "CrystalPoison" },
                    ],
                },
                {
                    min: 0,
                    max: 1,
                    percent: false,
                    items: [
                        { weight: 1, name: "TreasureHuntX" },
                    ],
                },
            ],
            resources: [
                {
                    min: 8,
                    max: 8,
                    percent: false,
                    items: [
                        { weight: 3, name: "RockGold01" },
                        { weight: 3, name: "RockGold02" },
                        { weight: 3, name: "RockGold03" },
                        { weight: 4, name: "OreWall" },
                    ],
                },
            ],
            setPieces: [
                {
                    min: 1,
                    max: 1,
                    percent: true,
                    items: [
                        {
                            weight: 30,
                            name: "ItemCageMineSetPiece",
                            choosers: [
                                [
                                    { weight: 3, name: "itemSpawner" },
                                    { weight: 1, name: "chestSpawner" },
                                ],
                                [
                                    { weight: 1, name: "TallRocks" },
                                    { weight: 1, name: "TallRocksSpikes" },
                                    { weight: 1, name: "LockedBlock" },
                                    { weight: 1, name: "TallRockLockedBlock" },
                                    { weight: 1, name: "CrystalRocks" },
                                    { weight: 1, name: "Spikes" },
                                    { weight: 1, name: "Rail" },
                                ],
                            ],
                        },
                    ],
                },
            ],
            floors: [
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    enemies: [
                        enemies.ratBasic,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                        enemies.ogreBombardier,
                        enemies.goblinEngineer,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    enemies: [
                        enemies.ratLarge,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.ratNest,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                        enemies.bat,
                        enemies.ogreBombardier,
                        enemies.goblinEngineer,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    enemies: [
                        enemies.ratLarge,
                        enemies.flyRanged,
                        enemies.jumperOli,
                        enemies.jumperWater,
                        enemies.lurkerBasic,
                        enemies.ratTough,
                        enemies.bobo,
                        enemies.spiderBasic,
                        enemies.bat,
                        enemies.shaman,
                        enemies.bugLightning,
                        enemies.ogreBombardier,
                        enemies.goblinEngineer,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
            ],
        },
    ],
    [
        {
            name: "dungeon1",
            map: "dungeon",
            connectivity: 0.15,
            zoneNumber: 2,
            startFloor: 6,
            baseDifficulty: 12,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.allBossesAlive,
            floors: [
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.jumperFire,
                        enemies.flyRanged,
                        enemies.flyRanged02,
                        enemies.ratTough,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.spiderBasic,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.flyRanged,
                        enemies.flyRanged02,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.crossbowman,
                        enemies.bishFish,
                        enemies.rookFish,
                    ],
                },
                {
                    override: {
                        enemyTypeWeight: [0, 0, 2, 1],
                    },
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                    ],
                },
                {
                    override: {
                        enemyTypeWeight: [0, 0, 2, 1],
                    },
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                    ],
                },
            ],
        },
        {
            name: "dungeon2",
            map: "dungeon",
            connectivity: 0.15,
            zoneNumber: 2,
            startFloor: 6,
            baseDifficulty: 16,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.sandwormKilled,
            floors: [
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.jumperFire,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.flyRanged,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                        enemies.bishFish,
                        enemies.rookFish,
                        enemies.goblinEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                        enemies.goblinEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                    ],
                },
            ],
        },
        {
            name: "dungeon3",
            map: "dungeon",
            connectivity: 0.15,
            zoneNumber: 2,
            startFloor: 6,
            baseDifficulty: 20,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.stonelordKilled,
            floors: [
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.jumperFire,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.flyRanged,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                        enemies.goblinEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.ticker,
                        enemies.crossbowman,
                    ],
                },
            ],
        },
        {
            name: "dungeon4",
            map: "dungeon",
            connectivity: 0.15,
            zoneNumber: 2,
            startFloor: 6,
            baseDifficulty: 24,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.shadowlordKilled,
            floors: [
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.jumperFire,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.flyRanged,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.ticker,
                        enemies.crossbowman,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.ticker,
                        enemies.skeletonEnemyNorm,
                        enemies.gargoyle,
                        enemies.gargoyleStatue,
                        enemies.crossbowman,
                    ],
                },
            ],
        },
        {
            name: "dungeon5",
            map: "dungeon",
            connectivity: 0.15,
            zoneNumber: 2,
            startFloor: 6,
            baseDifficulty: 28,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.crystallordKilled,
            floors: [
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.jumperFire,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.flyRanged,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.crossbowman,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.ticker,
                        enemies.crossbowman,
                    ],
                },
                {
                    enemies: [
                        enemies.bugLightning,
                        enemies.priest,
                        enemies.ratTough,
                        enemies.footman,
                        enemies.ratLarge,
                        enemies.bat,
                        enemies.shaman,
                        enemies.spiderBasic,
                        enemies.fireWitch,
                        enemies.flyRanged02,
                        enemies.ticker,
                        enemies.skeletonEnemyNorm,
                        enemies.gargoyle,
                        enemies.gargoyleStatue,
                        enemies.crossbowman,
                    ],
                },
            ],
        },
    ],
    [
        {
            name: "hall1",
            map: "hall",
            connectivity: 0.15,
            zoneNumber: 3,
            startFloor: 11,
            baseDifficulty: 20,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.stonelordNotKilled,
            floors: [
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.skeletonEnemyNorm,
                        enemies.lurkerCrypt,
                        enemies.jumperWaterLarge,
                        enemies.spiderWolf,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.lurkerCrypt,
                        enemies.throwbo,
                        enemies.spiderWolf,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.throwbo,
                        enemies.lurkerCrypt,
                        enemies.spiderWolf,
                        enemies.assassin,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyle,
                        enemies.gargoyleStatue,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.throwbo,
                        enemies.lurkerCrypt,
                        enemies.spiderWolf,
                        enemies.assassin,
                        enemies.goblinTickerEngineer,
                    ],
                },
            ],
        },
        {
            name: "hall2",
            map: "hall",
            connectivity: 0.15,
            zoneNumber: 3,
            startFloor: 11,
            baseDifficulty: 24,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.stonelordKilled,
            floors: [
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.skeletonEnemyNorm,
                        enemies.lurkerCrypt,
                        enemies.jumperWaterLarge,
                        enemies.spiderWolf,
                        enemies.throwbo,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.lurkerCrypt,
                        enemies.throwbo,
                        enemies.spiderWolf,
                        enemies.assassin,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.throwbo,
                        enemies.lurkerCrypt,
                        enemies.spiderWolf,
                        enemies.assassin,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyle,
                        enemies.gargoyleStatue,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.throwbo,
                        enemies.lurkerCrypt,
                        enemies.spiderWolf,
                        enemies.assassin,
                        enemies.goblinTickerEngineer,
                    ],
                },
            ],
        },
        {
            name: "hall3",
            map: "hall",
            connectivity: 0.15,
            zoneNumber: 3,
            startFloor: 11,
            baseDifficulty: 28,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.shadowlordKilled,
            floors: [
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.lurkerCrypt,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.spiderWolf,
                        enemies.throwbo,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.lurkerCrypt,
                        enemies.throwbo,
                        enemies.spiderWolf,
                        enemies.assassin,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.throwbo,
                        enemies.lurkerCrypt,
                        enemies.spiderWolf,
                        enemies.assassin,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyle,
                        enemies.gargoyleStatue,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.throwbo,
                        enemies.lurkerCrypt,
                        enemies.spiderWolf,
                        enemies.assassin,
                        enemies.goblinTickerEngineer,
                    ],
                },
            ],
        },
        {
            name: "hall4",
            map: "hall",
            connectivity: 0.15,
            zoneNumber: 3,
            startFloor: 11,
            baseDifficulty: 28,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 2, 1],
            requirements: conditions.crystallordKilled,
            floors: [
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.lurkerCrypt,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.spiderWolf,
                        enemies.throwbo,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.lurkerCrypt,
                        enemies.throwbo,
                        enemies.spiderWolf,
                        enemies.assassin,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.lurkerCrypt,
                        enemies.throwbo,
                        enemies.spiderWolf,
                        enemies.assassin,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.fireWitch,
                        enemies.cursedJar,
                        enemies.gargoyleStatue,
                        enemies.gargoyle,
                        enemies.ticker,
                        enemies.bat,
                        enemies.necromancer,
                        enemies.jumperWaterLarge,
                        enemies.jumperOliLarge,
                        enemies.skeletonEnemyNorm,
                        enemies.skeletonEnemyCrimson,
                        enemies.throwbo,
                        enemies.lurkerCrypt,
                        enemies.spiderWolf,
                        enemies.assassin,
                        enemies.trollBombardier,
                        enemies.goblinTickerEngineer,
                    ],
                },
            ],
        },
    ],
    [
        {
            name: "cavern1",
            map: "cavern",
            connectivity: 0.15,
            zoneNumber: 4,
            startFloor: 16,
            baseDifficulty: 28,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 3, 2],
            requirements: conditions.shadowlordNotKilled,
            floors: [
                {
                    enemies: [
                        enemies.jumperFireLarge,
                        enemies.throwbo,
                        enemies.assassin,
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.assassin,
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.scaleTunnel,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.scaleTunnel,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.ogreAnnihilator,
                        enemies.scaleTunnel,
                        enemies.goblinTickerEngineer,
                    ],
                },
            ],
        },
        {
            name: "cavern2",
            map: "cavern",
            connectivity: 0.15,
            zoneNumber: 4,
            startFloor: 16,
            baseDifficulty: 32,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 3, 2],
            requirements: conditions.shadowlordKilled,
            floors: [
                {
                    enemies: [
                        enemies.assassin,
                        enemies.throwbo,
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.scaleTunnel,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.assassin,
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.scaleTunnel,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.ogreAnnihilator,
                        enemies.scaleTunnel,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.ogreAnnihilator,
                        enemies.scaleTunnel,
                        enemies.shaman02,
                        enemies.goblinTickerEngineer,
                    ],
                },
            ],
        },
        {
            name: "cavern3",
            map: "cavern",
            connectivity: 0.15,
            zoneNumber: 4,
            startFloor: 16,
            baseDifficulty: 36,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 3, 2],
            requirements: conditions.crystallordKilled,
            floors: [
                {
                    enemies: [
                        enemies.assassin,
                        enemies.throwbo,
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.ogreAnnihilator,
                        enemies.scaleTunnel,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.ogreAnnihilator,
                        enemies.scaleTunnel,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.ogreAnnihilator,
                        enemies.scaleTunnel,
                        enemies.shaman02,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.glimmerweed,
                        enemies.trollBombardier,
                        enemies.churchbell,
                        enemies.lurkerCrystal,
                        enemies.crystalAnt,
                        enemies.crystalFly,
                        enemies.crystalSpider,
                        enemies.crystalWitch,
                        enemies.ogreAnnihilator,
                        enemies.scaleTunnel,
                        enemies.shaman02,
                        enemies.goblinTickerEngineer,
                    ],
                },
            ],
        },
    ],
    [
        {
            name: "core1",
            map: "core",
            connectivity: 0.15,
            zoneNumber: 5,
            startFloor: 21,
            baseDifficulty: 40,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 3, 2],
            requirements: conditions.crystallordNotKilled,
            floors: [
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.assassin,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.crystalWitch,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                        enemies.goblinTickerEngineer,
                    ],
                },
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.assassin,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.crystalWitch,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                    ],
                },
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.crystalWitch,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                        enemies.golem,
                    ],
                },
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                        enemies.golem,
                    ],
                },
            ],
        },
        {
            name: "core2",
            map: "core",
            connectivity: 0.15,
            zoneNumber: 5,
            startFloor: 21,
            baseDifficulty: 44,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 3, 2],
            requirements: conditions.crystallordKilledNotFire,
            floors: [
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.assassin,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.crystalWitch,
                        enemies.ogreFirebomber,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                    ],
                },
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.assassin,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                        enemies.golem,
                    ],
                },
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                        enemies.golem,
                    ],
                },
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                        enemies.golem,
                    ],
                },
            ],
        },
        {
            name: "core3",
            map: "core",
            connectivity: 0.15,
            zoneNumber: 5,
            startFloor: 21,
            baseDifficulty: 48,
            difficultyStep: 2,
            enemyTypeWeight: [0, 1, 3, 2],
            requirements: conditions.firelordKilled,
            floors: [
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.assassin,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.crystalWitch,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                        enemies.golem,
                    ],
                },
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                        enemies.golem,
                    ],
                },
                {
                    enemies: [
                        enemies.throwbo,
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                        enemies.golem,
                    ],
                },
                {
                    enemies: [
                        enemies.glimmerweed,
                        enemies.lurkerCrystal,
                        enemies.ogreFirebomber,
                        enemies.magmaWormHead,
                        enemies.firebat,
                        enemies.shaman02,
                        enemies.gremlin,
                        enemies.gremlinFiery,
                        enemies.flyRanged03,
                        enemies.golem,
                    ],
                },
            ],
        },
        {
            name: "bog",
            map: "bog",
            connectivity: 0.1,
            zoneNumber: 5,
            startFloor: 21,
            baseDifficulty: 50,
            difficultyStep: 6,
            enemyTypeWeight: [0, 1, 3, 2],
            requirements: conditions.enterBog,
            floors: [
                {
                    floorNumber: 21,
                    enemies: [
                        enemies.bingBong,
                        enemies.flyBloat,
                        enemies.quilledGolem,
                        enemies.poisonWormHead,
                        enemies.goblinPoisonEngineer,
                        enemies.corruptedPilfer,
                        enemies.shaman03,
                        enemies.highPriest,
                        enemies.queenFish,
                    ],
                },
                {
                    floorNumber: 22,
                    enemies: [
                        enemies.bingBong,
                        enemies.flyBloat,
                        enemies.quilledGolem,
                        enemies.poisonWormHead,
                        enemies.goblinPoisonEngineer,
                        enemies.corruptedPilfer,
                        enemies.shaman03,
                        enemies.highPriest,
                        enemies.queenFish,
                    ],
                },
                {
                    floorNumber: 23,
                    enemies: [
                        enemies.bingBong,
                        enemies.flyBloat,
                        enemies.quilledGolem,
                        enemies.poisonWormHead,
                        enemies.goblinPoisonEngineer,
                        enemies.corruptedPilfer,
                        enemies.shaman03,
                        enemies.highPriest,
                        enemies.queenFish,
                    ],
                },
                {
                    floorNumber: 24,
                    enemies: [
                        enemies.bingBong,
                        enemies.flyBloat,
                        enemies.quilledGolem,
                        enemies.poisonWormHead,
                        enemies.goblinPoisonEngineer,
                        enemies.corruptedPilfer,
                        enemies.shaman03,
                        enemies.highPriest,
                        enemies.queenFish,
                    ],
                },
            ],
        },
    ],
];
function getFloorData(floor, flags) {
    if (floor < 1 || floor > 20) {
        floor = 1;
    }
    ;
    const zoneNumber = Math.trunc((floor - 1) / 4);
    const zoneIndex = (floor - 1) % 4;
    const zone = zoneData[zoneNumber];
    for (const z of zone) {
        if (z.requirements(flags)) {
            const override = z.floors[zoneIndex].override ?? null;
            return {
                name: `${z.name}-${zoneIndex + 1}`,
                index: zoneIndex,
                map: override?.map ?? z.map,
                connectivity: override?.connectivity ?? z.connectivity,
                zoneNumber: z.zoneNumber,
                floor: z.startFloor + zoneIndex,
                difficulty: override?.difficulty ?? z.baseDifficulty + (z.difficultyStep * (zoneIndex)),
                enemyTypeWeight: override?.enemyTypeWeight ?? z.enemyTypeWeight,
                enemies: z.floors[zoneIndex].enemies,
                setPieces: z.setPieces ?? null,
                extras: z.extras ?? null,
                resources: z.resources ?? null,
            };
        }
    }
    return null;
}
const weightedDoors = {
    altar: [{ weight: 1, door: 1 }, { weight: 1, door: 5 }],
    shop: [{ weight: 10, door: 1 }, { weight: 7, door: 5 }],
};
const roomOptions = {
    begin: {
        icon: 1,
        sequence: rooms("hoodieTop"),
        autoSpawn: 23,
    },
    shop: {
        weightedDoor: weightedDoors.shop,
        requirements: conditions.storyMode,
    },
    relic: {
        icon: 4,
        door: 5,
        requirements: conditions.noHexDesolation,
        autoSpawn: 1,
    },
    relicUnlocked: {
        icon: 4,
        requirements: conditions.noHexDesolation,
        autoSpawn: 1,
    },
    secret: {
        icon: 7,
        door: 6,
        reqursion: 1,
        sequence: rooms("circinus"),
        autoSpawn: 7,
    },
    hidden: {
        door: 7,
        subFloor: 1,
        autoSpawn: 7,
    },
    altar: {
        icon: 5,
        sequence: rooms("guacamole"),
        weightedDoor: weightedDoors.altar,
        autoSpawn: 7,
        requirements: conditions.priestessRescued,
    },
    altarLocked: {
        icon: 5,
        sequence: rooms("guacamole"),
        door: 5,
        autoSpawn: 7,
        requirements: conditions.priestessRescued,
    },
    altarGuacamole: {
        icon: 5,
        door: 5,
        autoSpawn: 7,
        requirements: conditions.relicGuacamole,
    },
    altarGuacamoleBug: {
        icon: 5,
        door: 5,
        requirements: conditions.relicGuacamoleBug,
    },
    altarAlt: {
        icon: 5,
        autoSpawn: 7,
        sequence: rooms("guacamoleAlt"),
        weightedDoor: weightedDoors.altar,
        requirements: conditions.priestessRescued,
    },
    altarLockedAlt: {
        icon: 5,
        autoSpawn: 7,
        sequence: rooms("guacamoleAlt"),
        door: 5,
        requirements: conditions.priestessRescued,
    },
    altarGuacamoleAlt: {
        icon: 5,
        door: 5,
        autoSpawn: 7,
        requirements: conditions.priestessRescued,
    },
    fountain: {
        icon: 14,
        requirements: conditions.noFountain,
        autoSpawn: 7,
    },
    secretFountain: {
        icon: 14,
        requirements: conditions.secretFountain,
    },
    bard: {
        requirements: conditions.notBardMet,
        autoSpawn: 5,
    },
    relicAltar: {
        icon: 6,
        upgrade: (flag) => flag.relicAltar = 1,
        requirements: conditions.relicAltar,
        autoSpawn: 7,
    },
    blackRabbitShop: {
        requirements: conditions.blackRabbitMet,
    },
    secretShop: {
        icon: 10,
        door: 4,
        requirements: conditions.secretShop,
    },
    talismanSpawn: {
        requirements: conditions.priestessRescued,
    },
    bossRoom: {
        icon: 2,
        requirements: conditions.storyMode,
    },
    nextAreaEntrance: {
        icon: 3,
        noExit: 11,
        door: 2,
        requirements: conditions.storyMode,
    },
    undergroundMarket: {
        subFloor: 1,
        sequence: rooms("marketBaby"),
    },
    storeRoom: {
        subFloor: 1,
        icon: 0,
        door: 7,
        requirements: conditions.rougeMode,
    },
    hoodieMineLocked: {
        door: 4,
        requirements: conditions.hoodieMineL,
        autoSpawn: 5,
    },
    hoodieMineUnlocked: {
        icon: 13,
        requirements: conditions.hoodieMineU,
        autoSpawn: 5,
    },
    hoodieDungeonLocked: {
        door: 4,
        requirements: conditions.hoodieDungeonL,
        autoSpawn: 5,
    },
    hoodieDungeonUnlocked: {
        icon: 13,
        requirements: conditions.hoodieDungeonU,
        autoSpawn: 5,
    },
    hoodieHallLocked: {
        door: 4,
        requirements: conditions.hoodieHallL,
        autoSpawn: 5,
    },
    hoodieHallUnlocked: {
        icon: 13,
        requirements: conditions.hoodieHallU,
        autoSpawn: 5,
    },
    hoodieCavernLocked: {
        door: 4,
        requirements: conditions.hoodieCavernL,
        autoSpawn: 5,
    },
    hoodieCavernUnlocked: {
        icon: 13,
        requirements: conditions.hoodieCavernU,
        autoSpawn: 5,
    },
    dogShadow: {
        requirements: conditions.dogShadow,
    },
    dogEngine: {
        requirements: conditions.dogEngine,
    },
    dogDillion: {
        requirements: conditions.dogDillion,
    },
    alchemistApprentice0: {
        icon: 8,
        requirements: conditions.alchemistApprentice0,
        autoSpawn: 31,
    },
    alchemistApprentice3: {
        icon: 8,
        requirements: conditions.alchemistApprentice3,
        autoSpawn: 31,
    },
    mushroomPurple: {
        door: 3,
        requirements: conditions.mushroomPurple,
        autoSpawn: 7,
    },
    mushroomGreen: {
        noExit: 1,
        door: 3,
        requirements: conditions.mushroomGreen,
    },
    mushroomBlue: {
        requirements: conditions.mushroomBlue,
        autoSpawn: 7,
    },
    priestessEntrance: {
        sequence: rooms("priestesshall1"),
        requirements: conditions.priestessEntrance,
        autoSpawn: 6,
    },
    priestessHall1: {
        noExit: 10,
        requirements: conditions.storyNotWhip,
        autoSpawn: 6,
    },
    priestessHall2: {
        noExit: 10,
        requirements: conditions.storyNotWhip,
        autoSpawn: 6,
    },
    priestessHall3: {
        noExit: 10,
        requirements: conditions.storyNotWhip,
        autoSpawn: 6,
    },
    priestessMain: {
        icon: 8,
        noExit: 11,
        requirements: conditions.storyNotWhip,
        autoSpawn: 6,
    },
    mastersKey: {
        icon: 2,
        noExit: 11,
        requirements: conditions.mastersKey,
        autoSpawn: 6,
    },
    royalRoad: {
        icon: 7,
        requirements: conditions.notWhip,
    },
    royalRoadStart: {
        requirements: conditions.notWhip,
        sequence: rooms("royalRoad1", "royalRoad2", "royalRoad3", "royalRoad4", "queenRoom"),
    },
    queensRoom: {
        requirements: conditions.notWhip,
    },
    tutorialBegin: {
        icon: 1,
        sequence: rooms("tuSecret"),
    },
    dodson: {
        icon: 8,
        requirements: conditions.dodsonNotRescued,
        autoSpawn: 1,
    },
    wayland: {
        requirements: conditions.waylandShop,
        autoSpawn: 1,
    },
    blackRabbitFirst: {
        icon: 9,
        noExit: 13,
        requirements: conditions.blackRabbitFirst,
        autoSpawn: 5
    },
    treasureHunt: {
        noExit: 1,
        upgrade: (flag) => flag.secret_treasure_note = 1,
        requirements: conditions.treasureHunt,
        autoSpawn: 0,
    },
    ratFriendship: {
        tags: "mrrat",
        requirements: conditions.ratFriendship,
        autoSpawn: 3
    },
    rockMimic: {
        requirements: conditions.rockMimic,
        autoSpawn: 1,
    },
    dangerousToGo: {
        noExit: 11,
        requirements: conditions.dangerousToGo,
    },
    dungeonEntrance: {
        icon: 3,
        door: 2,
        requirements: conditions.storyMode,
    },
    dungeonLibrary: {
        door: 5,
        requirements: conditions.dungeonLibrary,
    },
    dibble: {
        icon: 8,
        noExit: 10,
        sequence: rooms("storeRoom"),
        requirements: conditions.dibble,
        autoSpawn: 3,
    },
    dibblesStoreRoom: {
        icon: 8,
        door: 6,
        requirements: conditions.dibblesStoreRoom,
        autoSpawn: 1,
    },
    kurtz: {
        tag: "kurtz",
        noExit: 5,
        requirements: conditions.kurtz,
    },
    threeChests: {
        noExit: 11,
        requirements: conditions.storyMode,
    },
    hallLibrary: {
        icon: 8,
        requirements: conditions.storyNotWhip,
    },
    pitSpikes: {
        sequence: rooms("threeChests"),
    },
    secretEast: {
        noExit: 5,
        sequence: rooms("secretEast"),
    },
    partyPopcornRoom: {
        requirements: conditions.partyPopcornRoom,
    },
    hallLibraryCombat: {
        noExit: 10,
        sequence: rooms("library"),
        requirements: conditions.hallLibraryCombat,
    },
    campsite: {
        sequence: rooms("hiddenHallway"),
    },
    secretWest: {
        sequence: rooms("secretWest"),
    },
    maybeSecretEast: {
        sequence: rooms("maybeSecretEast"),
    },
};
const encounters = {
    mine: {
        extra: {
            special: {
                rooms: [
                    { tag: "black_rabbit", name: "BlackRabbit" },
                    { tag: "bossRoom", name: "SandRoom" },
                ]
            },
            shop: {
                rooms: [
                    { weight: 1, name: "Shop", ...roomOptions.shop },
                ]
            },
            hoody: {
                rooms: [
                    { weight: 1, name: "sleepyHoodyRoom", door: 7 },
                ]
            },
        },
        small: {
            begin: {
                default: roomOptions.begin,
                rooms: [
                    { weight: 4, name: "begin" },
                ]
            },
            easy: {
                default: { autoSpawn: 31 },
                rooms: [
                    { weight: 1, name: "Spinner" },
                    { weight: 1, name: "Spikes" },
                    { weight: 3, name: "Pillar", difficulty: [1, 0] },
                    { weight: 1, name: "Plain", difficulty: [1, 0] },
                ]
            },
            normal: {
                default: { difficulty: [1, 0], autoSpawn: 31 },
                rooms: [
                    { weight: 3, name: "MineCart" },
                    { weight: 3, name: "Pillar" },
                    { weight: 3, name: "PillarHole" },
                    { weight: 3, name: "PillarSpinner" },
                    { weight: 3, name: "RockWall", difficulty: [1, -1] },
                    { weight: 3, name: "SouthNorthHole", prohibitedEnemies: ["bobo"], autoSpawn: 15 },
                    { weight: 3, name: "StationarySpinners" },
                    { weight: 3, name: "BrokenCarts" },
                    { weight: 3, name: "StatueSpinner" },
                    { weight: 2, name: "Bridge", difficulty: [0.5, 0] },
                    { weight: 3, name: "EWSpinners", noExit: 5, difficulty: [0.25, 0], autoSpawn: 15 },
                    { weight: 3, name: "CornerHoles", prohibitedEnemies: ["bobo"] },
                    { weight: 3, name: "DualPillar" },
                    { weight: 3, name: "Spikes" },
                    { weight: 3, name: "SpikePatch" },
                    { weight: 3, name: "PillarSpawner", difficulty: [1, 1] },
                    { weight: 1, name: "SetPiece", difficulty: [1, -2] },
                    { weight: 2, name: "HoleEW", noExit: 5, prohibitedEnemies: ["bobo"] },
                    { weight: 2, name: "HoleEWSpinner", noExit: 5, difficulty: [0, 0], prohibitedEnemies: ["bobo"] },
                    { weight: 4, name: "Plain", difficulty: [1, 1], autoSpawn: -1 },
                    { weight: 2, name: "HazardHeavy", difficulty: [1, -2] },
                    { weight: 2, name: "DangerWalls", noExit: 10 },
                    { weight: 3, name: "TilePattern", difficulty: [1, 1], autoSpawn: -1 },
                    { weight: 3, name: "CornerRocks", autoSpawn: -1 },
                    { weight: 2, name: "RockPath", autoSpawn: 15 },
                    { weight: 3, name: "DiagonalHole", difficulty: [1, -2], autoSpawn: -1 },
                    { weight: 3, name: "CenterTorches", autoSpawn: -1 },
                    { weight: 2, name: "Ruins", autoSpawn: -1 },
                    { weight: 3, name: "Statues", prohibitedEnemies: ["bobo"] },
                    { weight: 3, name: "LargeSpinnerTrack" },
                ]
            },
            special: {
                rooms: [
                    { tag: "DodsonCageEncounter", name: "DodsonCage", ...roomOptions.dodson },
                    { tag: "waylandshop", name: "WaylandShop", ...roomOptions.wayland },
                    { tag: "WaylandShopHallway", name: "WaylandShopHallway", ...roomOptions.wayland },
                    { tag: "mushroom", name: "MushroomFamily", ...roomOptions.mushroomGreen },
                    { tag: "mushroom", name: "MushroomFarm", ...roomOptions.mushroomBlue },
                    { tag: "black_rabbit_first", name: "BlackRabbit", ...roomOptions.blackRabbitFirst },
                    { tag: "hoodie_entrance", name: "Hoodie_Locked", ...roomOptions.hoodieMineLocked },
                    { tag: "hoodie_entrance", name: "Hoodie_Unlocked", ...roomOptions.hoodieMineUnlocked },
                    { tag: "tribute_fountain", name: "TributeFountain", ...roomOptions.fountain },
                    { tag: "begin_tutorial", name: "Begin", ...roomOptions.tutorialBegin },
                    { tag: "tutorial_jump", name: "Jump", autoSpawn: 4 },
                    { tag: "tutorial_attack", name: "Attack" },
                    { tag: "tutorial_bomb", name: "Bomb" },
                    { tag: "tutorial_relic", name: "Relic", autoSpawn: 1 },
                    { tag: "tutorial_secret", name: "Secret", autoSpawn: 4 },
                    { tag: "end", name: "Normal", icon: 3, autoSpawn: 23 },
                    { tag: "down_boss", name: "Boss", icon: 2, autoSpawn: 5 },
                    { tag: "end_tutorial", name: "Tutorial", icon: 3 },
                    { tag: "next_entrance", name: "DungeonEntrance", ...roomOptions.dungeonEntrance },
                ]
            },
            relic: {
                default: roomOptions.relic,
                rooms: [
                    { weight: 4, name: "Pots" },
                    { weight: 4, name: "Torches" },
                    { weight: 4, name: "Hole", noExit: 7 },
                    { weight: 1, name: "TorchPuzzle", noExit: 1 },
                    { weight: 4, name: "Statues", autoSpawn: 7 },
                ]
            },
            relic_unlocked: {
                default: roomOptions.relicUnlocked,
                rooms: [
                    { weight: 4, name: "Pots" },
                    { weight: 4, name: "Torches" },
                    { weight: 4, name: "Hole", noExit: 7 },
                    { weight: 1, name: "TorchPuzzle", noExit: 1 },
                    { weight: 4, name: "Statues", autoSpawn: 7 },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 4, name: "WaterChest" },
                    { weight: 4, name: "Carts" },
                    { weight: 1, name: "Altar" },
                    { weight: 3, name: "CursedTorch" },
                    { weight: 3, name: "Crystals" },
                    { weight: 4, name: "Chest" },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 1, name: "Blessing", autoSpawn: 6 },
                    { weight: 3, name: "Items", autoSpawn: 6 },
                    { weight: 3, name: "Chest", autoSpawn: 6 },
                    { weight: 3, name: "ChestCommon" },
                    { weight: 3, name: "KeyBlock" },
                    { weight: 3, name: "Bombs" },
                    { weight: 10, name: "DogShadow", ...roomOptions.dogShadow },
                    { weight: 2, name: "LeverBlocks", noExit: 1 },
                    { weight: 1, name: "Tent" },
                    { weight: 2, name: "Nugg" },
                    { weight: 3, name: "Bard", ...roomOptions.bard },
                    { weight: 3, name: "TributeFountain", ...roomOptions.secretFountain },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 3, name: "WaterChest" },
                    { weight: 3, name: "Carts" },
                    { weight: 1, name: "TreasureHunt", ...roomOptions.treasureHunt },
                    { weight: 1, name: "Altar" },
                    { weight: 2, name: "CursedTorch" },
                    { weight: 2, name: "CursedRelic" },
                    { weight: 3, name: "Crystals" },
                    { weight: 1, name: "Lab", autoSpawn: 5 },
                    { weight: 3, name: "Chest" },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 1, name: "Blessing" },
                    { weight: 1, name: "Blessing02", autoSpawn: 3 },
                    { weight: 1, name: "ButchersRoom", autoSpawn: 3 },
                    { weight: 1, name: "RatFriendship", ...roomOptions.ratFriendship },
                    { weight: 3, name: "Chest" },
                    { weight: 2, name: "Bard", ...roomOptions.bard },
                ]
            },
            treasure: {
                default: { autoSpawn: 31 },
                rooms: [
                    { weight: 3, name: "Skeleton", difficulty: [1, 0] },
                    { weight: 3, name: "Rocks", difficulty: [1, 0] },
                    { weight: 3, name: "Spikes", difficulty: [1, 0] },
                    { weight: 2, name: "HoleBridges", difficulty: [1, 0] },
                    { weight: 3, name: "LockedRocks", noExit: 4 },
                    { weight: 2, name: "StatuePuzzle", autoSpawn: 3 },
                    { weight: 3, name: "LockedBlocks", noExit: 1 },
                    { weight: 1, name: "CursedRelics", door: 5 },
                    { weight: 3, name: "SpikeCage", difficulty: [1, -4] },
                    { weight: 3, name: "RockCage", difficulty: [1, -4] },
                    { weight: 1, name: "SpikeSacrifice" },
                    { weight: 2, name: "DiagonalRocks" },
                    { weight: 2, name: "HealthLever", noExit: 4 },
                    { weight: 2, name: "Pillar", difficulty: [1, 0] },
                ]
            },
            treasureBasic: {
                default: { autoSpawn: 31 },
                rooms: [
                    { weight: 1, name: "Skeleton", difficulty: [0.5, 0] },
                    { weight: 1, name: "Rocks", difficulty: [0.5, 0] },
                    { weight: 1, name: "Plain", difficulty: [0.5, 0] },
                ]
            },
            altar: {
                default: roomOptions.altar,
                rooms: [
                    { weight: 2, name: "Torches" },
                    { weight: 2, name: "Statues" },
                    { weight: 2, name: "Bridges" },
                    { weight: 2, name: "Tiled", noExit: 4 },
                ]
            },
            altar_locked: {
                default: roomOptions.altarLocked,
                rooms: [
                    { weight: 2, name: "Torches" },
                    { weight: 2, name: "Statues" },
                    { weight: 2, name: "Bridges" },
                    { weight: 2, name: "Tiled", noExit: 4 },
                ]
            },
            altar_guacamole: {
                default: roomOptions.altarGuacamole,
                rooms: [
                    { weight: 2, name: "Torches" },
                    { weight: 2, name: "Statues" },
                    { weight: 2, name: "Bridges" },
                    { weight: 2, name: "Tiled", ...roomOptions.altarGuacamoleBug, noExit: 4 },
                ]
            },
        },
        large: {
            normal: {
                default: { difficulty: [1, 2], autoSpawn: -1 },
                rooms: [
                    { weight: 2, name: "RailStatues" },
                    { weight: 2, name: "LargeRail" },
                    { weight: 2, name: "HoleBridge" },
                    { weight: 2, name: "RailSnake" },
                    { weight: 2, name: "Staging", autoSpawn: 31 },
                    { weight: 2, name: "PillarRocks" },
                    { weight: 2, name: "SpikeDonut", autoSpawn: 31 },
                    { weight: 3, name: "RailBrideLoop", prohibitedEnemies: ["bobo"], autoSpawn: 31 },
                    { weight: 3, name: "RailBridge", difficulty: [1, -6], autoSpawn: 9,
                        enemies: [enemies.flyRanged, enemies.flyRanged02, enemies.bat,], },
                    { weight: 1, name: "OilBarrels", autoSpawn: 59 },
                    { weight: 2, name: "MineField", difficulty: [0.4, 2] },
                    { weight: 2, name: "Bridges", difficulty: [1, -2], autoSpawn: 13 },
                    { weight: 2, name: "Spikes" },
                    { weight: 3, name: "CornerNE", noExit: 12, difficulty: [1, 0], prohibitedEnemies: ["bobo"] },
                    { weight: 1, name: "LandBridgeNS", noExit: 10, difficulty: [1, 0], autoSpawn: 15,
                        enemies: [enemies.flyRanged,], },
                    { weight: 2, name: "DualPillars" },
                    { weight: 2, name: "SpikeBridge", noExit: 5, difficulty: [0, 0], autoSpawn: 15,
                        enemies: [enemies.flyRanged,], },
                    { weight: 3, name: "CornerSW", noExit: 3, difficulty: [1, 0], prohibitedEnemies: ["bobo"] },
                    { weight: 2, name: "RockCross", autoSpawn: 31 },
                    { weight: 2, name: "SlotHoles", autoSpawn: 31 },
                    { weight: 2, name: "RockColumns", autoSpawn: 31 },
                    { weight: 3, name: "ATrack" },
                    { weight: 3, name: "DynamicHole" },
                    { weight: 2, name: "TeeRocks" },
                    { weight: 3, name: "HoleArrows", difficulty: [1, 1], autoSpawn: 31 },
                    { weight: 1, name: "DualSetPiece", autoSpawn: 31 },
                    { weight: 3, name: "Arena", noExit: 10 },
                    { weight: 2, name: "ArenaTrack", noExit: 10, difficulty: [1, 1] },
                    { weight: 2, name: "QuadPillars" },
                    { weight: 2, name: "RockArrowMaze", autoSpawn: 11 },
                    { weight: 2, name: "RailGuantlet", noExit: 5, difficulty: [0.5, -2], autoSpawn: 31 },
                    { weight: 2, name: "HazardHeavy" },
                    { weight: 2, name: "ArrowGuantlet", difficulty: [1, -2], autoSpawn: 31 },
                    { weight: 2, name: "DonutSpinner", noExit: 5, difficulty: [1, -2], autoSpawn: 31 },
                    { weight: 2, name: "CornerRocks", prohibitedEnemies: ["jumperFire", "jumperOil", "jumperWater"] },
                    { weight: 2, name: "TeeJunction", noExit: 1, autoSpawn: 31 },
                    { weight: 3, name: "CornerBridge" },
                    { weight: 3, name: "BridgeHole" },
                    { weight: 3, name: "BigRocks", prohibitedEnemies: ["jumperFire", "jumperOil", "jumperWater"] },
                    { weight: 3, name: "TriangleRocks", autoSpawn: 31 },
                    { weight: 2, name: "SnakeBridge", autoSpawn: 31 },
                    { weight: 3, name: "RandomBlocks", autoSpawn: 31 },
                    { weight: 3, name: "Empty", difficulty: [1, 3] },
                    { weight: 4, name: "TwoSetPiece", autoSpawn: 31 },
                    { weight: 3, name: "Grassy", difficulty: [1, 3] },
                    { weight: 2, name: "FivePillars" },
                    { weight: 1, name: "SnakeTrack", difficulty: [1, 0] },
                    { weight: 3, name: "Torches", autoSpawn: 31 },
                    { weight: 2, name: "RailIslands", difficulty: [1, 0], autoSpawn: 31 },
                    { weight: 1, name: "MushroomGrowOp" },
                ]
            },
            special: {
                rooms: [
                    { tag: "RockMimicEncounter", name: "RockMimic", ...roomOptions.rockMimic },
                    { tag: "mushroom", name: "MushroomDarkness", ...roomOptions.mushroomPurple, difficulty: [1, 0] },
                    { tag: "mushroom_apprentice", name: "AlchemistApprentice0", ...roomOptions.alchemistApprentice0 },
                    { tag: "mushroom_apprentice", name: "AlchemistApprentice3", ...roomOptions.alchemistApprentice3 },
                    { tag: "relic_altar", name: "RelicAltar", ...roomOptions.relicAltar },
                    { tag: "tutorial_throw", name: "Throw", doorOverride: 2, autoSpawn: 4 },
                    { tag: "tutorial_pilfer", name: "Pilfer", autoSpawn: 4 },
                    { tag: "pre_room", name: "BeforeDungeonEntrance", ...roomOptions.dungeonEntrance },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 5, name: "GrassChests" },
                    { weight: 1, name: "Altar" },
                    { weight: 5, name: "Blessing" },
                    { weight: 5, name: "BasicItems" },
                    { weight: 5, name: "Gold" },
                    { weight: 3, name: "BlackRabbitShop", ...roomOptions.blackRabbitShop },
                    { weight: 5, name: "Potion" },
                    { weight: 5, name: "Chest" },
                    { weight: 3, name: "CursedTorch" },
                    { weight: 1, name: "DangerousToGo", ...roomOptions.dangerousToGo },
                    { weight: 5, name: "SpikedFood" },
                    { weight: 3, name: "DoubleLockBlock", noExit: 1 },
                    { weight: 4, name: "StatueBombPuzzle" },
                    { weight: 1, name: "Pillars" },
                    { weight: 1, name: "OilyBridge", noExit: 7, autoSpawn: 5 },
                    { weight: 1, name: "DarkMaze", autoSpawn: 6 },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 2, name: "LeverBlocks" },
                    { weight: 2, name: "GrassChests" },
                    { weight: 2, name: "TorchPuzzle", autoSpawn: 1 },
                    { weight: 2, name: "Keys", autoSpawn: 1 },
                    { weight: 2, name: "Potions" },
                    { weight: 1, name: "Blessing" },
                    { weight: 1, name: "Blessing02" },
                    { weight: 1, name: "CursedRelics", autoSpawn: 31 },
                    { weight: 1, name: "Altar", noExit: 1 },
                    { weight: 2, name: "PressureTrap", noExit: 3 },
                    { weight: 2, name: "ChooseBlessing", autoSpawn: 31 },
                    { weight: 2, name: "BobosLair" },
                    { weight: 2, name: "CaveIn", autoSpawn: 31 },
                    { weight: 1, name: "Gap" },
                ]
            },
            treasure: {
                default: { autoSpawn: 31 },
                rooms: [
                    { weight: 3, name: "ItemBlocks", difficulty: [0, -2] },
                    { weight: 3, name: "SpikedChest", noExit: 4, difficulty: [0, 1] },
                    { weight: 3, name: "HoleSpikeChest", noExit: 8, difficulty: [1, 1] },
                    { weight: 3, name: "TorchPuzzle", difficulty: [1, -2] },
                    { weight: 3, name: "SpikeRails", noExit: 11, difficulty: [0, -2], autoSpawn: 15 },
                    { weight: 3, name: "BridgePuzzle", noExit: 5, difficulty: [0, -2], autoSpawn: 5 },
                    { weight: 3, name: "BombPuzzle", difficulty: [0, -2], autoSpawn: 15 },
                    { weight: 4, name: "JustSomeTreasure", noExit: 1 },
                    { weight: 2, name: "LeverBridge", noExit: 8, difficulty: [0, -2] },
                    { weight: 3, name: "VerticalBridge", difficulty: [0, -2] },
                    { weight: 4, name: "Decision", noExit: 4, difficulty: [0, -2] },
                    { weight: 3, name: "HealthLever", difficulty: [0, -2], autoSpawn: -1 },
                    { weight: 5, name: "SecretShop", ...roomOptions.secretShop, autoSpawn: 5 },
                    { weight: 2, name: "OilChest", difficulty: [0.5, -6] },
                    { weight: 2, name: "FireyChest", autoSpawn: 7 },
                    { weight: 2, name: "ElectrifiedChest", autoSpawn: 7 },
                    { weight: 4, name: "JustSomeTreasure02" },
                    { weight: 2, name: "Nexus", autoSpawn: 7 },
                    { weight: 1, name: "TwoBombsOneKey" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 2, name: "Choice", autoSpawn: 7 },
                    { weight: 2, name: "DoubleRail", difficulty: [0, -2] },
                    { weight: 1, name: "RockLock", difficulty: [0.5, -4], autoSpawn: 15 },
                ]
            },
            challange: {
                rooms: [
                    { weight: 1, name: "GamblingRoom", icon: 9, autoSpawn: 7 },
                    { weight: 1, name: "Combat", icon: 11, autoSpawn: 15 },
                ]
            },
        },
    },
    dungeon: {
        extra: {
            special: {
                rooms: [
                    { tag: "hoody", name: "sleepyHoodyRoom" },
                    { tag: "black_rabbit", name: "BlackRabbit" },
                    { tag: "boss", name: "SandRoom", autoSpawn: 1 },
                ]
            },
            shop: {
                rooms: [
                    { weight: 1, name: "Shop", ...roomOptions.shop },
                ]
            },
        },
        small: {
            begin: {
                default: roomOptions.begin,
                rooms: [
                    { weight: 3, name: "Begin_Holes" },
                    { weight: 3, name: "Begin_Plain" },
                    { weight: 3, name: "Begin_Statues" },
                    { weight: 1, name: "Begin_Squeeze", autoSpawn: 7 },
                    { weight: 2, name: "Begin_Cells", autoSpawn: 7 },
                ]
            },
            normal: {
                default: { difficulty: [1, -2], autoSpawn: 31 },
                rooms: [
                    { weight: 3, name: "Spikes" },
                    { weight: 3, name: "LongHole" },
                    { weight: 3, name: "Pillars", noExit: 10, difficulty: [1, 0] },
                    { weight: 3, name: "Water", difficulty: [1, 0] },
                    { weight: 3, name: "Torches", difficulty: [1, -1] },
                    { weight: 3, name: "DynamicPillar", difficulty: [1, -1] },
                    { weight: 1, name: "ArrowBridge", noExit: 5, difficulty: [0, 0], autoSpawn: 15 },
                    { weight: 1, name: "FlamingArrows", noExit: 10, difficulty: [0, 0] },
                    { weight: 3, name: "TallBlocks", difficulty: [1, 0] },
                    { weight: 1, name: "SetPiece" },
                    { weight: 2, name: "Track" },
                    { weight: 3, name: "Empty", difficulty: [1, 0], autoSpawn: -1 },
                    { weight: 3, name: "SmallBowTie", noExit: 5, difficulty: [1, 0] },
                    { weight: 3, name: "RazorBlade", noExit: 10, difficulty: [1, 0] },
                    { weight: 2, name: "VerticalHole", noExit: 5, difficulty: [1, -6] },
                ]
            },
            special: {
                rooms: [
                    { tag: "dungeon_entrance", name: "Entrance", ...roomOptions.dungeonEntrance },
                    { tag: "store_room", name: "DibblesStoreRoom", ...roomOptions.dibblesStoreRoom },
                    { tag: "dungeonlibrary", name: "Library", ...roomOptions.dungeonLibrary },
                    { tag: "priestess", name: "PriestessEntrance", ...roomOptions.priestessEntrance },
                    { tag: "hoodie_entrance", name: "Hoodie_Locked", ...roomOptions.hoodieDungeonLocked },
                    { tag: "hoodie_entrance", name: "Hoodie_Unlocked", ...roomOptions.hoodieDungeonUnlocked },
                    { tag: "tribute_fountain", name: "TributeFountain", ...roomOptions.fountain },
                    { tag: "next_entrance", name: "HallEntrance", ...roomOptions.dungeonEntrance },
                    { tag: "end", name: "Normal", icon: 3, autoSpawn: 31 },
                    { tag: "endBoss", name: "Boss", icon: 2, autoSpawn: 23 },
                    { tag: "down_boss", name: "Boss", icon: 2 },
                ]
            },
            relic: {
                default: roomOptions.relic,
                rooms: [
                    { weight: 3, name: "Statues" },
                    { weight: 3, name: "Water" },
                    { weight: 2, name: "Pillars", noExit: 10 },
                    { weight: 3, name: "WindingBridge", noExit: 1 },
                    { weight: 3, name: "Holes" },
                ]
            },
            relic_unlocked: {
                default: roomOptions.relicUnlocked,
                rooms: [
                    { weight: 3, name: "Statues" },
                    { weight: 3, name: "Water" },
                    { weight: 2, name: "Pillars", noExit: 10 },
                    { weight: 3, name: "WindingBridge", noExit: 1 },
                    { weight: 3, name: "Holes" },
                ]
            },
            treasure: {
                default: { autoSpawn: 7 },
                rooms: [
                    { weight: 3, name: "StatueChest", autoSpawn: 1 },
                    { weight: 3, name: "CellNE", noExit: 3, autoSpawn: 1 },
                    { weight: 3, name: "CellsEW", noExit: 10, autoSpawn: 1 },
                    { weight: 3, name: "DiamondFloor" },
                    { weight: 3, name: "CellN", noExit: 1 },
                    { weight: 3, name: "CellE", noExit: 7 },
                    { weight: 3, name: "CellW", noExit: 13 },
                    { weight: 3, name: "BrickStatue" },
                    { weight: 3, name: "SpikedChest" },
                    { weight: 1, name: "SpikedHallwayW", noExit: 13 },
                    { weight: 1, name: "SpikedHallwayE", noExit: 11 },
                    { weight: 3, name: "Track" },
                    { weight: 1, name: "TinyRoom", noExit: 11, autoSpawn: 1 },
                    { weight: 2, name: "HealthLever" },
                    { weight: 3, name: "JustAChest" },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 5, name: "OpenedChest", autoSpawn: 5 },
                    { weight: 5, name: "WaterChest", autoSpawn: 5 },
                    { weight: 3, name: "SpikeSacrifice", autoSpawn: 5 },
                    { weight: 3, name: "CursedTorch", autoSpawn: 5 },
                    { weight: 3, name: "BombStorage", autoSpawn: 5 },
                    { weight: 1, name: "Altar" },
                    { weight: 3, name: "StorageRoom" },
                    { weight: 3, name: "DualLevers", autoSpawn: 3 },
                    { weight: 5, name: "Potion", autoSpawn: 3 },
                    { weight: 1, name: "Blessing", autoSpawn: 3 },
                    { weight: 10, name: "DogEngine", ...roomOptions.dogEngine, autoSpawn: 3 },
                    { weight: 3, name: "Talisman", ...roomOptions.talismanSpawn },
                    { weight: 2, name: "KeyPillar" },
                    { weight: 1, name: "BigChest" },
                    { weight: 2, name: "Nugg", autoSpawn: 5 },
                    { weight: 3, name: "Bard", ...roomOptions.bard, autoSpawn: 5 },
                    { weight: 3, name: "TributeFountain", ...roomOptions.secretFountain },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 10, name: "Kurtz", ...roomOptions.kurtz, autoSpawn: 5 },
                    { weight: 3, name: "DoubleChest", autoSpawn: 5 },
                    { weight: 3, name: "GoldStatue", autoSpawn: 5 },
                    { weight: 3, name: "WaterChest", autoSpawn: 5 },
                    { weight: 3, name: "SpikeSacrifice", autoSpawn: 5 },
                    { weight: 3, name: "CursedTorch", autoSpawn: 5 },
                    { weight: 3, name: "BombStorage", autoSpawn: 5 },
                    { weight: 1, name: "TripleCursedChest", autoSpawn: 6 },
                    { weight: 1, name: "DoubleBlessing" },
                    { weight: 1, name: "Lab", autoSpawn: 5 },
                    { weight: 2, name: "CursedRelic" },
                    { weight: 1, name: "PyroLair", autoSpawn: 3 },
                    { weight: 3, name: "StatueChest", autoSpawn: 5 },
                    { weight: 3, name: "CostedLever" },
                    { weight: 1, name: "CursedRelic", autoSpawn: 1 },
                    { weight: 1, name: "FourBigChests" },
                    { weight: 2, name: "Bard" },
                ]
            },
            altar: {
                default: roomOptions.altar,
                rooms: [
                    { weight: 1, name: "Pillars" },
                    { weight: 1, name: "CellE" },
                    { weight: 1, name: "Statues" },
                ]
            },
            altar_locked: {
                default: roomOptions.altarLocked,
                rooms: [
                    { weight: 1, name: "Pillars" },
                    { weight: 1, name: "CellE" },
                    { weight: 1, name: "Statues" },
                ]
            },
            altar_guacamole: {
                default: roomOptions.altarLocked,
                rooms: [
                    { weight: 1, name: "Pillars" },
                    { weight: 1, name: "CellE" },
                    { weight: 1, name: "Statues" },
                ]
            },
        },
        large: {
            normal: {
                default: { difficulty: [1, 2], autoSpawn: 31 },
                rooms: [
                    { weight: 3, name: "CenterTurret", difficulty: [1, 1] },
                    { weight: 3, name: "Flooded", autoSpawn: -1 },
                    { weight: 3, name: "DualPillar" },
                    { weight: 3, name: "SlotHoles", difficulty: [1, 0] },
                    { weight: 3, name: "CornerWater", autoSpawn: 59 },
                    { weight: 3, name: "BridgeEW", noExit: 5, difficulty: [1, -2] },
                    { weight: 3, name: "BridgeNS", noExit: 10, difficulty: [1, -2], autoSpawn: -1 },
                    { weight: 2, name: "BendNE", noExit: 12, difficulty: [1, -2], autoSpawn: -1 },
                    { weight: 1, name: "BendNEPillar", noExit: 12, difficulty: [1, -4] },
                    { weight: 2, name: "BendSW", noExit: 3, difficulty: [1, -2], autoSpawn: -1 },
                    { weight: 1, name: "BendSWPillar", noExit: 3, difficulty: [1, -4] },
                    { weight: 3, name: "BallChainDynamic", autoSpawn: -1 },
                    { weight: 3, name: "BallChain", difficulty: [1, -2], autoSpawn: 27 },
                    { weight: 3, name: "HolesBlocks" },
                    { weight: 3, name: "DynamicShape", difficulty: [1, 0], autoSpawn: -1 },
                    { weight: 3, name: "Cells", difficulty: [1, 0] },
                    { weight: 1, name: "Guantlet", noExit: 5, difficulty: [0, 0], autoSpawn: 9 },
                    { weight: 1, name: "Furnace", difficulty: [1, -10] },
                    { weight: 1, name: "Turrets", difficulty: [0.5, -6] },
                    { weight: 3, name: "DynamicHole", autoSpawn: -1 },
                    { weight: 3, name: "Gutters", autoSpawn: -1 },
                    { weight: 3, name: "Blocks" },
                    { weight: 3, name: "Sewer" },
                    { weight: 3, name: "CornerTurrets", difficulty: [1, 0] },
                    { weight: 2, name: "SpikeStrip", noExit: 5, difficulty: [1, -2] },
                    { weight: 3, name: "Cross", autoSpawn: -1 },
                    { weight: 3, name: "Ess", noExit: 5, difficulty: [1, 0] },
                    { weight: 3, name: "DonutSpinner", difficulty: [1, -4] },
                    { weight: 3, name: "Plain", difficulty: [1, 3], autoSpawn: -1 },
                    { weight: 3, name: "HazardHeavy", autoSpawn: -1 },
                    { weight: 3, name: "Pools", autoSpawn: -1 },
                    { weight: 3, name: "ElBlocks", autoSpawn: -1 },
                    { weight: 3, name: "BowTie", noExit: 5, autoSpawn: -1 },
                    { weight: 3, name: "RazorBlade", noExit: 10, autoSpawn: -1 },
                    { weight: 2, name: "WestDiagonal", noExit: 8, autoSpawn: -1 },
                    { weight: 2, name: "EastDiagonal", noExit: 2, autoSpawn: -1 },
                    { weight: 1, name: "FireWitchHunt", difficulty: [1, -4], autoSpawn: -1 },
                    { weight: 1, name: "FurnacePart2", difficulty: [1, -5], autoSpawn: 30 },
                    { weight: 2, name: "SlidingWall", autoSpawn: -1 },
                    { weight: 2, name: "SlantedPillars", autoSpawn: -1 },
                    { weight: 2, name: "CornerSlants", autoSpawn: -1 },
                    { weight: 1, name: "SewerPart2", noExit: 5, difficulty: [1, -8] },
                    { weight: 3, name: "SewerPart3", difficulty: [1, -4] },
                    { weight: 2, name: "SewerPart4", difficulty: [1, -4] },
                    { weight: 1, name: "VerticalHall", noExit: 10, difficulty: [0, 0], autoSpawn: 15 },
                    { weight: 3, name: "CornerPillar", autoSpawn: -1 },
                    { weight: 3, name: "OffsetTurrets", difficulty: [1, -2] },
                    { weight: 2, name: "MorningStarBridge", noExit: 1, difficulty: [0, 0] },
                ]
            },
            treasure: {
                default: { autoSpawn: 7 },
                rooms: [
                    { weight: 4, name: "CornerTurrets" },
                    { weight: 4, name: "RockTurret" },
                    { weight: 4, name: "StatuePuzzle" },
                    { weight: 4, name: "CrystalHole", noExit: 10 },
                    { weight: 4, name: "TorchPuzzle", noExit: 10 },
                    { weight: 4, name: "DoubleCell", noExit: 11 },
                    { weight: 4, name: "CellBlock", difficulty: [1, 0], autoSpawn: 31 },
                    { weight: 4, name: "SpikeSacrifice" },
                    { weight: 3, name: "SetPiece", difficulty: [1, 0] },
                    { weight: 2, name: "DoubleSetPiece", noExit: 1, difficulty: [1, -2], autoSpawn: 31 },
                    { weight: 4, name: "JustSomeTreasure", autoSpawn: 3 },
                    { weight: 3, name: "HealthLever", autoSpawn: 3 },
                    { weight: 3, name: "Guantlet", autoSpawn: 3 },
                    { weight: 5, name: "SecretShop", ...roomOptions.secretShop, autoSpawn: 5 },
                    { weight: 3, name: "TurretDodging" },
                    { weight: 3, name: "BarrelTimer", noExit: 12, difficulty: [1, -6] },
                    { weight: 1, name: "BladeBridge" },
                    { weight: 2, name: "Choice" },
                    { weight: 1, name: "SpikeWave", noExit: 8, autoSpawn: 5 },
                    { weight: 1, name: "StoreHouse" },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 1, name: "Altar" },
                    { weight: 3, name: "BlackRabbitShop", ...roomOptions.blackRabbitShop },
                    { weight: 5, name: "Chest" },
                    { weight: 5, name: "TorchPuzzle" },
                    { weight: 5, name: "Cells" },
                    { weight: 5, name: "Chest02" },
                    { weight: 5, name: "Gold" },
                    { weight: 4, name: "Bombs" },
                    { weight: 4, name: "Keys" },
                    { weight: 1, name: "Blessing" },
                    { weight: 4, name: "ChestItem" },
                    { weight: 3, name: "Talisman", ...roomOptions.talismanSpawn },
                    { weight: 1, name: "Garden", noExit: 14, autoSpawn: 1 },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 1, name: "AltarCell" },
                    { weight: 2, name: "WaterChest" },
                    { weight: 1, name: "Altar" },
                    { weight: 2, name: "ItemRocks" },
                    { weight: 2, name: "TorchPuzzle" },
                    { weight: 2, name: "Keys" },
                    { weight: 2, name: "Potions" },
                    { weight: 2, name: "DoubleWall" },
                    { weight: 1, name: "BlessingChoice" },
                    { weight: 2, name: "CellChests" },
                    { weight: 2, name: "DoubleLeverTrap" },
                    { weight: 2, name: "TorchTurretPuzzle" },
                    { weight: 2, name: "Gap" },
                    { weight: 2, name: "JumpTorchPuzzle" },
                    { weight: 1, name: "PressurePlate" },
                    { weight: 1, name: "Islands" },
                ]
            },
            special: {
                rooms: [
                    { tag: "dibble", name: "DibblesEmporium", ...roomOptions.hidden },
                    { tag: "priestesshall1", name: "PriestessHall1", ...roomOptions.priestessHall1 },
                    { tag: "priestesshall2", name: "PriestessHall2", ...roomOptions.priestessHall2 },
                    { tag: "priestesshall3", name: "PriestessHall3", ...roomOptions.priestessHall3 },
                    { tag: "priestess_main", name: "Priestess", ...roomOptions.priestessMain },
                    { tag: "masters_key", name: "MastersKey", ...roomOptions.mastersKey },
                    { tag: "down_boss", name: "StonelordEntrance", ...roomOptions.bossRoom },
                    { tag: "relic_altar", name: "RelicAltar", ...roomOptions.relicAltar },
                    { tag: "pre_room", name: "BeforeHallEntrance", ...roomOptions.dungeonEntrance },
                ]
            },
            relic: {
                default: roomOptions.relic,
                rooms: [
                    { weight: 1, name: "Dungeon_Large_Relic_Torches", autoSpawn: 7 },
                    { weight: 1, name: "Dungeon_Large_Relic_Circle", autoSpawn: 7 },
                    { weight: 1, name: "Dungeon_Large_Relic_Cell", autoSpawn: 7 },
                ]
            },
            unlocked: {
                default: roomOptions.relicUnlocked,
                rooms: [
                    { weight: 1, name: "Dungeon_Large_Relic_Torches", autoSpawn: 7 },
                    { weight: 1, name: "Dungeon_Large_Relic_Circle", autoSpawn: 7 },
                    { weight: 1, name: "Dungeon_Large_Relic_Cell", autoSpawn: 7 },
                ]
            },
            challange: {
                rooms: [
                    { weight: 1, name: "GamblingRoom", icon: 9, autoSpawn: 7 },
                    { weight: 1, name: "Combat", icon: 11, autoSpawn: 15 },
                ]
            },
        },
    },
    hall: {
        extra: {
            special: {
                rooms: [
                    { tag: "hoody", name: "sleepyHoodyRoom" },
                    { tag: "black_rabbit", name: "BlackRabbit" },
                    { tag: "shop", name: "Shop" },
                    { tag: "boss", name: "ShadowRoom" },
                ]
            },
        },
        small: {
            begin: {
                default: roomOptions.begin,
                rooms: [
                    { weight: 1, name: "Holes" },
                    { weight: 1, name: "Statues" },
                    { weight: 1, name: "Water" },
                    { weight: 1, name: "Pillars" },
                    { weight: 1, name: "Pillars02" },
                    { weight: 1, name: "Torches" },
                ]
            },
            normal: {
                default: { difficulty: [1, -4] },
                rooms: [
                    { weight: 5, name: "OddWalls" },
                    { weight: 5, name: "Torches" },
                    { weight: 5, name: "CornerRocks" },
                    { weight: 5, name: "TallRocks" },
                    { weight: 5, name: "Empty" },
                    { weight: 5, name: "Corners" },
                    { weight: 5, name: "CenterHole" },
                    { weight: 4, name: "DiagonalRocks" },
                    { weight: 3, name: "BridgeEW", noExit: 5, difficulty: [0, 0] },
                    { weight: 3, name: "BridgeNS", noExit: 10, difficulty: [0, 0] },
                    { weight: 3, name: "TallRocksEast", noExit: 2 },
                    { weight: 3, name: "TallRocksWest", noExit: 8 },
                    { weight: 4, name: "BowTie", noExit: 5 },
                    { weight: 4, name: "HourGlass", noExit: 10 },
                    { weight: 5, name: "CornerBlocks" },
                    { weight: 2, name: "Track" },
                    { weight: 4, name: "CornerHoles" },
                    { weight: 3, name: "Crypt", difficulty: [1, 8] },
                ]
            },
            special: {
                rooms: [
                    { tag: "hall_entrance", name: "Entrance", ...roomOptions.nextAreaEntrance },
                    { tag: "hall_hidden_three_chests", name: "ThreeChests", ...roomOptions.threeChests },
                    { tag: "hoodie_entrance", name: "Hoodie_Locked", ...roomOptions.hoodieHallLocked },
                    { tag: "hoodie_entrance", name: "Hoodie_Unlocked", ...roomOptions.hoodieHallUnlocked },
                    { tag: "hall_library", name: "Library", ...roomOptions.hallLibrary },
                    { tag: "tribute_fountain", name: "TributeFountain", ...roomOptions.fountain },
                    { tag: "next_entrance", name: "CavernEntrance", ...roomOptions.dungeonEntrance },
                    { tag: "end", name: "Normal", icon: 3 },
                    { tag: "endBoss", name: "Boss", icon: 2 },
                ]
            },
            relic: {
                default: roomOptions.relic,
                rooms: [
                    { weight: 1, name: "Lanterns" },
                    { weight: 1, name: "Statues" },
                    { weight: 1, name: "Pillars" },
                ]
            },
            relic_unlocked: {
                default: roomOptions.relicUnlocked,
                rooms: [
                    { weight: 1, name: "Lanterns" },
                    { weight: 1, name: "Statues" },
                    { weight: 1, name: "Pillars" },
                ]
            },
            treasure: {
                rooms: [
                    { weight: 2, name: "SetPiece", difficulty: [0.4, -6] },
                    { weight: 3, name: "ChestSpawner" },
                    { weight: 3, name: "HalfEast", noExit: 8 },
                    { weight: 3, name: "HalfWest", noExit: 2 },
                    { weight: 3, name: "SpikeChest" },
                    { weight: 3, name: "SpinnerChest" },
                    { weight: 2, name: "HealthLever" },
                    { weight: 1, name: "CursedRelics" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 2, name: "OilSpikes" },
                    { weight: 3, name: "JustAChest" },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 5, name: "Chest02" },
                    { weight: 5, name: "Skeletons" },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 1, name: "Altar" },
                    { weight: 5, name: "Items" },
                    { weight: 3, name: "Keys" },
                    { weight: 5, name: "Chest" },
                    { weight: 3, name: "Bombs" },
                    { weight: 1, name: "Blessing" },
                    { weight: 5, name: "HealthLever" },
                    { weight: 3, name: "Gold" },
                    { weight: 2, name: "DualLevers" },
                    { weight: 3, name: "TorchLighting" },
                    { weight: 10, name: "DogDillon", ...roomOptions.dogDillion },
                    { weight: 3, name: "Talisman", ...roomOptions.talismanSpawn },
                    { weight: 2, name: "KeyPillar" },
                    { weight: 1, name: "BigChest" },
                    { weight: 2, name: "Nugg" },
                    { weight: 3, name: "Bard", ...roomOptions.bard },
                    { weight: 3, name: "TributeFountain", ...roomOptions.secretFountain },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 3, name: "Chest" },
                    { weight: 3, name: "Skeletons", noExit: 1 },
                    { weight: 2, name: "CursedTorch", noExit: 1 },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 1, name: "Altar" },
                    { weight: 1, name: "CursedPedestal", noExit: 5 },
                    { weight: 3, name: "ChestBridge", noExit: 6 },
                    { weight: 1, name: "Lab" },
                    { weight: 2, name: "PitSpikes", ...roomOptions.pitSpikes },
                    { weight: 2, name: "Bard", ...roomOptions.bard },
                    { weight: 1, name: "OilBarricade" },
                ]
            },
            altar: {
                default: roomOptions.altar,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Holes" },
                    { weight: 1, name: "Gargoyles" },
                ]
            },
            altar_locked: {
                default: roomOptions.altarLocked,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Holes" },
                    { weight: 1, name: "Gargoyles" },
                ]
            },
            altar_guacamole: {
                default: roomOptions.altarGuacamole,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Holes" },
                    { weight: 1, name: "Gargoyles" },
                ]
            },
        },
        large: {
            normal: {
                default: { difficulty: [1, 2] },
                rooms: [
                    { weight: 5, name: "RandomPillars" },
                    { weight: 5, name: "DonutHoles", difficulty: [1, -4] },
                    { weight: 4, name: "Crossroads", difficulty: [1, 0] },
                    { weight: 5, name: "TallRocks" },
                    { weight: 5, name: "ZigZag" },
                    { weight: 3, name: "SetPieces", difficulty: [1, 0] },
                    { weight: 3, name: "ArrowTraps", difficulty: [1, 0] },
                    { weight: 5, name: "Empty" },
                    { weight: 3, name: "ArrowMaze", difficulty: [1, 0] },
                    { weight: 2, name: "BendSW", direction: 3, difficulty: [1, -4] },
                    { weight: 1, name: "BendSWPillar", direction: 3, difficulty: [1, -6] },
                    { weight: 2, name: "BendSE", direction: 9, difficulty: [1, -4] },
                    { weight: 1, name: "BendSEPillar", direction: 9, difficulty: [1, -6] },
                    { weight: 2, name: "BendNW", direction: 6, difficulty: [1, -4] },
                    { weight: 1, name: "BendNWPillar", direction: 6, difficulty: [1, -6] },
                    { weight: 1, name: "BendNEPillar", direction: 12, difficulty: [1, -6] },
                    { weight: 2, name: "BendNE", direction: 12, difficulty: [1, -4] },
                    { weight: 5, name: "Arena" },
                    { weight: 3, name: "DeadlyDonut", direction: 10, difficulty: [0, 0] },
                    { weight: 2, name: "Knot", difficulty: [1, -4] },
                    { weight: 2, name: "DonutSpinner", direction: 5, difficulty: [1, -4] },
                    { weight: 3, name: "HazardHeavy" },
                    { weight: 2, name: "GargoyleHall", difficulty: [1, -4] },
                    { weight: 2, name: "EWBridgeThin", direction: 5, difficulty: [0.8, -8] },
                    { weight: 3, name: "EWBridgeThick", direction: 5, difficulty: [0.8, -10] },
                    { weight: 3, name: "SpiderDen" },
                    { weight: 4, name: "ChessBoard" },
                    { weight: 5, name: "CrossHole" },
                    { weight: 2, name: "ArrowFun", direction: 5, difficulty: [1, -6] },
                    { weight: 5, name: "CornerPillars" },
                    { weight: 5, name: "CornerHoles" },
                    { weight: 4, name: "VerticalHoles" },
                    { weight: 3, name: "Pillars" },
                    { weight: 4, name: "SlidingWall", difficulty: [1, 0] },
                    { weight: 5, name: "CornerPillars" },
                    { weight: 1, name: "RandomWall", difficulty: [0, 0] },
                    { weight: 1, name: "Gargoyles", difficulty: [0, 0] },
                ]
            },
            treasure: {
                rooms: [
                    { weight: 3, name: "CursedRelics", door: 5 },
                    { weight: 3, name: "BridgeTorches", ...roomOptions.secretEast },
                    { weight: 4, name: "JustSomeTreasure", noExit: 1 },
                    { weight: 4, name: "GargoylesMaybe" },
                    { weight: 4, name: "HealthLever" },
                    { weight: 3, name: "Eggs", noExit: 1 },
                    { weight: 3, name: "SecretShop", ...roomOptions.secretShop },
                    { weight: 2, name: "SpikedRoom", noExit: 1 },
                    { weight: 3, name: "Cage" },
                    { weight: 2, name: "Levers", noExit: 7 },
                    { weight: 1, name: "Choice" },
                    { weight: 1, name: "Skull" },
                    { weight: 2, name: "CursedJars", noExit: 4 },
                    { weight: 1, name: "PilferLever" },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 6, name: "Chest02" },
                    { weight: 6, name: "Chest" },
                    { weight: 4, name: "KeyBombMaze" },
                    { weight: 5, name: "Items" },
                    { weight: 5, name: "Keys" },
                    { weight: 5, name: "Bombs" },
                    { weight: 5, name: "Food" },
                    { weight: 4, name: "HealthLever" },
                    { weight: 3, name: "PuzzleLock" },
                    { weight: 2, name: "BlessingPool" },
                    { weight: 4, name: "Gold" },
                    { weight: 5, name: "Altar" },
                    { weight: 1, name: "SpaceInvader", ...roomOptions.partyPopcornRoom },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 3, name: "BombPuzzle" },
                    { weight: 4, name: "Talisman", ...roomOptions.talismanSpawn },
                    { weight: 2, name: "KeyPillars" },
                    { weight: 2, name: "BlackRabbitShop" },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 3, name: "SpikeJump" },
                    { weight: 2, name: "TorchPuzzle" },
                    { weight: 2, name: "Keys" },
                    { weight: 3, name: "Potions" },
                    { weight: 2, name: "Crypt" },
                    { weight: 1, name: "CursedRelics" },
                    { weight: 1, name: "BlessingChoice" },
                    { weight: 3, name: "ChessBoard" },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 2, name: "SpiderGuantlet", difficulty: [1, -10] },
                    { weight: 1, name: "GargoyleGuards" },
                    { weight: 1, name: "SmallRoom", difficulty: [1, -10] },
                    { weight: 2, name: "Gap" },
                    { weight: 1, name: "DiagonalSpikes" },
                    { weight: 1, name: "Insulated" },
                ]
            },
            special: {
                rooms: [
                    { tag: "down_boss", name: "ShadowlordEntrance", ...roomOptions.bossRoom },
                    { tag: "hall_library_combat_arena", name: "CombatWave", ...roomOptions.hallLibraryCombat },
                    { tag: "relic_altar", name: "RelicAltar", ...roomOptions.relicAltar },
                    { tag: "pre_room", name: "BeforeCavernEntrance", ...roomOptions.dungeonEntrance },
                ]
            },
            challange: {
                rooms: [
                    { weight: 1, name: "GamblingRoom", icon: 9 },
                    { weight: 1, name: "Combat", icon: 11 },
                ]
            },
        },
    },
    cavern: {
        extra: {
            special: {
                rooms: [
                    { tag: "hoody", name: "sleepyHoodyRoom" },
                    { tag: "black_rabbit", name: "BlackRabbit" },
                    { tag: "shop", name: "Shop" },
                    { tag: "boss", name: "CrystalRoom" },
                ]
            },
        },
        small: {
            begin: {
                default: roomOptions.begin,
                rooms: [
                    { weight: 1, name: "Plain" },
                    { weight: 1, name: "PillarHole" },
                    { weight: 1, name: "Pillars" },
                    { weight: 1, name: "Round" },
                ]
            },
            normal: {
                default: { difficulty: [1, -8] },
                rooms: [
                    { weight: 3, name: "Empty" },
                    { weight: 3, name: "Corners" },
                    { weight: 3, name: "Donut" },
                    { weight: 3, name: "BowTie", noExit: 5 },
                    { weight: 3, name: "Razor", noExit: 10 },
                    { weight: 3, name: "Organic" },
                    { weight: 1, name: "HallwayEW", noExit: 5 },
                    { weight: 1, name: "HallwayNS", noExit: 10 },
                    { weight: 2, name: "NoWest", noExit: 8 },
                    { weight: 2, name: "NoEast", noExit: 2 },
                    { weight: 2, name: "NoNorth", noExit: 1 },
                    { weight: 2, name: "NoSouth", noExit: 4 },
                    { weight: 3, name: "HoleCorners" },
                    { weight: 3, name: "CenterHole" },
                    { weight: 2, name: "VerticalHole" },
                    { weight: 2, name: "ZPillars" },
                    { weight: 2, name: "SPillars" },
                    { weight: 2, name: "UPillar", noExit: 1 },
                    { weight: 2, name: "VPillar", noExit: 1 },
                    { weight: 2, name: "UPillarReverse", noExit: 4 },
                    { weight: 2, name: "VPillarReverse", noExit: 4 },
                    { weight: 2, name: "WestSlant", noExit: 8 },
                    { weight: 2, name: "EastSlant", noExit: 2 },
                    { weight: 3, name: "SpikePatch" },
                ]
            },
            special: {
                rooms: [
                    { tag: "cavern_entrance", name: "Entrance", ...roomOptions.nextAreaEntrance },
                    { tag: "hoodie_entrance", name: "Hoodie_Locked", ...roomOptions.hoodieCavernLocked },
                    { tag: "hoodie_entrance", name: "Hoodie_Unlocked", ...roomOptions.hoodieCavernUnlocked },
                    { tag: "hidden_campsite", name: "HiddenCampsite", door: 3 },
                    { tag: "hidden_hallway", name: "HiddenHallway", noExit: 10 },
                    { tag: "tribute_fountain", name: "TributeFountain", ...roomOptions.fountain },
                    { tag: "end", name: "Normal", icon: 3 },
                    { tag: "endBoss", name: "Boss", icon: 2 },
                    { tag: "next_entrance", name: "CoreEntrance", ...roomOptions.dungeonEntrance },
                    { tag: "next_entrance_bog", name: "BogEntrance", ...roomOptions.dungeonEntrance },
                ]
            },
            relic: {
                default: roomOptions.relic,
                rooms: [
                    { weight: 2, name: "ocked_Blocks" },
                    { weight: 2, name: "ocked_Round" },
                    { weight: 2, name: "Cavern_Small_Relic_Torches" },
                    { weight: 1, name: "Cavern_Small_Relic_Bridge" },
                ]
            },
            relic_unlocked: {
                default: roomOptions.relicUnlocked,
                rooms: [
                    { weight: 2, name: "ocked_Blocks" },
                    { weight: 2, name: "ocked_Round" },
                    { weight: 2, name: "Cavern_Small_Relic_Torches" },
                    { weight: 1, name: "Cavern_Small_Relic_Bridge" },
                ]
            },
            treasure: {
                rooms: [
                    { weight: 2, name: "JustTreasure" },
                    { weight: 2, name: "Spikes" },
                    { weight: 2, name: "DoubleRocked" },
                    { weight: 2, name: "ThiccRocks", noExit: 1 },
                    { weight: 2, name: "JustTreasureHole" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 2, name: "HealthLever" },
                    { weight: 2, name: "Diagonal" },
                    { weight: 2, name: "TorchPuzzle" },
                    { weight: 2, name: "BombPuzzle" },
                    { weight: 2, name: "AChillSkeleton" },
                    { weight: 2, name: "ManySkeletons" },
                    { weight: 1, name: "CursedRelics" },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 3, name: "Chest" },
                    { weight: 3, name: "SmallRoomChest", noExit: 11 },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 1, name: "Altar" },
                    { weight: 2, name: "CursedTorch", noExit: 2 },
                    { weight: 3, name: "SpikedFood" },
                    { weight: 3, name: "PlainChest" },
                    { weight: 3, name: "Bombs" },
                    { weight: 2, name: "KeyPillar" },
                    { weight: 3, name: "Keys" },
                    { weight: 1, name: "SWBlessing", noExit: 3 },
                    { weight: 1, name: "NEBlessing", noExit: 12 },
                    { weight: 1, name: "NothingHole" },
                    { weight: 2, name: "SneakyEast", noExit: 13 },
                    { weight: 2, name: "BlackRabbitShop" },
                    { weight: 3, name: "GoldNug" },
                    { weight: 3, name: "Potion" },
                    { weight: 2, name: "Nugg" },
                    { weight: 3, name: "Bard", ...roomOptions.bard },
                    { weight: 3, name: "TributeFountain", ...roomOptions.secretFountain },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 3, name: "Chest02" },
                    { weight: 3, name: "Chest" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 3, name: "ResourceChests" },
                    { weight: 1, name: "CursedTorches" },
                    { weight: 3, name: "OvergrownChest" },
                    { weight: 1, name: "Altar" },
                    { weight: 1, name: "Lab" },
                    { weight: 1, name: "Blessing" },
                    { weight: 3, name: "Food" },
                    { weight: 2, name: "CostedLever" },
                    { weight: 3, name: "Bombs" },
                    { weight: 3, name: "Keyring" },
                    { weight: 1, name: "CampsiteHall", ...roomOptions.campsite },
                    { weight: 2, name: "Bard", ...roomOptions.bard },
                ]
            },
            altar: {
                default: roomOptions.altar,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Holes" },
                    { weight: 1, name: "Overgrown" },
                ]
            },
            altar_locked: {
                default: roomOptions.altarLocked,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Holes" },
                    { weight: 1, name: "Overgrown" },
                ]
            },
            altar_guacamole: {
                default: roomOptions.altarGuacamole,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Holes" },
                    { weight: 1, name: "Overgrown" },
                ]
            },
        },
        large: {
            normal: {
                default: { difficulty: [1, 2] },
                rooms: [
                    { weight: 4, name: "Empty" },
                    { weight: 4, name: "Corners01" },
                    { weight: 4, name: "Corners02" },
                    { weight: 4, name: "CenterPillar", difficulty: [1, -5] },
                    { weight: 5, name: "Corners03" },
                    { weight: 3, name: "HallwayEW", noExit: 5, difficulty: [1, -5] },
                    { weight: 3, name: "HallwayNS", noExit: 10, difficulty: [1, -5] },
                    { weight: 4, name: "CornerColumns", difficulty: [1, 0] },
                    { weight: 4, name: "UPillar", noExit: 1, difficulty: [1, 0] },
                    { weight: 4, name: "VPillar", noExit: 1, difficulty: [1, 0] },
                    { weight: 4, name: "UPillarReverse", noExit: 4 },
                    { weight: 4, name: "VPillarReverse", noExit: 4, difficulty: [1, 0] },
                    { weight: 3, name: "ZPillar" },
                    { weight: 3, name: "SPillar" },
                    { weight: 3, name: "TriangleZPillar" },
                    { weight: 4, name: "CenterHole" },
                    { weight: 4, name: "VerticalHoles" },
                    { weight: 4, name: "DiamondHole", difficulty: [1, 0] },
                    { weight: 4, name: "FigureEight" },
                    { weight: 3, name: "WestSlant", noExit: 8, difficulty: [1, -2] },
                    { weight: 3, name: "EastSlant", noExit: 2, difficulty: [1, -2] },
                    { weight: 4, name: "Razor", noExit: 10 },
                    { weight: 4, name: "BowTie", noExit: 5, difficulty: [1, -2] },
                    { weight: 2, name: "BendNW", noExit: 6, difficulty: [1, -8] },
                    { weight: 2, name: "BendNE", noExit: 10, difficulty: [1, -8] },
                    { weight: 2, name: "BendSW", noExit: 3, difficulty: [1, -8] },
                    { weight: 2, name: "BendSE", noExit: 9, difficulty: [1, -8] },
                    { weight: 3, name: "Blocks" },
                    { weight: 4, name: "PrismStone" },
                    { weight: 3, name: "RockCage", difficulty: [1, 0] },
                    { weight: 4, name: "TwoLargeSetPiece", difficulty: [1, -2] },
                    { weight: 1, name: "NightmareScenario", difficulty: [1, -6] },
                    { weight: 3, name: "PrismRock", difficulty: [1, -6] },
                    { weight: 4, name: "VRock", difficulty: [1, 0] },
                    { weight: 4, name: "DoubleRock", difficulty: [1, 0] },
                    { weight: 4, name: "Separated" },
                ]
            },
            treasure: {
                rooms: [
                    { weight: 1, name: "CursedRelics", door: 5 },
                    { weight: 3, name: "JustTreasure01" },
                    { weight: 3, name: "BlocksNRocks", noExit: 1 },
                    { weight: 2, name: "HealthLever" },
                    { weight: 3, name: "BlockedBridge", noExit: 1 },
                    { weight: 3, name: "BombPuzzle" },
                    { weight: 2, name: "LeverBombs", noExit: 1 },
                    { weight: 3, name: "JustTreasure02", noExit: 9 },
                    { weight: 3, name: "BurriedStuff" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 2, name: "TorchPuzzle" },
                    { weight: 2, name: "CaveCells", noExit: 10 },
                    { weight: 3, name: "SecretShop", ...roomOptions.secretShop },
                    { weight: 2, name: "SpikePuzzle" },
                    { weight: 3, name: "BulletRocks" },
                    { weight: 3, name: "CoupleItems" },
                    { weight: 2, name: "WalkWithFire", noExit: 8 },
                    { weight: 1, name: "Choice" },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 3, name: "Chest" },
                    { weight: 3, name: "TallRocksChest" },
                    { weight: 3, name: "Chest" },
                    { weight: 2, name: "TorchPuzzle" },
                    { weight: 2, name: "OvergrownStatuePuzzle" },
                    { weight: 2, name: "3Chests", noExit: 1 },
                    { weight: 1, name: "Blessing" },
                    { weight: 3, name: "Keys" },
                    { weight: 2, name: "KeyBlocks" },
                    { weight: 2, name: "TiledRoom" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 3, name: "Bombs" },
                    { weight: 2, name: "BlackRabbitShop" },
                    { weight: 3, name: "Chest" },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 2, name: "SpikeJump" },
                    { weight: 3, name: "BulletChest" },
                    { weight: 2, name: "Potions" },
                    { weight: 3, name: "Chests" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 1, name: "Blessing" },
                    { weight: 1, name: "Altar" },
                    { weight: 1, name: "DoubleBigChest" },
                    { weight: 3, name: "Small" },
                    { weight: 2, name: "Gap" },
                    { weight: 3, name: "TwoItems" },
                ]
            },
            special: {
                rooms: [
                    { tag: "down_boss", name: "PonzuEntrance", ...roomOptions.bossRoom },
                    { tag: "relic_altar", name: "RelicAltar", ...roomOptions.relicAltar },
                    { tag: "pre_room", name: "BeforeCoreEntrance", ...roomOptions.dungeonEntrance },
                    { tag: "pre_bog", name: "BeforeBogEntrance", ...roomOptions.dungeonEntrance },
                ]
            },
            challange: {
                rooms: [
                    { weight: 1, name: "GamblingRoom", icon: 9 },
                    { weight: 1, name: "Combat", icon: 11 },
                ]
            },
        },
    },
    core: {
        extra: {
            special: {
                rooms: [
                    { tag: "boss", name: "FireRoom" },
                    { tag: "black_rabbit", name: "BlackRabbit" },
                    { tag: "shop", name: "Shop" },
                ]
            },
        },
        small: {
            begin: {
                default: roomOptions.begin,
                rooms: [
                    { weight: 1, name: "Plain" },
                    { weight: 1, name: "MoltenGold" },
                    { weight: 1, name: "Pillars" },
                    { weight: 1, name: "Holes" },
                    { weight: 1, name: "Blocks" },
                ]
            },
            normal: {
                default: { difficulty: [1, -15] },
                rooms: [
                    { weight: 5, name: "Empty" },
                    { weight: 4, name: "Corners" },
                    { weight: 4, name: "BowTie", noExit: 5 },
                    { weight: 4, name: "Razor", noExit: 10 },
                    { weight: 4, name: "Hole" },
                    { weight: 4, name: "Pillar" },
                    { weight: 4, name: "HoleNS" },
                    { weight: 4, name: "Spikes" },
                    { weight: 1, name: "VerticalHallHazard", noExit: 10 },
                    { weight: 3, name: "TSectionEast", noExit: 8 },
                    { weight: 3, name: "TSectionWest", noExit: 2 },
                    { weight: 3, name: "TSectionNorth", noExit: 4 },
                    { weight: 3, name: "TSectionSouth", noExit: 1 },
                    { weight: 2, name: "EastWestSpikes", noExit: 5 },
                    { weight: 3, name: "DiagonalHole" },
                    { weight: 2, name: "GoldPuddle" },
                    { weight: 2, name: "GoldPuddleNS" },
                    { weight: 1, name: "GoldPuddleCrossRoads" },
                    { weight: 4, name: "CentralPillar" },
                    { weight: 4, name: "Diamond" },
                    { weight: 3, name: "GoldRiver" },
                    { weight: 4, name: "MovingBlocks" },
                    { weight: 4, name: "MovingTorches" },
                    { weight: 3, name: "MovingVents" },
                    { weight: 3, name: "CornerRocks" },
                    { weight: 5, name: "Slant" },
                    { weight: 2, name: "BridgeEW", noExit: 5 },
                ]
            },
            special: {
                rooms: [
                    { tag: "core_entrance", name: "Entrance", ...roomOptions.nextAreaEntrance },
                    { tag: "end", name: "Normal", icon: 3 },
                    { tag: "endBoss", name: "Boss", icon: 2 },
                ]
            },
            relic: {
                default: roomOptions.relic,
                rooms: [
                    { weight: 1, name: "Blocks" },
                    { weight: 1, name: "GoldPool", noExit: 1 },
                    { weight: 1, name: "Basic" },
                    { weight: 1, name: "TorchPuzzle" },
                ]
            },
            relic_unlocked: {
                default: roomOptions.relicUnlocked,
                rooms: [
                    { weight: 1, name: "Blocks" },
                    { weight: 1, name: "GoldPool", noExit: 1 },
                    { weight: 1, name: "Basic" },
                    { weight: 1, name: "TorchPuzzle" },
                ]
            },
            treasure: {
                rooms: [
                    { weight: 4, name: "JustTreasure" },
                    { weight: 4, name: "GoldFlow", noExit: 1 },
                    { weight: 3, name: "WestWall", noExit: 8 },
                    { weight: 3, name: "EastWall", noExit: 2 },
                    { weight: 3, name: "NorthWall", noExit: 1 },
                    { weight: 3, name: "SouthWall", noExit: 4 },
                    { weight: 3, name: "Spikes" },
                    { weight: 4, name: "Rocked" },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 4, name: "BombPuzzle" },
                    { weight: 1, name: "CursedRelics" },
                    { weight: 3, name: "CrossBlock" },
                    { weight: 4, name: "LongReach", noExit: 8 },
                    { weight: 4, name: "AlternateLongReach", noExit: 2 },
                    { weight: 3, name: "GOLD", noExit: 1 },
                    { weight: 3, name: "TreasureSwitch" },
                    { weight: 3, name: "DoubleChest" },
                    { weight: 3, name: "HealthLever" },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 4, name: "Chest" },
                    { weight: 4, name: "Cookout" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 3, name: "GoldRoom" },
                    { weight: 3, name: "BombPuzzleSmall" },
                    { weight: 1, name: "Nuggs" },
                    { weight: 1, name: "Altar" },
                    { weight: 1, name: "LockedAltar", noExit: 1 },
                    { weight: 3, name: "Insulated" },
                    { weight: 2, name: "Tent" },
                    { weight: 4, name: "Bombs" },
                    { weight: 4, name: "Keys" },
                    { weight: 3, name: "Blessing", noExit: 8 },
                    { weight: 2, name: "PressurePlate", noExit: 3 },
                    { weight: 2, name: "GoldRocks" },
                    { weight: 2, name: "Bard", ...roomOptions.bard },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 4, name: "Chest" },
                    { weight: 3, name: "CursedTorch" },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 4, name: "Chest02" },
                    { weight: 2, name: "SecretRoom", ...roomOptions.secretWest },
                    { weight: 2, name: "KeyPillar" },
                    { weight: 1, name: "Altar" },
                    { weight: 3, name: "Blessing" },
                    { weight: 4, name: "Item" },
                    { weight: 2, name: "Lab" },
                    { weight: 3, name: "SkeletonRocks" },
                    { weight: 4, name: "CookedFood" },
                    { weight: 2, name: "Bard", ...roomOptions.bard },
                ]
            },
            altar: {
                default: roomOptions.altarAlt,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Golden" },
                    { weight: 1, name: "Pillars" },
                    { weight: 1, name: "Flow", noExit: 2 },
                    { weight: 1, name: "LongBridge", noExit: 13 },
                ]
            },
            altar_locked: {
                default: roomOptions.altarLocked,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Golden" },
                    { weight: 1, name: "Pillars" },
                    { weight: 1, name: "Flow", noExit: 2 },
                    { weight: 1, name: "LongBridge", noExit: 13 },
                ]
            },
            altar_guacamole: {
                default: roomOptions.altarGuacamoleAlt,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Golden" },
                    { weight: 1, name: "Pillars" },
                    { weight: 1, name: "Flow", noExit: 2 },
                    { weight: 1, name: "LongBridge", noExit: 13 },
                ]
            },
        },
        large: {
            normal: {
                default: { difficulty: [1, 2] },
                rooms: [
                    { weight: 4, name: "Empty" },
                    { weight: 4, name: "Corners" },
                    { weight: 3, name: "CentralPillar", difficulty: [1, 0] },
                    { weight: 3, name: "CornerHole", difficulty: [1, 0] },
                    { weight: 2, name: "NSBridge", noExit: 10, difficulty: [1, -6] },
                    { weight: 2, name: "EWSBridge", noExit: 1, difficulty: [1, -4] },
                    { weight: 2, name: "EWNBridge", noExit: 4, difficulty: [1, -2] },
                    { weight: 4, name: "VerticalHole", difficulty: [1, 0] },
                    { weight: 3, name: "ZHole" },
                    { weight: 3, name: "GoldCorners" },
                    { weight: 3, name: "NorthLedge", noExit: 1, difficulty: [1, 0] },
                    { weight: 3, name: "SouthLedge", noExit: 4, difficulty: [1, 0] },
                    { weight: 4, name: "Cross" },
                    { weight: 3, name: "NECorner", noExit: 12, difficulty: [1, -10] },
                    { weight: 3, name: "NWCorner", noExit: 6, difficulty: [1, -10] },
                    { weight: 3, name: "SECorner", noExit: 9, difficulty: [1, -10] },
                    { weight: 3, name: "SWCorner", noExit: 3, difficulty: [1, -10] },
                    { weight: 4, name: "CornerHoles" },
                    { weight: 2, name: "NSArena", noExit: 10, difficulty: [1, 0] },
                    { weight: 2, name: "NSDonut", noExit: 10, difficulty: [1, -4] },
                    { weight: 2, name: "SpikeBridgeEW", noExit: 5, difficulty: [0, 0] },
                    { weight: 3, name: "NorthGoldPool", noExit: 1, difficulty: [1, -4] },
                    { weight: 3, name: "AlternateCross", difficulty: [1, 0] },
                    { weight: 3, name: "Quotes" },
                    { weight: 3, name: "GoldHotdogs", difficulty: [1, 0] },
                    { weight: 3, name: "GoldTriangle" },
                    { weight: 3, name: "GoldRiver", difficulty: [1, 0] },
                    { weight: 4, name: "VentSetpeice" },
                    { weight: 3, name: "Checkboard" },
                    { weight: 3, name: "SkeletonRockPuddle" },
                    { weight: 2, name: "RollerBridgeEW", noExit: 5, difficulty: [0, 0] },
                    { weight: 3, name: "Snake", difficulty: [1, -2] },
                    { weight: 4, name: "SingleSetPiece" },
                    { weight: 2, name: "DoubleSetPiece", difficulty: [1, -9] },
                    { weight: 4, name: "RockTriangles", difficulty: [1, 0] },
                    { weight: 4, name: "CentralPillar" },
                    { weight: 4, name: "StubbyPillars" },
                    { weight: 3, name: "Ruins" },
                    { weight: 3, name: "SmallHoles", difficulty: [1, 0] },
                    { weight: 3, name: "ArrowMaze", difficulty: [0.5, -15] },
                    { weight: 3, name: "ThornPillars", difficulty: [1, 0] },
                    { weight: 4, name: "CornerVents" },
                    { weight: 2, name: "RollerHall", difficulty: [0, 0] },
                    { weight: 3, name: "GoldIsland", difficulty: [1, -4] },
                    { weight: 3, name: "CornerHall", difficulty: [1, 0] },
                    { weight: 4, name: "CornerPillar", difficulty: [1, 0] },
                ]
            },
            treasure: {
                rooms: [
                    { weight: 4, name: "JustTreasure01" },
                    { weight: 4, name: "BombPuzzle" },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 3, name: "Gold" },
                    { weight: 4, name: "JustTreasure02" },
                    { weight: 2, name: "River" },
                    { weight: 2, name: "TorchPuzzle" },
                    { weight: 2, name: "SecretShop", ...roomOptions.secretShop },
                    { weight: 3, name: "BridgeChest", noExit: 2 },
                    { weight: 2, name: "HealthLever" },
                    { weight: 3, name: "RollerBridge", noExit: 1 },
                    { weight: 3, name: "Rollers" },
                    { weight: 1, name: "CursedRelics" },
                    { weight: 4, name: "Items" },
                    { weight: 3, name: "Choice" },
                    { weight: 3, name: "DoubleSetPiece" },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 4, name: "Chest" },
                    { weight: 2, name: "BlackRabbitShop" },
                    { weight: 3, name: "BurriedChests", noExit: 1 },
                    { weight: 1, name: "Altar" },
                    { weight: 3, name: "TwoChests" },
                    { weight: 3, name: "SpikeSacrifice" },
                    { weight: 3, name: "Blessing" },
                    { weight: 3, name: "RollerBridge", noExit: 2 },
                    { weight: 4, name: "RollerBidgeNSItems", noExit: 10 },
                    { weight: 3, name: "Gold" },
                    { weight: 2, name: "Talisman" },
                    { weight: 4, name: "Bombs" },
                    { weight: 3, name: "KeyBlocks", noExit: 4 },
                    { weight: 4, name: "Chest02" },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 4, name: "GoldFlowEast" },
                    { weight: 4, name: "GoldFlowWest" },
                    { weight: 4, name: "GoldMoat" },
                    { weight: 3, name: "RockMaze" },
                    { weight: 3, name: "GoldLake" },
                    { weight: 2, name: "TinyRoom" },
                    { weight: 3, name: "RollerJumps" },
                    { weight: 3, name: "SmallSpikeSacrifice" },
                    { weight: 3, name: "RockedIn" },
                    { weight: 2, name: "Blessing" },
                    { weight: 1, name: "Altar" },
                    { weight: 3, name: "TorchPuzzle", ...roomOptions.maybeSecretEast },
                    { weight: 4, name: "Food" },
                    { weight: 4, name: "DoubleChestItem" },
                ]
            },
            special: {
                rooms: [
                    { tag: "down_boss", name: "SeerEntrance", ...roomOptions.bossRoom },
                    { tag: "relic_altar", name: "RelicAltar", ...roomOptions.relicAltar },
                    { tag: "end", name: "Normal", icon: 3 },
                    { tag: "endBoss", name: "Boss", icon: 2 },
                ]
            },
            challange: {
                rooms: [
                    { weight: 1, name: "GamblingRoom", icon: 9 },
                    { weight: 1, name: "Combat", icon: 11 },
                ]
            },
        },
    },
    bog: {
        extra: {
            special: {
                rooms: [
                    { tag: "boss", name: "PlunderRoom" },
                    { tag: "black_rabbit", name: "BlackRabbit" },
                    { tag: "shop", name: "Shop" },
                ]
            },
        },
        small: {
            begin: {
                default: roomOptions.begin,
                rooms: [
                    { weight: 2, name: "Plain" },
                    { weight: 2, name: "Tiled" },
                    { weight: 2, name: "Statues" },
                    { weight: 2, name: "Holes" },
                    { weight: 2, name: "Ruins" },
                    { weight: 1, name: "PressurePlate" },
                ]
            },
            normal: {
                default: { difficulty: [1, -22] },
                rooms: [
                    { weight: 3, name: "Empty" },
                    { weight: 3, name: "Wet" },
                    { weight: 3, name: "CornerHoles" },
                    { weight: 3, name: "VariableCorners" },
                    { weight: 3, name: "StatuePond" },
                    { weight: 3, name: "DoubleStatue" },
                    { weight: 3, name: "Creek" },
                    { weight: 3, name: "RockCorners" },
                    { weight: 3, name: "LongHole" },
                    { weight: 3, name: "OffsetHoles" },
                    { weight: 3, name: "OffsetTallRocks", difficulty: [1, -30] },
                    { weight: 3, name: "Checkered" },
                    { weight: 3, name: "PileOfRocks" },
                    { weight: 3, name: "EastHole", difficulty: [1, -26] },
                    { weight: 3, name: "WestHole", difficulty: [1, -26] },
                    { weight: 3, name: "Creek" },
                    { weight: 3, name: "CrossHole", difficulty: [1, -26] },
                    { weight: 3, name: "Torches", difficulty: [1, -26] },
                ]
            },
            special: {
                rooms: [
                    { tag: "bog_entrance", name: "Entrance", ...roomOptions.nextAreaEntrance },
                    { tag: "end", name: "Normal", icon: 3 },
                    { tag: "endBoss", name: "Boss", icon: 2 },
                ]
            },
            relic: {
                default: roomOptions.relic,
                rooms: [
                    { weight: 1, name: "Trees" },
                    { weight: 1, name: "Ruins" },
                    { weight: 1, name: "Water" },
                    { weight: 1, name: "Pillars", noExit: 4 },
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Bridge", noExit: 13 },
                ]
            },
            relic_unlocked: {
                default: roomOptions.relicUnlocked,
                rooms: [
                    { weight: 1, name: "Trees" },
                    { weight: 1, name: "Ruins" },
                    { weight: 1, name: "Water" },
                    { weight: 1, name: "Bridge", noExit: 13 },
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Pillars", noExit: 4 },
                ]
            },
            treasure: {
                rooms: [
                    { weight: 3, name: "JustTreasure" },
                    { weight: 3, name: "JustTreasure02" },
                    { weight: 2, name: "BlocksRocks", noExit: 1 },
                    { weight: 2, name: "HoleWest", noExit: 7 },
                    { weight: 2, name: "HoleEast", noExit: 13 },
                    { weight: 1, name: "HealthLever" },
                    { weight: 2, name: "BombPuzzle" },
                    { weight: 3, name: "PlantPath", noExit: 1 },
                    { weight: 3, name: "PressurePlate" },
                    { weight: 2, name: "RockedIn" },
                    { weight: 3, name: "Cross" },
                    { weight: 2, name: "SwitchCost", noExit: 5 },
                    { weight: 2, name: "SwitchCost02" },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 4, name: "Chest" },
                    { weight: 3, name: "BridgeEast", noExit: 7 },
                    { weight: 3, name: "BridgeWest", noExit: 13 },
                    { weight: 3, name: "BombPuzzle", noExit: 1 },
                    { weight: 3, name: "TorchPuzzle" },
                    { weight: 3, name: "CornerItem", noExit: 12 },
                    { weight: 3, name: "Nugg" },
                    { weight: 2, name: "Bard", ...roomOptions.bard, noExit: 8 },
                    { weight: 1, name: "HealthLever" },
                    { weight: 2, name: "RuinsChest" },
                    { weight: 1, name: "Altar" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 3, name: "RockedPotions" },
                    { weight: 2, name: "Tent" },
                    { weight: 3, name: "FlowerItem" },
                    { weight: 2, name: "CursedTorch" },
                    { weight: 4, name: "BridgeItem" },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 3, name: "Chest" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 1, name: "DoubleChestLockedIn" },
                    { weight: 2, name: "Potion" },
                    { weight: 2, name: "TinyIsland" },
                    { weight: 3, name: "PondItem" },
                    { weight: 2, name: "CursedTorch" },
                    { weight: 1, name: "Altar" },
                    { weight: 3, name: "BombStash" },
                    { weight: 3, name: "KeyStash" },
                    { weight: 2, name: "CursedRelic" },
                    { weight: 2, name: "LootTree" },
                    { weight: 1, name: "Blessing" },
                    { weight: 2, name: "RockedInChests" },
                    { weight: 2, name: "Bard", ...roomOptions.bard },
                ]
            },
            altar: {
                default: roomOptions.altarAlt,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Woods" },
                    { weight: 1, name: "Hole" },
                    { weight: 1, name: "Tiny", noExit: 11 },
                    { weight: 1, name: "TiledRoom" },
                    { weight: 1, name: "Water" },
                ]
            },
            altar_locked: {
                default: roomOptions.altarLockedAlt,
                rooms: [
                    { weight: 1, name: "Woods" },
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Hole" },
                    { weight: 1, name: "Tiny", noExit: 11 },
                    { weight: 1, name: "TiledRoom" },
                    { weight: 1, name: "Water" },
                ]
            },
            altar_guackamole: {
                default: roomOptions.altarGuacamoleAlt,
                rooms: [
                    { weight: 1, name: "Torches" },
                    { weight: 1, name: "Hole" },
                    { weight: 1, name: "Woods" },
                    { weight: 1, name: "Tiny", noExit: 11 },
                    { weight: 1, name: "TiledRoom" },
                    { weight: 1, name: "Water" },
                ]
            },
        },
        large: {
            normal: {
                default: { difficulty: [1, 10] },
                rooms: [
                    { weight: 3, name: "Empty" },
                    { weight: 3, name: "HoleStatue" },
                    { weight: 3, name: "RockTorch" },
                    { weight: 3, name: "Tiles" },
                    { weight: 3, name: "Water" },
                    { weight: 3, name: "BowTie", noExit: 5 },
                    { weight: 3, name: "Razor", noExit: 10 },
                    { weight: 3, name: "VerticalHoles" },
                    { weight: 3, name: "WindingHole" },
                    { weight: 3, name: "RockPile" },
                    { weight: 3, name: "CenterPillars", difficulty: [1, 8] },
                    { weight: 3, name: "GrassyPond" },
                    { weight: 3, name: "CornerNW", noExit: 6, difficulty: [1, 0] },
                    { weight: 3, name: "CornerNE", noExit: 12, difficulty: [1, 0] },
                    { weight: 3, name: "CornerSW", noExit: 3, difficulty: [1, 0] },
                    { weight: 3, name: "CornerSE", noExit: 9, difficulty: [1, 0] },
                    { weight: 3, name: "CheckeredHoles" },
                    { weight: 3, name: "BlocksTiles" },
                    { weight: 3, name: "Forest" },
                    { weight: 3, name: "CurveNE", noExit: 8 },
                    { weight: 3, name: "CurveNW", noExit: 2 },
                    { weight: 3, name: "CurveSE", noExit: 8 },
                    { weight: 3, name: "CurveSW", noExit: 2 },
                    { weight: 3, name: "DeadlyBridge", noExit: 10, difficulty: [1, -35] },
                    { weight: 3, name: "WiderBridge", noExit: 10, difficulty: [1, -25] },
                    { weight: 3, name: "NorthHole", difficulty: [1, -10] },
                    { weight: 3, name: "Bridges", difficulty: [1, -20] },
                    { weight: 3, name: "ArrowTrap" },
                    { weight: 3, name: "ElaborateTrap", difficulty: [1, 0] },
                    { weight: 3, name: "NorthEastHole", noExit: 2, difficulty: [1, 0] },
                    { weight: 3, name: "SouthWestHole", noExit: 8, difficulty: [1, 0] },
                    { weight: 3, name: "Rainbow", noExit: 4, difficulty: [1, 0] },
                    { weight: 3, name: "Ruins01" },
                    { weight: 3, name: "Ruins02" },
                    { weight: 3, name: "Ruins03" },
                    { weight: 3, name: "Ruins04" },
                    { weight: 3, name: "Ruins05a", difficulty: [1, 0] },
                    { weight: 3, name: "Ruins05b", difficulty: [1, 0] },
                    { weight: 3, name: "Ruins06", difficulty: [1, 0] },
                    { weight: 3, name: "HiddenFun" },
                    { weight: 3, name: "Trees" },
                    { weight: 3, name: "Plaza" },
                    { weight: 3, name: "TreeNest" },
                    { weight: 3, name: "TreeGrid", difficulty: [1, 0] },
                    { weight: 3, name: "TreeHole", difficulty: [1, 0] },
                    { weight: 3, name: "RockMaze", difficulty: [1, 0] },
                ]
            },
            treasure: {
                rooms: [
                    { weight: 3, name: "JustTreasure01" },
                    { weight: 3, name: "HoleRocks" },
                    { weight: 2, name: "ArrowTrapHole" },
                    { weight: 3, name: "DenseForest" },
                    { weight: 2, name: "ToughJumps" },
                    { weight: 2, name: "Plates" },
                    { weight: 2, name: "Plaza" },
                    { weight: 2, name: "Pond" },
                    { weight: 3, name: "Spikes" },
                    { weight: 3, name: "Overgrown", noExit: 8 },
                    { weight: 1, name: "DangerHallway" },
                    { weight: 2, name: "FourTorches" },
                    { weight: 2, name: "BombPuzzle" },
                    { weight: 1, name: "LotsOfStuff" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 3, name: "HoleSwitch", noExit: 7 },
                    { weight: 3, name: "JustTreasure02" },
                    { weight: 2, name: "SpikeCourse", noExit: 10 },
                    { weight: 3, name: "Bridge", noExit: 13 },
                    { weight: 1, name: "JustTreasureSmall", noExit: 11 },
                    { weight: 2, name: "AllOrNothing" },
                    { weight: 2, name: "SpookyFourTorches" },
                ]
            },
            secret: {
                default: roomOptions.secret,
                rooms: [
                    { weight: 3, name: "Chest" },
                    { weight: 1, name: "Altar" },
                    { weight: 1, name: "Camp", noExit: 3 },
                    { weight: 2, name: "PressureItems" },
                    { weight: 2, name: "BlackRabbitShop" },
                    { weight: 1, name: "Blessing" },
                    { weight: 2, name: "RockedItems" },
                    { weight: 2, name: "RockedItems02", noExit: 10 },
                    { weight: 3, name: "Bombs" },
                    { weight: 3, name: "Food" },
                    { weight: 3, name: "Keys" },
                    { weight: 2, name: "PoisonedItems" },
                    { weight: 2, name: "BlockLever" },
                    { weight: 3, name: "Diamond" },
                ]
            },
            hidden: {
                default: roomOptions.hidden,
                rooms: [
                    { weight: 1, name: "Altar" },
                    { weight: 1, name: "Blessing" },
                    { weight: 1, name: "DoubleLockedChest" },
                    { weight: 2, name: "CookedFood" },
                    { weight: 2, name: "OopsAllTorches" },
                    { weight: 2, name: "GoldTree" },
                    { weight: 2, name: "Series" },
                    { weight: 2, name: "SpikeSacrifice" },
                    { weight: 3, name: "SpikeIsland" },
                ]
            },
            special: {
                rooms: [
                    { tag: "relic_altar", name: "RelicAltar", ...roomOptions.relicAltar },
                    { tag: "down_boss", name: "PlunderKingEntrance", ...roomOptions.bossRoom },
                    { tag: "queen_room", name: "QueensRoom", ...roomOptions.queensRoom },
                    { tag: "royal_road_4", name: "RoyalRoad_4", ...roomOptions.royalRoad },
                    { tag: "royal_road_2", name: "RoyalRoad_2", ...roomOptions.royalRoad },
                    { tag: "royal_road_3", name: "RoyalRoad_3", ...roomOptions.royalRoad },
                    { tag: "royal_road_0", name: "RoyalRoad_0", ...roomOptions.royalRoadStart },
                    { tag: "royal_road_1", name: "RoyalRoad_1", ...roomOptions.royalRoad },
                ]
            },
            challange: {
                rooms: [
                    { weight: 1, name: "GamblingRoom", icon: 9 },
                    { weight: 1, name: "Combat", icon: 11 },
                ]
            },
        },
    },
    shop: {
        shop: {
            shop: {
                default: { icon: 10 },
                rooms: [
                    { tag: "pilfer_shop", name: "Encounter_Shop" },
                    { tag: "pilfer_shop, shop_om", name: "Encounter_Shop_OM" },
                    { tag: "pilfer_shop, market_baby", name: "Encounter_Market_Baby", subFloor: 1 },
                    { tag: "pilfer_shop, market", name: "Encounter_Market", ...roomOptions.undergroundMarket },
                    { tag: "shop_storeroom", name: "Encounter_StoreRoom", ...roomOptions.storeRoom },
                ]
            },
        },
    },
    blackRabbit: {
        blackRabbit: {
            blackRabbit: {
                default: roomOptions.blackRabbitShop,
                rooms: [
                    { weight: 4, name: "Encounter_BR_Shop" },
                    { weight: 1, name: "Encounter_BR_TreasureGame" },
                    { weight: 1, name: "Encounter_BR_LeverGame", tag: "test" },
                ]
            },
        },
    },
    sleepyHoodyRoom: {
        sleepyHoodyRoom: {
            sleepyHoodyRoom: {
                rooms: [
                    { weight: 1, name: "Hoody Room" },
                ]
            },
        },
    },
};
function getRooms(floor, seed, loops) {
    const flags = settings.flags;
    const floorData = getFloorData(floor, flags);
    seed = (seed ?? parseInt(e$("seed-input").value)) + floor;
    const rand = new Random(seed);
    const enemyCombos = [3, 5, 6];
    const seenRooms = [];
    let allRooms = [];
    const map = maps[floorData.map];
    const level = map.levels[floorData.index];
    flags.floor_number = floorData.floor;
    let previousRoom = null;
    for (const roomGroup of level) {
        allRooms = allRooms.concat(getAllFromGroup(roomGroup, 1));
    }
    allRooms = determineCoordinates(allRooms);
    let roomShuggle = [...allRooms];
    roomShuggle = addSetPieces(roomShuggle);
    roomShuggle = addExtras(roomShuggle);
    roomShuggle = addResource(roomShuggle);
    roomShuggle = crawlspace(roomShuggle);
    return allRooms;
    function getAllFromGroup(roomGroup, recursionCount = 1) {
        if (recursionCount > 0) {
            previousRoom = null;
        }
        let output = [];
        for (const room of roomGroup) {
            const currentRoom = getRoom(room, recursionCount);
            if (currentRoom) {
                output.push(currentRoom);
                if (currentRoom.sequence && recursionCount) {
                    output = output.concat(getAllFromGroup(currentRoom.sequence, --recursionCount));
                }
            }
            else {
                recursionCount = 1;
                break;
            }
            recursionCount = 1;
        }
        return output;
    }
    function getRoom(room, recursionCount) {
        if (room.chance && !rand.chance(room.chance)) {
            return null;
        }
        else if (room.floorChance && !rand.chance(room.floorChance(floorData.floor))) {
            return null;
        }
        if (room.requirements && !room.requirements(flags)) {
            return null;
        }
        const usedZone = encounters[room.zone] ?? encounters[map.default.zone];
        const stage = room.stage[rand.range(0, room.stage.length)];
        const tags = room.tags.split(",");
        let roomOut = null;
        let foundRoom = null;
        for (const tag of tags) {
            const encounterGroup = usedZone[stage][tag]?.rooms;
            if (encounterGroup) {
                const filteredRooms = encounterGroup.filter(current => {
                    const withDefault = { ...usedZone[stage][tag]?.default, ...current };
                    return (!seenRooms.includes(current) &&
                        (!withDefault.requirements || withDefault.requirements(flags)) &&
                        (!previousRoom || allowNeighbor(withDefault, previousRoom)));
                });
                if (filteredRooms.length) {
                    foundRoom = rand.getWeightedTable(filteredRooms);
                    if (foundRoom) {
                        seenRooms.push(foundRoom);
                        const withDefault = { ...usedZone[stage][tag]?.default, ...foundRoom };
                        roomOut = {
                            name: `${floorData.map}_${stage}_${tag}_${withDefault.name}`,
                            sequence: withDefault.sequence,
                            door: withDefault.door ?? 1,
                            noExit: withDefault.noExit ?? 0,
                            direction: withDefault.direction ?? 0,
                            weight: room.branchWeight ?? 0,
                            autoSpawn: withDefault.autoSpawn ?? 0,
                            neighbors: [],
                        };
                        if (withDefault.weightedDoor) {
                            roomOut.door = rand.getWeightedTable(withDefault.weightedDoor).door;
                        }
                        else {
                        }
                        if (recursionCount < 1) {
                            roomOut.secluded = true;
                        }
                        ;
                        roomOut.direction = room.direction;
                        roomOut.enemies = determineEnemies(withDefault);
                        if (roomOut.enemies) {
                            console.log(`${roomOut.name} with enemies: ${roomOut.enemies}`);
                        }
                        else {
                            console.log(`${roomOut.name}`);
                        }
                        break;
                    }
                }
                else {
                    roomOut = null;
                }
            }
            else {
                const foundRooms = usedZone[stage].special.rooms.filter(encounter => (encounter.tag === tag));
                for (const room of foundRooms) {
                    if (!room.requirements || room.requirements(flags)) {
                        foundRoom = room;
                        break;
                    }
                }
                if (foundRoom) {
                    seenRooms.push(foundRoom);
                    roomOut = {
                        name: `${floorData.map}_${stage}_${tag}_${foundRoom.name}`,
                        sequence: foundRoom.sequence,
                        door: foundRoom.door ?? 1,
                        noExit: foundRoom.noExit ?? 0,
                        direction: foundRoom.direction ?? 0,
                        weight: room.branchWeight ?? 0,
                        autoSpawn: foundRoom.autoSpawn ?? 0,
                        neighbors: [],
                    };
                    if (foundRoom.weightedDoor) {
                        roomOut.door = rand.getWeightedTable(foundRoom.weightedDoor).door;
                    }
                    else {
                    }
                    if (recursionCount < 1) {
                        roomOut.secluded = true;
                    }
                    ;
                    roomOut.direction = room.direction;
                    roomOut.enemies = determineEnemies(foundRoom);
                    console.log(roomOut.name);
                    break;
                }
                else {
                    roomOut = null;
                }
            }
        }
        if (previousRoom && roomOut) {
            roomOut.previousRoom = previousRoom;
        }
        if (recursionCount > 0) {
            previousRoom = roomOut;
        }
        ;
        return roomOut;
    }
    function determineEnemies(encounter) {
        let encounterChance = 0;
        if (encounter.difficulty) {
            encounterChance = encounter.difficulty[0];
        }
        ;
        if (rand.chance(encounterChance)) {
            const enemies = [];
            const difficultyAdjustemnt = encounter.difficulty[1] ?? 0;
            let enemyList = encounter.enemies ?? floorData.enemies;
            if (encounter.prohibitedEnemies) {
                enemyList = enemyList.filter(enemy => !encounter.prohibitedEnemies.includes(enemy.name));
            }
            if (enemyList.length) {
                enemyList = rand.shuffle(enemyList);
                let enemyCombo = 0;
                for (const enemyType of enemyCombos) {
                    if (enemyList[0].type & enemyType) {
                        enemyCombo = enemyType;
                        break;
                    }
                }
                enemyList = enemyList.filter(enemy => enemy.type & enemyCombo);
                let num = Math.min(enemyList.length, rand.arrayPick(floorData.enemyTypeWeight));
                if (num) {
                    enemyList = rand.shuffle(enemyList);
                    const pickedEnemies = [];
                    for (const enemy of enemyList) {
                        if (num != 1 || enemy.canBeSolo) {
                            if ((enemy.type & enemyCombo || !enemyCombo)) {
                                pickedEnemies.push(enemy);
                                enemyCombo ^= enemy.type;
                            }
                            if (pickedEnemies.length === num) {
                                break;
                            }
                        }
                    }
                    if (pickedEnemies.length) {
                        let remainingDifficulty = floorData.difficulty + difficultyAdjustemnt;
                        const totalOfType = [];
                        for (const enemy of pickedEnemies) {
                            enemies.push(enemy.name);
                            remainingDifficulty -= enemy.difficulty;
                            totalOfType.push(1);
                        }
                        while (remainingDifficulty > 0 && pickedEnemies.length > 0) {
                            const i = rand.range(0, pickedEnemies.length);
                            const enemy = pickedEnemies[i];
                            if (enemy.difficulty > remainingDifficulty || (enemy.max > 0 && enemy.max == totalOfType[i])) {
                                pickedEnemies.splice(i, 1);
                            }
                            else {
                                enemies.push(enemy.name);
                                remainingDifficulty -= enemy.difficulty;
                                ++totalOfType[i];
                            }
                        }
                    }
                    return enemies;
                }
            }
        }
        return null;
    }
    function determineCoordinates(roomList) {
        const startingRoom = roomList[0];
        let positionedRooms = [];
        for (let trial = 0; trial < 10; ++trial) {
            roomList.forEach(room => {
                room.neighbours = [];
                room.position = { x: 0, y: 0 };
            });
            positionedRooms = [startingRoom];
            roomList.slice(1).forEach(room => {
                if (setRoomPosition(room)) {
                    positionedRooms.push(room);
                }
            });
            if (positionedRooms.length === roomList.length) {
                const outArray = positionedRooms.map(room => [room.roomName, room.position.x, room.position.y]);
                positionedRooms = determineNeighbors(positionedRooms);
                return positionedRooms;
            }
            else {
                console.log(`%cLayout failed, trying again!`, "color:#a00;");
            }
        }
        return [false];
        function setRoomPosition(room) {
            let directions = [...cardinalDirections];
            let dir = room.direction ?? 0;
            if (room.previousRoom) {
                room.position = room.previousRoom.position;
                if (dir) {
                    if (canMove(room, dir)) {
                        room.position = newPosition(room.position, dir);
                    }
                    else {
                        dir = 0;
                    }
                }
                else {
                    directions = rand.shuffle(directions);
                    for (const newDirection of directions) {
                        if (canMove(room, newDirection)) {
                            dir = newDirection;
                            room.position = newPosition(room.position, dir);
                            break;
                        }
                    }
                }
            }
            else if (room.door === 0 || room.door === 7) {
                for (const startRoom of rand.shuffle(positionedRooms)) {
                    room.position = startRoom.position;
                    directions = rand.shuffle(directions);
                    for (const newDirection of directions) {
                        if (canMove(room, newDirection)) {
                            dir = newDirection;
                            room.position = newPosition(room.position, dir);
                            break;
                        }
                    }
                    if (dir) {
                        break;
                    }
                }
            }
            else {
                let weightedRooms = positionedRooms.filter(room => room.weight);
                while (!dir && weightedRooms.length > 0) {
                    let startRoom = null;
                    if (startRoom = rand.getWeightedTable(weightedRooms)) {
                        room.position = startRoom.position;
                        directions = rand.shuffle(directions);
                        for (const newDirection of directions) {
                            if (isValidNeighbor(room, startRoom, newDirection) && canMove(room, newDirection)) {
                                dir = newDirection;
                                room.position = newPosition(room.position, dir);
                                break;
                            }
                        }
                        weightedRooms = weightedRooms.filter(current => current != startRoom);
                    }
                }
            }
            if (dir) {
                if (room.door != 0 && room.door != 7) {
                    let opposingDirection = opposite(dir);
                    let opposingRoom = getRoomInPos(newPosition(room.position, opposingDirection));
                    if (opposingRoom != null) {
                        opposingRoom.neighbors[dir] = room;
                        room.neighbors[opposingDirection] = opposingRoom;
                    }
                }
                return true;
            }
            return false;
        }
        function canMove(room, dir) {
            if (room.previousRoom && !isValidNeighbor(room, room.previousRoom, dir))
                return false;
            else if (!getRoomInPos(newPosition(room.position, dir)))
                return true;
            else
                return false;
        }
        function getRoomInPos(pos) {
            return roomList.find(room => (room.position.x === pos.x && room.position.y === pos.y));
        }
        function isValidNeighbor(room1, room2, dir = 0) {
            return dir ? validDirection(room1, room2, dir) : allowNeighbor(room1, room2);
            function validDirection(room1, room2, dir) {
                return !(room2.noExit & dir) && !(room1.noExit & opposite(dir));
            }
        }
        function newPosition(position, dir) {
            const [xPos, yPos] = [position.x, position.y];
            switch (dir) {
                case 1: return { x: xPos, y: yPos - 1 };
                case 4: return { x: xPos, y: yPos + 1 };
                case 2: return { x: xPos + 1, y: yPos };
                case 8: return { x: xPos - 1, y: yPos };
                default: return { x: xPos, y: yPos };
            }
        }
        function determineNeighbors(rooms) {
            let num = 0;
            for (const room of rooms) {
                if (!room.secluded && room.door === 6) {
                    for (const dir of cardinalDirections) {
                        const neighbor = getRoomInPos(newPosition(room.position, dir));
                        if (neighbor &&
                            (isValidNeighbor(neighbor, room, dir)) &&
                            (neighbor.door != 0) &&
                            (neighbor.door != 7) &&
                            (neighbor.door != 4) &&
                            (!neighbor.secluded)) {
                            room.neighbors[dir] = neighbor;
                            neighbor.neighbors[opposite(dir)] = room;
                        }
                    }
                }
                if (room.door === 1) {
                    for (const dir of cardinalDirections) {
                        const neighbor = getRoomInPos(newPosition(room.position, dir));
                        if (neighbor &&
                            neighbor.door === 1 &&
                            (isValidNeighbor(neighbor, room, dir))) {
                            num++;
                            if (rand.chance(floorData.connectivity)) {
                                room.neighbors[dir] = neighbor;
                                neighbor.neighbors[opposite(dir)] = room;
                            }
                        }
                        else {
                        }
                    }
                }
            }
            return rooms;
        }
    }
    function addSetPieces(roomList) {
        if (!floorData.setPieces) {
            return roomList;
        }
        for (const setPiece of floorData.setPieces) {
            const num = rand.range(setPiece.min, setPiece.max + 1);
            for (let i = 0; i < num; ++i) {
                const item = getWeightedItem(setPiece);
                if (item) {
                    roomList = rand.shuffle(roomList);
                    let pickedRoom = null;
                    for (const room of roomList) {
                        if (room.autoSpawn & 32) {
                            pickedRoom = room;
                            break;
                        }
                    }
                    if (pickedRoom) {
                        console.log("SetPiece: " + pickedRoom.name);
                        if (!pickedRoom.setPiece) {
                            pickedRoom.setPiece = [];
                        }
                        ;
                        pickedRoom.setPiece.push(item);
                    }
                }
            }
        }
        return roomList;
    }
    function addExtras(roomList) {
        if (!floorData.extras) {
            return roomList;
        }
        let total = [];
        for (const extra of floorData.extras) {
            const num = rand.range(extra.min, extra.max + 1);
            for (let i = 0; i < num; ++i) {
                const item = getWeightedItem(extra);
                if (item) {
                    roomList = rand.shuffle(roomList);
                    let pickedRoom = null;
                    for (const room of roomList) {
                        if (room.autoSpawn & 16) {
                            pickedRoom = room;
                            break;
                        }
                    }
                    total.push(item);
                    if (pickedRoom) {
                        if (!pickedRoom.extras) {
                            pickedRoom.extras = [];
                        }
                        ;
                        pickedRoom.extras.push(item);
                    }
                }
            }
        }
        return roomList;
    }
    function addResource(roomList) {
        if (!floorData.resources) {
            return roomList;
        }
        let total = [];
        for (const resource of floorData.resources) {
            const num = rand.range(resource.min, resource.max + 1);
            for (let i = 0; i < num; ++i) {
                const item = getWeightedItem(resource);
                if (item) {
                    roomList = rand.shuffle(roomList);
                    let pickedRoom = null;
                    for (const room of roomList) {
                        if (room.autoSpawn & 16) {
                            pickedRoom = room;
                            break;
                        }
                    }
                    total.push(item);
                    if (pickedRoom) {
                        if (!pickedRoom.resources) {
                            pickedRoom.resources = [];
                        }
                        ;
                        pickedRoom.resources.push(item);
                    }
                }
            }
        }
        return roomList;
    }
    function crawlspace(roomList) {
        roomList = rand.shuffle(roomList);
        for (const room of roomList) {
            if (room.autoSpawn & 16) {
                console.log("Crawlspace: " + room.name);
                break;
            }
        }
        return roomShuggle;
    }
    function getWeightedItem(object) {
        for (const item of object.items) {
            item.adjustedWeight = (!item.requirements || item.requirements(flags)) ? item.weight : 0;
        }
        const max = object.percent ? 100 : object.items.reduce((totalWeight, currentItem) => totalWeight + currentItem.adjustedWeight, 0);
        let randomPick = rand.rangeInclusive(1, max);
        for (const item of object.items) {
            randomPick -= item.adjustedWeight;
            if (randomPick <= 0) {
                return item;
            }
        }
        return null;
    }
}
const maps = {
    mineTutorial: {
        default: { zone: "mine" },
        levels: [
            rooms("tuBegin", "tuJump", "tuAttack", "tuBomb", "tuThrow", "tuPilfer", "tuRelic", "tuEnd"),
        ]
    },
    mineEarly: {
        default: { zone: "mine" },
        levels: [
            [
                rooms("begin", "easy", "easy", "easy", "end"),
                ...rooms.multi("relicUnlocked", "treasureBasic", "treasureBasicChance", "secret", "secret", "secretChance", "hoody", "hidden", "relicAltar"),
            ],
            [
                rooms("begin", "normal", "normal", "normal", "end"),
                rooms("waylandHallway", "waylandShop"),
                ...rooms.multi("relic", "altar", "treasureBasic", "treasureBasicChance", "shop", "secret", "secret", "secretChance", "hidden", "relicAltar"),
            ],
            [
                rooms("begin", "normal", "normal"),
                rooms("dodsonCage", "mimicFight", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "hidden", "relicAltar"),
            ],
            [
                rooms("begin"),
                rooms("normal", "normal", "normal", "normal", "endBossSmall"),
                rooms("prechamber", "nextDown"),
                ...rooms.multi("relic", "altar", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "bossRoom", "hidden", "relicAltar"),
            ],
        ],
    },
    mine: {
        default: { zone: "mine" },
        levels: [
            [
                rooms("begin", "normal", "normalLarge", "normal", "normalLarge", "end"),
                ...rooms.multi("relicUnlocked", "blackRabbitFirst", "treasure", "treasureChance", "altar", "secret", "secret", "secretChance", "hoody", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normal", "normalLarge", "normal", "normalLarge", "end"),
                rooms("waylandHallway", "waylandShop"),
                ...rooms.multi("relic", "apprentice", "mushroom", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normal", "normalLarge"),
                rooms("dodsonCage", "mimicFight", "end"),
                ...rooms.multi("relic", "apprentice", "mushroom", "treasure", "treasureChance", "shop", "altar", "blackRabbit", "secret", "secret", "secretChance", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin"),
                rooms("normal", "normalLarge", "normal", "normalLarge", "endBossSmall"),
                rooms("prechamber", "nextDown"),
                ...rooms.multi("relic", "apprentice", "mushroom", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "bossRoom", "hidden", "relicAltar", "fountain"),
            ],
        ],
    },
    dungeon: {
        default: { zone: "dungeon" },
        levels: [
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("dibble", "relic", "treasure", "treasureChance", "shop", "altar", "mastersKey", "secret", "secret", "secretChance", "priestess", "hoody", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "altar", "mastersKey", "secret", "secret", "secretChance", "priestess", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "blackRabbit", "mastersKey", "secret", "secret", "secretChance", "priestess", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "endBoss"),
                rooms("prechamber", "nextDown"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "altar", "mastersKey", "secret", "secret", "secretChance", "bossRoom", "priestess", "hidden", "relicAltar", "fountain"),
            ],
        ]
    },
    dungeonBug: {
        default: { zone: "dungeon" },
        levels: [
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("dibble", "relic", "treasure", "treasureChance", "shop", "altar", "mastersKey", "secret", "secret", "secretChance", "hoody", "priestess", "hidden", "relicAltar", "fountain"),
            ],
        ],
    },
    hall: {
        default: { zone: "hall" },
        levels: [
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("libraryCombat", "relic", "treasure", "treasureChance", "shop", "altar", "secret", "secret", "secretChance", "hoody", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "altar", "blackRabbit", "secret", "secret", "secretChance", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "endBoss"),
                rooms("prechamber", "nextDown"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "bossRoom", "hidden", "relicAltar", "fountain"),
            ],
        ],
    },
    cavern: {
        default: { zone: "cavern" },
        levels: [
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "altar", "secret", "secret", "secretChance", "hoody", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "altar", "blackReddit", "secret", "secret", "secretChance", "hidden", "relicAltar", "fountain"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "endBoss"),
                rooms("prechamber", "nextDown"),
                rooms("bogPrechamber", "bogDown"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "bossRoom", "hidden", "relicAltar", "fountain"),
            ],
        ]
    },
    cavernBug: {
        default: { zone: "cavern" },
        levels: [
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "endBoss"),
                rooms("prechamber", "nextDown"),
                rooms("bogPrechamberBug", "bogDownBug"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "bossRoom", "hidden", "relicAltar", "fountain"),
            ]
        ]
    },
    core: {
        default: { zone: "core" },
        levels: [
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "altar", "secret", "secret", "secretChance", "hidden", "relicAltarLastFloors"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "hidden", "relicAltarLastFloors"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "altar", "blackRabbit", "secret", "secret", "secretChance", "hidden", "relicAltarLastFloors"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "endBoss"),
                ...rooms.multi("relic", "treasure", "treasureChance", "shop", "secret", "secret", "secretChance", "bossRoom", "hidden", "relicAltarLastFloors"),
            ],
        ],
    },
    bog: {
        default: { zone: "bog" },
        levels: [
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relicExtraCost", "treasure", "treasureChance", "shopExtraCost", "altar", "royalRoad0", "secret", "secret", "secretChance", "hidden", "relicAltarLastFloors"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relicExtraCost", "treasure", "treasureChance", "shopExtraCost", "royalRoad0", "secret", "secret", "secretChance", "hidden", "relicAltarLastFloors"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "end"),
                ...rooms.multi("relicExtraCost", "treasure", "treasureChance", "shopExtraCost", "altar", "blackRabbit", "royalRoad0", "secret", "secret", "secretChance", "hidden", "relicAltarLastFloors"),
            ],
            [
                rooms("begin", "normalLarge", "normal", "normalLarge", "normalLarge", "endBoss"),
                ...rooms.multi("relicExtraCost", "treasure", "treasureChance", "shopExtraCost", "royalRoad0", "secret", "secret", "secretChance", "hidden", "bossRoom", "relicAltarLastFloors"),
            ],
        ]
    }
};
function listCraftable(table) {
    const selection = document.getElementById(`${table}-selection`);
    selection.innerHTML = `<legend>Crafted ${table}s</legend>`;
    masterTable[table].forEach((item, index) => {
        if (item.crafting != 0) {
            const itemCheckbox = document.createElement("input");
            itemCheckbox.type = "checkbox";
            itemCheckbox.value = String(index);
            const itemLable = document.createElement("label");
            itemLable.appendChild(itemCheckbox);
            itemLable.innerHTML += item.display;
            selection.appendChild(itemLable);
        }
    });
}
function checkAll(boxCollaction, toggle) {
    for (const box of boxCollaction) {
        box.checked = toggle;
    }
}
function populateAltar() {
    const relics = [];
    const altarSelection = document.getElementById("altar");
    masterTable.relic.forEach((relic, index) => {
        relics.push([relic.display, index]);
    });
    relics.pop();
    relics.sort();
    for (const [display, index] of relics) {
        const selection = document.createElement("option");
        selection.value = index;
        selection.text = display;
        altarSelection.add(selection);
    }
    altarSelection.value = "";
}
function loadLootTables() {
    Object.values(masterTable).forEach((subTable) => {
        subTable.forEach((item, mIndex) => {
            Object.entries(item.tables).forEach(([key, table]) => {
                const tableB = table;
                lootTables[key][tableB.index] = { weight: tableB.weight, masterIndex: mIndex };
            });
        });
    });
}
function nextItem(table = "relic", randState = "relic") {
    table = lootTables[table];
    console.log(table)
    const itemID = rand[randState].getWeightedElement(table);
    return { relic: masterTable.relic[itemID], masterIndex: itemID };
}
function toggleWeight(indices = 167, subTable = "relic", isZero = true) {
    if (!Array.isArray(indices)) {
        indices = [indices];
    }
    indices.forEach((index) => {
        Object.entries(masterTable[subTable][index].tables).forEach(([key, table]) => {
            const tableB = table;
            lootTables[key][tableB.index].index
            lootTables[key][tableB.index].weight = (tableB.weight * !isZero);
        });
    });
}
function toggleOthermine(on = false) {
    toggleWeight([133, 134, 157], "relic", !on);
}
function toggleUncheckedItems() {
    const CBRelic = Object.values(e$("relic-selection").getElementsByTagName("input"));
    const CBPotion = Object.values(e$("potion-selection").getElementsByTagName("input"));
    toggleWeight(CBRelic.flatMap(box => !(box.checked) ? parseInt(box.value) : []), "relic");
    toggleWeight(CBPotion.flatMap(box => !(box.checked) ? parseInt(box.value) : []), "potion");
}
function dibble(seed) {
    const wTables = [], items = [];
    const totalItems = 2 + settings.flags.dibble_extra_item;
    if (settings.flags.dibble_relic) {
        wTables.push(rand.shop.getWeightedTable(lootTables.dibbleRelic));
    }
    for (let i = settings.flags.dibble_relic; i < totalItems; ++i) {
        wTables.push(rand.shop.getWeightedTable(lootTables.dibble));
    }
    wTables.forEach((subtable) => {
        const table = masterTable.weightedTables[subtable.masterIndex];
        const index = rand[table.randState].loot(lootTables[table.key]);
        items.push({ display: masterTable[table.type][index].display, type: table.type });
        if (table.type === "relic") {
            toggleWeight(index, "relic");
        }
    });
    const field = e$c("fieldset", { id: "hub", classList: "zone" });
    const legend = e$c("legend", { textContent: "Hub" });
    field.appendChild(legend);
    e$("levels").appendChild(field);
    const subfield = e$c("fieldset", { id: "dibble", classList: "level" });
    const sublegend = e$c("legend", { textContent: "Dibble" });
    subfield.appendChild(sublegend);
    e$("hub").appendChild(subfield);
    items.forEach((item, i) => {
        e$("dibble").appendChild(e$c("div", { classList: `icon-${item.type}`, innerText: item.display }));
    });
}
function shop(zone, level) {
    const shopName = `shop-${zone}-${level}`;
    const totalPoR = settings.flags.shop_potion_relic;
    const totalFood = settings.flags.shop_food;
    const totalBasic = settings.flags.shop_basic_item;
    const totalItems = totalPoR + totalFood + totalBasic;
    const wTable = [], table = [], index = [], item = [], html = [];
    let shopRoom = document.createElement("div");
    shopRoom.id = shopName;
    shopRoom.classList.add("icon-shop");
    e$(zone + level).appendChild(shopRoom);
    for (let i = 0; i < totalPoR; ++i) {
        wTable.push(rand.shop.getWeightedTable(lootTables.potionOrRelic));
        table.push(masterTable.weightedTables[wTable[i].masterIndex]);
        index.push(rand[table[i].randState].loot(lootTables[table[i].key]));
        if (table[i].type === "relic") {
            toggleWeight(index[i]);
        }
    }
    for (let i = 0; i < totalFood; ++i) {
        table.push({ key: "food", type: "food", randState: "shopHealth" });
    }
    for (let i = 0; i < totalBasic; ++i) {
        table.push({ key: "item", type: "item", randState: "shopBasicItem" });
    }
    for (let i = totalPoR; i < totalItems; ++i) {
        index.push(rand[table[i].randState].loot(lootTables[table[i].key]));
    }
    index.forEach((index, i) => {
        item.push(masterTable[table[i].type][index]);
    });
    table.forEach((table, i) => {
        html.push(document.createElement("div"));
        html[i].classList.add(`icon-${table.type}`);
        html[i].innerHTML = item[i].display;
        e$(shopName).appendChild(html[i]);
    });
}
function shoguul(seed, zone, level) {
    const shoguulName = `shoguul-${zone}-${level}`;
    
    const shoguulRoom = document.createElement("div");
    shoguulRoom.id = shoguulName;
    shoguulRoom.classList.add("icon-shoguul");
    e$(zone + level).appendChild(shoguulRoom);

    nextRand(seed + level);
    const Lrelic = nextItem("relicLegendary");
    toggleWeight(Lrelic.masterIndex);
    const Rrelic = nextItem("relicLegendary");
    toggleWeight(Lrelic.masterIndex, "relic", false);

    const curseRandom = new Random(seed + level);
    var curseResult = curseRandom.rangeInclusive(0, 100);
    const curseRollHtml = document.createElement("div");
    curseRollHtml.classList.add('curse-roll');
    curseRollHtml.innerHTML = Math.floor(curseResult / 5);
    e$(shoguulName).appendChild(curseRollHtml);
    
    const LrelicHtml = document.createElement("div");
    LrelicHtml.classList.add('icon-relic');
    LrelicHtml.innerHTML = Lrelic.relic.display;
    e$(shoguulName).appendChild(LrelicHtml);

    const RrelicHtml = document.createElement("div");
    RrelicHtml.classList.add('icon-relic');
    RrelicHtml.innerHTML = Rrelic.relic.display;
    e$(shoguulName).appendChild(RrelicHtml);
}
function randomSeed() {
    e$("seed-input").value = (seedRand.range()).toString();
    loadSeed();
}
function nextRand(seed) {
    Object.keys(rand).forEach((key) => {
        rand[key] = new Random(seed);
    });
}
let loadSeed = () => {
    start(parseInt((e$("seed-input").value)));
};
function start(seed) {
    e$("levels").innerHTML = "";
    loadLootTables();
    nextRand(seed);
    toggleUncheckedItems();
    let currentRelics;
    let relicText;
    toggleWeight(3, "food");
    toggleWeight(66, "potion");
    toggleWeight(133);
    toggleWeight(134);
    toggleWeight(157);
    if (settings.flags.hasMaxGoldIntegrity) {
        toggleWeight(43);
    }
    if (!settings.flags.unlockedKurtzStache) {
        toggleWeight(146);
    }
    if (e$("altar").value) {
        toggleWeight(e$("altar").value);
    }

    dibble(seed);
    
    nextZone("mine", 0, "The Goldmines", seed);
    nextZone("dungeon", 1, "Delvemore Dungeon", seed);
    nextZone("halls", 2, "Halls of Din", seed);
    nextZone("caverns", 3, "The Shimmering Caverns", seed);
    nextZone("core", 4, "Golden Core", seed);
    function nextZone(zone, zoneID, title, seed) {
        seed += zoneID * 5;
        let field = document.createElement("fieldset");
        field.id = zone;
        field.classList.add("zone");
        let legend = document.createElement("legend");
        legend.textContent = title;
        field.appendChild(legend);
        e$("levels").appendChild(field);
        for (let i = 1; i <= 4; ++i) {
            nextRand(seed + i);
            let subfield = document.createElement("fieldset");
            subfield.id = zone + i;
            subfield.classList.add("level");
            let sublegend = document.createElement("legend");
            sublegend.textContent = "Level " + i;
            subfield.appendChild(sublegend);
            e$(zone).appendChild(subfield);
            let relicRoom = document.createElement("div");
            relicRoom.id = "relic" + zone + i;
            relicRoom.classList.add("icon-relicOn");
            e$(zone + i).appendChild(relicRoom);
            let relic;
            if (((zoneID + i) === 1) && e$("new-save-radio").checked) {
                relic = nextItem("relicStarter");
                console.log(relic.relic.display)
            }
            else {
                relic = nextItem("relic");
            }
            toggleWeight(relic.masterIndex);
            relicText = document.createElement("div");
            relicText.classList.add("icon-relic");
            console.log(relic.relic.tables.relic.index)
            console.log(relic.relic.display)
            relicText.innerHTML = relic.relic.display;
            e$("relic" + zone + i).appendChild(relicText);
            if ((zoneID + i) > 1) {
                shop(zone, i);
            }
            if (((zoneID + i) !== 1) || !e$("new-save-radio").checked) {
                shoguul(seed, zone, i);
            }
        }
    }
}
const saveSelection = e$("save-radio-selecion");
const newRadio = e$("new-save-radio");
const fullRadio = e$("100-save-radio");
const loadRadio = e$("own-save-radio");
const loadInput = e$("save-file-input");
const randomSeedButton = e$("random-seed-button");
const loadSeedButton = e$("load-seed-button");
listCraftable("relic");
listCraftable("potion");
populateAltar();
loadLootTables();
randomSeed();
const radioEvent = new Event("change");
e$("menu-options").addEventListener("change", () => loadSeed());
e$("save-radio-selecion").addEventListener("change", (event) => {
    if (loadRadio.checked) {
        if (loadInput.files[0]) {
            let file = loadInput.files[0];
            const reader = new FileReader();
            reader.onload = (e) => {
                settings = loadSave(loadRadio, e.target.result);
                applySettings(settings);
                loadSeed();
            };
            reader.readAsText(file);
        }
        else {
            loadInput.click();
        }
    }
    else if (fullRadio.checked) {
        settings = loadSave(fullRadio);
        applySettings(settings);
        loadSeed();
    }
    else {
        settings = loadSave(newRadio);
        applySettings(settings);
        loadSeed();
    }
});
loadInput.addEventListener("change", () => {
    loadRadio.checked = true;
    e$("save-radio-selecion").dispatchEvent(radioEvent);
});
randomSeedButton.addEventListener("click", randomSeed);
loadSeedButton.addEventListener("click", loadSeed);
fullRadio.checked = true;
e$("save-radio-selecion").dispatchEvent(radioEvent);
