var http = require('http');
var https = require('https');

function httpsGet(url, handler, errorHandler) {
  https.get(
    url,
    function(response){
      console.log('Got response, receiving body by chunks.');
      response.setEncoding('utf8');
      var data = '';
      response.on(
        'data',
        function (chunk) {
          data += chunk;
        }
      );
      response.on(
        'end',
        function() {
          console.log('Body received, calling handler.');
          if (200 <= response.statusCode && response.statusCode < 400)
            return handler(data);
          return errorHandler(data);
        }
      );
    }
  ).on('error', errorHandler);
}

var server = http.createServer(
  function (req, res) {  
    console.log('Has request!');
    var baseAddress = 'https://hacker-news.firebaseio.com';
    function errorHandler(e) {
      console.log('Error:'+e);
      return res.end('Service is not available!');
    }
    httpsGet(
      baseAddress+'/v0/topstories.json?print=pretty',
      function(data) {
        console.log(data);
        var arr = JSON.parse(data);
        var responseId = arr[0];
        var itemUrl = '/v0/item/'+ responseId +'.json?print=pretty';
        httpsGet(
          baseAddress + itemUrl,
          function(data) {
            var obj = JSON.parse(data);
            var title = obj['title'];
            res.end(title);
          },
          errorHandler
        );
      },
      errorHandler
    );
  }
);

var port = 5001;
server.listen(port);
console.log('Server running at http://localhost:'+port);