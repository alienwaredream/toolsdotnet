using System;
using System.IO;

namespace Tools.Logging
{
    /// <summary>
    /// A skeleton filter that overrides Stream properly.
    /// </summary>
    public abstract class HttpFilter : Stream
    {
        private readonly Stream baseStream;
        private bool closed;

        /// <summary>
        /// Base class constructor.  Base class holds the underlying stream.
        /// </summary>
        /// <param name="baseStream">The stream to write to after filtering.</param>
        protected HttpFilter(Stream baseStream)
        {
            this.baseStream = baseStream;
            closed = false;
        }

        /// <summary>
        /// The stream to write to after filtering
        /// </summary>
        protected Stream BaseStream
        {
            get { return baseStream; }
        }

        /// <summary>
        /// This is an output stream.  We cannot read from it
        /// </summary>
        public override bool CanRead
        {
            get { return false; }
        }

        /// <summary>
        /// We can certainly write to this stream.
        /// </summary>
        public override bool CanWrite
        {
            get { return !closed; }
        }

        /// <summary>
        /// We cannot seek in a filter.
        /// </summary>
        public override bool CanSeek
        {
            get { return false; }
        }

        /// <summary>
        /// Indicates if the Stream is closed or open
        /// </summary>
        /// <remarks>
        /// Implmentors should always check Closed before
        /// Writing anything to the BaseStream.</remarks>
        protected bool Closed
        {
            get { return closed; }
        }

        /// <summary>
        /// This method is not supported for an HttpFilter
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// This method is not supported for an HttpFilter
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// A basic Close implementation.
        /// </summary>
        /// <remarks>
        /// If an inheriting class overrides Close, 
        /// they should always call base.Close as part
        /// of the implementation.</remarks>
        public override void Close()
        {
            closed = true;
            baseStream.Close();
        }

        /// <summary>
        /// Flushes the BaseStream
        /// </summary>
        public override void Flush()
        {
            baseStream.Flush();
        }

        /// <summary>
        /// This method is not supported for an HttpFilter
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This method is not supported for an HttpFilter
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This method is not supported for an HttpFilter
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown</exception>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
    }
}