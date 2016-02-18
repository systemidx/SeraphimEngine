using System;

namespace SeraphimEngine.Definitions
{
    /// <summary>
    /// Class Matrix3.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Matrix3<T>
    {
        #region Variables
        
        /// <summary>
        /// The matrix
        /// </summary>
        private readonly T[,] _matrix = new T[3 , 3];

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3{T}"/> class.
        /// </summary>
        public Matrix3()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3{T}"/> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public Matrix3(T[,] matrix)
        {
            _matrix = matrix;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3{T}"/> class.
        /// </summary>
        /// <param name="firstRow">The first row.</param>
        /// <param name="secondRow">The second row.</param>
        /// <param name="thirdRow">The third row.</param>
        public Matrix3(T[] firstRow, T[] secondRow, T[] thirdRow)
        {
            for (int i = 0; i < 3; ++i)
                _matrix[0, i] = firstRow[i];
            for (int i = 0; i < 3; ++i)
                _matrix[1, i] = secondRow[i];
            for (int i = 0; i < 3; ++i)
                _matrix[2, i] = thirdRow[i];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3{T}"/> class.
        /// </summary>
        /// <param name="m1">The m1.</param>
        /// <param name="m2">The m2.</param>
        /// <param name="m3">The m3.</param>
        /// <param name="m4">The m4.</param>
        /// <param name="m5">The m5.</param>
        /// <param name="m6">The m6.</param>
        /// <param name="m7">The m7.</param>
        /// <param name="m8">The m8.</param>
        /// <param name="m9">The m9.</param>
        public Matrix3(T m1, T m2, T m3, T m4, T m5, T m6, T m7, T m8, T m9)
        {
            _matrix[0, 0] = m1;
            _matrix[0, 1] = m2;
            _matrix[0, 2] = m3;
            _matrix[1, 0] = m4;
            _matrix[1, 1] = m5;
            _matrix[1, 2] = m6;
            _matrix[2, 0] = m7;
            _matrix[2, 1] = m8;
            _matrix[3, 2] = m9;
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>T.</returns>
        /// <exception cref="System.IndexOutOfRangeException">
        /// </exception>
        public T this[int row, int column]
        {
            get
            {
                if ((row < 3) && (column < 3))
                    return _matrix[row, column];
                throw new IndexOutOfRangeException();
            }
            set
            {
                if ((row < 3) && (column < 3))
                    _matrix[row, column] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
        #endregion
    }
}