# Dreamer Hearth Card Helper
A web application for managing and tracking characters and their abilities for the fantasy game **Dreamer Hearth**.

## Technologies Used
- ASP.NET Core (Razor Pages)
- Entity Framework Core
- ASP.NET Core Identity
- SQL Server
- Docker & Docker Compose
- C#
- Javascript
- Bootstrap

## Prerequisites
Make sure you have the following installed:
- [Docker](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/downloads)

## Getting started
```
git clone https://github.com/klaudia021/DHCardHelper
cd DHCardHelper
```
### Docker setup
In the main project folder (where the `.env.example` file is located)
 - Create a new `.env` file.
 - Copy the contents of `.env.example` into it.
 - Replace the `PasswordPlaceholder` in **both** places with a **strong password** (e.g. Str0ngP4$$w0rd!).
 - _(Optional) Change the database name (`MyDatabase`) to a custom one._
    > Only letters, numbers, and underscores are allowed

### Build and run container
- In the main project folder (where the `Dockerfile` file is located):
```
docker compose up --build
```
_This process could take a while._

Once the container is running, the application will be available at:
http://localhost:8000/

To shut down the container:
```
docker compose down
```

### Clean-up (Remove Database Volume)
This will delete the SQL data volume!
```
docker compose down -v
```

### Local setup (Without Docker Compose)
In the `\DHCardHelper` folder, where `appsettings.example.json` is located:
 - Create a new `appsettings.json` file.
 - Copy the contents of `appsettings.example.json` into it.
 - Replace the `ChangeMe!` placeholder with a **strong password** (e.g. Str0ngP4$$w0rd!)
 - _(Optional) Change the database name (`MyDatabase`) to a custom one._
    > Only letters, numbers, and underscores are allowed
 - **If using the local setup with the Docker SQL Server, ensure that the password and database name in both files match exactly!**

## How to use
Login with a predefined user, or create a new account.
### Roles
  - Player
    - Can view cards, create characters and assign cards to their own characters.
    - Can delete their own characters and remove cards from them.
  - GameMaster
    - Can do everything a player can, plus:
    - Can add, delete and edit cards. Can see all player's characters, but cannot modify or delete them.
  - Admin (WIP)
    - Currently the same as GameMaster

### Menus
#### Players
  - Domains - Lists the Domain type Cards
  - Subclasses - Lists the Subclass type Cards
  - Heritage - Lists the Heritage type Cards
  - Character Sheets - Lists the player's characters
  - Add Character - Create a new character

#### GM and Admins:
  - 'Domains', 'Subclasses', 'Heritage' and 'Add Character' menus are the same
  - Character Sheets - Lists all player's characters
  - Add Card (Domain, Heritage, Subclass) - Create new Cards
