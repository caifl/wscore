using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace WSCore
{
    class OutputStream : Stream
    {
        private readonly HttpResponse response;
        private readonly Stream stream;

        public OutputStream(HttpResponse response)
        {
            this.response = response;
            this.stream = response.Body;
        }

        public override bool CanRead => this.stream.CanRead;

        public override bool CanSeek => this.stream.CanSeek;

        public override bool CanWrite => this.stream.CanWrite;

        public override long Length => this.stream.Length;

        public override long Position
        {
            get => this.stream.Position;
            set => this.stream.Position = value;
        }

        public override void Flush()
        {
            this.stream.FlushAsync().GetAwaiter().GetResult();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var text = Encoding.UTF8.GetString(buffer, offset, count);
            this.response.WriteAsync(text)
                .GetAwaiter().GetResult();
        }
    }
}
