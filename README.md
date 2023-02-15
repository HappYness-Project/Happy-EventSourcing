Eventsourcing implementation for managing Human being's life. 

# Eventsourcing Happiness project impelmentation
* CQRS implementation.
* Evcent sourcing implement
* Storing Event data in the Event Store db.
* Build the Todo Service - named as HP project for now. 
* Build the Finance service
* Build the Broker Service(TBD, future objective) 


## Technologies Used:
* Asp.net core Dotnet 7
* AutoMapper
* MediatR
* Kafka Producer/Consumer
* MongoDB


## Architecture Implemented:
* Full architecture with responsibility separation concerns, SOLID and Clean Code.
* Domain Driven Design (Layers and Domain Model Pattern)
* CQRS 
* Event Sourcing
* Mediator Design Pattern - Multiple objects to communicate with each other without knowing each other's structure
* Repository Layer for ReadDb.
* Persistence Layer for WriteDb.
* pub/sub architecture

# How to use:
* You will need the latest Visual Studio 2022 and the latest .NET Core SDK.
* Docker setup for Kafka, MongoDB and Postgres.


# Explanation of the Event Sourcing / CQRS pattern. 

Event sourcing is a way of capturing/storing an application's state through the history of events that have happened already. It provides a single source of truth about what occured in the whole application.


## CQRS (Command and Query Responsibility Segregation)
- A pattern that separates read and update operations of a data source.
* Query - Querying data from a source (READ)
* Command - Insert/update/Delete
Commands should be task-based, rather than data centric.

## Event Store Implementation
- Updating...

### Write Model and Persistence
- Updating...

### Read Model and Projection
- Updating...

### Snapshot
Hasn't been updated yet. 


## Solution
CQRS separates reads and writes into different models, using commands to update data, and queries to read data.

Commands should be task-based, rather than data centric. ("Book hotel room", not "set ReservationStatus to Reserved").
Commands may be placed on a queue for asynchronous processing, rather than being processed synchronously.
Queries never modify the database. A query returns a DTO that does not encapsulate any domain knowledge.
Most of the complex business logic goes into the write model, which is **Commands**. In terms of read model, the application is able to avoid complex joins or complex ORM mappings by storing a materialized view in the read database. 

### Disadvantages
* Inability to scaffold models using ORM tools and set of additional classes required to segregate data models and read/write operations.    
* The application can be extremely complex depends on how it's designed. 


https://medium.com/codex/ddd-command-query-responsibility-segregation-cqrs-a24bfc30a8e2

