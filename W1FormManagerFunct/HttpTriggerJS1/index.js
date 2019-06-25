


module.exports = function (context, req) {
    

    context.log('JavaScript HTTP trigger function processed a request.');

    if (req.query.name || (req.body && req.body.name)) {
        context.res = {
            // status: 200, /* Defaults to 200 */
            body: "Hello " + (req.query.name || req.body.name)
        };
    }
    else {
        context.res = {
            status: 400,
            body: "Please pass a name on the query string or in the request body"
        };
    }

    const optionsGetCSRFToken = {
        method: "GET",
        url: "http://AODEVGW01.waterone.org:8000/sap/opu/odata/sap/" + "/ZCC_BACKFLOW_SRV/TestNotificationSet('16189256')",
        simple: false,                                  // Don't handle 405 as an error
        resolveWithFullResponse: true,                  // Read headers and not only body
        auth: {
            user: "BKFLRFC", 
            password: "(bxzHz+R]VhxGlwibC2Hm2icUZ{+Be4(h#sieYQz"
        },
        headers: {
            'X-CSRF-Token': 'fetch'
        }
    };

    rp = require('request-promise').defaults({ jar: true });

    context.log(rp(optionsGetCSRFToken));

    context.done();
};