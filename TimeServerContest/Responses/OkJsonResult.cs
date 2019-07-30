using Newtonsoft.Json;
using System;
using System.Text;

namespace TimeServerContest.Responses
{
    class OkJsonResult : BaseResult
    {
        public OkJsonResult(object toSerialize)
        {
            if (toSerialize is null)
            {
                throw new ArgumentNullException(nameof(toSerialize));
            }

            var responseStr = JsonConvert.SerializeObject(toSerialize);
            Content = Encoding.UTF8.GetBytes(responseStr);
            ContentType = "application/json";
        }
    }
}
