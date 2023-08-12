# CraftBuddy
CraftBuddy is an ASP.NET Core MVC Web Project for final project at SoftUni.
CraftBuddy is a workshop where you can buy craft products for birthay parties or for what event you would like to.

# Technical description
The web app have two areas. One for crafters, who craft products and sell them and one for clients, who can buy already made products or describe exactly what product they want and send a request to a crafter to make it. On the other side, the crafter can control the status of the order and set a price after deciding how much would it cost to make it. There are two different form for register of a crafter and a client.

There also are workshop events and a blog which are controled by the crafters. The clients can participate in the workshops and read and like topics in the blog.

# Front-end
There is one background for the login register home page and one background for all other pages. The images of the products are automatically set by the type of the product. There currently 4 types of products: Hat, Banner, Topper and a Flag. They are all party decorations. On the home page before you login, there is a carousel with example images of the products which is constantly changing. When you login you can sort the products on the home page as you desire: by price (ascending and descending), by type of the product or by the crafter of the products.

# Used technologies
Entity Framework Core 6
ASP.NET Core 6
ASP.NET Core Areas
HTML and CSS
Bootstrap
Limonte-sweetalert2
In-Memory Database
NUnit
JQuery
JavaScript
SOLID Principles
SQL Server

# Database
When migrations are applied the database is seeded only with the for product types and the three order statuses as they are needed from the beginning of the app. When the app is up and running you can register and explore.

# Important
Better documentation and screenshots of the app with better explanations is comming up soon!
