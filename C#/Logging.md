# Logging

## Injection vs Static reference

There doesn't appear to be a clear cut approach to this. One recommendation is to inject the Logger where it is explicitly used,
otherwise use the static reference. More research on this matter is needed.

One thing to consider is how logging will be handled when unit testing. For explicit logging, then you will likely provide your own logger (such as a mock) and see that results are going to it. For static logging, you would likely need to disable or throw away any logging that occurs. (Currently unsure how this is best achieved for libraries such as Serilog).

See Also:
- https://stackoverflow.com/a/50716248/1926027

## Serilog

 - https://serilog.net/
 - https://github.com/serilog/serilog
 - https://nblumhardt.com/2019/10/serilog-in-aspnetcore-3/
 - https://www.codewithmukesh.com/blog/serilog-in-aspnet-core-3-1/
