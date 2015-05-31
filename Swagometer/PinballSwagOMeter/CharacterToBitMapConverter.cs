﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace PinballSwagOMeter
{
    public class CharacterToBitMapConverter
    {
        #region charMap
        private static readonly IDictionary<char, ulong> Map = new Dictionary<char, ulong> {
            {'A', 0xCF3FFFCF3FDE},
            {'B', 0x17FFCDF7F3FDF},
            {'C', 0x21FBF0C30C3FFE},
            {'D', 0x7FFCF3CF3FDF},
            {'E', 0xFFF0FFFC3FFF},
            {'F', 0x200C30FFFC3FFF},
            {'G', 0x17BFCFB0F3FDE},
            {'H', 0x32CCF3CFFFF3CF3},
            {'I', 0x7FFF30C30C30C30C},
            {'J', 0x30C37BFCF0C30C30},
            {'K', 0xCF3CDF7F3CF3},
            {'L', 0xFFF0C30C30C3},
            {'M', 0xCF3CF3FFFCE1},
            {'N', 0XCF3CFBFF7CF3},
            {'O', 0X7BFCF3CF3FDE},
            {'P', 0XC30DFFF3FDF},
            {'Q', 0XFBFEF3CF3FDE},
            {'R', 0XCF3CDFFF3FDF},
            {'S', 0X7FFC3E7C3FFE},
            {'T', 0X30C30C30CFFF},
            {'U', 0X7BFCF3CF3CF3},
            {'V', 0X31EFF3CF3CF3},
            {'W', 0X873FFFCF3CF3},
            {'X', 0XCF3CDE7B3CF3},
            {'Y', 0X30C31EFF3CF3},
            {'Z', 0XFFF0C6318FFF},
            {'0', 0X7BFCF3CF3FDE},
            {'1', 0X78C30C30C38C},
            {'2', 0XFFF0CE730FDF},
            {'3', 0X7FFC1E7B0FDF},
            {'4', 0XC30C3EFF3CF3},
            {'5', 0X7FFC3F7C3FFF},
            {'6', 0X7BFCFF7C3FFE},
            {'7', 0X618618C30FFF},
            {'8', 0X7BFCDE7B3FDE},
            {'9', 0XC30C3EFF3FDE},
            {' ', 0X0},
            {'£', 0xFFF09F7C289C},
            {'*', 0x115395100},
            {':', 0xC30000C300}
        };
        #endregion

        private readonly Bitmap _offBitmap;
        private readonly Bitmap _onBitmap;

        public CharacterToBitMapConverter(Bitmap onBitmap, Bitmap offBitmap)
        {
            _onBitmap = onBitmap;
            _offBitmap = offBitmap;
        }

        public BigInteger[] GetBitPattern(string line1, string line2, string line3, string line4)
        {
            var bitPatterns = new List<BigInteger>();
            bitPatterns.AddRange(CentreAndGetBitPattern(line1));
            bitPatterns.Add(0);
            bitPatterns.AddRange(CentreAndGetBitPattern(line2));
            bitPatterns.Add(0);
            bitPatterns.AddRange(CentreAndGetBitPattern(line3));
            bitPatterns.Add(0);
            bitPatterns.AddRange(CentreAndGetBitPattern(line4));
            return bitPatterns.ToArray();
        }

        public void BuildBitMapPicture(BigInteger[] bitPatterns, int imageWidth, int imageHeight, Graphics bitmapGraphics)
        {
            for (var row = 0; row < bitPatterns.Length; ++row)
            {
                var bitMask = bitPatterns.ElementAt(row);
                var bit = new BigInteger(Math.Pow(2, Constants.Columns - 1));
                for (var col = 0; col < Constants.Columns; ++col)
                {
                    var bitmapForBit = (bitMask & bit) == bit ? _onBitmap : _offBitmap;
                    bitmapGraphics.DrawImage(bitmapForBit, col * imageWidth, row * imageHeight, imageWidth, imageHeight);
                    bit >>= 1;
                }
            }
        }

        private static IEnumerable<BigInteger> CentreAndGetBitPattern(string input)
        {
            input = new string(input.ToUpper().Where(Map.ContainsKey).ToArray());
            input = input.PadLeft(input.Length + ((20 - input.Length) / 2));

            var numberOfBitsForInput = input.Length * 7;

            var lines = new BigInteger[8];

            for (var line = 0; line < 8; ++line)
            {
                BigInteger outputLine = 0;
                foreach (var character in input.ToUpper())
                {
                    var bitMask = Map[character];
                    var bits = bitMask >> line * 6;
                    for (var col = 0; col < 6; ++col)
                    {
                        outputLine <<= 1;
                        outputLine += bits & 1;
                        bits >>= 1;
                    }
                    outputLine <<= 1;
                }

                outputLine <<= (Constants.Columns - numberOfBitsForInput);
                lines[line] = outputLine;
            }
            return lines;
        }
    }
}
