# Ecom-App.Net-8-With-Angular

## Project Overview
Ecom-App.Net-8-With-Angular is an e-commerce web application built using .NET for the backend and Angular for the frontend. This project aims to provide a seamless shopping experience with a modern user interface and efficient backend services.

## Features
- User authentication and authorization
- Product listing and detail pages
- Shopping cart functionality
- Order management
- Admin dashboard for managing products and orders

## Project Structure
```
Ecom-App.Net-8-With-Angular/
├── client/                     # Angular frontend
│   ├── src/                   # Source files
│   ├── angular.json           # Angular configuration
│   ├── package.json           # NPM dependencies
├── server/                     # .NET backend
│   ├── EcomApp/               # ASP.NET core project
│   ├── EcomApp.csproj         # Project file
│   ├── Startup.cs             # Startup configuration
├── README.md                  # Project documentation
└── .gitignore                 # Git ignore file
```

## Technology Stack
- **Frontend**: Angular, TypeScript, HTML, CSS
- **Backend**: ASP.NET Core, Entity Framework
- **Database**: SQL Server
- **Development Tools**: Visual Studio, Visual Studio Code, Git

## Setup Instructions
1. **Clone the repository**:
   ```bash
   git clone https://github.com/ziad-abdo96/Ecom-App.Net-8-With-Angular.git
   cd Ecom-App.Net-8-With-Angular
   ```

2. **Set up the backend**:
   - Open the `server` directory in Visual Studio and restore the NuGet packages.
   - Update the connection string in `appsettings.json` to connect to your SQL Server.
   - Run the migrations to set up the database.
     ```bash
     dotnet ef database update
     ```
   - Start the .NET server:
     ```bash
     dotnet run
     ```

3. **Set up the frontend**:
   - Navigate to the `client` directory and install the dependencies:
     ```bash
     npm install
     ```
   - Start the Angular application:
     ```bash
     ng serve
     ```

4. **Access the application**:
   Open your browser and go to `http://localhost:4200` to view the application.


## Contributing
Contributions are welcome! Please open an issue or submit a pull request for suggestions or improvements.

---

**Author**: Ziad Abdo
**Date**: 2026-04-10 12:58:22 UTC
