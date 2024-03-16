Hi this is a test Task project for possible Employment in the www.Omega.ir company

###migration commands: assuming we are in /TestTaskOmega/ solution
for identity : 
```bash
dotnet ef add mirgration --context ApplicationUserDbcontext --project ./TestTaskOmega.Identity --startup-project ./TestTaskOmega.API
```
for All Enitities (we have one which is called <span style="color:#31A32C;">This text is green!</span>

```bash
dotnet ef add mirgration --context ApplicationDbcontext --project ./TestTaskOmega.DataAccess --startup-project ./TestTaskOmega.API
```
and to update the database : 
for identity : 
```bash
dotnet ef database update --context ApplicationUserDbcontext --project ./TestTaskOmega.Identity --startup-project ./TestTaskOmega.API
```
for All Enitities (we have one which is called <span style="color:#31A32C;">This text is green!</span>

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
and you can start creating users.

