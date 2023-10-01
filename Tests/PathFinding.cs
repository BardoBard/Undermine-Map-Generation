using System.Collections.Generic;
using Map_Generator;
using Map_Generator.Math;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PathFinding
    {
        [Test]
        public void Search01()
        {
            var begin = new Room()
            {
                Name = "begin",
                RoomType = RoomType.Begin,
                Position = new Vector2Int(0, 0),
            };
            var inbetween01 = new Room()
            {
                Name = "inbetween01",
                Position = new Vector2Int(0, 1),
            };
            var inbetween02 = new Room()
            {
                Encounter = new Map_Generator.Parsing.Json.Classes.Encounter()
                {
                    RoomEnemies = //enemies make this room more difficult
                        new List<Enemy>()
                        {
                            new Enemy()
                            {
                                Name = "spiderbasic",
                                Difficulty = 2
                            },
                            new Enemy()
                            {
                                Name = "jumperwater",
                                Difficulty = 4
                            },
                        }
                },
                Name = "inbetween02",
                Position = new Vector2Int(0, 2),
            };
            var inbetween03 = new Room()
            {
                Name = "inbetween03",
                Position = new Vector2Int(1, 0),
                RoomType = RoomType.Secret //it takes a bomb to get to this room, so not be preferred over a normal room
            };
            var inbetween04 = new Room()
            {
                Name = "inbetween04",
                Position = new Vector2Int(1, 1),
            };
            var inbetween05 = new Room()
            {
                Name = "inbetween05",
                Position = new Vector2Int(2, 2),
            };
            var end = new Room()
            {
                Name = "end",
                RoomType = RoomType.End,
                Position = new Vector2Int(1, 2),
            };

            begin.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.North, inbetween01
                },
                {
                    Direction.East, inbetween03
                }
            };
            inbetween01.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.South, begin
                },
                {
                    Direction.North, inbetween02
                },
                {
                    Direction.West, inbetween04
                }
            };
            inbetween02.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.South, inbetween01
                },
                {
                    Direction.West, end
                }
            };
            inbetween03.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.West, begin
                },
                {
                    Direction.North, inbetween04
                }
            };
            inbetween04.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.South, inbetween03
                },
                {
                    Direction.North, end
                }
            };
            inbetween05.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.West, end
                }
            };
            end.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.South, inbetween04
                },
                {
                    Direction.East, inbetween05
                }
            };
            Map_Generator.Program.PositionedRooms = new List<Room>()
            {
                begin,
                inbetween01,
                inbetween02,
                inbetween03,
                inbetween04,
                inbetween05,
                end
            };
            var breadthFirstSearch = Map_Generator.Program.PositionedRooms.BreadthFirstSearch();
            var aStar = Map_Generator.Program.PositionedRooms.AStarSearch(Map_Generator.PathFinding.AdvancedHeuristics);
            Assert.AreEqual(new List<Room>()
            {
                begin,
                inbetween01,
                inbetween04,
                end
            }, aStar);

            Assert.That(breadthFirstSearch, Is.EqualTo(new List<Room>()
            {
                begin,
                inbetween01,
                inbetween02,
                end
            }).Or.EqualTo(new List<Room>()
            {
                begin,
                inbetween03,
                inbetween04,
                end
            }).Or.EqualTo(new List<Room>()
            {
                begin,
                inbetween01,
                inbetween04,
                end
            }));
        }

        [Test]
        public void Search02()
        {
            var begin = new Room()
            {
                Name = "begin",
                RoomType = RoomType.Begin,
                Position = new Vector2Int(0, 0),
            };
            var inbetween01 = new Room()
            {
                Name = "inbetween01",
                Position = new Vector2Int(-1, -1),
            };
            var inbetween02 = new Room()
            {
                Name = "inbetween02",
                Position = new Vector2Int(0, 1),
            };
            var inbetween03 = new Room()
            {
                Name = "inbetween03",
                Position = new Vector2Int(1, 0),
            };
            var inbetween04 = new Room()
            {
                Name = "inbetween04",
                Position = new Vector2Int(1, 1),
            };
            var inbetween05 = new Room()
            {
                Name = "inbetween05",
                Position = new Vector2Int(2, 2),
            };
            var inbetween06 = new Room()
            {
                Name = "inbetween06",
                Position = new Vector2Int(3, 3),
            };
            var inbetween07 = new Room()
            {
                Name = "inbetween07",
                Position = new Vector2Int(2, 3),
            };
            var inbetween08 = new Room()
            {
                Name = "inbetween08",
                Position = new Vector2Int(1, 3),
            };
            var inbetween09 = new Room()
            {
                Name = "inbetween09",
                Position = new Vector2Int(-2, 2),
            };
            var inbetween10 = new Room()
            {
                Name = "inbetween10",
                Position = new Vector2Int(-3, 3),
            };
            var inbetween11 = new Room()
            {
                Name = "inbetween11",
                Position = new Vector2Int(-1, 1),
            };
            var inbetween12 = new Room()
            {
                Name = "inbetween12",
                Position = new Vector2Int(-2, 1),
            };
            var end = new Room()
            {
                Name = "end",
                RoomType = RoomType.End,
                Position = new Vector2Int(-2, 3),
            };

            begin.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.North, inbetween02
                },
                {
                    Direction.East, inbetween03
                }
            };
            inbetween01.Neighbors = new Dictionary<Direction, Room>()
            {
            };
            inbetween02.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.South, begin
                },
                {
                    Direction.East, inbetween04
                },
                {
                    Direction.West, inbetween11
                }
            };
            inbetween03.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.West, begin
                },
                {
                    Direction.North, inbetween04
                }
            };
            inbetween04.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.South, inbetween03
                },
                {
                    Direction.North, inbetween08
                },
                {
                    Direction.West, inbetween02
                }
            };
            inbetween05.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.West, inbetween08
                },
                {
                    Direction.North, inbetween07
                }
            };
            inbetween06.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.West, inbetween07
                }
            };
            inbetween07.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.South, inbetween05
                },
                {
                    Direction.East, inbetween06
                }
            };
            inbetween08.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.South, inbetween04
                },
                {
                    Direction.East, inbetween05
                }
            };
            inbetween09.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.South, inbetween12
                },
                {
                    Direction.North, end
                }
            };
            inbetween10.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.East, end
                }
            };
            inbetween11.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.East, inbetween02
                },
                {
                    Direction.West, inbetween12
                }
            };
            inbetween12.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.East, inbetween11
                },
                {
                    Direction.North, inbetween09
                }
            };
            end.Neighbors = new Dictionary<Direction, Room>()
            {
                {
                    Direction.South, inbetween09
                },
                {
                    Direction.West, inbetween10
                }
            };
            Map_Generator.Program.PositionedRooms = new List<Room>()
            {
                begin,
                inbetween01,
                inbetween02,
                inbetween03,
                inbetween04,
                inbetween05,
                inbetween06,
                inbetween07,
                inbetween08,
                inbetween09,
                inbetween10,
                inbetween11,
                inbetween12,
                end
            };
            var breadthFirstSearch = Map_Generator.Program.PositionedRooms.BreadthFirstSearch();
            var aStar = Map_Generator.Program.PositionedRooms.AStarSearch(Map_Generator.PathFinding.AdvancedHeuristics);

            Assert.AreEqual(new List<Room>()
            {
                begin,
                inbetween02,
                inbetween11,
                inbetween12,
                inbetween09,
                end
            }, aStar);

            Assert.That(breadthFirstSearch, Is.EqualTo(new List<Room>()
            {
                begin,
                inbetween02,
                inbetween11,
                inbetween12,
                inbetween09,
                end
            }));
            Assert.That(breadthFirstSearch, Is.Not.EqualTo(new List<Room>()
            {
                begin,
                inbetween03,
                inbetween04,
                inbetween02,
                inbetween11,
                inbetween12,
                inbetween09,
                end
            }));
        }
    }
}