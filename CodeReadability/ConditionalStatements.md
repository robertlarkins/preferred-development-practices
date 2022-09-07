# Conditional Statements

## Favour positive conditions

When writing condtional statements to compare two values, you can use equals (`==`) or not equals (`!=`).
For example if we have an `if-else` for `vehicle.Type` is `VehicleType.Sedan`, it can be written as 

```csharp
if (vehicle.Type != VehicleType.Sedan)
{
}
else
{
}
```
but it has less cognitive load to write it (or invert the if statement) as

```csharp
if (vehicle.Type == VehicleType.Sedan)
{
}
else
{
}
```
If the if statement exists without the else, then it might be more suitable to keep the negative conditional.

Another example is:
```
if (loggedIn)
{
}
```
is easier to read and understand more quickly than
```
if (!isNotLoggedIn)
{
}
```

See:
- Clean Code - Chapter 17 Smells and Heuristics: G29
