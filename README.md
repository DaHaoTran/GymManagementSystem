# Gym Management System
This is a project to test methods and approaches in terms of professionalism and handling from one's own point of view.
## Install
Place this code to appSetting.json at API project:
Database
> "ConnectionStrings": {
  "GymConnection": "Your-connection-string"
},

<sup> Then run command line to create datbase </sup>

Json web token validation
> "Jwt": {
  "Key": "Your-secret-key",
  "Issuer": "Your-issuer", 
  "Audience": "Your-audience" 
}

After update database, open StoredProcedure.sql in sql folder to install procedures for database (use sql server managements studio 20).

## Some images
### Admin UI:
![Admin UI](<Screenshot 2025-03-25 123944.jpg>)
### Staff UI:
![Staff UI](<Screenshot 2025-03-25 123316.jpg>)
### API:
![API UI](<Screenshot 2025-03-25 122725.jpg>)
