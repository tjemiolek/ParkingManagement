# ParkingManagement

## Specification:
ParkingManager API is a Web API application based on .NET 9. As a database I've used SQL Server and EF Core ORM.


## How to run locally:
Provide a value for 'LocalDb' with connection string. After that - update local database. Application contains a seeder for Parking Spaces (part of initial migration).


## Main assumptions:
- only 1 reservation can be active for a vehicle,
- unable to make a reservations in advance,
- prices are fixed,
- every minute that the car spend on a parking space is rounded up,
- full 5 minutes must pass in order to additional fee to be charged
