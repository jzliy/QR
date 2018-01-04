using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixLibrary
{
     public class GetLog
    {
        public int[] log;
        public int[] alog;

        public GetLog()
        {
            log = new int[256];
            alog = new int[256];
            int pp = 301;
            int gf = 256;
            alog[0] = 1;
            alog[255] = 0;
            for (int i = 1; i < gf - 1; i++)
            {
                alog[i] = alog[i - 1] * 2;
                if (alog[i] >= gf)
                    alog[i] ^= pp;
                log[alog[i]] = i;
            }
        }
    }
}
