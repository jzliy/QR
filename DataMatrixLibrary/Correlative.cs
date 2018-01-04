using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixLibrary
{
    class Correlative
    {
        public int dataLength;
        public int errorLength;
        public int nrow;
        public int ncol;
        public int symbolrows;
        public int symbolcols;
        public int KeyArray;

        public Correlative(int codeLength)
        {
            //if (codeLength > NumberofDataandError.codeArray[NumberofDataandError.codeArray.Length - 1])
            //    throw new ArgumentOutOfRangeException("code length is too large.");

            int i = 0;
            while (i < NumberofDataandError.codeArray.Length)
            {
                if (codeLength > NumberofDataandError.codeArray[i])
                    i++;
                else
                    break;
            }

            dataLength = NumberofDataandError.codeArray[i];
            errorLength = NumberofDataandError.errorArray[i];
            nrow = NumberofDataandError.DataRegionRows[i - 1];
            ncol = NumberofDataandError.DataRegionCols[i - 1];
            symbolrows = NumberofDataandError.SymbolRows[i - 1];
            symbolcols = NumberofDataandError.SymbolCols[i - 1];
            //KeyArray = ValueArray = new int[nrow * ncol];
        }
    }
}
