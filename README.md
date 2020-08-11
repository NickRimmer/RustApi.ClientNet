![Validation build](https://github.com/NickRimmer/RustApi.ClientNet/workflows/Validation%20build/badge.svg?branch=master) ![NuGet package](https://buildstats.info/nuget/RustApi.ClientNet)

# RustApi.ClientNet
This is library for connection to Rust game server with [RustApi extension](https://github.com/NickRimmer/RustApi).

It is quite simple one (=

## How to use
```C#
// define connection options
var options = new ConnectionOptions("http://localhost:28017", "admin", "secret1");

// create connection instance
IConnection connection = new Connection(options);
```

Then you can use this `connection` **to call hooks**:

```C#
// name of hook
var name = "TestHook";

// parameters
var data = new Dictionary<string, object>
{
    {"name", "MyNameIs"},
    {"value", 1},
};

var result1 = await connection.CallHookAsync<ExpectedResultType>(name, data);
// var result1 = await connection.CallHookAsync<ExpectedResultType>(name);
// await connection.CallHookAsync(name, data);
// await connection.CallHookAsync(name);
```

Or you can execute **Api commands**:
```C#
// name of command
var name = "test_arguments_3";

// parameters
var data = new Dictionary<string, object>
{
    {"p1", "1"},
    {"p2", 1},
    {"p3", true},
};

var result1 = await connection.SendCommandAsync<string>(name, data);
// var result1 = await connection.SendCommandAsync<string>(name);
// await connection.SendCommandAsync(name, data);
// await connection.SendCommandAsync(name);
```

Or just check if server **is online**:
```C#
var result = await connection.SystemPing();
if (result != "Pong"){
    // oops
}
```
