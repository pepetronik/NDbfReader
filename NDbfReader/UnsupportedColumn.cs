﻿using System;
using System.Diagnostics;
using System.Text;

namespace NDbfReader
{
    /// <summary>
    /// Represents an unsupported column. 
    /// </summary>
    [DebuggerDisplay("Unsupported {NativeTypeHex} {Name}")]
    public sealed class UnsupportedColumn : Column<byte[]>
    {
        /// <summary>
        /// Initializes a new instance with the specified name and offset.
        /// </summary>
        /// <param name="name">The column name.</param>
        /// <param name="offset">The column offset in a row in bytes.</param>
        /// <param name="size">The column size in bytes.</param>
        /// <param name="nativeType">The column's native type code.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c> or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> is &lt; 0.</exception>
        public UnsupportedColumn(string name, int offset, int size, byte nativeType) : base(name, offset, size)
        {
            NativeType = nativeType;
        }

        /// <summary>
        /// Loads a value from the specified buffer.
        /// </summary>
        /// <param name="buffer">The byte array from which a value should be loaded.</param>
        /// <param name="offset">The byte offset in <paramref name="buffer"/> at which loading begins. </param>
        /// <param name="encoding">The encoding that should be used when loading a value. The encoding is never <c>null</c>.</param>
        /// <returns>A column value.</returns>
        protected override byte[] DoLoad(byte[] buffer, int offset, Encoding encoding)
        {
            var result = new byte[Size];
            Array.Copy(buffer, offset, result, 0, result.Length);

            return result;
        }

        /// <summary>
        /// Gets the native type code of column value.
        /// </summary>
        public byte NativeType { get; }

        private string NativeTypeHex => $"0x{NativeType:X2}";
    }
}