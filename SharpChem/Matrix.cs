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
        /// Constructs an empty (zero-filled) square matrix with dimensions N × N. Used when one intends to use row or
        /// column options directly to set values.
        /// </summary>
        /// <param name="N">The dimension to construct the matrix over</param>
        /// <param name="fill">The desired value to insert into the matrix's cells</param>
        public Matrix(int N, decimal fill)
        {
            matrix = new decimal[N, N];
            for (int i = 0; i < N; i++) {
                for (int j = 0; j < N; j++)
                    matrix[i, j] = fill;
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
                for (int j = 0; j < size;j++) {
                    for (int k = 0; k < size; k++) {
                        int i = (j * size) + k;
                        temp[j, k] = data[i];
                    }
                }
                matrix = temp;
                this.Size = size;
            }
        }

        #endregion

        #region Accessors and Indexers

        /// <summary>
        /// Indexer which provides access to <c>Matrix</c> entries in the conventional array syntax.
        /// </summary>
        public decimal this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

        /// <summary>
        /// Retrieves a row from the matrix.
        /// </summary>
        /// <param name="rowNumber">The index to retrieve</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of bounds</exception>
        /// <returns>A decimal array corresponding to the row contents</returns>
        public decimal[] GetRow(int rowNumber)
        {
            int N = matrix.GetLength(0);
            if (rowNumber >= N)
                throw new ArgumentOutOfRangeException(nameof(GetRow), $"Index {rowNumber} not in bounds ({N})");
            else {
                decimal[] row = new decimal[N];
                for (int i = 0; i < N; i++)
                    row[i] = matrix[rowNumber, i];
                return row;
            }
        }

        /// <summary>
        /// Sets a row within the matrix equal to a supplied array.
        /// </summary>
        /// <param name="rowNumber">The row at which to place the array within the matrix</param>
        /// <param name="rowValue">The array to place at the indicated row</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if row not the same size as matrix width</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <c>rowNumber</c> not in bounds</exception>
        public void SetRow(int rowNumber, decimal[] rowValue)
        {
            int N = matrix.GetLength(0);
            if (rowValue.Length != N || rowNumber >= N) {
                if (rowValue.Length != N)
                    throw new ArgumentOutOfRangeException(nameof(rowValue), $"Row not equal to matrix size");
                else
                    throw new ArgumentOutOfRangeException(nameof(SetRow), $"Index {rowNumber} not in bounds ({N})");
            }
            else {
                for (int i = 0; i < N; i++)
                    matrix[rowNumber, i] = rowValue[i];
            }
        }

        /// <summary>
        /// Retrieves a column from the matrix.
        /// </summary>
        /// <param name="colNumber">The index to retrieve</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of bounds</exception>
        /// <returns>A decimal array corresponding to the column contents</returns>
        public decimal[] GetColumn(int colNumber)
        {
            int N = matrix.GetLength(0);
            if (colNumber >= N)
                throw new ArgumentOutOfRangeException(nameof(GetColumn), $"Index {colNumber} not in bounds ({N})");
            else {
                decimal[] column = new decimal[N];
                for (int i = 0; i < N; i++)
                    column[i] = matrix[i, colNumber];
                return column;
            }
        }

        /// <summary>
        /// Sets a column within the matrix equal to a supplied array.
        /// </summary>
        /// <param name="colNumber">The column at which to place the array within the matrix</param>
        /// <param name="colValue">The array to place at the indicated column</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if column not the same size as matrix width</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <c>colNumber</c> not in bounds</exception>
        public void SetColumn(int colNumber, decimal[] colValue)
        {
            int N = matrix.GetLength(0);
            if (colValue.Length != N || colNumber >= N) {
                if (colValue.Length != N)
                    throw new ArgumentOutOfRangeException(nameof(colValue), $"Row not equal to matrix size");
                else
                    throw new ArgumentOutOfRangeException(nameof(SetColumn), $"Index {colNumber} not in bounds ({N})");
            }
            else {
                for (int i = 0; i < N; i++)
                    matrix[i, colNumber] = colValue[i];
            }
        }

        #endregion

        #region Operator Overloads and Class Methods

        /// <summary>
        /// Constructs an identity matrix with dimension N.
        /// </summary>
        /// <param name="N">The dimension of the matrix</param>
        /// <returns>An identity matrix with dimension N</returns>
        public static Matrix Identity(int N)
        {
            Matrix identity = new Matrix(N);
            for (int i = 0; i < N; i++)
                identity[i, i] = 1.0m;
            return identity;
        }

        /// <summary>
        /// Operator overload which performs piecewise matrix addition. Fails if the two matrices are of different 
        /// dimension.
        /// </summary>
        /// <param name="lhs">The left hand side of the operator</param>
        /// <param name="rhs">The right hand side of the operator</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the two matrices differ in dimension</exception>
        /// <returns>A matrix C = A + B</returns>
        public static Matrix operator +(Matrix lhs, Matrix rhs)
        {
            if (lhs.Size != rhs.Size) {
                int L = lhs.Size, R = rhs.Size;
                throw new ArgumentOutOfRangeException(nameof(lhs), $"Matrix dimensions unequal ({L}, {R})");
            }
            else {
                Matrix outputMatrix = new Matrix(lhs.Size);
                for (int i = 0; i < lhs.Size; i++) {
                    for (int j = 0; j < lhs.Size; j++)
                        outputMatrix[i, j] = lhs[i, j] + lhs[i, j];
                }
                return outputMatrix;
            }
        }

        /// <summary>
        /// Operator overload which performs piecewise matrix subtraction. Fails if the two matrices are of different 
        /// dimension.
        /// </summary>
        /// <param name="lhs">The left hand side of the operator</param>
        /// <param name="rhs">The right hand side of the operator</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the two matrices differ in dimension</exception>
        /// <returns>A matrix C = A - B</returns>
        public static Matrix operator -(Matrix lhs, Matrix rhs)
        {
            if (lhs.Size != rhs.Size) {
                int L = lhs.Size, R = rhs.Size;
                throw new ArgumentOutOfRangeException(nameof(lhs), $"Matrix dimensions unequal ({L}, {R})");
            }
            else {
                Matrix outputMatrix = new Matrix(lhs.Size);
                for (int i = 0; i < lhs.Size; i++) {
                    for (int j = 0; j < lhs.Size; j++)
                        outputMatrix[i, j] = lhs[i, j] - lhs[i, j];
                }
                return outputMatrix;
            }
        }

        /// <summary>
        /// Operator overload which performs scalar multiplication.
        /// </summary>
        /// <param name="lhs">The scalar to multiply by</param>
        /// <param name="rhs">The matrix to be multiplied</param>
        /// <returns>A matrix B = k * A</returns>
        public static Matrix operator *(decimal lhs, Matrix rhs)
        {
            Matrix outputMatrix = new Matrix(rhs.Size);
            for (int i = 0; i < rhs.Size; i++) {
                for (int j = 0; j < rhs.Size; j++)
                    outputMatrix[i, j] = lhs * rhs[i, j];
            }
            return outputMatrix;
        }

        /// <summary>
        /// Performs matrix multiplication of two square matrices. Fails if matrix dimensions are not equal.
        /// </summary>
        /// <param name="lhs">The left-hand matrix</param>
        /// <param name="rhs">The right-hand matrix</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the two matrices differ in dimension</exception>
        /// <returns>A matrix C = A * B</returns>
        public static Matrix operator *(Matrix lhs, Matrix rhs)
        {
            if (lhs.Size != rhs.Size) {
                int L = lhs.Size, R = rhs.Size;
                throw new ArgumentOutOfRangeException(nameof(lhs), $"Matrix dimensions unequal ({L}, {R})");
            }
            else {
                Matrix outputMatrix = new Matrix(lhs.Size);
                for (int i = 0; i < lhs.Size; i++) {
                    for (int j = 0; j < lhs.Size; j++) {
                        for (int k = 0; k < lhs.Size; k++)
                            outputMatrix[i, j] += (lhs[i, k] * rhs[k, j]);
                    }
                }
                return outputMatrix;
            }
        }

        #endregion

        #region Additional Product Operations

        public Matrix Hadamard(Matrix rhs)
        {
            if (rhs.Size != this.Size)
                throw new ArgumentOutOfRangeException($"Matrix dimensions ({this.Size},{rhs.Size}) incompatible");
            else {
                Matrix output = new Matrix(N: this.Size);
                for (int i = 0; i < this.Size; i++) {
                    for (int j = 0; j < this.Size; j++)
                        output[i, j] = matrix[i, j] * rhs[i, j];
                }
                return output;
            }
        }

        public Matrix Kronecker(Matrix rhs)
        { 
            // Blah        
        }

        #endregion

        #region Properties

        public bool IsInvertible()
        {

        }

        public bool IsIdentity()
        {

        }

        public bool IsUpperTriangular()
        {

        }

        public bool IsLowerTriangular()
        {

        }

        #endregion 
    }
}
