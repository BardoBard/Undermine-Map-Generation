using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Map_Generator.Math;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;
using Map_Generator.Parsing.Json.Interfaces;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PathFinding
    {
        [Test]
        public void BreadthFirstSearch01()
        {
            var begin = new RoomType()
            {
                Name = "begin",
                Position = new Vector2Int(0, 0),
            };
            var inbetween01 = new RoomType()
            {
                Name = "inbetween01",
                Position = new Vector2Int(0, 1),
            };
            var inbetween02 = new RoomType()
            {
                Name = "inbetween02",
                Position = new Vector2Int(0, 2),
            };
            var inbetween03 = new RoomType()
            {
                Name = "inbetween03",
                Position = new Vector2Int(1, 0),
            };
            var inbetween04 = new RoomType()
            {
                Name = "inbetween04",
                Position = new Vector2Int(1, 1),
            };
            var inbetween05 = new RoomType()
            {
                Name = "inbetween05",
                Position = new Vector2Int(2, 2),
            };
            var end = new RoomType()
            {
                Name = "end",
                Position = new Vector2Int(1, 2),
            };

            begin.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.North, inbetween01
                },
                {
                    Direction.East, inbetween03
                }
            };
            inbetween01.Neighbors = new Dictionary<Direction, RoomType>()
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
            inbetween02.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.South, inbetween01
                },
                {
                    Direction.West, end
                }
            };
            inbetween03.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.West, begin
                },
                {
                    Direction.North, inbetween04
                }
            };
            inbetween04.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.South, inbetween03
                },
                {
                    Direction.North, end
                }
            };
            inbetween05.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.West, end
                }
            };
            end.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.South, inbetween04
                },
                {
                    Direction.East, inbetween05
                }
            };
            Map_Generator.Program.PositionedRooms = new List<RoomType>()
            {
                begin,
                inbetween01,
                inbetween02,
                inbetween03,
                inbetween04,
                inbetween05,
                end
            };
            var result = Map_Generator.Program.BreadthFirstSearch();

            Assert.That(result, Is.EqualTo(new List<RoomType>()
            {
                begin,
                inbetween01,
                inbetween02,
                end
            }).Or.EqualTo(new List<RoomType>()
            {
                begin,
                inbetween03,
                inbetween04,
                end
            }).Or.EqualTo(new List<RoomType>()
            {
                begin,
                inbetween01,
                inbetween04,
                end
            }));
        }

        [Test]
        public void BreadthFirstSearch02()
        {
            var begin = new RoomType()
            {
                Name = "begin",
                Position = new Vector2Int(0, 0),
            };
            var inbetween01 = new RoomType()
            {
                Name = "inbetween01",
                Position = new Vector2Int(-1, -1),
            };
            var inbetween02 = new RoomType()
            {
                Name = "inbetween02",
                Position = new Vector2Int(0, 1),
            };
            var inbetween03 = new RoomType()
            {
                Name = "inbetween03",
                Position = new Vector2Int(1, 0),
            };
            var inbetween04 = new RoomType()
            {
                Name = "inbetween04",
                Position = new Vector2Int(1, 1),
            };
            var inbetween05 = new RoomType()
            {
                Name = "inbetween05",
                Position = new Vector2Int(2, 2),
            };
            var inbetween06 = new RoomType()
            {
                Name = "inbetween06",
                Position = new Vector2Int(3, 3),
            };
            var inbetween07 = new RoomType()
            {
                Name = "inbetween07",
                Position = new Vector2Int(2, 3),
            };
            var inbetween08 = new RoomType()
            {
                Name = "inbetween08",
                Position = new Vector2Int(1, 3),
            };
            var inbetween09 = new RoomType()
            {
                Name = "inbetween09",
                Position = new Vector2Int(-2, 2),
            };
            var inbetween10 = new RoomType()
            {
                Name = "inbetween10",
                Position = new Vector2Int(-3, 3),
            };
            var inbetween11 = new RoomType()
            {
                Name = "inbetween11",
                Position = new Vector2Int(-1, 1),
            };
            var inbetween12 = new RoomType()
            {
                Name = "inbetween12",
                Position = new Vector2Int(-2, 1),
            };
            var end = new RoomType()
            {
                Name = "end",
                Position = new Vector2Int(-2, 3),
            };

            begin.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.North, inbetween02
                },
                {
                    Direction.East, inbetween03
                }
            };
            inbetween01.Neighbors = new Dictionary<Direction, RoomType>()
            {
            };
            inbetween02.Neighbors = new Dictionary<Direction, RoomType>()
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
            inbetween03.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.West, begin
                },
                {
                    Direction.North, inbetween04
                }
            };
            inbetween04.Neighbors = new Dictionary<Direction, RoomType>()
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
            inbetween05.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.West, inbetween08
                },
                {
                    Direction.North, inbetween07
                }
            };
            inbetween06.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.West, inbetween07
                }
            };
            inbetween07.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.South, inbetween05
                },
                {
                    Direction.East, inbetween06
                }
            };
            inbetween08.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.South, inbetween04
                },
                {
                    Direction.East, inbetween05
                }
            };
            inbetween09.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.South, inbetween12
                },
                {
                    Direction.North, end
                }
            };
            inbetween10.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.East, end
                }
            };
            inbetween11.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.East, inbetween02
                },
                {
                    Direction.West, inbetween12
                }
            };
            inbetween12.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.East, inbetween11
                },
                {
                    Direction.North, inbetween09
                }
            };
            end.Neighbors = new Dictionary<Direction, RoomType>()
            {
                {
                    Direction.South, inbetween09
                },
                {
                    Direction.West, inbetween10
                }
            };
            Map_Generator.Program.PositionedRooms = new List<RoomType>()
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
            var result = Map_Generator.Program.BreadthFirstSearch();

            Assert.That(result, Is.EqualTo(new List<RoomType>()
            {
                begin,
                inbetween02,
                inbetween11,
                inbetween12,
                inbetween09,
                end
            }));
            Assert.That(result, Is.Not.EqualTo(new List<RoomType>()
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