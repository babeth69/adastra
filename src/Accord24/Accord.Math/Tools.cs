﻿// Accord Math Library
// The Accord.NET Framework
// http://accord-net.origo.ethz.ch
//
// Copyright © César Souza, 2009-2012
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.Math
{
    using System;
    using System.Collections.Generic;
    using AForge;

    /// <summary>
    ///   Set of mathematical tools.
    /// </summary>
    /// 
    public static class Tools
    {

        #region Framework-wide random number generator
        private static Random random = new Random();

        /// <summary>
        ///   Gets a reference to the random number generator used
        ///   internally by the Accord.NET classes and methods.
        /// </summary>
        public static Random Random { get { return random; } }

        /// <summary>
        ///   Sets a random seed for the internal number generator.
        /// </summary>
        public static void SetupGenerator(int seed)
        {
            random = new Random(seed);
        }
        #endregion


        /// <summary>
        ///   Gets the angle formed by the vector [x,y].
        /// </summary>
        /// 
        public static float Angle(float x, float y)
        {
            if (y >= 0)
            {
                if (x >= 0)
                    return (float)Math.Atan(y / x);
                return (float)(Math.PI - Math.Atan(-y / x));
            }
            else
            {
                if (x >= 0)
                    return (float)(2 * Math.PI - Math.Atan(-y / x));
                return (float)(Math.PI + Math.Atan(y / x));
            }
        }

        /// <summary>
        ///   Gets the angle formed by the vector [x,y].
        /// </summary>
        /// 
        public static double Angle(double x, double y)
        {
            if (y >= 0)
            {
                if (x >= 0)
                    return Math.Atan2(y, x);
                return Math.PI - Math.Atan(-y/ x);
            }
            else
            {
                if (x >= 0)
                    return 2.0 * Math.PI - Math.Atan2(-y, x);
                return Math.PI + Math.Atan(y/ x);
            }
        }

        /// <summary>
        ///   Gets the displacement angle between two points.
        /// </summary>
        /// 
        public static double Angle(IntPoint previous, IntPoint next)
        {
            double dx = next.X - previous.X;
            double dy = next.Y - previous.Y;

            return Accord.Math.Tools.Angle(dx, dy);
        }

        /// <summary>
        ///   Gets the displacement angle between two points, coded
        ///   as an integer varying from 0 to 20.
        /// </summary>
        /// 
        public static int Direction(IntPoint previous, IntPoint next)
        {
            double dx = next.X - previous.X;
            double dy = next.Y - previous.Y;

            double radians = Accord.Math.Tools.Angle(dx, dy);

            // code = Floor(20 / (2*System.Math.PI))
            int code = (int)System.Math.Floor(radians * 3.183098861837907);

            return code;
        }

        /// <summary>
        ///   Returns the next power of 2 after the input value x.
        /// </summary>
        /// 
        /// <param name="x">Input value x.</param>
        /// <returns>Returns the next power of 2 after the input value x.</returns>
        /// 
        public static int NextPowerOf2(int x)
        {
            --x;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return ++x;
        }

        /// <summary>
        ///   Returns the previous power of 2 after the input value x.
        /// </summary>
        /// 
        /// <param name="x">Input value x.</param>
        /// <returns>Returns the previous power of 2 after the input value x.</returns>
        /// 
        public static int PreviousPowerOf2(int x)
        {
            return NextPowerOf2(x + 1) / 2;
        }


        /// <summary>
        ///   Hypotenuse calculus without overflow/underflow
        /// </summary>
        /// 
        /// <param name="a">first value</param>
        /// <param name="b">second value</param>
        /// <returns>The hypotenuse Sqrt(a^2 + b^2)</returns>
        /// 
        public static double Hypotenuse(double a, double b)
        {
            double r = 0.0;
            double absA = System.Math.Abs(a);
            double absB = System.Math.Abs(b);

            if (absA > absB)
            {
                r = b / a;
                r = absA * System.Math.Sqrt(1 + r * r);
            }
            else if (b != 0)
            {
                r = a / b;
                r = absB * System.Math.Sqrt(1 + r * r);
            }

            return r;
        }

        /// <summary>
        ///   Hypotenuse calculus without overflow/underflow
        /// </summary>
        /// 
        /// <param name="a">first value</param>
        /// <param name="b">second value</param>
        /// <returns>The hypotenuse Sqrt(a^2 + b^2)</returns>
        /// 
        public static decimal Hypotenuse(decimal a, decimal b)
        {
            decimal r = 0;
            decimal absA = System.Math.Abs(a);
            decimal absB = System.Math.Abs(b);

            if (absA > absB)
            {
                r = b / a;
                r = absA * (decimal)System.Math.Sqrt((double)(1 + r * r));
            }
            else if (b != 0)
            {
                r = a / b;
                r = absB * (decimal)System.Math.Sqrt((double)(1 + r * r));
            }

            return r;
        }

        /// <summary>
        ///   Hypotenuse calculus without overflow/underflow
        /// </summary>
        /// 
        /// <param name="a">first value</param>
        /// <param name="b">second value</param>
        /// <returns>The hypotenuse Sqrt(a^2 + b^2)</returns>
        /// 
        public static float Hypotenuse(float a, float b)
        {
            double r = 0;
            float absA = System.Math.Abs(a);
            float absB = System.Math.Abs(b);

            if (absA > absB)
            {
                r = b / a;
                r = absA * System.Math.Sqrt(1 + r * r);
            }
            else if (b != 0)
            {
                r = a / b;
                r = absB * System.Math.Sqrt(1 + r * r);
            }

            return (float)r;
        }

        /// <summary>
        ///   Gets the proper modulus operation for
        ///   a integer x and modulo m.
        /// </summary>
        public static int Mod(int x, int m)
        {
            if (m < 0) m = -m;
            int r = x % m;
            return r < 0 ? r + m : r;
        }


        #region Scaling functions
        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static int Scale(this IntRange from, IntRange to, int x)
        {
            if (from.Length == 0) return 0;
            return (to.Length) * (x - from.Min) / from.Length + to.Min;
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static double Scale(this DoubleRange from, DoubleRange to, double x)
        {
            if (from.Length == 0) return 0;
            return (to.Length) * (x - from.Min) / from.Length + to.Min;
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static double Scale(double fromMin, double fromMax, double toMin, double toMax, double x)
        {
            if (fromMax - fromMin == 0) return 0;
            return (toMax - toMin) * (x - fromMin) / (fromMax - fromMin) + toMin;
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static double[] Scale(double fromMin, double fromMax, double toMin, double toMax, double[] x)
        {
            double[] result = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                result[i] = (toMax - toMin) * (x[i] - fromMin) / (fromMax - fromMin) + toMin;

            return result;
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static float[] Scale(float fromMin, float fromMax, float toMin, float toMax, float[] x)
        {
            float[] result = new float[x.Length];
            for (int i = 0; i < x.Length; i++)
                result[i] = (toMax - toMin) * (x[i] - fromMin) / (fromMax - fromMin) + toMin;

            return result;
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static double[] Scale(double toMin, double toMax, double[] x)
        {
            return Scale(Matrix.Min(x), Matrix.Max(x), toMin, toMax, x);
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static double[][] Scale(double[] fromMin, double[] fromMax, double[] toMin, double[] toMax, double[][] x)
        {
            int rows = x.Length;
            int cols = fromMin.Length;

            double[][] result = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                result[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    result[i][j] = (toMax[j] - toMin[j]) * (x[i][j] - fromMin[j]) / (fromMax[j] - fromMin[j]) + toMin[j];
                }
            }

            return result;
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static double[][] Scale(double[] fromMin, double[] fromMax, double toMin, double toMax, double[][] x)
        {
            int rows = x.Length;
            int cols = fromMin.Length;

            double[][] result = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                result[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    result[i][j] = (toMax - toMin) * (x[i][j] - fromMin[j]) / (fromMax[j] - fromMin[j]) + toMin;
                }
            }

            return result;
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static double[][] Scale(double[] toMin, double[] toMax, double[][] x)
        {
            var min = Matrix.Min(x);
            var max = Matrix.Max(x);
            return Scale(min, max, toMin, toMax, x);
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static double[][] Scale(double toMin, double toMax, double[][] x)
        {
            return Scale(Matrix.Min(x), Matrix.Max(x), toMin, toMax, x);
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static float Scale(float fromMin, float fromMax, float toMin, float toMax, float x)
        {
            if (fromMax - fromMin == 0) return 0;
            return (toMax - toMin) * (x - fromMin) / (fromMax - fromMin) + toMin;
        }

        /// <summary>
        ///   Converts the value x (which is measured in the scale
        ///   'from') to another value measured in the scale 'to'.
        /// </summary>
        /// 
        public static double Scale(IntRange from, DoubleRange to, int x)
        {
            if (from.Length == 0) return 0;
            return (to.Length) * (x - from.Min) / from.Length + to.Min;
        }
        #endregion


        /// <summary>
        ///   Returns the hyperbolic arc cosine of the specified value.
        /// </summary>
        /// 
        public static double Acosh(double x)
        {
            if (x < 1.0)
                throw new ArgumentOutOfRangeException("x");
            return System.Math.Log(x + System.Math.Sqrt(x * x - 1));
        }

        /// <summary>
        /// Returns the hyperbolic arc sine of the specified value.
        /// </summary>
        /// 
        public static double Asinh(double d)
        {
            double x;
            int sign;

            if (d == 0.0)
                return d;

            if (d < 0.0)
            {
                sign = -1;
                x = -d;
            }
            else
            {
                sign = 1;
                x = d;
            }
            return sign * System.Math.Log(x + System.Math.Sqrt(x * x + 1));
        }

        /// <summary>
        /// Returns the hyperbolic arc tangent of the specified value.
        /// </summary>
        /// 
        public static double Atanh(double d)
        {
            if (d > 1.0 || d < -1.0)
                throw new ArgumentOutOfRangeException("d");
            return 0.5 * System.Math.Log((1.0 + d) / (1.0 - d));
        }



        /// <summary>
        ///   Returns the factorial falling power of the specified value.
        /// </summary>
        /// 
        public static int FactorialPower(int value, int degree)
        {
            int t = value;
            for (int i = 0; i < degree; i++)
                t *= degree--;
            return t;
        }

        /// <summary>
        ///   Truncated power function.
        /// </summary>
        /// 
        public static double TruncatedPower(double value, double degree)
        {
            double x = System.Math.Pow(value, degree);
            return (x > 0) ? x : 0.0;
        }

        /// <summary>
        ///   Fast inverse floating-point square root.
        /// </summary>
        /// 
        public unsafe static float InvSqrt(float f)
        {
            float xhalf = 0.5f * f;
            Int32 i = *(Int32*)&f;
            i = 0x5f375a86 - (i >> 1);
            f = *(float*)&i;
            f = f * (1.5f - xhalf * f * f);
            return f;
        }

        /// <summary>
        ///   Generates all possible combinations of a given
        ///   number of symbols of a given length.
        /// </summary>
        /// 
        /// <param name="symbols">The number of symbols.</param>
        /// <param name="length">The length of the sequence to generate.</param>
        ///
        public static int[][] Combinations(int symbols, int length)
        {
            int size = (int)Math.Pow(symbols, length);

            int[][] sequences = new int[size][];
            for (int i = 0; i < sequences.Length; i++)
                sequences[i] = new int[length];


            for (int i = 0; i < length; i++)
            {
                int m1 = (int)Math.Pow(symbols, i);
                int m2 = (int)Math.Pow(symbols, length - i);
                int j = 0;

                for (int k2 = 0; k2 < m2; k2++)
                {
                    int s = k2 % symbols;
                    for (int k = 0; k < m1; k++)
                    {
                        sequences[j][i] = s;
                        j++;
                    }
                }
            }


            return sequences;
        }
    }

    /// <summary>
    ///   Directions for the General Comparer.
    /// </summary>
    /// 
    public enum ComparerDirection
    {
        /// <summary>
        ///   Sorting will be performed in ascending order.
        /// </summary>
        /// 
        Ascending,

        /// <summary>
        ///   Sorting will be performed in descending order.
        /// </summary>
        /// 
        Descending
    };

    /// <summary>
    ///   Custom comparer which accepts any delegate or
    ///   anonymous function to perform comparison of values.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    /// 
    public class CustomComparer<T> : IComparer<T>
    {
        
        private Func<T, T, int> comparer;


        /// <summary>
        ///   Constructs a new <see cref="CustomComparer&lt;T&gt;"/>.
        /// </summary>
        /// 
        /// <param name="comparer">The comparer function.</param>
        /// 
        public CustomComparer(Func<T,T,int> comparer)
        {
            this.comparer = comparer;
        }

        /// <summary>
        ///   Compares two objects and returns a value indicating
        ///   whether one is less than, equal to, or greater than
        ///   the other.
        /// </summary>
        /// 
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// 
        /// <returns>A signed integer that indicates the relative values of x and y.</returns>
        /// 
        public int Compare(T x, T y)
        {
            return comparer(x, y);
        }
    }

    /// <summary>
    ///   General comparer which supports multiple directions
    ///   and comparison of absolute values.
    /// </summary>
    /// 
    public class GeneralComparer : IComparer<double>
    {
        private bool absolute;
        private int direction = 1;

        /// <summary>
        ///   Constructs a new General Comparer.
        /// </summary>
        /// 
        /// <param name="direction">The direction to compare.</param>
        /// 
        public GeneralComparer(ComparerDirection direction)
            : this(direction, false)
        {
        }

        /// <summary>
        ///   Constructs a new General Comparer.
        /// </summary>
        /// 
        /// <param name="direction">The direction to compare.</param>
        /// <param name="useAbsoluteValues">True to compare absolute values, false otherwise. Default is false.</param>
        /// 
        public GeneralComparer(ComparerDirection direction, bool useAbsoluteValues)
        {
            this.direction = (direction == ComparerDirection.Ascending) ? 1 : -1;
            this.absolute = useAbsoluteValues;
        }

        /// <summary>
        ///   Compares two objects and returns a value indicating whether one is less than,
        ///    equal to, or greater than the other.
        /// </summary>
        /// 
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// 
        public int Compare(double x, double y)
        {
            if (absolute)
                return direction * (System.Math.Abs(x).CompareTo(System.Math.Abs(y)));
            else
                return direction * (x.CompareTo(y));
        }
    }
}