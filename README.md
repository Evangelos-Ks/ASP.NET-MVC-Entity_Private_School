## Before you run the application

* #### Change the connection string.
  To run the application you must have a database, so you must change the Data Source in the
  connection string in the project Assignment2.Web.
  The way to go there is:
  Assignment2.Web ⟹ Web.config ⟹ line 65 ⟹ Data Source 

* #### Create the database
  To run the migration you must update the database from the Assignment2.Database project.
  If you use Visual Studio you can do that from the Package Manager Console. Make sure that
  the default project in the Default project tab is the Assignment2.Database and type the command
  ”update‐database”.
  
* #### Run
  If the database has been created run the application from the project Assignment2.Web.

## Description
An assignment with CRUD operations, sorting, searching, pagination and statistics in courses, students, assignments and trainers using repository design pattern in ASP.NET.  This project holds information of courses, students, assignments, trainers with the relationships between them. In the starting page the users could find information that will help them to learn and navigate in the app.

Generally the user could:

•	Insert entities, edit marks, edit entities, see details and delete

•	Search in the entities

•	Ascend or descend

•	Select the number of items per page

There is data in seeding hence you could "update-database" and give it a try.
