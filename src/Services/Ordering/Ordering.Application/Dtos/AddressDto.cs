﻿

namespace Ordering.Application.Dtos
{
    public record AddressDto(string FirstName,string LastName,
        string EmailAddress,string AddressLine,string Country,string City,string State,string ZipCode);
}
