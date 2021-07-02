# Result class examples

Examples for how the Result class can be used.

## Result with custom error
When returning a failed result, sometimes it is useful to use a custom error object.
One use case for this is returning the object instance that caused the failure, particularly if it is a List of results being returned.

The `Result` class can also have the signature `Result<T, E>` where `T` is the result value and `E` is a custom error type.
An example would be if there were two entities, `Vehicle` and `Ferry`, and a Ferry can have Vehicles added to it for a voyage.
But if the Vehicle is too large, or the Ferry is at capacity, then it might be useful to get the Vehicle returned as part of the result.
`Result<Vehicle, VehicleBoardingError>`

```C#
public class Vehicle
{
    public double Height { get; set; }
}

public class Ferry
{
    private double vehicleHeightLimit = 3.5;
    private int vehicleLimit = 100;

    public List<Vehicle> BoardedVehicles { get; set; } = new();
    
    public Result<Vehicle, VehicleBoardingError> BoardVehicle(Vehicle vehicle)
    {
        if (vehicle.Height > vehicleHeightLimit)
        {
            return new VehicleBoardingError("This vehicle is over the height limit", vehicle);
        }
        
        if (BoardedVehicles.Count >= 100)
        {
            return new VehicleBoardingError("Ferry is already at capacity", vehicle);
        }
        
        BoardedVehicles.Add(vehicle);
        
        return vehicle;
    }
    
    public List<Result<Vehicle, VehicleBoardingError>> BoardVehicles(List<Vehicle> vehicles)
    {
        var results = new List<Result<Vehicle, VehicleBoardingError>>();
    
        foreach (var vehicle in vehicles)
        {
            var result = BoardVehicle(vehicle);
        
            results.Add(result);
        }
        
        return results;
    }
}

public class VehicleBoardingError
{
    public class VehicleBoardingError(string message, Vehicle vehicle)
    {
        Message = message;
        Vehicle = vehicle;
    }
    
    public string Message { get; }
    
    public Vehicle Vehicle { get; }
}
```

> Note:
> If using the CQS pattern, this code would have a different structure.
