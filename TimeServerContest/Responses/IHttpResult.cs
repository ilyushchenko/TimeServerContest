namespace TimeServerContest.Responses
{
    public interface IHttpResult
    {
        byte[] Content { get; set; }
        long ContentLength { get; }
        string ContentType {get;set;}
        int StatusCode { get; set; }
    }
}