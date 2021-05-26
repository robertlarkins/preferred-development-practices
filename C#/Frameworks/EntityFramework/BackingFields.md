# Backing Fields

## Populate Entity Backing Field
When an entity has a backing field, the backing field is not populated by EF Core when LazyLoading is used.
So having a call like this will leave appointments empty `context.Schedules.Find(id)`.
Therefore, if `RemoveAppointment` is called, the appointments list will not contain anything,
and the Remove will return false, even if that appointment is associated with that schedule in the database.

```C#
public class Schedule : Entity<int>
{
    private readonly List<Appointment> appointments = new();
    
    public virtual IReadOnlyList<Appointment> Appointments => appointments;
    
    public int NumberOfAppointments => Appointments.Count;
    
    public void RemoveAppointment(Appointment appointment)
    {
        appointments.Remove(appointment);
    }
}
```

Calling the `NumberOfAppointments` property will work, as it uses the public virtual property Appointments which triggers
EF Core to populate the backingfield. Calling `RemoveAppointment` after this would then work (need to confirm this).

To prepopulate the backing fields the get Schedule code should be changed to
```C#
public Schedule? GetById(int scheduleId)
{
    var schedule = context.Schedules.Find(scheduleId);
    
    if (schedule is null)
    {
        return null;
    }
    
    context.Entry(schedule).Collection(x => x.Appointments).Load();
}
```

in other cases where `.SingleOrDefault` or equivalent is used instead of `.Find`, then the following can be done:
```C#
public Schedule? GetById(int scheduleId)
{
    return context.Schedules
        .Include(x => x.Appointments)
        .SingleOrDefault(x => x.Id == scheduleId);
}
```

See: https://github.com/vkhorikov/DddAndEFCore/issues/3
