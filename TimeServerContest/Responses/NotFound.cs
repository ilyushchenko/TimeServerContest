using System.Text;

namespace TimeServerContest.Responses
{
    class NotFoundResult : BaseResult
    {
        public NotFoundResult()
        {
            StatusCode = 404;
            Content = Encoding.UTF8.GetBytes("Route not found");
        }
    }
}
