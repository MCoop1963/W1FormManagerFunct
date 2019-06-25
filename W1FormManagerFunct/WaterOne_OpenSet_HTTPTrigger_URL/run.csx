using System.Net;
using System.Net.Http;


// private static var credentials = new NetworkCredential("RIMIRFC", "G%Mp]B~U[G,b8");
private static var credentials = new NetworkCredential("BKFLRFC", "(bxzHz+R]VhxGlwibC2Hm2icUZ{+Be4(h#sieYQz");


private static var handler = new HttpClientHandler { Credentials = credentials };
private static HttpClient httpClient = new HttpClient(handler);

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    try
    {
        string resource = "https://AODEVGW01.waterone.org:44300/sap/opu/odata/sap/";

        // parse query parameter
        string url = req.GetQueryNameValuePairs()
                        .Single(q => string.Compare(q.Key, "url", true) == 0)
                        .Value;

        if (req.Method == HttpMethod.Put)
        {
            if (url == null)
            {
                throw new Exception("No url set");
            }

            req.Headers.Remove("Host");
            req.RequestUri = new Uri(resource + url);
            return (await httpClient.SendAsync(req)).EnsureSuccessStatusCode();
        }

        if (url == null)
        {
            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();
            url = data?.url;
        }




        string defaultURL = "Z_DIST_PROJECTS_SRV/DIST_PROJECT_ETSet";

        if (url != null)
            resource = resource + url;
        else
            resource = resource + defaultURL;


        log.Info("WaterOne HTTP trigger function processed a request.");

        var response = await httpClient.GetAsync(resource);

        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        //return req.CreateResponse(HttpStatusCode.OK, " Response: " + content);

        return req.CreateResponse(HttpStatusCode.OK, content);
    }
    catch (Exception ex)
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Error: " + ex.Message);
    }




}
