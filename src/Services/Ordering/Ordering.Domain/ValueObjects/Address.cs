﻿

namespace Ordering.Domain.ValueObjects
{
    public record Address 
    {
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string EmailAddress { get; private set; } = default!;
        public string AddressLine { get; private set; } = default!;
        public string Country { get; private set; } = default!;
        public string City { get; private set; } = default!;
        public string State { get; private set; } = default!;
        public string ZipCode { get; private set; } = default!;

        protected Address()
        {
            
        }

        private Address(
            string firstName,
            string lastName,
            string emailAddress,
            string addressLine,
            string country,
            string city,
            string state,
            string zipCode
            )
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            City = city;
            State = state;
            ZipCode = zipCode;

        }

        public static Address Of(
            string firstName,
            string lastName,
            string emailAddress,
            string addressLine,
            string country,
            string city,
            string state,
            string zipCode
            )
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress, nameof(emailAddress));
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine, nameof(addressLine));

            return new Address(
             firstName,
             lastName,
             emailAddress,
             addressLine,
             country,
             city,
             state,
             zipCode
            );
        }
    }
}
