using System.Collections.Generic;
using System.Linq;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private AplicationDbContext _context;
        public ProductController(AplicationDbContext context){
            _context = context;
        }

        [HttpGet]
        public List<Product> GetList(){
            List<Product> resutl = _context.Products.ToList();
            return resutl;
        }
        [HttpGet("search")]
        public IActionResult Search(string query){
            if(query == null || query == "") return BadRequest();
            List<Product> result = _context.Products.Where(item => item.proName.Contains(query)).ToList();
            return Ok(result);
        }
        [HttpGet("{id}")]

        public Product Get(int id){
            if(id<=0) return null;
            Product result = _context.Products.Find(id);
            return result;
        }
        [HttpDelete("{id}")]     
           public IActionResult Delete(int id)
        {    
            if(id<=0) return NotFound();
            Product obj = _context.Products.Find(id);
            if(obj == null) return NotFound();
            _context.Remove(obj);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product )
        {    
            if(id<=0) return BadRequest();
            Product obj = _context.Products.Find(id);
            if(obj == null) return BadRequest();
            //
            obj.proName = product.proName;
            obj.proPrice = product.proPrice;
            obj.proDescription = product.proDescription;
            obj.Image = product.Image;
            //
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost]
        public IActionResult Create(Product product )
        {    
            _context.Add(product);
            _context.SaveChanges();
            return Ok();
        }

    }
}