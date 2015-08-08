var http = require('http');

var request = require('superagent');
var co = require('co');

var server = http.createServer(
  function (req, res) {  
    console.log('Has request for '+req.url+'!');    
    if (req.url !== '/')
      return res.end();
 
    co(function *() {
      var baseAddress = 'https://hacker-news.firebaseio.com';
      var topUrl = baseAddress+'/v0/topstories.json?print=pretty';
      var httpRes = yield request.get(topUrl);
      console.log('Got first response.');
      if (httpRes.ok) {
        var arr = httpRes.body;
        var responseId = arr[0];
        var itemUrl = '/v0/item/'+ responseId +'.json?print=pretty';
        var httpRes = yield request.get(baseAddress + itemUrl);
        console.log('Got second response.');
        if (httpRes.ok) {
          var obj = httpRes.body;
          var title = obj['title'];
          return res.end(title);
        }
      }
    }).catch(
      function (err) {
        console.log('Error:'+e);
        return res.end('Service is not available!');
      }
    );
  }
);

var port = 5001;
server.listen(port);
console.log('Server running at http://localhost:'+port);