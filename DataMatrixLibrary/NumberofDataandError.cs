using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixLibrary
{
    public  class NumberofDataandError
    {
        public static int[] SymbolRows = new int[] { 10, 12, 14, 16, 18, 20,  22,  24,  26,
                                                 32, 36, 40,  44,  48,  52,
                                                 64, 72, 80,  88,  96, 104,
                                                 120, 132, 144,
                                                  8,  8, 12,  12,  16,  16 };

        public static int[] SymbolCols = new int[] { 10, 12, 14, 16, 18, 20,  22,  24,  26,
                                                 32, 36, 40,  44,  48,  52,
                                                 64, 72, 80,  88,  96, 104,
                                                 120, 132, 144,
                                                 18, 32, 26,  36,  36,  48 };
        public static int[] DataRegionRows = new int[] { 8, 10, 12, 14, 16, 18, 20, 22, 24,
                                                    28,32,36,40,44,48,56,64,72,80,88,96,108,120,132,6,6,10,10,14,14
                                                     };

        public static int[] DataRegionCols = new int[] { 8, 10, 12, 14, 16, 18, 20, 22, 24,
             28,32,36,40,44,48,56,64,72,80,88,96,108,120,132,16,28,24,32,32,44
                                                    };

        public static int[] HorizDataRegions = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                                    2, 2, 2, 2, 2, 2,
                                                    4, 4, 4, 4, 4, 4,
                                                    6, 6, 6,
                                                    1, 2, 1, 2, 2, 2 };

        public static int[] InterleavedBlocks = new int[] { 1, 1, 1, 1, 1, 1, 1,  1, 1,
                                                     1, 1, 1, 1,  1, 2,
                                                     2, 4, 4, 4,  4, 6,
                                                     6, 8, 10,
                                                     1, 1, 1, 1,  1, 1 };
        public static int[] codeArray = new int[]{ -1, 3, 5, 8, 12, 18, 22, 30, 36,
                                             44, 62, 86, 114, 144, 174, 204, 280,
                                             368, 456, 576, 696, 816, 1050, 1304, 1558,
                                             5, 10, 16, 22, 32, 49};

        public static int[] errorArray = new int[]{ -1, 5, 7, 10, 12, 14, 18 ,20, 24,
                                              28, 36, 42, 48, 56, 68, 84, 112,
                                              144, 192, 224, 272, 336, 408, 496, 620,
                                              7, 11, 14, 18, 24, 28};

        public static int[] BlockMaxCorrectable = new int[] { 2, 3, 5,  6,  7,  9,  10,  12,  14,
                                                       18, 21, 24,  28,  34,  21,
                                                       28, 18, 24,  28,  34,  28,
                                                       34,  31,  31,
                                                       3,  5,  7,   9,  12,  14 };
    }
}
