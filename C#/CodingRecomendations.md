# Recommended Coding Practices

## Using `AsReadOnly()` when casting list to `IReadOnlyList()`
https://enterprisecraftsmanship.com/posts/ef-core-vs-nhibernate-ddd-perspective/
See the comments on this page. There is a recommendation that when using `IReadOnly` instead of doing this:
```C#
public class Student : Entity
{
    private readonly List<Enrollment> _enrollments = new List<Enrollment>();
    public virtual IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();
    
    public void AddEnrollment(Course course, Grade grade)
    {
        var enrollment = new Enrollment
        {
            Course = course,
            Student = this,
            Grade = grade
        };
        _enrollments.Add(enrollment);
    }
}
```

it should instead be:

```C#
public virtual IReadOnlyList<enrollment> Enrollments { get; }

public Student()
{
  Enrollments = _enrollments.AsReadOnly();
}
```
The problem with the first approach is that it creates a new list every time the `Enrollments` property is accessed. While unlikely to be an issue, it also means that if `Enrollments` is called twice, the two items do not have reference equality.



http://disq.us/p/1typ1xn
