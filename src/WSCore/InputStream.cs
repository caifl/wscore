using System.IO;
using Microsoft.AspNetCore.Http;

namespace WSCore
{
    class InputStream : Stream
    {
        private readonly HttpRequest request;
        private readonly Stream stream;
        private readonly long contentLength = 0;

        public InputStream(HttpRequest request)
        {
            this.request = request;
            this.contentLength = request.ContentLength ?? 0;
            this.stream = new BufferedStream( request.Body );
            //await context.Request.Body.CopyToAsync(this.stream);
            //request.Body;
        }

        public override bool CanRead => this.stream.CanRead;

        public override bool CanSeek => true;//this.stream.CanSeek;

        public override bool CanWrite => false; //this.stream.CanWrite;

        public override long Length => this.contentLength;

        public override long Position
        {
            get => this.stream.Position;
            set => this.stream.Position = value;
        }

        public override void Flush()
        {
            this.stream.Flush();
            //this.stream.FlushAsync().GetAwaiter().GetResult();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.stream.Read(buffer, offset, count);
            //return this.request.Body.ReadAsync(buffer, offset, count)
            //    .GetAwaiter().GetResult();
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
            //var text = Encoding.UTF8.GetString(buffer, offset, count);
            this.request.Body.WriteAsync(buffer, offset, count)
                .GetAwaiter().GetResult();
        }
    }
}
