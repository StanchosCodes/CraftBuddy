# CraftBuddy
CraftBuddy is an ASP.NET Core MVC Web Project for final project at SoftUni.
CraftBuddy is an online workshop where you can buy craft decorations for birthay parties or for what event you would like to.

# Technical description
The web app have two areas. One for crafters, who craft products and sell them and one for clients, who can buy already made products or describe exactly what product they want and send a request to a crafter to make it. On the other side, the crafter can control the status of the order and set a price after deciding how much would it cost to make it. There are two different forms for register of a crafter and a client.

There also are workshop events and a blog which are controled by the crafters. The clients can participate in the workshops and read and like/dislike topics in the blog.

# Front-end
There is one background for the login register home page and one background for all other pages. The images of the products are automatically set by the type of the product. There currently 4 types of products: Hat, Banner, Topper and a Flag. They are all party decorations. On the home page before you login, there is a carousel with example images of the products which is constantly changing. When you login you can sort the products on the home page as you desire: by price (ascending and descending), by type of the product or by the crafter of the products.

# Used technologies
Entity Framework Core 6
ASP.NET Core 6
ASP.NET Core Areas
HTML5
CSS3
Bootstrap
Limonte-sweetalert2
NUnit
JQuery
JavaScript
SOLID Principles
MVC Design Pattern
In-Memory Database - for Unit Tests
SQL Server - for Development

# Database
When migrations are applied the database is seeded only with the four product types and the three order statuses as they are needed from the beginning of the app. When the app is up and running you can register and explore.

# Database Diagram
![Database Diagram](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/6247a8e7-eac5-4765-87c9-cf686ca6fa06)

# Home Page before Login
Here is a carousel with photos of the products.
![Home Page Before Login](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/8b8edb2e-d915-4ba7-89a3-80416867a855)

### You can choose to register as client or a crafter.
![Register Options](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/3cd8b0a8-7cbb-4f09-8668-363b80c406bb)

# Register Pages
<p align="center">
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/1cb628fd-8f0e-44ce-a206-4c281c6713be" width="49%" />
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/3ecb2474-9b00-4c5a-82e5-a2bf262dab22" width="49%" />
</p>

# Login Page
![Login Page](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/ecc187e3-8460-4974-bd2f-a85b383f8598)

# Client Pages
Home page for a client.
![Login As Client](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/eaa4d4ca-3a88-4ed5-b864-d23a7fb37fbf)

### The orders drop-down menu contains a page with all orders of the client and a page for ordering a custom product.
![Orders Menu](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/6df79672-0646-43a6-966d-f5f36ba7d8bf)

Orders of the client.
![My Orders Page](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/bd2480eb-9113-4309-8f99-8262a07547a3)

Order a custom product page.
![Custom Order Page](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/aa219516-8a0c-41f8-b884-b9e2a501834d)

Drop-down menu with all available crafters to craft the desired custom product.
![Custom Order Page Crafters Menu](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/1630fda9-f23c-4360-953a-fa43b2d251dd)

The user can see the details of its own orders.
![My Orders Details Page](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/00184ba7-232f-40d3-a124-f8ad470d7b26)

### The workshops drop-down menu contains a page for all available workshops and a page with all workshops joined by the user.
![Workshops Menu](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/f62b4901-89fb-45e2-8619-c571506112a1)

<p>
  <img src"https://github.com/StanchosCodes/CraftBuddy/assets/102748080/dfabf98b-705d-4e81-af5b-d4b2e2ef04b5" width="49%" />
  <img src"https://github.com/StanchosCodes/CraftBuddy/assets/102748080/c1d1ab9d-87b8-420d-b7af-2b85780c7b10" width="49%" />
</p>

Details for joined and not joined workshop.
<p>
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/07ae7293-f528-4683-ab75-1d11a2742282" width="49%" />
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/5581c7a6-2fd3-44bf-9043-cee0ef00a7dc" width="49%" />
</p>

### Articles Page
![Articles Page](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/82ee6bcd-1b17-40f0-bdd5-57a213808bb5)

Details about an article in the blog.
![Articles Details Page](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/631471ec-661a-4af4-b65d-3efea6556ff8)

# Crafter Pages
Home page for crafters. Products drop-down menu contains pages for all products and adding a product. If the user is the owner of the product it can be editted.
![Products Menu](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/61fff168-0471-4ac1-b3de-d87cd1538db8)

Add product page.
![Add Product Page](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/15940cad-c1b3-49d4-8fe6-98b67a27e932)

Edit product page.
<p>
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/e2a26ded-3e73-4a23-a86d-c7cf4f752f50" width="49%" />
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/14d98beb-6252-4a5b-b1e7-6a6871311680" width="49%" />
</p>

### Orders drop-down menu contains pages for all orders of the user, page for ordering a product from another crafter, all waiting orders to be crafted by the user and a page with all already crafted orders.
![Orders Menu](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/e5de2320-ea90-4f7d-ba21-38e64af87228)

Waiting orders page and Crafted orders pages.
<p>
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/826208ae-daea-4e0a-9d90-7db012ffb319" width="49%" />
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/a06f68c8-7d25-4f8d-8499-12126f87b1a5" width="49%" />
</p>

All waiting and crafted orders can be edited so the crafter can change them price or status.
<p>
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/6682a13c-2ad9-40db-9e64-0bbf41b195ba" width="49%" />
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/76d4e730-2e85-4eee-8579-c62ea5109293" width="49%" />
</p>

### Workshops drop-down menu contains pages for all available workshops, adding a workshop and all joined workshops by the user.
![Workshops Menu](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/3c15a8ab-3f7a-4925-ae83-4d0416b262e8)

Add workshop page.
<p>
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/ff2dd06c-b149-41f9-be96-aa721f23425b" width="49%" />
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/f3ed05d6-d486-4c8a-bb7d-596080f2144e" width="49%" />
</p>

### The blog drop-down menu contains pages for all available articles in the blog and for adding an article.
<p>
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/d8883bac-f7bf-4706-b033-f0e4f918f544" width="49%" />
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/3681588d-1d92-42d9-a85b-3f2d19c7e8d9" width="49%" />
</p>

# Different sortings available for all users.
<p>
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/369e5661-8609-4533-927f-4d1288b1c0ca" width="49%" />
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/364ecce6-917b-4951-bd8e-6c9a37835043" width="49%" />
</p>
<p>
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/b521c57c-304e-4743-8902-15471e0787c3" width="49%" />
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/65358eea-a9d4-4be2-abb8-db24ddcdf7f2" width="49%" />
</p>

![Sorted Products To Be 8 Hats Only Per Page](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/47cbf846-0e88-480a-898f-2362d7711c38)

Details about a product.
![Product Details Page](https://github.com/StanchosCodes/CraftBuddy/assets/102748080/3b8693e4-8096-405f-8ef9-8af5323e0c39)

# Sweet alert messages
For all edit, add, join, leave, delete pages there is a sweet alert message.
<p>
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/2c7751a4-c9fa-4942-869c-e237ac6e0b5c" width="49%" />
  <img src="https://github.com/StanchosCodes/CraftBuddy/assets/102748080/d252aa41-4362-4a8f-a05d-8bd332654f8c" width="49%" />
</p>

# Author
[Stanislav Stamatov](https://www.linkedin.com/in/stanislav-stamatov-402647255)

# Feedback would be appreciated
If you like my project give it a star. ⭐
