![Validation build](https://github.com/NickRimmer/RustApi.ClientNet/workflows/Validation%20build/badge.svg?branch=master)

# RustApi.ClientNet
This is library for RustApiClient to Rust game server with [RustApi extension](https://github.com/NickRimmer/RustApi).

It is quite simple one (=

## How to use
```C#
// define RustApiClient options
var options = new RustApiClientOptions("http://localhost:28017", "admin", "secret1");

// create RustApiClient instance
IRustApiClient rustApiClient = new RustApiClient(options);
```

Then you can use this `rustApiClient` **to call hooks**:

```C#
// name of hook
var name = "TestHook";

// parameters
var data = new Dictionary<string, object>
{
    {"name", "MyNameIs"},
    {"value", 1},
};

var result1 = await rustApiClient.CallHookAsync<ExpectedResultType>(name, data);
// var result1 = await rustApiClient.CallHookAsync<ExpectedResultType>(name);
// await rustApiClient.CallHookAsync(name, data);
// await rustApiClient.CallHookAsync(name);
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

var result1 = await rustApiClient.SendCommandAsync<string>(name, data);
// var result1 = await rustApiClient.SendCommandAsync<string>(name);
// await rustApiClient.SendCommandAsync(name, data);
// await rustApiClient.SendCommandAsync(name);
```

Or just check if server **is online**:
```C#
var result = await rustApiClient.SystemPing();
if (result != "Pong"){
    // oops
}
```
