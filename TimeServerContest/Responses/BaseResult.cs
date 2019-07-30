using System;

namespace TimeServerContest.Responses
{
    class BaseResult : IHttpResult
    {
        public BaseResult()
        {
            Content = new byte[0];
        }

        public BaseResult(byte[] content)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public byte[] Content { get; set; }

        public long ContentLength => Content.Length;

        public string ContentType { get; set; } = "text/plain";
        public int StatusCode { get; set; } = 200;
    }
}
