# VehicleTrackingSystem 

 Design pattern used: strategy and repository pattern.
 Language: c#, .Net5
 Database: sql
 ORM: Entityframework
 Logging : Serilog
 Authentication: JWT
1. Ensure the environment where the project would be run has sql installed.
2. Open the Package manager console in visual studio and run migration:
  1. Add-Migration InitialMigration
  2. Update-Database
3. Build the application with visual studio or visual studio code. Run/ Start the application
4. When the application is launched, a swagger page comes up.
- /api/Auth/v1/Register - is used for user registration or onboarding
- /api/Auth/v1/RegisterAdmin - is for Administrator registration
-. /api/Auth/v1/Login - the login for both user and administrator
- /api/Vehicles/v1/Register - used by the user to register thier vehicle details
-/api/Vehicles/v1/{trackid} - used by the user to get the registered vehicle details
-/api/Vehicles/v1/Position/Add - for posting vehicle position
-/api/Vehicles/v1/Position/{trackingId} - used by admin for getting the position of a vehicle
-/api/Vehicles/v1/Position/All - used by admin for getting the positions of a vehicle within a particualr date/time range
5. When a registered user/admin logs in, a token is generated.
6. The token is what will be used for further access to the endpoints with an expiry lifespan of 60 minutes. This can be achieved by 
  using the authorize button at the top right hand corner and passing in the token.
  
7. According the task, only the admin can check the location/locations of a vehicle
8. The user can only register the vehicle and post the vehicle location 

9. If there is a need to add more properties to the request dto, 
  1. a class can be created to inherit the current dto class properties and then have the new set of properties added to it.
    Because of this change, there will be a change in the contract with the current consumer of the endpoint, 
    hence, a new url version endpoint will have to be exposed for the new changes,
    so that those who are still consuming the current version won't be affected. eg. /api/Vehicles/v2/Register.
  2. Tthe database table will have to be updated with the new set of properties as column in order to store the new properties value.
  3. Automapping will still be the same. 
  
  
