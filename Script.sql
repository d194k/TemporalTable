CREATE TABLE employees (
    employeeid INT NOT NULL primary key clustered,
    employeename VARCHAR(128) NOT NULL,
    employeesalary INT NOT NULL,
    existencestartutc DATETIME2 GENERATED ALWAYS AS ROW START,
    existenceendutc DATETIME2 GENERATED ALWAYS AS ROW END,
	period for system_time(existencestartutc,existenceendutc)
)
with (system_versioning = on ( history_table = dbo.employeesHistory));