// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

[ApiController]
[Route("/[controller]")]
public class ProductsController : ControllerBase
{
    private const string FilePath = "products.json";

    // อ่านข้อมูลจากไฟล์ JSON
    private List<Product> ReadProductsFromFile()
    {
        if (!System.IO.File.Exists(FilePath))
        {
            // สร้างไฟล์เริ่มต้นถ้าไม่มี
            var initialData = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 999.99M },
                new Product { Id = 2, Name = "Smartphone", Price = 499.99M }
            };
            SaveProductsToFile(initialData);
        }

        var json = System.IO.File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
    }

    // เขียนข้อมูลลงไฟล์ JSON
    private void SaveProductsToFile(List<Product> products)
    {
        var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText(FilePath, json);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        var products = ReadProductsFromFile();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var products = ReadProductsFromFile();
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        var products = ReadProductsFromFile();
        product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
        products.Add(product);
        SaveProductsToFile(products);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product updatedProduct)
    {
        var products = ReadProductsFromFile();
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        SaveProductsToFile(products);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var products = ReadProductsFromFile();
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        products.Remove(product);
        SaveProductsToFile(products);
        return NoContent();
    }
}
