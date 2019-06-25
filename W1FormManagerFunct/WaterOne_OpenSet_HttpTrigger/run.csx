using System.Net;
using System.Net.Http;


// private static var credentials = new NetworkCredential("RIMIRFC", "G%Mp]B~U[G,b8");
private static var credentials = new NetworkCredential("BKFLRFC", "(bxzHz+R]VhxGlwibC2Hm2icUZ{+Be4(h#sieYQz");

private static var handler = new HttpClientHandler { Credentials = credentials };
private static HttpClient httpClient = new HttpClient(handler);

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, string path, TraceWriter log)
{


    path = "Z_DIST_PROJECTS_SRV/DIST_PROJECT_ETSet?$format=json";

    string resource = "http://AODEVGW01.waterone.org:8000/sap/opu/odata/sap/";
    resource = resource + path;


    log.Info("WaterOne HTTP trigger function processed a request.");

    try {
        var response = await httpClient.GetAsync(resource);
       
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        //return req.CreateResponse(HttpStatusCode.OK, " Response: " + content);

        return req.CreateResponse(HttpStatusCode.OK, content);
    }
    catch (Exception ex) {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Error: " + ex.Message); 
    }

    


}
