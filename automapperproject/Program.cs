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
