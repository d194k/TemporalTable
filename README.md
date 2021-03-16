# Build a set of services to get and set employees

## Task
This task is to create a simple console app to `get` and `set` employee data.
The app should be built in dotnet core 3.1
e.g.
* dotnet run set-employee --employeeId 2 --employeeName John --employeeSalary 333
* dotnet run get-employee --employeeId 2
 
## Database
There are tools included to set up and tear down a postgres database using docker: 
* Setup database: ./setUpDatabase.ps1 
* Database schema: ./DatabaseSchema/dbSchema.sql

If you choose to use a different database, that is fine, just include details in your submission.## Time travellingEmployee records are a snapshot in time, and the `get` service will be required to get an employee record "as it appeared at a point in time". This is accounted for in the database schema, where `existencestartutc` is the time that an employee record is valid from, and `existenceendutc` is the time that an employee record is valid to.
For more information 
* https://en.wikipedia.org/wiki/Temporal_database#Uni-Temporal 
* https://martinfowler.com/eaaDev/timeNarrative.html#dimensions

## Interface parametersThese are the methods that the console app must support, along with the arguments that are required.

### get-employee 
1. --employeeId, INT, Mandatory 
2. 2. --simulatedTimeUtc, DateTime, Optional, Default = Now

### set-employee 
1. --employeeId, INT, Mandatory 
2. --employeeName, STRING Mandatory 
3. --employeeSalary, INT Mandatory

## Testing your submissions 
* The submission will require tests 
* The script ./verifySubmission.ps1 can verify that functionality is complete" 
