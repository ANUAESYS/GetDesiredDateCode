public static class Function1
{
    [FunctionName("SayHello1")]
    public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
    {
        log.Info("C# HTTP trigger function processed a request.");

        // parse query parameter
        string name = req.GetQueryNameValuePairs()
            .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
            .Value;

        if (name == null)
        {
            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();
            name = data?.name;
        }

        return name == null
            ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
            : req.CreateResponse(HttpStatusCode.OK, "Hello Hey There !! I am your host. Welcome " + name);
    }
}