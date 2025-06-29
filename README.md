# ASP.NET Core Application FlashMedia

## Introduction
FlashMedia is a responsive photo sharing platform inspired by Flickr, built with ASP.NET Razor Pages. The app supports multiple user roles, photo uploads by category, interactive maps, and profile-based album management.

## Description
FlashMedia is a web application for sharing photos, similar to Flickr. Users can browse and upload photos, organize them into albums, search by category or location, and interact through comments. The platform supports different user roles (visitor, user, admin) and includes features like location tagging, a minimal photo editor, and content moderation. Built with ASP.NET Razor Pages and designed to be responsive and user-friendly.

<p align="center">
    <img src="https://github.com/mariaxadina/ASP.NET-Core-Application-FlashMedia/blob/main/images/1.png" width="70%" />
</p>

## Project Features
- User Roles: Visitor, Registered User, Moderator/Administrator

- Gallery: Browse all photos and see latest uploads as thumbnails

- Categories: Filter photos by categories like Cars, Nature, Landscapes, etc.

- User Profiles: Registered users can create albums and upload categorized photos with descriptions

- Photo Pages: Each photo has a detailed page with zoom, comments, and moderation

- Search: Smart search engine including map-based location search

- Map Support: Add/view photos by location with coordinates auto-filled

- Photo Editor: Minimal photo editor available for logged-in users

- Moderation Tools: Admins can manage inappropriate content

- Modern UI: Fully responsive and visually appealing layout
  
## Technologies Used

### ASP.NET Core Razor Pages
FlashMedia is built using ASP.NET Core Razor Pages, a lightweight and page-focused web framework that simplifies building dynamic, data-driven websites. Razor Pages allows for clean separation of concerns by combining the HTML markup with server-side C# logic in .cshtml and .cshtml.cs files. This structure makes the application easier to manage and scale, especially for CRUD operations like adding photos, editing profiles, or moderating content. Razor Pages also integrates seamlessly with ASP.NET Core features like authentication, dependency injection, and model binding.

### SQL Server Database
FlashMedia uses a local SQL Server database provided by Visual Studio 2022 through SQL Server Object Explorer. The database stores essential data such as user accounts, photo details (name, description, file path, upload date, coordinates), albums, categories, and comments. It supports entity relationships like users to albums, albums to photos, and photos to comments. Entity Framework Core is used for seamless integration and data manipulation within the ASP.NET Razor Pages application.

<p>
  <img src="https://github.com/mariaxadina/ASP.NET-Core-Application-FlashMedia/blob/main/images/4.png" width="49%" />
  <img src="https://github.com/mariaxadina/ASP.NET-Core-Application-FlashMedia/blob/main/images/5.png" width="49%" />
</p>

### Entity Framework Core 
Entity Framework Core was used as the Object-Relational Mapper (ORM) to interact with the database in a more intuitive and efficient way. It allowed us to work with data using .NET objects, eliminating the need for most SQL queries. Through EF Core, we handled data models, relationships, and migrations seamlessly, ensuring strong integration between the backend logic and the underlying database structure.

### Location Feature with OpenStreetMap API
FlashMedia integrates the OpenStreetMap Nominatim API to enhance photo location tagging and search. When a user uploads a photo, they can enter a location name. The app uses the API to convert this address into geographic coordinates (latitude & longitude), which are then saved in the database with the photo. These coordinates are later used on an interactive map to search for and display photos taken near a specific location.

<p align="center">
    <img src="https://github.com/mariaxadina/ASP.NET-Core-Application-FlashMedia/blob/main/images/3.png" width="70%" />
</p>

## Results
The final outcome of the project successfully met all the initial objectives. FlashMedia provides a complete photo-sharing experience, while offering custom features built with Razor Pages, Entity Framework, and ASP.NET Core.

### Collaboration
This project was developed in collaboration with [Bichel Stefan-Adrian](https://github.com/StefanAdrian2003) and [Chera Gabriel-Alexandru](https://github.com/gabirelul).
