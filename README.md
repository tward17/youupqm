# You Up?
Simple tool to check computers/websites are up and running

Add the nodes you want to check to json config file, with the type of check you want to perform (ping or http).

Use the webapi endpoint to perform the check.

Example config:
```
  [
    {
      "Id": 0,
      "Name": "Node to Check One",
      "DestinationAddress": "192.168.0.1",
      "CheckType": "Ping",
      "CheckTimeout": 1000,
      "CheckAttempts": 4,
      "Enabled": true
    },
    {
      "Id": 1,
      "Name": "Node to Check Two",
      "DestinationAddress": "https://www.google.com",
      "CheckType": "Http",
      "CheckTimeout": 1000,
      "CheckAttempts": 4,
      "Enabled": true
    }
  ]
```
