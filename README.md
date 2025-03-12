### AutoMapper in C#

AutoMapper is a convention-based object-object mapper in .NET. It automates the process of mapping properties from one object to another, reducing boilerplate code and improving maintainability.

#### Why use AutoMapper?

Reduces boilerplate code: Manually mapping properties between objects can be tedious and error-prone, especially with complex objects. AutoMapper handles this automatically.
Improves code readability: Mapping logic is centralized and easily understood.
Simplifies refactoring: Changes to object structures are easier to manage, as AutoMapper handles the mapping logic.
Supports complex mappings: AutoMapper can handle nested objects, collections, and custom mapping logic.

Example:

Let's say we have two classes: Source and Destination. We want to map properties from Source to Destination.

```C#

using AutoMapper;
using System;
using System.Collections.Generic;

public class Source
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Address SourceAddress {get; set;}
}

public class Destination
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string FormattedBirthDate { get; set; }
    public Address DestinationAddress {get; set;}

}

public class Address{
    public string Street {get; set;}
    public string City {get; set;}
    public string ZipCode {get; set;}
}

public class Program
{
    public static void Main(string[] args)
    {
        // 1. Create a MapperConfiguration
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Source, Destination>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.FormattedBirthDate, opt => opt.MapFrom(src => src.BirthDate.ToString("yyyy-MM-dd")));
            cfg.CreateMap<Address, Address>(); //map address objects
        });

        // 2. Create an IMapper instance
        IMapper mapper = config.CreateMapper();

        // 3. Create a Source object
        var source = new Source
        {
            Id = 123,
            Name = "John Doe",
            BirthDate = new DateTime(1990, 1, 1),
            SourceAddress = new Address{
                Street = "123 Main St",
                City = "Anytown",
                ZipCode = "12345"
            }
        };

        // 4. Map the Source object to a Destination object
        var destination = mapper.Map<Destination>(source);

        // 5. Output the mapped properties
        Console.WriteLine($"UserId: {destination.UserId}");
        Console.WriteLine($"FullName: {destination.FullName}");
        Console.WriteLine($"FormattedBirthDate: {destination.FormattedBirthDate}");
        Console.WriteLine($"Address: {destination.DestinationAddress.Street}, {destination.DestinationAddress.City}, {destination.DestinationAddress.ZipCode}");

    }
}
```

### Explanation:

MapperConfiguration:

We create a MapperConfiguration object, which defines the mapping rules.
cfg.CreateMap<Source, Destination>() creates a mapping between the Source and Destination classes.
.ForMember() allows you to customize individual property mappings. In this case:
dest.UserId is mapped from src.Id.
dest.FullName is mapped from src.Name.
dest.FormattedBirthDate is mapped from src.BirthDate, and we format it as a string.
cfg.CreateMap<Address, Address>(); maps the address objects, so nested objects are correctly mapped.
IMapper:

We create an IMapper instance from the MapperConfiguration. This is the object that performs the actual mapping.
Source object:

We create an instance of the Source class with sample data.
mapper.Map<Destination>(source):

We use the mapper.Map<Destination>(source) method to map the Source object to a Destination object.
Output:

We output the mapped properties to the console.
Installation:

You can install AutoMapper using NuGet Package Manager:

```Bash
Install-Package AutoMapper
```
This example demonstrates basic mapping. AutoMapper supports many advanced features, including:

Custom type converters.
Value resolvers.
Conditional mapping.
Reverse mapping.
Profiles.
By using AutoMapper, you can significantly reduce the amount of code required for object-object mapping, making your code cleaner, more maintainable, and less prone to errors.
