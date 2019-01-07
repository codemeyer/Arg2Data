using System.Collections.Generic;
using Arg2Data.Entities;

namespace Arg2Data.Internals
{
    internal static class TrackSectionCommandFactory
    {
        public static TrackSectionCommand Get(byte command, short[] arguments)
        {
            return new TrackSectionCommand(command, arguments);
        }

        internal static int GetArgumentCountForCommand(byte command, TrackSectionCommandOptions options)
        {
            if (command == 0xc5)
            {
                return options.Command0xC5Length;
            }

            return ArgumentCount[command];
        }

        private static readonly Dictionary<byte, int> ArgumentCount = new Dictionary<byte, int>
        {
            // http://www.grandprix2.de/Anleitung/tutus/Command_Lib/command%202.7b/GP2%20Track%20Editing%20-%20Command%20Library%202_7b.htm
            {0x80, 2},  // Object Position
            {0x81, 2},  // View-Distance In Front
            {0x82, 2},  // View-Distance Behind
            {0x83, 1},
            {0x84, 1},
            {0x85, 3},  // Track Width Change
            {0x86, 1},  // Connect Pit Lane Start
            {0x87, 1},  // Connect Pit Lane End
            {0x88, 2},  // Pit Lane Cmd; Left Pits
            {0x89, 2},  // Pit Lane Cmd; Right  Pits
            {0x8a, 6},
            {0x8b, 6},
            {0x8c, 2},
            {0x8d, 2},
            {0x8e, 3},
            {0x8f, 3},

            {0x90, 2},
            {0x91, 2},
            {0x92, 2},
            {0x93, 2},
            {0x94, 2},
            {0x95, 2},
            {0x96, 1},
            {0x97, 1},
            {0x98, 2},
            {0x99, 2},
            {0x9a, 3},
            {0x9b, 1},
            {0x9c, 1},
            {0x9d, 1},
            {0x9e, 1},
            {0x9f, 1},

            {0xa0, 1},
            {0xa1, 1},
            {0xa2, 1},
            {0xa3, 1},
            {0xa4, 1},
            {0xa5, 1},
            {0xa6, 3},
            {0xa7, 3},
            {0xa8, 1},
            {0xa9, 2},
            {0xaa, 4},
            {0xab, 3},
            {0xac, 5},

            // GP2 ONLY
            {0xaf, 3},      // pair of swivel arms
            {0xb0, 2},      // turn ribbons off
            {0xb4, 3},      // track width change left
            {0xb5, 3},      // track width change right
            {0xb8, 14},     // scenery ribbon structure
            {0xb9, 2},      // turn ribbons on
            {0xba, 3},      // bridge scenery
            {0xbb, 5},      // texture mapping light
            {0xbc, 8},      // Loc4 ribbon
            {0xbd, 3},      // light source
            {0xbe, 3},      // view distance to banks in front
            {0xbf, 3},      // view distance to banks behind

            {0xc0, 2},      // single swivel arm left
            {0xc1, 2},      // single swivel arm right
            {0xc2, 3},      // unknown
            {0xc3, 3},      // unknown
            {0xc4, 4},      // unknown
            {0xc5, 7},      // define far sight (F1CT06)        // this is a special one, will be handled by GetArgumentCount code...
            {0xc6, 2},      // far sight area
            {0xc7, 3},      // UNK scenery bridging?
            {0xc8, 8},      // scenery definition
            {0xc9, 9},      // set color GP2
            {0xca, 5},      // kerb A definition
            {0xcb, 5},      // kerb B definition
            {0xcc, 3},      // adjust horizon, not used in original tracks
            {0xcd, 4},      // scenery ribbons darken/lighten
            {0xce, 2},      // unknown, only in Monaco?
            {0xcf, 2},      // show pit objects through pit wall
            {0xd0, 3},      // fix scenery texture
            {0xd1, 1},      // unknown, or fix scenery texture
            {0xd2, 2},      // unknown
            {0xd3, 2},      // view into pitlane entrance
            {0xd4, 1},      // View All Pit Lane From Entry
            {0xd5, 2},      // view into pitlane exit
            {0xd6, 1},      // View All Pit Lane From Exit
            {0xd7, 1},      // unknown
            {0xd8, 1},      // unknown
            {0xd9, 2},      // turn ribbons on
            {0xda, 2},      // silly scenery command
            {0xdb, 1},      // Switch Pits/Track Drawing Order
            {0xdc, 2},      // unknown
            {0xdd, 1},      // weirdo enabler, "gravity constant"
            {0xde, 4},      // black flag area left
            {0xdf, 4},      // black flag area right
            {0xe0, 2},      // kerb adjust left
        };
    }
}
