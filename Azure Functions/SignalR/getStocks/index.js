module.exports = async function (context, req, stocks) {
    //context.res.body = stocks;
    
    context.res = {
        body: stocks,
         headers: {   
          'Access-Control-Allow-Credentials': 'true',
          'Access-Control-Allow-Origin': 'http://localhost:8080',
          'Access-Control-Allow-Methods': 'GET',
          'Access-Control-Request-Headers': 'X-Custom-Header'
         }
      }

};