using System.Net;

public static async Task<HttpResponseMessage> Run(
    HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    // parse query parameter
    string testValue = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "testValue", true) == 0)
        .Value;

    if (testValue == null)
    {
        // Get request body
        dynamic data = await req.Content.ReadAsAsync<object>();
        testValue = data?.testValue;
    }

    return testValue == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a testValue on the query string or in the request body")
        : req.CreateResponse(HttpStatusCode.OK, "Hello " + testValue);
}
