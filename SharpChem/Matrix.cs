/* File:        Matrix.cs
 * Project:     SharpChem
 * Author:      Elijah Creed Fedele
 * Date:        July 9, 2022
 * Description: Provides an implementation of numerical matrices for the linear and Gaussian routines found in the
 * SharpChem thermodynamic logic. This implementation is designed for square matrices only.
 * 
 * Licensed to the Apache Software Foundation (ASF) under one or more contributor license agreements.  See the NOTICE 
 * file distributed with this work for additional information regarding copyright ownership.  The ASF licenses this 
 * file to you under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
 * with the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on 
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License for the 
 * specific language governing permissions and limitations under the License. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpChem.Math
{
    public class Matrix
    {
        public int Size;
        private decimal[,] matrix;

        #region Constructors

        /// <summary>
        /// Constructs an empty (zero-filled) square matrix with dimensions N × N. Used when one intends to use row or
        /// column options directly to set values.
        /// </summary>
        /// <param name="N">The dimension to construct the matrix over</param>
        public Matrix(int N)
        {
            matrix = new decimal[N, N];
            for (int i = 0; i < N; i++) {
                for (int j = 0; j < N; j++)
                    matrix[i, j] = 0m;
            }
            this.Size = N;
        }

        /// <summary>
        /// Constructs an array with the data specified by <c>data</c>. Used when one already possesses square-formatted
        /// data but requires the methods of <c>Matrix</c>. Fails if <c>data</c> is not a square matrix.
        /// </summary>
        /// <param name="data">A two dimensional container of data to load</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the data is not square</exception>
        public Matrix(decimal[,] data)
        {
            if (data.GetLength(0) != data.GetLength(1)) {
                int n = data.GetLength(0), m = data.GetLength(1);
                throw new ArgumentOutOfRangeException(nameof(data), $"Bounds not equal ({n},{m}");
            }
            else {
                matrix = data;
                this.Size = data.GetLength(0);
            }
        }

        /// <summary>
        /// Constructs a matrix with the data specified in the vector (single-dimensional array) <c>data</c>. Uses the 
        /// <c>size</c> parameter to unroll the size m = n x n data into a n x n matrix. Fails if the length of 
        /// <c>data</c> is not equal to the square of <c>size</c>.
        /// </summary>
        /// <param name="size">The dimension of the matrix to fill</param>
        /// <param name="data">The single-dimensional data to fill</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the sizes specified are not equal</exception>
        public Matrix(int size, decimal[] data)
        {
            if (size * size != data.GetLength(0)) {
                int n = data.GetLength(0), m = data.GetLength(1);
                throw new ArgumentOutOfRangeException(nameof(data), $"Bounds not equal ({n},{m}");
            }
            else {
                decimal[,] temp = new decimal[size, size];
            }
        }

        #endregion

        #region Product Operations

        #endregion 
    }
}
