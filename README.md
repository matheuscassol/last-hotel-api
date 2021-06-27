# Last Hotel API

This project is a REST API developed in **.NET Core 3.1** with **xUnit** unit tests.

## Usage
### Running
The project is designed to run locally just running the ***Application*** project.

*To achieve this design, the database is In Memory and some capabilities of relational databases might not work as expected. ex: Unique Indexes. However, those configurations are still present.*

### API Documentation
The default route leads to the **Swagger** documentation, which provides the right routes and data formats for each request.

### Order of action 
***Create Client** -> **Create Booking with Client Id** ->
**Execute Additional Operations (Modifying, Deleting etc)***

### Special Remarks

Please note that, when booking, the *Start Date* and *End Date* are going be assigned to the correct time of the day, regardless of the input. 

Ex:
 
#### Input 

```json
{
  "clientId": "eab68f03-a9ec-4292-b166-db9c404e744d",
  "startDate": "2021-06-28T21:57:14.326Z",
  "endDate": "2021-06-28T21:57:14.326Z"
}
```
#### Output:

```json
{
  "clientId": "eab68f03-a9ec-4292-b166-db9c404e744d",
  "startDate": "2021-06-28T00:00:00Z",
  "endDate": "2021-06-28T23:59:59Z",
  "id": "e15256c1-0830-45f3-b102-6161ef4d827f",
  "createdAt": "2021-06-27T21:57:27.8823268Z"
}
```

## Architecture & Principles
This project was designed with an architecture that applies **Domain Driven Design**.

There is an architectural diagram in this repository with an overview of the layers present in this project.

It should be relatively easy to replace the ***Application*** layer for a different project, that uses the same ***Services*** but for a desktop app. The same should be true for replacing the ***Data*** layer for one that connects to a different API, instead of a database.

It should also be easy to add new Controllers, Services, and Objects as this project was coded having the **SOLID principles** and **Clean Code** in mind.
## Shortcuts
This is the list of features left out of the project for simplicity. 

Feature                          | Possible Technology
-------------                    | -------------
Integration Tests                | xUnit or SpecFlow
Logging                          | NLog
Authentication/Authorization     | Azure AD, JWT
Messaging                        | RabbitMQ
Containers                       | Docker, Kubernetes
