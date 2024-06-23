using DatabaseApp.Models;
using DatabaseApp.Services;

var productService = new ProductService();
var customerService = new CustomerService();

// Insert a new product
productService.AddProduct(new Product { ProductName = "New Product", Price = 99.99m });

// Retrieve all products
var allProducts = productService.GetAllProducts();
Console.WriteLine("Products:");
foreach (var product in allProducts)
{
    Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.Price}");
}

// Insert a new customer
customerService.AddCustomer(new Customer { Name = "Kingsley Okafor", Email = "kingsleychiboy22@gmail.com" });

// Retrieve all customers
var allCustomers = customerService.GetAllCustomers();
Console.WriteLine("\nCustomers:");
foreach (var customer in allCustomers)
{
    Console.WriteLine($"ID: {customer.CustomerId}, Name: {customer.Name}, Email: {customer.Email}");
}

