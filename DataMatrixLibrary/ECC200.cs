using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;


namespace DataMatrixLibrary
{
    public class ECC200
    {
        public int nrow;              //对应的数据区域行；            
        public int ncol;              //对应的数据区域列；
        public int dataLength;        //对应的数据码长度；
        public int errorLength;       //对应的纠错码长度；
        public int symbolrows;        //对应的总区域行；
        public int symbolcols;        //对应的总区域列；

        public int[] KeyArray;        //放置标号数组；
        public int[] ValueArray;      //二进制数组；

        public ECC200(int codeLength)
        {

            Correlative cor = new Correlative(codeLength);
            nrow = cor.nrow;
            ncol = cor.ncol;
            dataLength = cor.dataLength;
            errorLength = cor.errorLength;
            symbolrows = cor.symbolrows;
            symbolcols = cor.symbolcols;

            int row, col, chr;
            KeyArray = ValueArray = new int[ncol * nrow];
            chr = 1; row = 4; col = 0;

            do
            {
                if ((row == nrow) && (col == 0))
                    corner1(chr++);
                if ((row == nrow - 2) && (col == 0) && (ncol % 4 != 0))
                    corner2(chr++);
                if ((row == nrow - 2) && (col == 0) && (ncol % 8 == 4))
                    corner3(chr++);
                if ((row == nrow + 4) && (col == 2) && ((ncol % 8) == 0))
                    corner4(chr++);
                do
                {
                    if ((row < nrow) && (col >= 0) && (KeyArray[row * ncol + col] == 0))
                        utah(row, col, chr++);
                    row -= 2;
                    col += 2;
                } while ((row >= 0) && (col < ncol));

                row += 1; col += 3;

                do
                {
                    if ((row >= 0) && (col < ncol) && (KeyArray[row * ncol + col] == 0))
                        utah(row, col, chr++);
                    row += 2; col -= 2;
                } while ((col >= 0) && (row < nrow));
                row += 3; col += 1;
            } while ((row < nrow) || (col < ncol));
            if (KeyArray[nrow * ncol - 1] == 0)
            {
                KeyArray[nrow * ncol - 1] = KeyArray[nrow * ncol - ncol - 2] = 1;
            }
        }



        /// <summary>
        /// 给KeyArray赋值
        /// </summary>
        /// <param name="row">行索引</param>
        /// <param name="col">列索引</param>
        /// <param name="chr">前标</param>
        /// <param name="bit">后标</param>
        /// <param name="nrow">总行数</param>
        /// <param name="ncol">总列数</param>
        void module(int row, int col, int chr, int bit)
        {

            if (row < 0)
            {
                row += nrow;
                col += 4 - (ncol + 4) % 8;
            }
            if (col < 0)
            {
                col += ncol;
                row += 4 - (nrow + 4) % 8;
            }
            KeyArray[row * ncol + col] = 10 * chr + bit;
        }

        GetLog getlog = new GetLog();

        /// <summary>
        /// 在八位二进制数字位置上分别赋值
        /// </summary>
        /// <param name="row">行索引</param>
        /// <param name="col">列索引</param>
        /// <param name="chr">前标</param>
        void utah(int row, int col, int chr)
        {
            module(row - 2, col - 2, chr, 1);
            module(row - 2, col - 1, chr, 2);
            module(row - 1, col - 2, chr, 3);
            module(row - 1, col - 1, chr, 4);
            module(row - 1, col, chr, 5);
            module(row, col - 2, chr, 6);
            module(row, col - 1, chr, 7);
            module(row, col, chr, 8);
        }

        /// <summary>
        /// DataMatrix有4中特殊边界排列，第1种情况的每个位置分别赋值
        /// </summary>
        /// <param name="chr">前标</param>
        void corner1(int chr)
        {
            module(nrow - 1, 0, chr, 1);
            module(nrow - 1, 1, chr, 2);
            module(nrow - 1, 2, chr, 3);
            module(0, ncol - 2, chr, 4);
            module(0, ncol - 1, chr, 5);
            module(1, ncol - 1, chr, 6);
            module(2, ncol - 1, chr, 7);
            module(3, ncol - 1, chr, 8);
        }

        /// <summary>
        ///第2种边界情况的每个位置赋值
        /// </summary>
        /// <param name="chr">前标</param>
        void corner2(int chr)
        {
            module(nrow - 3, 0, chr, 1);
            module(nrow - 2, 0, chr, 2);
            module(nrow - 1, 0, chr, 3);
            module(0, ncol - 4, chr, 4);
            module(0, ncol - 3, chr, 5);
            module(0, ncol - 2, chr, 6);
            module(0, ncol - 1, chr, 7);
            module(1, ncol - 1, chr, 8);
        }

        /// <summary>
        /// 第3种边界情况的每个位置赋值
        /// </summary>
        /// <param name="chr">前标</param>
        void corner3(int chr)
        {
            module(nrow - 3, 0, chr, 1);
            module(nrow - 2, 0, chr, 2);
            module(nrow - 1, 0, chr, 3);
            module(0, ncol - 2, chr, 4);
            module(0, ncol - 1, chr, 5);
            module(1, ncol - 1, chr, 6);
            module(2, ncol - 1, chr, 7);
            module(3, ncol - 1, chr, 8);
        }

        /// <summary>
        /// 第4种边界情况的每个位置赋值
        /// </summary>
        /// <param name="chr">前标</param>
        void corner4(int chr)
        {
            module(nrow - 1, 0, chr, 1);
            module(nrow - 1, ncol - 1, chr, 2);
            module(0, ncol - 3, chr, 3);
            module(0, ncol - 2, chr, 4);
            module(0, ncol - 1, chr, 5);
            module(1, ncol - 3, chr, 6);
            module(1, ncol - 2, chr, 7);
            module(1, ncol - 1, chr, 8);
        }





        /// <summary>
        /// 将字符串转化为Byte
        /// </summary>
        /// <param name="a">字符串</param>
        byte[] StringtoInt(string a)
        {
            byte[] code = Encoding.ASCII.GetBytes(a);
            return code;
        }


        /// <summary>
        /// 将输入的Byte数组转化为Int
        /// </summary>
        /// <param name="code">Byte数组</param>
        List<int> Encode(byte[] code, int datalength, int errorlength, int inputlength)
        {
            List<int> CodeArray = new List<int>();
            int i = 0;

            while (i < code.Length)
            {
                if (char.IsDigit((char)code[i]) &&
                    (i + 1 < code.Length && char.IsDigit((char)code[i + 1])))
                {
                    CodeArray.Add((code[i] - '0') * 10 + (code[i + 1] - '0') + 130);
                    i += 2; ;
                    inputlength -= 1;
                }
                else
                {
                    CodeArray.Add(code[i] + 1);
                    i++;
                }
            }

            ComputePseudoRandomCode(CodeArray, datalength);
            Gaussion(CodeArray, datalength, errorlength);
            CodeArray.RemoveAt(datalength + errorlength);
            return CodeArray;
        }

        /// <summary>
        /// 计算伪随机码值
        /// </summary>
        /// <param name="num">伪随机码个数</param>
        /// <param name="datalen">数据长度</param>
        void ComputePseudoRandomCode(List<int> codeArray, int datalen)
        {
            if (codeArray == null)
                return;

            int num = datalen - codeArray.Count;

            if (num > 0)
            {
                int[] AddArray = new int[num];
                AddArray[0] = 129;
                codeArray.Add(AddArray[0]);
                for (int k = 1; k < num; k++)
                {
                    AddArray[k] = (149 * (datalen - num + 1 + k) % 253 + 1 + 129) % 254;
                    codeArray.Add(AddArray[k]);
                }
            }
        }


        int prod(int x, int y, int[] log, int[] alog, int gf)
        {
            if (x == 0 || y == 0)
                return 0;
            else return alog[(log[x] + log[y]) % (gf - 1)];
        }

        /// <summary>
        /// 计算纠错码值
        /// </summary>
        /// <param name="total">总条码长度</param>
        /// <param name="data">数据长度</param>
        /// <param name="error">纠错码长度</param>
        /// <returns></returns>
        void Gaussion(List<int> codeArray, int data, int error)
        {

            int gf = 256;
            int total = data + error;
            int[] codes = new int[total + 1];
            int i, j, k;
            int[] c = new int[error + 1];


            for (i = 0; i < codeArray.Count; i++)
                codes[i] = codeArray[i];
            for (i = 1; i <= error; i++)
                c[i] = 0;
            c[0] = 1;
            for (i = 1; i <= error; i++)
            {
                c[i] = c[i - 1];
                for (j = i - 1; j >= 1; j--)
                {
                    c[j] = c[j - 1] ^ prod(c[j], getlog.alog[i], getlog.log, getlog.alog, gf);
                }
                c[0] = prod(c[0], getlog.alog[i], getlog.log, getlog.alog, gf);
            }
            for (i = 0; i < data; i++)
            {
                k = codes[data] ^ codes[i];
                for (j = 0; j < error; j++)
                {
                    codes[data + j] = codes[data + j + 1] ^ prod(k, c[error - j - 1], getlog.log, getlog.alog, gf);
                }
            }
            for (i = data; i <= total; i++)
                codeArray.Add(codes[i]);
        }


        /// <summary>
        /// 将int数据分别转换为二进制并对二进制数组赋值
        /// </summary>
        /// <param name="CodeArray">原int数组</param>
        /// <returns></returns>
        int[] GetByteValue(List<int> CodeArray)
        {
            int start = CodeArray.Count * 10 + 8 + 2;

            for (int k = CodeArray.Count - 1; k >= 0; k--)
            {
                start -= 2;
                int n = CodeArray[k];
                //start = start - 2;
                for (int j = 0; j < 8; j++)
                {
                    int i = GetIndexByKeyArray(start);
                    if (i >= 0)
                        ValueArray[i] = n % 2;
                    n /= 2;
                    start = start - 1;
                }
            }
            return ValueArray;
        }

        /// <summary>
        /// 获取数组中某查询数字的索引
        /// </summary>
        /// <param name="key">查询数字</param>
        /// <returns></returns>
        int GetIndexByKeyArray(int key)
        {
            for (int i = 0; i < KeyArray.Length; i++)
            {
                if (KeyArray[i] == key)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="data">输入字符串</param>
        /// <param name="Xamplify">X方向放大因子</param>
        /// <param name="Yamplify">Y方向放大因子</param>
        /// <returns></returns>
        public Image GetBitmap(string data, int Xamplify, int Yamplify)
        {
            int inputlength;
            byte[] code = StringtoInt(data);
            inputlength = code.Length;

            List<int> CodeArray = Encode(code, dataLength, errorLength, inputlength);

            ValueArray = GetByteValue(CodeArray);

            Bitmap bitmap = new Bitmap(symbolcols * Xamplify, symbolrows * Yamplify);
            Brush brush1 = new SolidBrush(Color.Black);

            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);

            int a = nrow / 25;
            int b;
            if (a == 0)
                b = 1;
            else if (a == 1)
                b = 2;
            else if (a == 2 || a == 3)
                b = 4;
            else
                b = 6;
            int c = symbolrows / b;

            g.FillRectangle(brush1, 0, 0, Xamplify, symbolrows * Yamplify);
            g.FillRectangle(brush1, 0, (symbolrows - 1) * Yamplify, Xamplify * symbolcols, Yamplify);

            for (int i = 0; i < symbolrows; i++)
            {
                for (int j = 0; j < symbolcols; j++)
                {
                    g.FillRectangle(brush1, j * Xamplify, 0, Xamplify, Yamplify);
                    g.FillRectangle(brush1, (symbolcols - 1) * Xamplify, (i + 1) * Yamplify, Xamplify, Yamplify);
                    for (int m = 0; m < b - 1; m++)
                    {
                        g.FillRectangle(brush1, (c + m * c) * Xamplify, 0, Xamplify, symbolrows * Yamplify);
                        g.FillRectangle(brush1, 0, (c - 1 + m * c) * Yamplify, symbolcols * Xamplify, Yamplify);
                        g.FillRectangle(brush1, (c - 1 + m * c) * Xamplify, (i + 1) * Yamplify, Xamplify, Yamplify);
                        g.FillRectangle(brush1, j * Xamplify, (c + m * c) * Yamplify, Xamplify, Yamplify);
                    }
                    j++;
                }
                i++;
            }

            int p = nrow / b;
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++)
                {
                    if (ValueArray[i * ncol + j] == 1)
                        g.FillRectangle(brush1, (j + 1 + j / p * 2) * Xamplify, (i + 1 + i / p * 2) * Yamplify, Xamplify, Yamplify);
                }
            }
            return bitmap;
        }
    }
}
