using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Claims; 


// private static var credentials = new NetworkCredential("RIMIRFC", "G%Mp]B~U[G,b8");
private static var credentials = new NetworkCredential("BKFLRFC", "(bxzHz+R]VhxGlwibC2Hm2icUZ{+Be4(h#sieYQz");

private static var handler = new HttpClientHandler { Credentials = credentials };
private static HttpClient httpClient = new HttpClient(handler);



public static async Task<HttpResponseMessage> Run(
    HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    // parse query parameter
    string token = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "token", true) == 0)
        .Value;

    string url = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "url", true) == 0)
        .Value;


    if (token == null)
    {
        // Get request body
        dynamic data = await req.Content.ReadAsAsync<object>();
        token = data?.token;
    }

    if (url == null)
    {
        // Get request body
        dynamic data = await req.Content.ReadAsAsync<object>();
        url = data?.url;
    }

    //req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    log.Verbose(req.ToString());



    string resource = "http://AODEVGW01.waterone.org:8000/sap/opu/odata/sap/";
    string defaultURL = "Z_DIST_PROJECTS_SRV/DIST_PROJECT_ETSet";

    if (url != null)
        resource = resource + url;
    else
        resource = resource + defaultURL;

    log.Info("WaterOne HTTP trigger function processed a request.");


    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, resource);  
    //requestMessage.Headers.Add("X-CSRF-Token", token);  

    // Add our custom content type
    //requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("MimeType");


    try {
        httpClient.DefaultRequestHeaders.Add("Accept", "application/atom+xml,application/atomsvc+xml,application/xml");
        httpClient.DefaultRequestHeaders.Add("X-CSRF-Token", "Fetch");

        var response = await httpClient.GetAsync(resource);
        response.EnsureSuccessStatusCode();


        string headers = response.Headers.ToString();
        log.Verbose(headers.ToString());


        string content = await response.Content.ReadAsStringAsync();
        //return req.CreateResponse(HttpStatusCode.OK, " Response: " + content);

        return req.CreateResponse(HttpStatusCode.OK, content);
    }
    catch (Exception ex) {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Error: " + ex.Message); 
    }

        
    //return token == null
    //    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a token on the query string or in the request body")
    //   : req.CreateResponse(HttpStatusCode.OK, "token: " + token);
}



