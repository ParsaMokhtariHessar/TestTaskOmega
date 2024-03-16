Hi this is a test Task project for possible Employment in the www.Omega.ir company

###migration commands: assuming we are in /TestTaskOmega/ solution
for identity : 
```bash
dotnet ef migrations add "MYmigration" --context ApplicationUserDbcontext --project ./TestTaskOmega.Identity --startup-project ./TestTaskOmega.API
```
for All Enitities we have one which is called <code style="color: green">Services</code>

```bash
dotnet ef migrations add "MYmigration"  --context ApplicationDbcontext --project ./TestTaskOmega.DataAccess --startup-project ./TestTaskOmega.API
```
and to update the database :

for identity : 
```bash
dotnet ef database update --context ApplicationUserDbcontext --project ./TestTaskOmega.Identity --startup-project ./TestTaskOmega.API
```
for entities :
```bash
dotnet ef database update --context ApplicationDbcontext --project ./TestTaskOmega.DataAccess --startup-project ./TestTaskOmega.API
```
but I have a command in the code that automatically creates and updates the database whenever it is in the developer environment and 
we have a pending migration.

there is an admin use seeded in the IdentitySeeds/UserSeed : 
```
{
Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
Email = "admin@admin.com",
UserName = "admin",
Password = "P@ssword1"
}
```
you can log in with that and start creating and managing users. 
and you can start creating users. You can copy and paste the following :
```
{
  "username": "admin",
  "password": "P@ssword1"
}
```
after that you can make a new user using the CreateUser Endpoint (Only Managers can Create a new user):
```
{
  "email": "user@user.com",
  "username": "user",
  "password": "P@ssw0rd"
}
```
the project uses JSON Web Token Authentication :

it has swagger UI for automatically attaching your token after you get it click Login and type Bearer yourtokenwithoutparathesis

all authenticated users can use the <code>Services</code> API: Only Admins can use UserManager.

if you have a question email me at <email>parsamokhtarihessar@gmail.com</email>

things that remain to do:

implement soft delete to prevent data corruption.

validation for email.

making EntityHistory more memory efficient!

implement GetAllDeleted(),

ServiceResponce.cs/ making sure it brings back the correct status code

User-friendly Exceptions.

Debugg and Polish!

